﻿using CodingTracker.Interfaces;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    /// <summary>
    /// Manages reading and validation of user input 
    /// Implements <see cref="IInputManager"/>
    /// </summary>
    public class InputManager : IInputManager
    {
        /// <inheritdoc/>
        public string DateTimeFormat {  get; init; }

        /// <summary>
        /// Initiates new object of <see cref="InputManager"/>
        /// </summary>
        /// <param name="dateTimeFormat"><see cref="string"/> value representing date and time format</param>
        public InputManager(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        /// <inheritdoc/>
        public UserChoice GetMenuInput()
        {
            UserChoice[] menuOptions = (UserChoice[])Enum.GetValues(typeof(UserChoice));

            UserChoice input = AnsiConsole.Prompt(
                new SelectionPrompt<UserChoice>()
                .AddChoices(menuOptions)
                .WrapAround(true)
                .UseConverter((x) => x switch
                {
                    UserChoice.View => "View sessions",
                    UserChoice.Insert => "Add session",
                    UserChoice.Delete => "Remove session",
                    UserChoice.Update => "Update session",
                    UserChoice.Track => "Start to track session",
                    UserChoice.Report => "Generate Report",
                    UserChoice.Exit => "Exit the application",
                    _ => throw new NotImplementedException("Invalid value passed"),
                }));
            return input;
        }
        /// <inheritdoc/>
        public CodingSession GetNewSession()
        {
            DateTime start = GetStartTime();
            DateTime end = GetEndTime(start);

            return new CodingSession() { Start = start, End = end };
        }
        /// <inheritdoc/>
        public DateTime GetStartTime()
        {

            string input = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter start date and time in format [green]{DateTimeFormat.ToUpper()}[/]: ")
                .Validate(input => Validator.IsValidDate(input, DateTimeFormat))
                .ValidationErrorMessage("[red]Invalid input format[/]")
                );

            return DateTime.ParseExact(input,DateTimeFormat, CultureInfo.InvariantCulture);
        }
        /// <inheritdoc/>
        public DateTime GetEndTime(DateTime startDate)
        {
            string input = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter end date and time in format [green]{DateTimeFormat.ToUpper()}[/]: ")
                .Validate((input) => 
                {   
                    if (!Validator.IsValidDate(input, DateTimeFormat))
                    {
                        return ValidationResult.Error("[red]Invalid input format[/]");
                    }

                    if (!Validator.IsValidEndDate(input, startDate, DateTimeFormat))
                    {
                        return ValidationResult.Error("[red]End is sooner than start[/]");
                    }
                    return ValidationResult.Success();
                }));

            return DateTime.ParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture);
        }
        /// <inheritdoc/>
        public CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation)
        {
            HashSet<int> validIds = new HashSet<int>(sessions.Select(x => x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>($"Enter ID of record you with to [green]{operation}[/] or 0 to go back to main menu: ")
                .Validate(input => Validator.IsSessionIdValidOrZero(input,validIds))
                );

            return id == 0 ? null : sessions.Find(x => x.Id == id);
        }
        /// <inheritdoc/>
        public bool ConfirmOperation(CodingSession session, string operation)
        {
            Console.WriteLine();

            string prompt = $"Session - Start: {session.Start.ToString(DateTimeFormat)} | " +
                $"End: {session.End.ToString(DateTimeFormat)} | Duration: {session.Duration.TotalMinutes} minutes will be {operation}d\n" +
                $"Please confirm";

            return AnsiConsole.Confirm(prompt);
        }
        /// <inheritdoc/>
        public bool ConfirmUpdate(CodingSession original, CodingSession updated)
        {
            Console.WriteLine();
            string prompt = $"Session - Start: {original.Start.ToString(DateTimeFormat)} | " +
                $"End: {original.End.ToString(DateTimeFormat)} | Duration: {original.Duration.TotalMinutes} minutes will be updated\n"+
                $"Session - Start: {updated.Start.ToString(DateTimeFormat)} | " +
                $"End: {updated.End.ToString(DateTimeFormat)} | Duration: {updated.Duration.TotalMinutes} minutes\n" +
                $"Please confirm";

            return AnsiConsole.Confirm(prompt);
        }
        /// <inheritdoc/>
        public bool ConfirmTrackingStart()
        {
            ConsoleKey input;
            do
            {
                Console.WriteLine("Press 'ENTER' to start tracking or 'ESC' to exit: ");
                input = Console.ReadKey(true).Key;
            } 
            while ((input != ConsoleKey.Enter) && (input != ConsoleKey.Escape));

            return input == ConsoleKey.Enter;
        }
        /// <inheritdoc/>
        public ReportTimeFrame GetTimeRangeForReport()
        {
            ReportTimeFrame[] reportTimeFrames = (ReportTimeFrame[])Enum.GetValues(typeof(ReportTimeFrame));

            ReportTimeFrame input = AnsiConsole.Prompt(
                new SelectionPrompt<ReportTimeFrame>()
                .AddChoices(reportTimeFrames)
                .WrapAround(true)
                .UseConverter((x) => x switch
                {
                    ReportTimeFrame.ThisYear => "This Year",
                    ReportTimeFrame.ThisMonth => "This Month",
                    ReportTimeFrame.ThisWeek => "This Week",
                    ReportTimeFrame.Custom => "Custom",
                    _ => throw new NotImplementedException("Invalid value passed"),
                }));

            return input;
        }
    }
}