using CodingTracker.Model;
using Spectre.Console;
using System.Configuration;

namespace CodingTracker
{
    public static class Display
    {
        public static string PrintMainMenu()
        {
            Console.Clear();

            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("MAIN MENU")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Close Application", "View All Records", "Add Record", "Edit Record", "Delete Record", "View Report"
                }));

            return menuChoice;
        }

        public static void PrintReport((int TotalDistinctDays, int TotalSessions, string TotalDuration, int LongestStreak) reportData)
        {
            Console.Clear();

            // Print a simple heading
            var rule = new Rule("[green]View Report[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);

            var table = new Table()
              .Border(TableBorder.Rounded)
              .AddColumn(new TableColumn("[dodgerBlue1]Total Days[/]").Centered())
              .AddColumn(new TableColumn("[dodgerBlue1]Total Sessions[/]").Centered())
              .AddColumn(new TableColumn("[dodgerBlue1]Total Duration[/]").Centered())
              .AddColumn(new TableColumn("[dodgerBlue1]Longest Streak of Days[/]").Centered());

            table.AddRow(
                reportData.TotalDistinctDays.ToString(),
                reportData.TotalSessions.ToString(),
                reportData.TotalDuration,
                reportData.LongestStreak.ToString());

            AnsiConsole.Write(table);
        }

        public static void PrintAllRecords(string heading)
        {
            var repository = new CodingSessionRepository(ConfigurationManager.AppSettings.Get("dbPath"));
            var sessions = repository.GetAllCodingSessions();

            // Clear the console
            Console.Clear();

            // Print a simple heading
            var rule = new Rule($"[green]{heading}[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);

            // Create a table
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[dodgerblue1]ID[/]").Centered())
                .AddColumn(new TableColumn("[dodgerblue1]Date ▼[/]").Centered())
                .AddColumn(new TableColumn("[dodgerblue1]Start Time[/]").Centered())
                .AddColumn(new TableColumn("[dodgerblue1]End Time[/]").Centered())
                .AddColumn(new TableColumn("[dodgerblue1]Duration[/]").Centered());

            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No records found.[/]");
                return;
            }

            // Add rows to the table from the CodingSession objects
            foreach (var session in sessions)
            {
                table.AddRow(
                    session.Id.ToString(),
                    session.Date.ToString("MM/dd/yyyy"),
                    session.StartTime.ToString("h:mm tt"),
                    session.EndTime.ToString("h:mm tt"),
                    session.Duration.ToString(@"h\:mm") // Format duration as hours and minutes
                );
            }

            // Render the table
            AnsiConsole.Write(table);
        }

        public static void PrintEditRecordData(int recordId, DateTime? date, DateTime? startTime, DateTime? endTime)
        {
            Console.WriteLine($"Selected record ID: {recordId}");
            Console.WriteLine($"Date: {date:MM/dd/yyyy}");
            Console.WriteLine($"Start Time: {startTime:h:mm tt}");
            Console.WriteLine($"End Time: {endTime:h:mm tt}");
        }
    }
}

