using System.Globalization;
using Alvind0.CodingTracker.Data;
using Alvind0.CodingTracker.Models;
using Alvind0.CodingTracker.Views;
using Spectre.Console;
namespace Alvind0.CodingTracker.Controllers;

public class GoalController
{
    private readonly GoalRepository _repository;
    private readonly CodingSessionRepository _sessionRepository;
    private readonly TableRenderer _tableRenderer;

    public GoalController(GoalRepository repository, CodingSessionRepository sessionRepository, TableRenderer renderer)
    {
        _repository = repository;
        _sessionRepository = sessionRepository;
        _tableRenderer = renderer;
    }

    public void AddGoal()
    {

        var goal = new Goal
        {
            DurationGoal = GetDurationGoal(),
            StartDate = GetDate("Enter start date(format: \'MM-dd-yy\': "),
            EndDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ")
        };
        if (AnsiConsole.Confirm("Do you want to name your goal?")) goal.Name = GetGoalName();

        _repository.AddGoal(goal.Name, goal.DurationGoal, goal.StartDate, goal.EndDate);
    }

    public void EditGoal()
    {
        var isUpdateName = false;
        var isUpdateGoal = false;
        var isUpdateStartDate = false;
        var isUpdateEndDate = false;

        var id = GetId("Enter the Id for the goal do you want to edit.");
        var updateItems = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Choose items to edit: ")
            .InstructionsText(
                "(Press [blue]<space>[/] to toggle options and [green]<enter>[/] to accept.")
            .AddChoices(new[]
            {
                "Name", "Goal", "Start Date", "Deadline"
            }));

        if (updateItems.Contains("Name")) isUpdateName = true;
        if (updateItems.Contains("Goal")) isUpdateGoal = true;
        if (updateItems.Contains("Start Date")) isUpdateStartDate = true;
        if (updateItems.Contains("Deadline")) isUpdateEndDate = true;

        string query = "";
        string? name; TimeSpan? durationGoal; DateTime? startDate, endDate;

        int flags = (isUpdateName ? 1 : 0)
                     | (isUpdateGoal ? 2 : 0)
                     | (isUpdateStartDate ? 4 : 0)
                     | (isUpdateEndDate ? 8 : 0);

        switch (flags)
        {
            case 0:
                Console.WriteLine("No updates needed.");
                return;
            case 1:
                query = @"UPDATE Goals SET 'Goal Name' = @Name WHERE Id = @Id";
                name = GetGoalName();
                _repository.UpdateGoal(id, query, name);
                break;
            case 2:
                query = @"UPDATE Goals SET Goal = @DurationGoalString WHERE Id = @Id";
                durationGoal = GetDurationGoal();
                _repository.UpdateGoal(id, query, null, durationGoal);
                break;
            case 3:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, Goal = @DurationGoalString WHERE Id = @Id";
                name = GetGoalName();
                isUpdateName = true;
                durationGoal = GetDurationGoal();
                _repository.UpdateGoal(id, query, name, durationGoal);
                break;
            case 4:
                query = @"UPDATE Goals SET StartDate = @StartDate WHERE Id = @Id";
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, null, startDate);
                break;
            case 5:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, StartDate = @StartDate WHERE Id = @Id";
                name = GetGoalName();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, null, startDate);
                break;
            case 6:
                query = @"UPDATE Goals SET Goal = @DurationGoalString, StartDate = @StartDate WHERE Id = @Id";
                durationGoal = GetDurationGoal();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, durationGoal, startDate);
                break;
            case 7:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, Goal = @DurationGoalString, StartDate = @StartDate WHERE Id = @Id";
                name = GetGoalName();
                durationGoal = GetDurationGoal();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, durationGoal, startDate);
                break;
            case 8:
                query = @"UPDATE Goals SET EndDate = @EndDate WHERE Id = @Id";
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, null, null, endDate);
                break;
            case 9:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, EndDate = @EndDate WHERE Id = @Id";
                name = GetGoalName();
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, null, null, endDate);
                break;
            case 10:
                query = @"UPDATE Goals SET Goal = @DurationGoalString, EndDate = @EndDate WHERE Id = @Id";
                durationGoal = GetDurationGoal();
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, durationGoal, null, endDate);
                break;
            case 11:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, Goal = @DurationGoalString, EndDate = @EndDate WHERE Id = @Id";
                name = GetGoalName();
                durationGoal = GetDurationGoal();
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, durationGoal, null, endDate);
                break;
            case 12:
                query = @"UPDATE Goals SET StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, null, startDate, endDate);
                break;
            case 13:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";
                name = GetGoalName();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, null, startDate, endDate);
                break;
            case 14:
                query = @"UPDATE Goals SET Goal = @DurationGoalString, StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";
                durationGoal = GetDurationGoal();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, null, durationGoal, startDate, endDate);
                break;
            case 15:
                query = @"UPDATE Goals SET 'Goal Name' = @Name, Goal = @DurationGoalString, StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";
                name = GetGoalName();
                durationGoal = GetDurationGoal();
                startDate = GetDate("Enter start date (format : 'MM-dd-yy'): ");
                endDate = GetDate("Enter deadline (format : 'MM-dd-yy'): ");
                _repository.UpdateGoal(id, query, name, durationGoal, startDate, endDate);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(flags), flags, "Invalid combination of update flags.");
        }
    }

    public void RemoveGoal()
    {
        var id = GetId("Enterr the Id for the goal you want to delete.");
        _repository.DeleteGoal(id);
    }

    public void ViewGoals()
    {
        var goals = _repository.GetGoals();
        var sessions = _sessionRepository.GetCodingSessions();

        foreach (var goal in goals)
        {
            goal.Progress = CalculateProgress(goal, sessions);
            goal.ProgressString = goal.Progress.ToString(@"hh\:mm");
            goal.DurationGoalString = goal.DurationGoal.ToString(@"hh\:mm");
        }
        _tableRenderer.RenderGoalsTable(goals);
    }

    private TimeSpan CalculateProgress(Goal goal, IEnumerable<CodingSession> sessions)
    {
        var progress = TimeSpan.Zero;
        foreach (var session in sessions)
        {
            if (session.StartTime >= goal.StartDate && session.EndTime <= goal.EndDate)
            {
                progress += session.Duration;
            }
        }
        return progress;
    }

    private string GetGoalName()
    {
        return AnsiConsole.Ask<String>("What would you like to name the goal? ");
    }

    private TimeSpan GetDurationGoal()
    {
        while (true)
        {
            var userInput = AnsiConsole.Ask<string>(@"Input your coding goal (formats : ""hh:mm"", ""h:mm"")");
            if (TimeSpan.TryParseExact(userInput, new string[] { @"hh\:mm", @"h\:mm" }, CultureInfo.InvariantCulture, out var durationGoal))
                return durationGoal;

            AnsiConsole.Clear();
            AnsiConsole.WriteLine("Please enter in the correct format.");
        }
    }

    private DateTime GetDate(string message)
    {
        while (true)
        {
            var time = AnsiConsole.Ask<string>(message);
            if (DateTime.TryParseExact(time, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("Please enter in the correct format.");
        }
    }

    private int GetId(string message)
    {
        ViewGoals();
        while (true)
        {
            var id = AnsiConsole.Ask<int>(message);
            var isExists = _repository.VerifyIfIdExists(id);
            if (isExists) return id;
            Console.WriteLine("Record does not exists.");
        }
    }


}
