using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace CodingTrackerLibrary;

public static class CodingGoalController
{
    public static List<CodingGoal> GetCodingGoals(string connStr)
    {
        List<CodingGoal> goals = new List<CodingGoal>();
        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                goals = connection.Query<CodingGoal>("SELECT Id, Name, StartTime, EndTime, Hours FROM CodingGoals").ToList();

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ocurred: {ex.Message}");
        }

        return goals;
    }

    public static CodingGoal GetCodingGoalById(int id, string connStr)
    {
        CodingGoal goal = null;

        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                goal = connection.QueryFirstOrDefault<CodingGoal>("SELECT Id, Name, StartTime, EndTime, Hours FROM CodingGoals WHERE Id = @Id", new { Id = id });

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ocurred: {ex.Message}");
        }

        return goal;
    }

    public static bool InsertCodingGoal(CodingGoal goal, string connStr)
    {
        string sql = $@"INSERT INTO CodingGoals (Name, StartTime, EndTime, Hours) 
                                        VALUES (@Name, @StartTime, @EndTime, @Hours);";

        return SqlExecutionService.ExecuteCommand<CodingGoal>(sql, goal, connStr);
    }

    public static bool UpdateCodingGoal(CodingGoal goal, string connStr)
    {
        string sql = $@"UPDATE CodingGoals SET
                                Name = @Name,
                                StartTime = @StartTime, 
                                EndTime = @EndTime,
                                Hours = @Hours
                                WHERE Id = @Id;";

        return SqlExecutionService.ExecuteCommand<CodingGoal>(sql, goal, connStr);
    }

    public static bool DeleteCodingGoal(CodingGoal goal, string connStr)
    {
        string sql = $@"DELETE FROM CodingGoals
                                WHERE Id = @Id;";

        return SqlExecutionService.ExecuteCommand<CodingGoal>(sql, goal, connStr);
    }
}