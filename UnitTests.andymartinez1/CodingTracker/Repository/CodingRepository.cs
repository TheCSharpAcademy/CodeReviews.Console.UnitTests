using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Repository;

public class CodingRepository : ICodingRepository
{
    private readonly CodingDbContext _codingDbContext;

    public CodingRepository(CodingDbContext codingDbContext)
    {
        _codingDbContext = codingDbContext;
    }

    public List<CodingSession> GetAllSessions()
    {
        {
            using (var connection = _codingDbContext.ConnectionString)
            {
                connection.Open();

                var selectQuery = "SELECT * FROM CodingSessions";

                var sessions = connection.Query<CodingSession>(selectQuery).ToList();

                return sessions;
            }
        }
    }

    public CodingSession GetSession(int id)
    {
        using (var connection = _codingDbContext.ConnectionString)
        {
            connection.Open();

            var selectQuery = "SELECT * FROM CodingSessions WHERE Id = @Id";

            var session = connection
                .Query<CodingSession>(selectQuery, new { Id = id })
                .FirstOrDefault();

            return session;
        }
    }

    public void InsertSession(CodingSession session)
    {
        using (var connection = _codingDbContext.ConnectionString)
        {
            connection.Open();

            var insertQuery =
                @"
                    INSERT INTO CodingSessions (ProjectName, StartTime, EndTime)
                    VALUES (@ProjectName, @StartTime, @EndTime)";

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

    public void UpdateSession(CodingSession session)
    {
        using (var connection = _codingDbContext.ConnectionString)
        {
            connection.Open();

            var updateQuery =
                @"
                    UPDATE CodingSessions
                    SET ProjectName = @ProjectName, StartTime = @StartTime, EndTime = @EndTime
                    WHERE Id = @Id";

            connection.Execute(
                updateQuery,
                new
                {
                    session.ProjectName,
                    session.StartTime,
                    session.EndTime,
                    session.Id,
                }
            );
        }
    }

    public void DeleteSession(int id)
    {
        using (var connection = _codingDbContext.ConnectionString)
        {
            connection.Open();

            var deleteQuery = "DELETE FROM CodingSessions WHERE Id = @Id";

            connection.Execute(deleteQuery, new { Id = id });
        }
    }
}
