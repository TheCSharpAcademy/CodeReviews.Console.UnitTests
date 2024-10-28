using Microsoft.Extensions.Configuration;

namespace CodingTracker;

public static class Settings
{
    // Access settings
    public static string GetConnectionString()
    {
        var builder = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();
        return configuration["AppSettings:ConnectionString"]!;
    }
}
