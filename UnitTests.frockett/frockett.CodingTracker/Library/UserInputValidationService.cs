using Library;
using Spectre.Console;
using System.Globalization;

namespace frockett.CodingTracker.Library;

public class UserInputValidationService
{
    public CodingSession GetStartEndTimeInput()
    {
        DateTime startTime = GetDateTime("Enter start time (DD-MM-YYYY HH:mm) || Please use 24-hour clock: ");
        DateTime endTime = GetDateTime("Enter end time (DD-MM-YYYY HH:mm) || Please use 24-hour clock: ");

        while (!Validation.IsValidEndAndStartTime(startTime, endTime))
        {
            AnsiConsole.Markup("\n[red]Invalid input. Start time can not be later than end time. Don't get them confused![/]\n\n");
            startTime = GetDateTime("Enter start time (DD-MM-YYYY HH:mm) || Please use 24-hour clock: ");
            endTime = GetDateTime("Enter end time (DD-MM-YYYY HH:mm) || Please use 24-hour clock: ");
        }

        return new CodingSession { StartTime = startTime, EndTime = endTime, Duration = endTime - startTime };
    }


    public int GetSessionId(string prompt)
    {
        int sessionId = AnsiConsole.Ask<int>(prompt);
        return sessionId;
    }

    public DateOnly GetYearOnly(string prompt)
    {
        DateOnly date;

        string sDate = AnsiConsole.Ask<string>(prompt);

        while (!DateOnly.TryParseExact(sDate, "yyyy", out date))
        {
            AnsiConsole.WriteLine("Invalid input, please input a valid year in format yyyy");
            sDate = AnsiConsole.Ask<string>(prompt);
        }
        return date;
    }

    public DateOnly GetMonthAndYear(string prompt)
    {
        string sDate = AnsiConsole.Ask<string>(prompt);

        var (isValid, date) = Validation.IsValidMonthAndYear(sDate);

        while (!isValid)
        {
            AnsiConsole.WriteLine("Invalid input, please input a valid month and year in format MM-yyyy");
            sDate = AnsiConsole.Ask<string>(prompt);
            (isValid, date) = Validation.IsValidMonthAndYear(sDate);
        }
        return date;
    }

    private DateTime GetDateTime(string prompt)
    {
        DateTime dateTime;
        string validFormat = "dd-MM-yyyy HH:mm";

        var sDateTime = AnsiConsole.Ask<string>(prompt);
        
        while (!DateTime.TryParseExact(sDateTime, validFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            AnsiConsole.WriteLine("\nIncorrect date/time format.");
            sDateTime = AnsiConsole.Ask<string>(prompt);
        }

        return dateTime;
    }
}
