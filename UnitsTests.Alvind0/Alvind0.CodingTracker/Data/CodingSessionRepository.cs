using Alvind0.CodingTracker.Models;
using Dapper;
using static Alvind0.CodingTracker.Models.Enums;

namespace Alvind0.CodingTracker.Data;


public class CodingSessionRepository : Repository
{
    public CodingSessionRepository(string connectionString) : base(connectionString) { }

    public void CreateTableIfNotExists()
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            var query = @"
            CREATE TABLE IF NOT EXISTS 'Coding Sessions'(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StartTime TEXT NOT NULL,
            EndTime TEXT NOT NULL,
            Duration TEXT)";

            connection.Execute(query);
        }
    }

    public void AddSession(DateTime start, DateTime end)
    {
        var session = new CodingSession
        {
            StartTime = start,
            EndTime = end
        };

        using (var connection = GetConnection())
        {
            connection.Open();
            var query = @"
INSERT INTO 'Coding Sessions' (StartTime, EndTime, Duration) 
VALUES (@StartTime, @EndTime, @Duration);";
            connection.Execute(query, session);
        }
    }

    public void UpdateSession(int id, bool isUpdateStart, bool isUpdateEnd, DateTime? startTime = null, DateTime? endTime = null)
    {
        if (!isUpdateStart && !isUpdateEnd)
        {
            Console.WriteLine("Nothing to update here.");
            return;
        }

        using (var connection = GetConnection())
        {
            connection.Open();
            if (startTime == null)
            {
                var databaseStartTime = @"SELECT StartTime FROM 'Coding Sessions' WHERE Id = @Id";
                startTime = connection.QuerySingle<DateTime>(databaseStartTime, new CodingSession { Id = id });
            }

            if (endTime == null)
            {
                var databaseEndTime = @"SELECT EndTime FROM 'Coding Sessions' WHERE Id = @Id";
                endTime = connection.QuerySingle<DateTime>(databaseEndTime, new CodingSession { Id = id });
            }

            var session = new CodingSession
            {
                Id = id,
                StartTime = startTime.HasValue ? startTime.Value : throw new Exception("StartTime has no value."),
                EndTime = endTime
            };

            var query = @"
UPDATE 'Coding Sessions' SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";

            connection.Execute(query, session);
        }
    }

    public void DeleteSession(int id)
    {
        using (var connection = GetConnection())
        {
            var query = @"DELETE FROM 'Coding Sessions' WHERE Id = @Id";
            connection.Execute(query, new CodingSession { Id = id });
        }
    }

    // Get coding sessions with the ability to sort/filter by period
    // Can get coding sessions without any sort/filter
    public IEnumerable<CodingSession> GetCodingSessions
        (PeriodFilter period = PeriodFilter.None, SortType sortType = SortType.Default, SortOrder sortOrder = SortOrder.Default)
    {
        var query = GetSortingQuery(sortType, sortOrder) + GetFilterQuery(period);

        using (var connection = GetConnection())
        {
            var sessions = connection.Query<CodingSession>(query);
            return sessions;
        }
    }

    public string GetFilterQuery(PeriodFilter period)
    {
        var filterQuery = "";
        switch (period)
        {
            case PeriodFilter.ThisYear:
                filterQuery = $@"WHERE strftime('%Y', StartTime) = strftime('%Y', 'now')";
                break;
            case PeriodFilter.ThisMonth:
                filterQuery = $@"WHERE strftime('%Y', StartTime) = strftime('%Y', 'now') AND strftime('%m', StartTime) = strftime('%m', 'now')";
                break;
            case PeriodFilter.ThisWeek:
                filterQuery = $@"WHERE strftime('%Y', StartTime) = strftime('%Y', 'now') AND strftime('%U', StartTime) = strftime('%U', 'now')";
                break;
        }
        return filterQuery;
    }

    public static string GetSortingQuery(SortType sortType, SortOrder sortOrder)
    {
        var query = "";
        switch (sortType, sortOrder)
        {
            case (SortType.Id, SortOrder.Ascending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY Id ASC ";
                break;
            case (SortType.Id, SortOrder.Descending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY Id DESC ";
                break;
            case (SortType.Date, SortOrder.Ascending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY StartTime ASC ";
                break;
            case (SortType.Date, SortOrder.Descending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY StartTime DESC ";
                break;
            case (SortType.Duration, SortOrder.Ascending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY Duration ASC ";
                break;
            case (SortType.Duration, SortOrder.Descending):
                query = @"SELECT * FROM 'Coding Sessions' ORDER BY Duration DESC ";
                break;
            default:
                query = @"SELECT * FROM 'Coding Sessions' ";
                break;
        }
        return query;
    }

    public bool VerifyIfIdExists(int id)
    {
        using (var connection = GetConnection())
        {
            var query = @"Select Id FROM 'Coding Sessions' WHERE Id = @Id";
            var isExists = connection.QuerySingleOrDefault<int?>(query, new { Id = id });
            return isExists != null ? true : false;
        }
    }
}
