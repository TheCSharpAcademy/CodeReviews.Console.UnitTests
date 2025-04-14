using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace CodingTracker;

public class SpectreConsole(IConfiguration config, SpectreValidation validation)
{
    private readonly string _dtFormat = config["DateTimeFormat"] ?? "yyyy-MM-dd HH:mm";
    private readonly SpectreValidation _validation = validation;

    public void Welcome()
    {
        AnsiConsole.MarkupLine("[underline green bold slowblink invert]CODING TRACKER[/]");
    }

    public string MainMenu()
    {
        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nWhat would you like to do?")
                .PageSize(10)
                .AddChoices(
                    [
                        "Add Session",
                        "Start New Timed Session",
                        "Review Sessions",
                        "Edit Session",
                        "Delete Session",
                        "Delete All Sessions",
                        "Exit",
                    ]));

        return operation;
    }

    public (string, string) AddSession()
    {
        var startPrompt = new TextPrompt<string>($"Start time ({_dtFormat}):")
            .ValidationErrorMessage("[red]Invalid start time![/]")
            .Validate<string>(_validation.Time);

        var startTime = AnsiConsole.Prompt<string>(startPrompt);

        var endPrompt = new TextPrompt<string>($"End time ({_dtFormat}):")
            .ValidationErrorMessage("[red]Invalid end time![/]")
            .Validate<string>(_validation.Time)
            .Validate<string>(endTime => _validation.EndTime(startTime, endTime));

        var endTime = AnsiConsole.Prompt<string>(endPrompt);

        return (startTime, endTime);
    }

    public string ReviewSessionsMenu()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("How would you like to view your sessions?")
            .PageSize(10)
            .AddChoices([
                "Get Sessions By...",
                "Browse All Sessions",
                "Exit",
                ]);

        return AnsiConsole.Prompt<string> (prompt);
    }

    public string GetSessionByMenu()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("Get sessions by...")
            .AddChoices([
                "Year",
                "Month",
                "Duration",
                "Exit",
                ]);

        return AnsiConsole.Prompt<string>(prompt);
    }

    public string GetSessionsByYearMenu()
    {
        var yearPrompt = new TextPrompt<string>("Year (yyyy):")
            .Validate<string>(_validation.Year);

        return AnsiConsole.Prompt<string>(yearPrompt);
    }

    public string GetSessionsByMonthMenu()
    {
        var monthPrompt = new TextPrompt<string>("Month (yyyy-MM):")
            .Validate<string>(_validation.Month);

        return AnsiConsole.Prompt<string>(monthPrompt);
    }

    public (int, int) GetSessionsByDurationMenu()
    {
        var minPrompt = new TextPrompt<int>("Minimum duration in minutes:")
            .Validate<int>(_validation.MinDuration);
        var minimum = AnsiConsole.Prompt(minPrompt);

        var maxPrompt = new TextPrompt<int>("Maximum duration in minutes:")
            .Validate<int>(_validation.MinDuration)
            .Validate<int>(minutes => _validation.MaxDuration(minutes, minimum));
        var maximum = AnsiConsole.Prompt(maxPrompt);

        return (minimum, maximum);
    }

    public void DisplaySessions(List<CodingSession> sessions)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("StartTime");
        table.AddColumn("EndTime");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            table.AddRow([
                session.Id.ToString(),
                session.StartTime,
                session.EndTime,
                session.Duration.ToString() + " m",
                ]);
        }

        AnsiConsole.Write(table);
    }

    public int EditSessionSelectionMenu()
    {
        var prompt = new TextPrompt<int>("ID of session to edit (or 0 to exit):")
            .Validate<int>(_validation.PositiveId);

        int id = AnsiConsole.Prompt(prompt);

        return id;
    }

    public (string, string) EditSessionMenu(CodingSession session)
    {
        DisplaySessions([session]);

        var startPrompt = new TextPrompt<string>($"New start time ({_dtFormat}):")
            .ValidationErrorMessage("[red]Invalid start time![/]")
            .Validate<string>(_validation.Time);

        var startTime = AnsiConsole.Prompt<string>(startPrompt);

        var endPrompt = new TextPrompt<string>($"New end time ({_dtFormat}):")
            .ValidationErrorMessage("[red]Invalid end time![/]")
            .Validate<string>(_validation.Time)
            .Validate<string>(endTime => _validation.EndTime(startTime, endTime));

        var endTime = AnsiConsole.Prompt<string>(endPrompt);

        return (startTime, endTime);
    }

    public int DeleteSessionSelectionMenu()
    {
        var prompt = new TextPrompt<int>("ID of session to delete (or 0 to exit):")
            .Validate<int>(_validation.PositiveId);

        int id = AnsiConsole.Prompt(prompt);

        return id;
    }

    public bool DeleteSessionConfirmation(CodingSession session)
    {
        DisplaySessions([session]);

        var confirmationPrompt = new ConfirmationPrompt("Delete this session?");
        return AnsiConsole.Prompt<bool>(confirmationPrompt);
    }

    public bool DeleteAllSessionsConfirmation()
    {
        if (!AnsiConsole.Prompt<bool>(
            new ConfirmationPrompt("Are you sure you want to delete all sessions?")))
        {
            return false;
        }

        if (!AnsiConsole.Prompt<bool>(
            new ConfirmationPrompt("Are you really sure?"))) {
            return false;
        }

        return true;
    }

    public bool StartNewSessionPrompt()
    {
        return AnsiConsole.Prompt<bool>(new ConfirmationPrompt("Begin new coding session?"));
    }

    public void StartNewSession()
    {
        AnsiConsole.MarkupLine("\nEnter 'Q' to end session.\n");

        var cts = new CancellationTokenSource();

        var spinnerThread = new Thread(() =>
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Default)
                .Start("Coding...", ctx =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        Thread.Sleep(100);
                    }
                });
        });

        spinnerThread.Start();


        ConsoleKeyInfo cki;
        do
        {
            while (!Console.KeyAvailable)
            {
                Thread.Sleep(100);
            }

            cki = Console.ReadKey(true);
        } while (cki.Key != ConsoleKey.Q);

        cts.Cancel();
        spinnerThread.Join();

        Success("Coding session ended.");
    }

    public void Goodbye()
    {
        Success("Thank you for using the Coding Tracker!");
    }

    public void Error(string message)
    {
        AnsiConsole.MarkupLine($"[red bold]\nERROR: {message}\n[/]");
    }

    public void Success(string message)
    {
        AnsiConsole.MarkupLine($"[green bold]\n{message}\n[/]");
    }
}
