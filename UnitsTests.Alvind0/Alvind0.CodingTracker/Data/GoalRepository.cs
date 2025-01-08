using System.Globalization;
using Alvind0.CodingTracker.Models;
using Dapper;

namespace Alvind0.CodingTracker.Data;
public class GoalRepository : Repository
{
    public GoalRepository(string connectionString) : base(connectionString) { }

    public void CreateTableIfNotExists()
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            var query = @"
            CREATE TABLE IF NOT EXISTS Goals(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            'Goal Name' TEXT NOT NULL,
            Goal TEXT NOT NULL,
            StartDate TEXT NOT NULL,
            EndDate TEXT NOT NULL,
            Progress TEXT DEFAULT '00:00:00',
            IsCompleted INTEGER DEFAULT 0);
            ";

            connection.Execute(query);
        }
    }

    public void AddGoal(string name, TimeSpan durationGoal, DateTime start, DateTime end)
    {
        var goal = new Goal
        {
            Name = name,
            DurationGoal = durationGoal,
            StartDate = start,
            EndDate = end,
        };

        using (var connection = GetConnection())
        {
            connection.Open();
            var query = @"
INSERT INTO Goals ('Goal Name', Goal, StartDate, EndDate) 
VALUES (@Name, @DurationGoal, @StartDate, @EndDate)";
            connection.Execute(query, goal);
        }
    }

    public void UpdateGoal
        (int id, string query, string? name = null, TimeSpan? durationGoal = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        using (var connection = GetConnection())
        {
            var goal = new Goal
            {
                Id = id,
            };

            if (name != null) goal.Name = name;
            if (durationGoal != null)
            {
                goal.DurationGoal = durationGoal.Value;
                goal.DurationGoalString = durationGoal.Value.ToString(@"hh\:mm");
            }
            if (startDate != null) goal.StartDate = startDate.Value;
            if (endDate != null) goal.EndDate = endDate.Value;

            connection.Execute(query, goal);
        }
    }

    public void DeleteGoal(int id)
    {
        using (var connection = GetConnection())
        {
            var query = @"DELETE FROM Goals WHERE Id = @Id";
            connection.Execute(query, new Goal { Id = id });
        }
    }

    public IEnumerable<Goal> GetGoals()
    {
        using (var connection = GetConnection())
        {
            var query = @"SELECT Id, ""Goal Name"" as Name, Goal as DurationGoalString, StartDate, EndDate, Progress as ProgressString, IsCompleted FROM Goals"; // Select into DurationGoalString
            var goals = connection.Query<Goal>(query);

            foreach (var goal in goals)
            {
                if (TimeSpan.TryParseExact(goal.DurationGoalString, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var durationGoal))
                {
                    goal.DurationGoal = durationGoal;
                }
                else
                {
                    goal.DurationGoal = TimeSpan.Zero; 
                    Console.WriteLine($"Warning: Could not parse DurationGoal for Goal Id: {goal.Id}");
                }

                if (TimeSpan.TryParseExact(goal.ProgressString, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var progress))
                {
                    goal.Progress = progress;
                }
                else
                {
                    goal.Progress = TimeSpan.Zero;
                    Console.WriteLine($"Warning: Could not parse Progress for Goal Id: {goal.Id}");
                }
            }

            return goals;
        }
    }

    internal bool VerifyIfIdExists(int id)
    {
        using (var connection = GetConnection())
        {
            var query = @"Select Id FROM Goals WHERE Id = @Id";
            var isExists = connection.QuerySingleOrDefault<int?>(query, new Goal { Id = id });
            return isExists != null ? true : false;
        }
    }
}

