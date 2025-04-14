using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker;

public class DapperHelper
{
    private readonly SqliteConnection _connection;
    private readonly IConfiguration _config;
    private readonly string _table;

    public DapperHelper(IConfiguration config)
    {
        _config = config;
        _table = config["TableName"] ?? "CodingSessions";
        _connection = new SqliteConnection(_config.GetConnectionString("SQLite"));

        InitializeDb();
    }

    public void InitializeDb()
    {
        var sql = $@"CREATE TABLE IF NOT EXISTS {_table} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration INTEGER
                    );";

        _connection.Execute(sql);

        if (_config["UseExampleData"] is not null and "True")
        {
            DeleteAllSessions();
            PopulateDb();
        }
    }

    public bool IsTableCreated()
    {
        var sql = $@"SELECT COUNT() FROM (
                        SELECT name FROM sqlite_master 
                        WHERE type='table' AND name=@TableName)";

        var parameters = new { TableName = _table };

        var res = _connection.ExecuteScalar<int>(sql, parameters);
        return res > 0;
    }

    public void PopulateDb()
    {
        Random rand = new();
        DateTime startTime = new(2023, 1, 1);
        for (int i = 0; i < 100; i++)
        {
            startTime = startTime.AddDays(rand.Next(4));
            int hour = rand.Next(7, 20);
            int minute = rand.Next(60);
            startTime = startTime.AddHours(hour);
            startTime = startTime.AddMinutes(minute);

            var endTime = startTime.AddMinutes(rand.Next(240));
            Console.WriteLine("Adding session...");
            Insert(new CodingSession(startTime, endTime));
        }
    }

    public void TeardownDB()
    {
        var sql = $@"DROP TABLE {_table}";
        _connection.Execute(sql);
    }

    // CREATE
    public int Insert(CodingSession sesh)
    {
        var sql = $"INSERT INTO {_table} (StartTime, EndTime, Duration) values (@StartTime, @EndTime, @Duration);";
        var parameters = new { sesh.StartTime, sesh.EndTime, sesh.Duration };
        var affectedRows = _connection.Execute(sql, parameters);
        return affectedRows;
    }

    // RETRIEVE
    public bool TryGetSession(int id, out CodingSession session)
    {
        var sql = $"SELECT * FROM {_table} WHERE id = @Id";
        var parameters = new { Id = id };

        session = _connection.QueryFirstOrDefault<CodingSession>(sql, parameters)!;
        
        return session is not null;
    }

    public List<CodingSession> GetAllSessions()
    {
        var sql = $"SELECT * FROM {_table}";

        return _connection.Query<CodingSession>(sql).ToList();
    }

    public List<CodingSession> GetSessionsByYear(string year)
    {
        var sql = $"SELECT * FROM {_table} WHERE StartTime LIKE @Year";

        return _connection.Query<CodingSession>(sql, new { Year = $"{year}%" }).ToList();
    }

    public List<CodingSession> GetSessionsByMonth(string month)
    {
        var sql = $"SELECT * FROM {_table} WHERE StartTime LIKE @Month";

        return _connection.Query<CodingSession>(sql, new { Month = $"{month}%" }).ToList();
    }

    public List<CodingSession> GetSessionsByDuration(int min, int max)
    {
        var sql = $"SELECT * FROM {_table} WHERE Duration BETWEEN @min AND @max;";

        return _connection.Query<CodingSession>(sql, new { min, max }).ToList();
    }

    // UPDATE
    public bool UpdateSession(CodingSession session)
    {
        if (TryGetSession(session.Id, out var _))
        {
            session.SaveDuration();
            var sql = $@"UPDATE {_table} 
                         SET StartTime = @StartTime,
                             EndTime = @EndTime,
                             Duration = @Duration
                         WHERE Id = @Id;";
            var parameters = new {session.StartTime, session.EndTime, session.Duration, session.Id};
            int rowsAffected = _connection.Execute(sql, parameters);
            return rowsAffected == 1;
        }
        return false;
    }

    // DELETE
    public bool DeleteSessionById(int id)
    {
        var sql = $"DELETE FROM {_table} WHERE Id = @Id;";
        var parameters = new { Id = id };
        return _connection.Execute(sql, parameters) == 1;
    }

    public bool DeleteAllSessions()
    {
        var sql = $"DELETE FROM {_table};";

        _connection.Execute(sql);

        return GetAllSessions().Count == 0;
    }
}
