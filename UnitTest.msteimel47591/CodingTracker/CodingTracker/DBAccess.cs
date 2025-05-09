using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;


namespace CodingTracker
{
    public static class DBAccess
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        private static string tableName = ConfigurationManager.AppSettings.Get("TableName");

        internal static void CreateDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                SqliteCommand tblCmd = connection.CreateCommand();

                tblCmd.CommandText =
                    $@"CREATE TABLE IF NOT EXISTS {tableName}(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration TEXT,
                        Focus TEXT
                        )";

                tblCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static void AddSession(CodingSession codingSession)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {

                var sql = $@"INSERT INTO {tableName} (StartTime, EndTime, Duration, Focus) 
                             VALUES (@StartTime, @EndTime, @Duration, @Focus)";

                var rowsAffected = connection.Execute(sql, new
                {
                    StartTime = codingSession.StartTime,
                    EndTime = codingSession.EndTime,
                    Duration = codingSession.Duration,
                    Focus = codingSession.Focus
                });

            }
        }

        public static List<CodingSession> GetAllSessions()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var sql = $@"SELECT Id, StartTime, EndTime, Duration, Focus FROM {tableName}";

                var sessions = connection.Query<CodingSession>(sql).AsList();
                return sessions;
            }
        }

        internal static void DeleteSession(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var sql = $@"DELETE FROM {tableName} WHERE Id = @Id";

                var rowsAffected = connection.Execute(sql, new { Id = id });

            }
        }

        internal static void EditSession(CodingSession codingSession)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var sql = $@"UPDATE {tableName} 
                     SET StartTime = @StartTime, 
                         EndTime = @EndTime, 
                         Duration = @Duration, 
                         Focus = @Focus 
                     WHERE Id = @Id";

                var rowsAffected = connection.Execute(sql, new
                {
                    Id = codingSession.Id,
                    StartTime = codingSession.StartTime,
                    EndTime = codingSession.EndTime,
                    Duration = codingSession.Duration,
                    Focus = codingSession.Focus
                });

            }
        }

        public static List<CodingSession> GetSessionsByDay(DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string query = $@"
            SELECT 
                Id, 
                StartTime, 
                EndTime, 
                Focus, 
                Duration 
            FROM {tableName}
            WHERE strftime('%Y-%m-%d', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = @Date";


                return connection.Query<CodingSession>(query, new { Date = date.ToString("yyyy-MM-dd") }).ToList();
            }
        }

        public static List<CodingSession> GetSessionsByWeek(DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string query = $@"
        SELECT 
            Id, 
            StartTime, 
            EndTime, 
            Focus, 
            Duration 
        FROM {tableName}
        WHERE strftime('%W', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = strftime('%W', @Date)
          AND strftime('%Y', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = strftime('%Y', @Date)";

                return connection.Query<CodingSession>(query, new { Date = date.ToString("yyyy-MM-dd") }).ToList();
            }
        }

        public static List<CodingSession> GetSessionsByMonth(DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string query = $@"
        SELECT 
            Id, 
            StartTime, 
            EndTime, 
            Focus, 
            Duration 
        FROM {tableName}
        WHERE strftime('%m', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = strftime('%m', @Date)
          AND strftime('%Y', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = strftime('%Y', @Date)";

                return connection.Query<CodingSession>(query, new { Date = date.ToString("yyyy-MM-dd") }).ToList();
            }
        }

        public static List<CodingSession> GetSessionsByYear(DateTime date)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string query = $@"
        SELECT 
            Id, 
            StartTime, 
            EndTime, 
            Focus, 
            Duration 
        FROM {tableName}
        WHERE strftime('%Y', '20' || substr(StartTime, 7, 2) || '-' || substr(StartTime, 1, 2) || '-' || substr(StartTime, 4, 2)) = strftime('%Y', @Date)";

                return connection.Query<CodingSession>(query, new { Date = date.ToString("yyyy-MM-dd") }).ToList();
            }
        }
    }
}
