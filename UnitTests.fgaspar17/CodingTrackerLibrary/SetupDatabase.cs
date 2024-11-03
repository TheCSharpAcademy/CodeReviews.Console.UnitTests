using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace CodingTrackerLibrary;

public class SetupDatabase
{
    public string ConnStr { get; set; }
    public SetupDatabase(string connStr)
    {
        ConnStr = connStr;
    }

    public void InitializeDatabase()
    {
        try
        {
            using (IDbConnection connection = new SqliteConnection(ConnStr))
            {
                connection.Open();

                connection.Execute(@"CREATE TABLE IF NOT EXISTS ""CodingSessions"" (
	                            ""Id""	INTEGER NOT NULL,
	                            ""StartTime""	TEXT NOT NULL,
	                            ""EndTime""	TEXT NOT NULL,
	                            PRIMARY KEY(""Id"" AUTOINCREMENT)
                                );

                                CREATE TABLE IF NOT EXISTS ""CodingGoals"" (
	                            ""Id""	INTEGER NOT NULL,
                                ""Name"" TEXT NOT NULL,
	                            ""StartTime""	TEXT NOT NULL,
	                            ""EndTime""	TEXT NOT NULL,
                                ""Hours"" INTEGER NOT NULL,
	                            PRIMARY KEY(""Id"" AUTOINCREMENT)
                                );");

                connection.Close();
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error ocurred: {ex.Message}");
        }
    }

    public void SeedData()
    {
        string table = "CodingSessions";
        try
        {
            InitializeDatabase();

            using (IDbConnection connection = new SqliteConnection(ConnStr))
            {
                connection.Open();

                connection.Execute(RandomInsertValues(table, 20));

                connection.Close();
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error ocurred: {ex.Message}");
        }
    }

    private string RandomInsertValues(string table, int numberOfInserts)
    {
        Random rnd = new Random();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < numberOfInserts; i++)
        {
            DateTime startTime = new DateTime(rnd.Next(2003, 2024), rnd.Next(1, 13), rnd.Next(1, 29), rnd.Next(0, 24), rnd.Next(0, 60), 0);
            DateTime endTime = startTime.AddHours(rnd.Next(0, 24)).AddMinutes(rnd.Next(0, 60));

            sb.Append($"\nINSERT INTO {table}(StartTime, EndTime) VALUES('{startTime:yyyy-MM-dd HH:mm}', '{endTime:yyyy-MM-dd HH:mm}');");
        }

        return sb.ToString();
    }
}
