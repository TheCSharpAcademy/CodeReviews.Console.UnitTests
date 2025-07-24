using System.Globalization;
using Coding_Tracker.Models;
using Coding_Tracker.Views;
using Spectre.Console;

namespace Coding_Tracker.Utils;

public static class Helpers
{
    public static int GetSessionId(List<CodingSession> sessions)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllSessions(sessions);

        var sessionArray = sessions.Select(s => s.Id).ToArray();

        if (sessions.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No sessions found![/]");
            AnsiConsole.MarkupLine("Please add a session first.");
            return 0;
        }

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<int>().Title("Select the session:").AddChoices(sessionArray)
        );
        return option;
    }

    public static DateTime[] GetDates()
    {
        var startDateInput = AnsiConsole.Ask<string>(
            "Enter Coding Start Time (yyyy-MM-dd HH:mm): "
        );

        while (!Validation.IsValidDate(startDateInput, "yyyy-MM-dd HH:mm"))
            startDateInput = AnsiConsole.Ask<string>(
                "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
            );

        var endDateInput = AnsiConsole.Ask<string>("Enter Coding End Time (yyyy-MM-dd HH:mm): ");

        while (!Validation.IsValidDate(endDateInput, "yyyy-MM-dd HH:mm"))
            endDateInput = AnsiConsole.Ask<string>(
                "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
            );

        while (!Validation.IsStartDateBeforeEndDate(startDateInput, endDateInput))
        {
            AnsiConsole.MarkupLine(
                "\n[red]Start date must be before end date. Please try again:[/]"
            );
            startDateInput = AnsiConsole.Ask<string>(
                "Enter Coding Start Time (yyyy-MM-dd HH:mm): "
            );

            while (!Validation.IsValidDate(startDateInput, "yyyy-MM-dd HH:mm"))
                startDateInput = AnsiConsole.Ask<string>(
                    "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
                );

            endDateInput = AnsiConsole.Ask<string>("Enter Coding End Time (yyyy-MM-dd HH:mm): ");

            while (!Validation.IsValidDate(endDateInput, "yyyy-MM-dd HH:mm"))
                endDateInput = AnsiConsole.Ask<string>(
                    "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
                );
        }

        var startDate = DateTime.ParseExact(
            startDateInput,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );
        var endDate = DateTime.ParseExact(
            endDateInput,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );

        return [startDate, endDate];
    }
}
