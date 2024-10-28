using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;

namespace CodingTrackerLibrary;

public static class SqlExecutionService
{
    public static bool ExecuteCommand<T>(string sql, T model, string connStr)
    {
        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                connection.Execute(sql, model);

                connection.Close();
            }

            return true;
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error ocurred: {ex.Message}");
            return false;
        }
    }
}
