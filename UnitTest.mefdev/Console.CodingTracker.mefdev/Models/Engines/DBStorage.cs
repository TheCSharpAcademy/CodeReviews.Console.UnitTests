using System.Data;
using Microsoft.Data.Sqlite;
namespace CodingLogger.Models
{
    public class DBStorage
    {
        private readonly string _connectionString;
        public readonly IDbConnectionFactory _connectionFactory;
        public DBStorage(IDbConnectionFactory connectionFactory, string connectionString, string tableName)
        {
            _connectionFactory = connectionFactory;
            _connectionString = connectionString;
            CreateTable(tableName);

        }
        protected DBStorage()
        {

        }
        public IDbConnection GetConnection()
        {
            return _connectionFactory.CreateConnection(_connectionString);
        }
        public void CreateTable(string tableName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS {tableName}(id INTEGER PRIMARY KEY, duration INTEGER, StartTime dateTime, EndTime datetime)";
                command.ExecuteNonQuery();
                CheckTableCreation(connection, tableName);
            }
        }
        public static void CheckTableCreation(IDbConnection connection, string tableName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName";
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@tableName";
                parameter.Value = tableName;
                command.Parameters.Add(parameter);
                var tableExists = command.ExecuteScalar();
                if (tableExists != null)
                {
                    Console.WriteLine("The table is created successfully.\n");
                }
                else
                {
                    Console.WriteLine("The table is not created successfully.");
                }
            }
            
        }
    }

    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(string connectionString);
    }
    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}

