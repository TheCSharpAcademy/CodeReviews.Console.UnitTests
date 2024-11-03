using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace CodingTrackerLibrary;

public static class CodingSessionController
{
    public static List<CodingSession> GetCodingSessions(string connStr)
    {
        List<CodingSession> sessions = new List<CodingSession>();
        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                sessions = connection.Query<CodingSession>("SELECT Id, StartTime, EndTime FROM CodingSessions").ToList();

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ocurred: {ex.Message}");
        }

        return sessions;
    }

    public static CodingSession GetCodingSessionById(int id, string connStr)
    {
        CodingSession session = null;

        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                session = connection.QueryFirstOrDefault<CodingSession>("SELECT Id, StartTime, EndTime FROM CodingSessions WHERE Id = @Id", new { Id = id });

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ocurred: {ex.Message}");
        }

        return session;
    }

    public static bool InsertCodingSession(CodingSession session, string connStr)
    {
        string sql = $@"INSERT INTO CodingSessions (StartTime, EndTime) 
                                        VALUES (@StartTime, @EndTime);";

        return SqlExecutionService.ExecuteCommand(sql, session, connStr);
    }

    public static bool UpdateCodingSession(CodingSession session, string connStr)
    {
        string sql = $@"UPDATE CodingSessions SET
                                StartTime = @StartTime, 
                                EndTime = @EndTime
                                WHERE Id = @Id;";

        return SqlExecutionService.ExecuteCommand(sql, session, connStr);
    }

    public static bool DeleteCodingSession(CodingSession session, string connStr)
    {
        string sql = $@"DELETE FROM CodingSessions
                                WHERE Id = @Id;";

        return SqlExecutionService.ExecuteCommand(sql, session, connStr);
    }
}
