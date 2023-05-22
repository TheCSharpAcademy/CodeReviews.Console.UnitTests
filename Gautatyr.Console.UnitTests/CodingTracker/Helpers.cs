using CodingTracker.Models;
using ConsoleTableExt;
using static CodingTracker.DataValidation;

namespace CodingTracker;

public static class Helpers
{
    public static string InputSessionDate()
    {
        Console.WriteLine("\nPlease write the date of the session in the 'dd-mm-yy' format, or type 0 to return to the main menu\n");

        string sessionDate = Console.ReadLine();

        while (!IsValidDate(sessionDate) && sessionDate != "0")
        {
            Console.WriteLine("\n|---> Invalid Input ! Please use the 'dd-MM-yy' format and try again or type 0 to get back to the main menu ! <---|\n");
            sessionDate = Console.ReadLine();
        }

        return sessionDate;
    }

    public static string InputSessionTime()
    {
        Console.WriteLine("\nPlease write the time you started the session in the 'hh:mm' format, or type 0 to go back to the main menu\n");

        string sessionStart = Console.ReadLine();
        DateTime sessionStartFormated;

        while (!IsValidTime(sessionStart) && sessionStart != "0")
        {
            Console.Clear();
            Console.WriteLine("\n|---> Invalid Input ! <---|\n");
            Console.WriteLine("\nPlease write the time you started the session in the 'hh:mm' format, or type 0 to go back to the main menu\n");
            sessionStart = Console.ReadLine();
        }

        sessionStartFormated = DateTime.Parse(sessionStart);

        if (sessionStart == "0") return sessionStart;

        Console.WriteLine("\nPlease write the time you ended the session in the 'hh:mm' format, or type 0 to go back to the main menu\n");

        string sessionEnd = Console.ReadLine();
        DateTime sessionEndFormated;

        while (!IsValidTime(sessionEnd) && sessionEnd != "0")
        {
            Console.Clear();
            Console.WriteLine("\n|---> Invalid Input ! <---|\n");
            Console.WriteLine("\nPlease write the time you ended the session in the 'hh:mm' format, or type 0 to go back to the main menu\n");
            sessionEnd = Console.ReadLine();
        }

        sessionEndFormated = DateTime.Parse(sessionEnd);

        if (sessionEnd == "0") return sessionEnd;

        double sessionDuration;

        // This is needed to avoid asking the user about the date of start and date of end
        // in case a session is longer than a day
        if (sessionEndFormated < sessionStartFormated)
        {
            DateTime defaultTime = DateTime.Parse("01-01-01");
            DateTime defaultTimeTomorrow = DateTime.Parse("02-01-01");

            DateTime properStartTime = defaultTime.Date.Add(sessionStartFormated.TimeOfDay);
            DateTime properEndTime = defaultTimeTomorrow.Date.Add(sessionEndFormated.TimeOfDay);

            sessionDuration = (properEndTime - properStartTime).TotalMinutes;
        }
        else
        {
            sessionDuration = (sessionEndFormated - sessionStartFormated).TotalMinutes;
        }

        TimeSpan hoursSpentCoding = TimeSpan.FromMinutes(sessionDuration);

        string minutes = hoursSpentCoding.Minutes.ToString();
        if (int.Parse(minutes) < 10) minutes = $"0{minutes}";

        return $"{hoursSpentCoding.Hours}h{minutes}mn";
    }

    public static void DisplaySessions(List<CodingSessions> codingSessions, string message = "", bool skipReadLine = false)
    {
        Console.Clear();

        ConsoleTableBuilder.From(codingSessions)
            .WithColumn("Id", "Date", "Time spent coding")
            .ExportAndWriteLine();

        Console.WriteLine(message);
        if (skipReadLine) return;
    }

    public static int InputNumber(string message = "")
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();

        while (!IsPositiveNumber(numberInput))
        {
            Console.WriteLine("\n|---> Invalid number <---|\n");
            numberInput = Console.ReadLine();
        }

        return Convert.ToInt32(numberInput);
    }

    public static int AskToContinueOperation()
    {
        Console.WriteLine("If you wish to continue this operation type 1, if not type 0");

        if (InputNumber() == 1)
        {
            return 1;
        }
        return 0;
    }
}