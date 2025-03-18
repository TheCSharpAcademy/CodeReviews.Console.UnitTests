using cacheMe512.CodeTracker.Models;
using Spectre.Console;
using System.Globalization;

namespace cacheMe512.CodeTracker
{
    public class Validation
    {
        public static DateTime GetDateTimeInput(string promptMessage)
        {
            DateTime dateTime;
            string input;

            do
            {
                input = AnsiConsole.Ask<string>(promptMessage);
                if (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime))
                {
                    AnsiConsole.MarkupLine("[red]Invalid date and time format! Please use the format (dd-MM-yy HH:mm).[/]");
                }
            } while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime));

            return dateTime;
        }

        public static bool DateTimeInSequence(DateTime startTime, DateTime endTime)
        {
            int result = DateTime.Compare(startTime, endTime);

            if(result > 0)
                return false;
            return true;
        }

        public static int GetNumberInput(string message)
        {
            int number;
            do
            {
                var input = AnsiConsole.Ask<string>(message);
                if (!int.TryParse(input, out number) || number < 0)
                {
                    AnsiConsole.MarkupLine("[red]Invalid number. Please enter a positive number.[/]");
                }
            } while (number < 0);

            return number;
        }

        public static string GetStringInput(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }

        public static bool ConfirmDeletion(CodingSession session)
        {
            var confirm = AnsiConsole.Confirm($"Are you sure you want to delete the following session:\n" +
                $"[red]Session #{session.Id} - start: {session.StartTime} " +
                $"end: {session.EndTime} duration: {session.Duration}[/]?");

            return confirm;
        }

        public static void DisplayMessage(string message, string color = "yellow")
        {
            AnsiConsole.MarkupLine($"[{color}]{message}[/]");
        }
    }
}
