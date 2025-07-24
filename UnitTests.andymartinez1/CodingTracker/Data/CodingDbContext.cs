using System.Data;
using System.Data.Common;
using Coding_Tracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Data;

public class CodingDbContext
{
    internal readonly IDbConnection ConnectionString;

    public CodingDbContext(IDbConnection connectionString)
    {
        ConnectionString = connectionString;
    }

    public void CreateDatabase()
    {
        using (var connection = ConnectionString)
        {
            connection.Open();

            // Ensure the table exists
            var createTableQuery =
                @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                Id INTEGER PRIMARY KEY,
                ProjectName TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL
                )";

            connection.Execute(createTableQuery);
        }

        // Seed the database with initial data if it's empty
        var isEmpty = IsTableEmpty();
        if (isEmpty)
            SeedSessions(5);
    }

    public void InsertSeedSessions(List<CodingSession> sessions)
    {
        using (var connection = ConnectionString)
        {
            connection.Open();

            var insertQuery =
                @"
                    INSERT INTO CodingSessions (ProjectName, StartTime, EndTime)
                    VALUES (@ProjectName, @StartTime, @EndTime)";

            foreach (var session in sessions)
                connection.Execute(
                    insertQuery,
                    new
                    {
                        session.ProjectName,
                        session.StartTime,
                        session.EndTime,
                    }
                );
        }
    }

    public void SeedSessions(int count)
    {
        var random = new Random();
        var currentDate = DateTime.Now.Date;

        var sessions = new List<CodingSession>();

        for (var i = 0; i < count; i++)
        {
            var startTime = currentDate.AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60));
            var endTime = startTime.AddHours(random.Next(1, 12)).AddMinutes(random.Next(0, 60));

            var session = new CodingSession
            {
                ProjectName = $"Project {i + 1}",
                StartTime = startTime,
                EndTime = endTime,
            };

            sessions.Add(session);
            currentDate = currentDate.AddDays(1);
        }

        InsertSeedSessions(sessions);
    }

    public bool IsTableEmpty()
    {
        using (var connection = ConnectionString)
        {
            connection.Open();

            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingSessions");

            return count == 0;
        }
    }
}
