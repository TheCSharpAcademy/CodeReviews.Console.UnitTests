using System.Globalization;
using Alvind0.CodingTracker.Data;
using Alvind0.CodingTracker.Models;
using Alvind0.CodingTracker.Utilities;
using Alvind0.CodingTracker.Views;
using Spectre.Console;
using static Alvind0.CodingTracker.Models.Enums;

namespace Alvind0.CodingTracker.Controllers;

public class CodingSessionController
{
    private readonly CodingSessionRepository _repository;
    private readonly TableRenderer _renderer;
    private readonly StopwatchHelper _stopwatchHelper = new();
    public CodingSessionController(CodingSessionRepository repository, TableRenderer renderer)
    {
        _repository = repository;
        _renderer = renderer;
    }

    public async Task RunStopwatch()
    {
        DateTime startTime = new(), endTime = new();

        while (true)
        {
            bool isEndedStopwatch = false;

            var options = MenuHelper.GetStopwatchMenu(_stopwatchHelper.State);
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices<string>(options));

            switch (selectedOption)
            {
                case "Start":
                    startTime = DateTime.Now;
                    _stopwatchHelper.StartStopwatch();
                    break;
                case "End":
                    endTime = DateTime.Now;
                    isEndedStopwatch = true;
                    _stopwatchHelper.StopStopwatch();
                    break;
            }
            if (isEndedStopwatch) break;
            await Task.Delay(60);
        }
        if (AnsiConsole.Confirm("Log this session?")) LogSession(startTime, endTime);
    }

    public void LogSession(DateTime startTime, DateTime endTime)
    {
        _repository.AddSession(startTime, endTime);
    }
    public void LogSessionManually()
    {
        DateTime startTime, endTime;
        while (true)
        {
            startTime = GetTime("Insert Start Time (Format: MM-dd-yy H:mm): ");
            endTime = GetTime("Insert End Time (Format: MM-dd-yy H:mm): ");
            if (ValidateStartAndEndTime(startTime, endTime)) break;

            Console.Clear();
            Console.WriteLine("Start time cannot be later than end time.");
        }
        _repository.AddSession(startTime, endTime);
    }

    public void EditSession()
    {
        var isUpdateStart = false;
        var isUpdateEnd = false;
        DateTime startTime, endTime;
        var id = GetId("Enter the Id for the session do you want to edit.");
        var updateItems = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Choose items to edit: ")
            .InstructionsText(
                "(Press [blue]<space>[/] to toggle options and [green]<enter>[/] to accept.")
            .AddChoices(new[]
            {
                "Start time", "End time"
            }));


        if (updateItems.Contains("Start time") && updateItems.Contains("End time"))
        {
            isUpdateStart = true;
            isUpdateEnd = true;

            while (true)
            {
                startTime = GetTime(@"Enter start time (format: MM-dd-yy H:mm)");
                endTime = GetTime(@"Enter end time (format: MM-dd-yy H:mm)");
                if (ValidateStartAndEndTime(startTime, endTime)) break;

                Console.Clear();
                Console.WriteLine("Start time cannot be later than End time.");
            }
            _repository.UpdateSession(id, isUpdateStart, isUpdateEnd, startTime, endTime);
        }
        else if (updateItems.Contains("Start time"))
        {
            isUpdateStart = true;
            startTime = GetTime(@"Enter start time (format: MM-dd-yy H:mm)");
            _repository.UpdateSession(id, isUpdateStart, isUpdateEnd, startTime);
        }
        else if (updateItems.Contains("End time"))
        {
            isUpdateEnd = true;
            endTime = GetTime(@"Enter end time (format: MM-dd-yy H:mm)");
            _repository.UpdateSession(id, isUpdateStart, isUpdateEnd, endTime);
        }
    }

    private int GetId(string message)
    {
        ShowCodingSessions();
        while (true)
        {
            var id = AnsiConsole.Ask<int>(message);
            var isExists = _repository.VerifyIfIdExists(id);
            if (isExists) return id;
            Console.WriteLine("Record does not exists.");
        }
    }

    public DateTime GetTime(string message)
    {
        while (true)
        {
            var time = AnsiConsole.Ask<string>(message);
            var isValidDate = ValidateDateTime(time);

            if (isValidDate)
            {
                return DateTime.ParseExact(time, "MM-dd-yy H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }

            Console.Clear();
            Console.WriteLine("Please enter in the correct format.");
        }
    }

    public bool ValidateDateTime(string time)
    {
        time = time.Trim();
        if (DateTime.TryParseExact(time, "MM-dd-yy H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return true;
        }

        return false;
    }

    public bool ValidateStartAndEndTime(DateTime startTime, DateTime endTime)
    {
        return startTime <= endTime;
    }

    public void ShowCodingSessions(bool isOnlyView = false)
    {
        IEnumerable<CodingSession> sessions;
        if (isOnlyView)
        {
            var periodFilter = GetPeriodFilter();
            var sortType = GetSortType();
            var sortOrder = GetSortOrder();

            sessions = _repository.GetCodingSessions(periodFilter, sortType, sortOrder);
        }
        else sessions = _repository.GetCodingSessions();

        _renderer.RenderSesionsTable(sessions);
    }

    internal void DeleteSession()
    {
        ShowCodingSessions();
        var id = GetId("Enter the Id of the session you want to delete.");
        _repository.DeleteSession(id);
    }

    internal SortType GetSortType()
    {
        var sortType = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
         .Title("Sort by type: ")
                 .AddChoices<string>(SortingHelper.GetSortTypes()));

        return SortingHelper.GetSortTypeFromDescription(sortType);
    }

    internal SortOrder GetSortOrder()
    {
        var choices = Enum.GetValues<SortOrder>().Select(a => a.ToString()).ToList();
        var sortOrder = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Sort by order: ")
            .AddChoices<string>(choices));

        return Enum.TryParse<SortOrder>(sortOrder, out var result) ? result : SortOrder.Default;
    }

    internal PeriodFilter GetPeriodFilter()
    {
        var periodFilter = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Filter by period: ")
            .AddChoices<string>(SortingHelper.GetPeriodFilters()));

        return SortingHelper.GetPeriodFilterFromDescription(periodFilter);
    }

    internal void ShowReport()
    {
        var sessions = _repository.GetCodingSessions();
        var totalDuration = TimeSpan.Zero;
        foreach (var session in sessions)
        {
            totalDuration += session.Duration;
        }
        var sessionsCount = sessions.Count();
        var averageDuration = totalDuration / sessionsCount;

        _renderer.RenderSessionsReport(sessionsCount, totalDuration, averageDuration);
    }
}
