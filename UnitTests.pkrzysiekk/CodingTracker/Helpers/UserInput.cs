using CodingTracker.Models;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using ValidationResult = Spectre.Console.ValidationResult;

namespace CodingTracker.Controllers
{
    public static class UserInput
    {
        public static MenuChoices GetUserChoice()
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
                .Title("What do you want to do?")
                .AddChoices(Enum.GetValues<MenuChoices>())
                .UseConverter(c => c.GetType()
                    .GetMember(c.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()?.Name ?? c.ToString()));

            return choice;
        }

        public static DateTime GetUserDate()
        {
            string input = AnsiConsole.Prompt(new TextPrompt<string>("Enter the date in dd-MM-yyyy HH:mm format")
                .Validate(d =>
                    InputValidator.IsDateValid(d) ? ValidationResult.Success() : ValidationResult.Error("Invalid date"))
            );
            return DateTime.ParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        public static int CalculateDateDifference(DateTime startDate, DateTime endDate)
        {
            var dateDiff = endDate - startDate;
            return (int)dateDiff.TotalMinutes;
        }

        public static TimeSpan GetUserTime()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            AnsiConsole.Ask<string>("[violet]Type anything to stop the session and save the time:[/]");
            sw.Stop();
            return sw.Elapsed;
        }

        public static CodingSession GetUserRecordInput()
        {
            DateTime startDate;
            DateTime endDate;
            bool manualSession =
                AnsiConsole.Confirm("Do you want to start the session now and end when you're finished?");
            if (manualSession)
            {
                startDate = DateTime.Now;
                endDate = startDate + GetUserTime();
            }
            else
            {
                startDate = GetUserDate();
                AnsiConsole.MarkupLine("[Blue] Now enter the end date [/]");
                endDate = GetUserDate();
                while (!InputValidator.AreDatesValid(startDate, endDate))
                {
                    AnsiConsole.MarkupLine("[Red]Invalid dates[/]");
                    startDate=GetUserDate();
                    endDate = GetUserDate();
                }
            }

            int duration = CalculateDateDifference(startDate, endDate);

            CodingSession newRecord = new CodingSession
            {
                StartDate = startDate,
                EndDate = endDate,
                Duration = duration,
            };

            return newRecord;
        }

        public static int? GetUserId(List<CodingSession> list)
        {
            if (list.Count == 0)
            {
                return null;
            }

            var selectedSession = AnsiConsole.Prompt(new SelectionPrompt<CodingSession>()
                .Title("[green]Select a session:[/]")
                .PageSize(10)
                .AddChoices(list)
                .MoreChoicesText("Scroll down to see more records")
                .UseConverter(s =>
                    $"ID: {s.Id}, Start: {s.StartDate}:yyyy-MM-dd HH:mm, End: {s.EndDate} Duration: {s.Duration} min"));
            AnsiConsole.MarkupLine(
                $"[Blue]ID: {selectedSession.Id}, Start: {selectedSession.StartDate}:yyyy-MM-dd HH:mm, End: {selectedSession.EndDate} Duration: {selectedSession.Duration} min[/]");
            return selectedSession.Id;
        }
    }
}