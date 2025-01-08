using Microsoft.Data.Sqlite;

namespace Alvind0.CodingTracker.Data;

public class Repository
{
    protected string _connectionString;

    protected Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}
