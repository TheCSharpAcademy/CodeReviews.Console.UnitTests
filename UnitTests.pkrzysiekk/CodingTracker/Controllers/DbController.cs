using System.Configuration;
using CodingTracker.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker.Controllers
{
    public class DbController
    {
        private readonly string? _connectionString;
        public DbController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SQLiteDB"].ConnectionString;
            if (String.IsNullOrEmpty(_connectionString))
            {
                throw new ConfigurationErrorsException();
            }
            InitializeDB();
        }
        public void InitializeDB()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = @"CREATE TABLE IF NOT EXISTS Coding_Session(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StartDate TEXT,
                EndDate TEXT,
                Duration INTEGER
                );";
                connection.Execute(sql);
            }
        }
        public void Insert(CodingSession session)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = @"INSERT INTO Coding_Session (StartDate,EndDate,Duration) VALUES
                     
                    (@StartDate,@EndDate,@Duration)
                   ";
                connection.Execute(sql, session);
            }
        }
        public void Remove(int? Id)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                var sql = @"DELETE FROM Coding_Session WHERE Id=@Id";
                connection.Execute(sql,new { Id });
            }
        }

        public void Update(int? id,CodingSession session)
        {
            session.Id = id;
            using(var connection =new SqliteConnection(_connectionString))
            {
                var sql = @"UPDATE Coding_Session
                        SET StartDate=@StartDate,
                            EndDate=@EndDate,
                            Duration=@Duration
                        WHERE Id=@Id";
                connection.Execute(sql, session);
            }
        }
    public List<CodingSession> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Coding_Session";
                List<CodingSession> sessions = connection.Query<CodingSession>(sql).ToList();
                return sessions;
            }
        }
    }
}
