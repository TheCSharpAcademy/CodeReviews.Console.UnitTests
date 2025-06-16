using CodingTracker.KamilKolanowski.Services;
using CodingTracker.KamilKolanowski.Data;
using CodingTracker.KamilKolanowski.Enums;
using Spectre.Console;

namespace CodingTracker.KamilKolanowski.Controllers;

internal class SessionsController : IBaseController
{
    private static DateTime _startTime;
    private static DateTime _endTime;
    private decimal _duration; 
    
    PrepareReport _prepareReport = new();
    SessionsService _sessionsService = new();
    Validation _validation = new();

    public void AddSessionManually(DatabaseManager databaseManager)
    {
        DateTime userSessionStartTime = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Add starting date of your session [cadetblue](yyyy-MM-dd HH:mm:ss)[/]:")
                .Validate(_validation.ValidateStartTime));
        
        DateTime userSessionEndTime = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Add ending date of your session [cadetblue](yyyy-MM-dd HH:mm:ss)[/]:")
                .Validate(input => _validation.ValidateEndTime(input, userSessionStartTime)));

        decimal userSessionDuration = _sessionsService.GetDuration(userSessionStartTime, userSessionEndTime);
        databaseManager.WriteTable(userSessionStartTime, userSessionEndTime, userSessionDuration);
        
        Console.WriteLine("Session saved. Press any key to go back to menu.");
        Console.ReadKey();
    }
    public void StartSession(bool isSessionStarted)
    {
        if (isSessionStarted)
        {
            Console.WriteLine("The session is already started.");
            Console.WriteLine("Press any key to go back to menu.");

            Console.ReadKey();
        }
        else
        {
            _startTime = _sessionsService.GetCurrentTime();
            Console.WriteLine($"Starting session at {_startTime}");
            Console.WriteLine("Session started. Press any key to go back to menu.");
            
            Console.ReadKey();
        }
    }

    public void EndSession(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager)
    {
        if (isSessionEnded && isSessionStarted)
        {
            _endTime = _sessionsService.GetCurrentTime();
            _duration = _sessionsService.GetDuration(_startTime, _endTime);
            
            databaseManager.WriteTable(_startTime, _endTime, _duration);
            Console.WriteLine($"Ending session at {_endTime} - Your session lasted {_duration} seconds.");
            Console.WriteLine("Session saved. Press any key to go back to menu.");
            
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("There's no active session.");
            Console.WriteLine("Press any key to go back to menu.");

            Console.ReadKey();
        }
    }

    public void ViewSessions(DatabaseManager databaseManager)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        
        table.AddColumn("[lime]Session Id[/]");
        table.AddColumn("[lime]Start Date Time[/]");
        table.AddColumn("[lime]End Date Time[/]");
        table.AddColumn("[lime]Duration (s)[/]");

        var sessions = databaseManager.ReadTable();
        int idx = 1;
        
        foreach (var session in sessions)
        {
            table.AddRow(
                $"[turquoise2]{idx.ToString()}[/]",
                $"[dodgerblue1]{session.StartDateTime}[/]",
                $"[dodgerblue1]{session.EndDateTime}[/]",
                $"[dodgerblue1]{session.Duration}[/]"
            );
            
            idx++;
        }

        table.Columns[0].Centered();
        table.Columns[1].Centered();
        table.Columns[2].Centered();
        table.Columns[3].Centered();
        
        AnsiConsole.Write(table);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void GetReports()
    {
        var reportingChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(Options.ReportingOptionDisplayNames.Values));
            
        var selectedReportingOption = Options.ReportingOptionDisplayNames
            .FirstOrDefault(x => x.Value == reportingChoice).Key;

        var orderingChoice = AnsiConsole.Prompt(
            new SelectionPrompt<Options.OrderingReport>()
                .AddChoices(Options.OrderingOptionDisplayNames.Keys)
                .UseConverter(opt => opt.ToString()));

        var orderingOption = Options.OrderingOptionDisplayNames[orderingChoice];
        
        switch (selectedReportingOption)
        {
            case Options.ReportingOptions.GetWeeklyReport:
                _prepareReport.PreparePeriodicReport(Options.ReportingOptions.GetWeeklyReport, orderingOption);
                break;
            case Options.ReportingOptions.GetMonthlyReport:
                _prepareReport.PreparePeriodicReport(Options.ReportingOptions.GetMonthlyReport, orderingOption);
                break;
            case Options.ReportingOptions.GetYearlyReport:
                _prepareReport.PreparePeriodicReport(Options.ReportingOptions.GetYearlyReport, orderingOption);
                break;
        }
    }

    public void QuitApplication(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager)
    {
        if (!isSessionEnded && isSessionStarted)
        {
            _endTime = _sessionsService.GetCurrentTime();
            _duration = _sessionsService.GetDuration(_startTime, _endTime);
            
            databaseManager.WriteTable(_startTime, _endTime, _duration);
            Console.WriteLine($"Ending session at {_endTime} - Your session lasted {_duration} seconds.");
            Console.WriteLine("Session saved. Thank you for using Coding Tracker\nGoodbye!");
            
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Thank you for using Coding Tracker\nGoodbye!");
            Environment.Exit(0);
        }
    }
}