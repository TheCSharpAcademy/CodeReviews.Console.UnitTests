using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace cacheMe512.CodeTracker;

internal static class Database
{
    private static string ConnectionString;
    private static string LogLevel;

    static Database()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConnectionString = configuration.GetConnectionString("CodeTrackerDatabase");
        LogLevel = configuration["AppSettings:LogLevel"];
    }

    internal static SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    internal static void InitializeDatabase()
    {
        if (LogLevel == "Debug" && File.Exists("code-tracker.db"))
        {
            File.Delete("code-tracker.db");
            Console.WriteLine("Existing database deleted for testing.");
        }

        using (var connection = GetConnection())
        {
            connection.Execute(
                @"CREATE TABLE IF NOT EXISTS coding_sessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL,
                        Duration INTEGER NOT NULL
                    );");

            SeedData(connection);
        }
    }

    private static void SeedData(SqliteConnection connection)
    {
        var random = new Random();

        for (int i = 0; i < 100; i++)
        {
            DateTime start = DateTime.Now.AddDays(-random.Next(0, 30));

            int randomHour = random.Next(0, 24);
            int randomMinute = random.Next(0, 60);
            int randomSecond = random.Next(0, 60);

            start = new DateTime(start.Year, start.Month, start.Day, randomHour, randomMinute, randomSecond);

            int randomDurationMinutes = random.Next(15, 121);
            TimeSpan randomDuration = TimeSpan.FromMinutes(randomDurationMinutes);

            DateTime endTime = start.Add(randomDuration);
            int totalSeconds = (int)randomDuration.TotalSeconds;

            connection.Execute(
                "INSERT INTO coding_sessions (StartTime, EndTime, Duration) VALUES (@Start, @End, @Duration)",
                new { Start = start.ToString("yyyy-MM-dd HH:mm:ss"), End = endTime.ToString("yyyy-MM-dd HH:mm:ss"), Duration = totalSeconds });
        }

        Console.WriteLine("Database seeded with sample data.");
    }

}
