using Dapper;
using System.Data.SQLite;

namespace CodingTracker
{
    /// <summary>
    /// Repository for managing <see cref="CodingSession"/> entities
    /// Implements <see cref="ICodingSessionRepository"/>
    /// </summary>
    internal class CodingSessionRepository : ICodingSessionRepository
    {
        private readonly string _connectionString;
        public string RepositoryPath { get; }

        /// <summary>
        /// Initializes new repository object
        /// </summary>
        /// <param name="connectionString">string value defining the connection string</param>
        /// <param name="repositoryPath">string value containing path to database file</param>
        public CodingSessionRepository(string connectionString, string repositoryPath)
        {
            _connectionString = connectionString;
            RepositoryPath = repositoryPath;
        }
        /// <inheritdoc/>
        public void CreateRepository()
        {
            string sql = "CREATE TABLE [CodingSessions] (" +
                "Id INTEGER PRIMARY KEY," +
                "Start TEXT," +
                "End TEXT," +
                "Duration TEXT);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql);
        }
        /// <inheritdoc/>
        public void Insert(CodingSession entity)
        {
            string sql = "INSERT INTO [CodingSessions] ([Start], [End], [Duration]) VALUES (@Start, @End, @Duration);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }
        /// <inheritdoc/>
        public void Delete(CodingSession entity)
        {
            string sql = "DELETE FROM [CodingSessions] WHERE Id=@Id;";
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, new { Id = entity.Id });
        }
        /// <inheritdoc/>
        public IEnumerable<CodingSession> GetAll()
        {
            string sql = "SELECT [ID], [Start], [End] FROM [CodingSessions]";
            using var connection = new SQLiteConnection(_connectionString);
            var sessions = connection.Query<CodingSession>(sql);
            return sessions;
        }
        /// <inheritdoc/>
        public void Update(CodingSession entity)
        {
            string sql = "UPDATE [CodingSessions] " +
                "SET Start=@Start, End=@End, Duration=@Duration " +
                "WHERE Id=@Id;";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }
        /// <inheritdoc/>
        public void InsertBulk(IEnumerable<CodingSession> entities)
        {
            string sql = "INSERT INTO [CodingSessions] ([Start], [End], [Duration]) VALUES (@Start, @End, @Duration);";

            using var connection = new SQLiteConnection(_connectionString);

            foreach (var entity in entities)
            {
                connection.Execute(sql, entity);
            }
        }
        /// <inheritdoc/>
        public IEnumerable<CodingSession> GetSessionsWithinRange(DateTime startDate, DateTime endDate)
        {
            string sql = "SELECT * FROM [CodingSessions] WHERE [Start] >= @start AND [END]<@end;";

            using var connection = new SQLiteConnection(_connectionString);
            var sessions = connection.Query<CodingSession>(sql, new { start = startDate, end = endDate });

            return sessions;
        }
    }
}