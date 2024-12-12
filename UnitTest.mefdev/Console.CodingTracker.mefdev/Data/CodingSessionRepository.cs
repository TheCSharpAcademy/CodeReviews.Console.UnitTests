using CodingLogger.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingLogger.Data
{
    public class CodingSessionRepository: ICodingSessionRepository
    {
        public string _connectionString;

        public CodingSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public async Task Create(CodingSession codingSession)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"INSERT INTO codingSession (" +
                        $"{nameof(codingSession.Id)}, " +
                        $"{nameof(codingSession.Duration)}, " +
                        $"{nameof(codingSession.StartTime)}," +
                        $"{nameof(codingSession.EndTime)})" +
                        $"VALUES (@{nameof(codingSession.Id)}, " +
                        $"@{nameof(codingSession.Duration)}, " +
                        $"@{nameof(codingSession.StartTime)}, " +
                        $"@{nameof(codingSession.EndTime)})";
                    var parm = new
                    {
                        Id = codingSession.Id,
                        Duration = codingSession.Duration,
                        StartTime = codingSession.StartTime,
                        EndTime = codingSession.EndTime,
                    };
                    await connection.ExecuteAsync(sql, parm);

                }
            }
            catch
            {
                throw new Exception("Invalid operation: An error has occured while creating a coding session");
            }

        }

        public async Task Delete(int key)
        {
            try
            {
                string sql = $"DELETE FROM {nameof(CodingSession)} WHERE Id=@Id";
                using (var connection = GetConnection())
                {
                    await connection.ExecuteAsync(sql, new { Id = key });
                }
            }
            catch
            {
                throw new Exception("Invalid operation: An error has occured while deleting a coding session");

            }
        }

        public async Task<CodingSession?> Retrieve(int key)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT * FROM {nameof(CodingSession)} WHERE Id = @Id";
                    var param = new { Id = key };
                    var codingSession = await connection.QueryFirstOrDefaultAsync<CodingSession>(sql, param);
                    if (codingSession == null)
                    {
                        AnsiConsole.MarkupLine("[yellow]Notice: The coding session cannot be found[/]");
                    }
                    return codingSession;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                throw new Exception("Invalid Operation: An error has occured while retreiving a coding session");
            }


        }
        public async Task<List<CodingSession>> RetrieveAll()
        {
            try
            {
                List<CodingSession> codingSessionList = new List<CodingSession>();
                using (var connection = GetConnection())
                {
                    var reader = await connection.ExecuteReaderAsync($@"SELECT * FROM {nameof(CodingSession)};");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        long duration = reader.GetInt64(1);
                        DateTime start = reader.GetDateTime(2);
                        DateTime end = reader.GetDateTime(3);
                        var codingSession = new CodingSession(id, duration, start, end);
                        codingSessionList.Add(codingSession);
                    }
                }
                return codingSessionList;
            }
            catch
            {
                throw new Exception("Invalid Operation: An error has occured while retreiving all coding sessions");

            }

        }
        public async Task Update(CodingSession codingSession)
        {
            try
            {
                var sql = $"UPDATE {nameof(codingSession)} SET " +
                  $"{nameof(codingSession.Duration)} = @Duration, " +
                  $"{nameof(codingSession.StartTime)} = @StartTime, " +
                  $"{nameof(codingSession.EndTime)} = @EndTime " +
                  $"WHERE {nameof(codingSession.Id)} = @Id";
                using (var connection = GetConnection())
                {
                    var parms = new
                    {
                        Id = codingSession.Id,
                        Duration = codingSession.Duration,
                        StartTime = codingSession.StartTime,
                        EndTime = codingSession.EndTime,
                    };
                    await connection.ExecuteAsync(sql, parms);
                }

            }
            catch
            {
                throw new Exception("Invalid Operation: An error has occured while updating a coding session");
            }

        }

    }
}
