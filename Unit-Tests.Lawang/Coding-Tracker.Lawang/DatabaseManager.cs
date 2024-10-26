using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace Lawang.Coding_Tracker;

public class DatabaseManager
{
    private readonly string _connectionString;
    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void CreateTable()
    {
        try
        {
            var createSQL =
                @"CREATE TABLE IF NOT EXISTS CodingSessions(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT,
                    Date TEXT
                )";

            var createGoalsSQL = 
            @"CREATE TABLE IF NOT EXISTS CodingGoals(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Time_to_complete TEXT,
                Avg_Time_To_Code TEXT,
                Days_left INTEGER 
            );";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Execute(createSQL);
                connection.Execute(createGoalsSQL);
            }

        }
        catch (SqliteException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
    }

    public void SeedDataIntoTable()
    {
        try
        {
            if (IsEmpty())
            {
                var insertSQL =
                                @"INSERT INTO CodingSessions (StartTime, EndTime, Duration, Date)
                    VALUES('10:00 PM', '02:30 PM', '04:30:00', '22/08/2023'),
                          ('02:00 PM', '04:40 PM', '02:40:00', '23/05/2024'),
                          ('05:00 PM', '09:00 PM', '04:00:00', '24/06/2024'),
                          ('05:00 AM', '10:00 AM', '05:00:00', '14/07/2024'),
                          ('03:00 PM', '10:00 PM', '07:00:00', '01/07/2024'),
                          ('08:00 PM', '11:00 PM', '03:00:00', '10/08/2024'),
                          ('09:00 PM', '11:00 PM', '02:00:00', '15/08/2024'),
                          ('08:00 PM', '10:00 PM', '02:00:00', '20/08/2024')";

                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                connection.Execute(insertSQL);
                connection.Close();
            }

        }
        catch (SqliteException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Console.ReadLine();
        }
    }

    private bool IsEmpty()
    {
        try
        {
            string countRow =
                @"SELECT COUNT(*) FROM CodingSessions";

            using var connection = new SqliteConnection(_connectionString);
            int rowCount = connection.ExecuteScalar<int>(countRow);

            return rowCount == 0;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        return false;
    }
}
