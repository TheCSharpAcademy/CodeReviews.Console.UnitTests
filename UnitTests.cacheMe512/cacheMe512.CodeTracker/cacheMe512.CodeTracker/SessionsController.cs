using cacheMe512.CodeTracker.Models;
using Dapper;

namespace cacheMe512.CodeTracker;

internal class SessionsController
{
    public IEnumerable<CodingSession> GetAllSessions()
    {
        try
        {
            using var connection = Database.GetConnection();
            var sessions = connection.Query<CodingSession>(
                "SELECT Id, StartTime, EndTime, Duration AS DurationInSeconds FROM coding_sessions").ToList();

            return sessions;
        }
        catch (Exception ex)
        {
            Validation.DisplayMessage($"Error retrieving sessions: {ex.Message}", "red");
            return Enumerable.Empty<CodingSession>();
        }
    }

    public void InsertSession(CodingSession session)
    {
        try
        {
            using var connection = Database.GetConnection();
            using var transaction = connection.BeginTransaction();
            int durationInSeconds = (int)session.CalculateDuration().TotalSeconds;

            connection.Execute(
                "INSERT INTO coding_sessions (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
                new { StartTime = session.StartTime, EndTime = session.EndTime, Duration = durationInSeconds },
                transaction: transaction);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Validation.DisplayMessage($"Error inserting session: {ex.Message}", "red");
        }
    }

    public void UpdateSession(CodingSession session)
    {
        try
        {
            using var connection = Database.GetConnection();
            using var transaction = connection.BeginTransaction();

            int durationInSeconds = (int)session.CalculateDuration().TotalSeconds;

            connection.Execute(
                "UPDATE coding_sessions SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id",
                new { StartTime = session.StartTime, EndTime = session.EndTime, Duration = durationInSeconds, Id = session.Id },
                transaction: transaction);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Validation.DisplayMessage($"Error updating session: {ex.Message}", "red");
        }
    }

    public bool DeleteSession(int id)
    {
        try
        {
            using var connection = Database.GetConnection();
            using var transaction = connection.BeginTransaction();

            var rowsAffected = connection.Execute(
                "DELETE FROM coding_sessions WHERE Id = @Id", new { Id = id }, transaction: transaction);

            if (rowsAffected > 0)
            {
                transaction.Commit();
                return true;
            }

            transaction.Rollback();
            return false;
        }
        catch (Exception ex)
        {
            Validation.DisplayMessage($"Error deleting session: {ex.Message}", "red");
            return false;
        }
    }


}
