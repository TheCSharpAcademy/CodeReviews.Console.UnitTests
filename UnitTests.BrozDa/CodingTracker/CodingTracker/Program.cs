using CodingTracker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodingTracker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var app = serviceProvider.GetRequiredService<CodingSessionTrackerApp>();

            app.Run();
        }

        /// <summary>
        /// Facilitate all necessary dependencies for the application
        /// </summary>
        private static ServiceProvider ConfigureServices()
        {
            string defaultConnectionString = "Data Source=coding-tracker.sqlite;Version=3;";
            string defaultRepositoryPath = "coding-tracker.sqlite";

            string? connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");
            connectionString ??= defaultConnectionString;

            string? repositoryName = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseName");
            repositoryName ??= defaultRepositoryPath;

            string dateTimeFormat = "dd-MM-yyyy HH:mm";

            var services = new ServiceCollection();

            services.AddSingleton<ICodingSessionRepository>(sp => new CodingSessionRepository(connectionString, repositoryName));
            services.AddSingleton<IInputManager>(sp => new InputManager(dateTimeFormat));
            services.AddSingleton<IOutputManager>(sp => new OutputManager(dateTimeFormat));

            services.AddSingleton<IReportManager>(sp =>
                new ReportManager(
                    sp.GetRequiredService<IInputManager>(),
                    sp.GetRequiredService<IOutputManager>(),
                    sp.GetRequiredService<ICodingSessionRepository>()
            ));

            services.AddSingleton<ISessionTracker>(sp =>
                new SessionTracker(
                    sp.GetRequiredService<IInputManager>(),
                    sp.GetRequiredService<IOutputManager>(),
                    sp.GetRequiredService<ICodingSessionRepository>()
            ));

            services.AddSingleton<ICodingSessionManager>(sp =>
                new CodingSessionManager(
                    sp.GetRequiredService<IInputManager>(),
                    sp.GetRequiredService<IOutputManager>(),
                    sp.GetRequiredService<ICodingSessionRepository>()
            ));

            services.AddSingleton<CodingSessionTrackerApp>(sp => 
            new CodingSessionTrackerApp(
                sp.GetRequiredService<IInputManager>(), 
                sp.GetRequiredService<IOutputManager>(), 
                sp.GetRequiredService<ICodingSessionManager>(), 
                sp.GetRequiredService<ISessionTracker>(), 
                sp.GetRequiredService<IReportManager>()));

            return services.BuildServiceProvider();
        }
    }
}