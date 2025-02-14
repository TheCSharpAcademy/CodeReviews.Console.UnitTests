using CodingTracker.Data;
using CodingTracker.Models;
using Spectre.Console;
using System.Diagnostics;

namespace CodingTracker.Controllers
{
    internal class CodingController : ConsoleController
    {
        static public Stopwatch _timer = new Stopwatch();
        internal static void StartSession() {
            bool isCurrentSesssionActive = DbConnection.CheckIfCurrentSessionExists();
            if (isCurrentSesssionActive)
            {
                ErrorMessage("There is already an active session. Please, finish it before starting a new one.");
                return;
            }
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _timer.Start();
            CodingSession newSession = new() { StartTime = currentTime};
            DbConnection.AddSession(newSession);
            AnsiConsole.MarkupLine("Press [yellow]Q[/] to finish the session");
            AnsiConsole.MarkupLine("\n\nFOCUS TIME!!!\n\n");
            Table table = new Table();
            table.AddColumn("Current Session");
            table.AddEmptyRow();
            AnsiConsole.Live(table)
                .AutoClear(true)
                .Start(ctx =>
                {
                    do
                    {
                        var elapsed = CodingController._timer.Elapsed;
                        table.UpdateCell(0, 0, elapsed.ToString());
                        ctx.Refresh();
                        Thread.Sleep(1000);
                    } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q));
            });
            EndSession();
        }
        internal static void EndSession() {
            bool isCurrentSesssionActive = DbConnection.CheckIfCurrentSessionExists();
            if (!isCurrentSesssionActive)
            {
                ErrorMessage("There is no active session now. Please, start one.");
                return;
            }
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CodingSession currentSession = DbConnection.GetCurrentSession();
            currentSession.EndTime = currentTime;
            currentSession.Duration = GetDurationOfSession(currentSession.StartTime, currentSession.EndTime);
            _timer.Stop();
            DbConnection.UpdateSession(currentSession);
            DisplayMessage($"The session has been ended. The duration was: {TimeController.ConvertFromSeconds(currentSession.Duration)}.");
        }

        internal static int GetDurationOfSession(string startTime, string endTime)
        {
            return (int)(DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime))).TotalSeconds;
        }

        internal static void EditSession(bool current = false) {
            if (current)
            {
                bool isCurrentSessionActive = DbConnection.CheckIfCurrentSessionExists();
                if (isCurrentSessionActive)
                {
                    EditSessionChoices();
                } else
                {
                    ErrorMessage("There is no current session. Please, start one.");
                    return;
                }
            } else
            {
                ViewSessions();
                string choice = AnsiConsole.Prompt(
                new TextPrompt<string>("Please, enter the id of the session you would like to edit."));
                if (DbConnection.GetSession(choice) != null)
                {
                    EditSessionChoices(choice);
                }
                else
                {
                    ErrorMessage($"There is no session with an id of {choice}");
                }
            }
        }

        internal static void EditSessionChoices(string id = "current")
        {
            CodingSession session;
            if (id == "current") { 
                session = DbConnection.GetCurrentSession(); 
            }
            else { 
                session = DbConnection.GetSession(id); 
            }
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<EditSessionChoice>()
                .Title("The session has been found. Please, select what you would like to edit:")
                .AddChoices(Enum.GetValues<EditSessionChoice>()));
            switch (choice)
            {
                case EditSessionChoice.StartTime:
                    session.StartTime = EditSessionReducer("Start Time", session);
                    if (UserInterface.CheckIfQuit(session.StartTime)) break;
                    if (id != "current")
                    {
                        session.Duration = GetDurationOfSession(session.StartTime, session.EndTime);
                    }
                    DbConnection.UpdateSession(session);
                    break;
                case EditSessionChoice.EndTime:
                    session.EndTime = EditSessionReducer("End Time", session);
                    if (UserInterface.CheckIfQuit(session.StartTime)) break;
                    session.Duration = GetDurationOfSession(session.StartTime, session.EndTime);
                    DbConnection.UpdateSession(session);
                    break;
                case EditSessionChoice.GoBack:
                    break;
            }
        }

        internal static string EditSessionReducer(string dateName, CodingSession? session)
        {
            string choice = AnsiConsole.Prompt(
                new TextPrompt<string>($"Please, enter your new {dateName} in the format of YYYY-MM-DD HH:MM:SS: ")
                .Validate(input =>
                {
                    if (input.ToLower() == "q") return ValidationResult.Success();
                    DateTime newDate;
                    if (!DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out newDate))
                    {
                        return ValidationResult.Error("[red]Your date input is wrong, please change it according to the format, or type in Q and press Enter to exit.[/]");
                    }
                    if (dateName == "Start Time" && (newDate >= DateTime.Parse(session.EndTime)))
                    {
                        return ValidationResult.Error("[red]The start time of session cannot be past the end time, type in Q and press Enter to exit, or try again.[/]");
                    }
                    else if (dateName == "End Time" && (newDate <= DateTime.Parse(session.StartTime)))
                    {
                        return ValidationResult.Error("[red]The end time of session cannot be before the start time, type in Q and press Enter to exit, or try again.[/]");
                    }
                    return ValidationResult.Success();
                })
                );

            if (choice == "q")
            {
                return "q";
            } else
            {
                return choice;
            }
        }

        internal static void ViewSessions(RecordsFilterPeriodMenu periodChoice = RecordsFilterPeriodMenu.All, RecordsFilterOrderMenu orderChoice = RecordsFilterOrderMenu.Ascending, RecordsFilterFieldMenu orderFieldChoice = RecordsFilterFieldMenu.Duration) {
            Console.Clear();
            List<CodingSession>? sessions;
            string order = "ASC";
            if (orderChoice == RecordsFilterOrderMenu.Descending) order = "DESC";
            if (periodChoice == RecordsFilterPeriodMenu.All) {
                switch (orderFieldChoice)
                {
                    case RecordsFilterFieldMenu.Date:
                        Console.WriteLine("sorted by date");
                        sessions = DbConnection.GetSessions($" ORDER BY StartTime {order}");
                        break;
                    default:
                        sessions = DbConnection.GetSessions($" ORDER BY Duration {order}");
                        break;
                }
                TableVisualizationEngine.VisualizeSessions(sessions);
            } else
            {
                string startDate = AnsiConsole.Prompt(
            new TextPrompt<string>($"Please, enter the start date for filtering in the format of YYYY-MM-DD: ")
            .Validate(input =>
            {
                if (input.ToLower() == "q") return ValidationResult.Success();
                DateTime newDate;
                if (!DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out newDate))
                {
                    return ValidationResult.Error("[red]Your date input is wrong, please change it according to the format, or type in Q and press Enter to exit.[/]");
                }
                return ValidationResult.Success();
            })
            );

                if (startDate == "q")
                {
                    return;
                }
                string endDate = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Please, enter the start date for filtering in the format of YYYY-MM-DD: ")
                    .Validate(input =>
                    {
                        if (input.ToLower() == "q") return ValidationResult.Success();
                        DateTime newDate;
                        if (!DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out newDate))
                        {
                            return ValidationResult.Error("[red]Your date input is wrong, please change it according to the format, or type in Q and press Enter to exit.[/]");
                        }
                        else if (newDate <= DateTime.Parse(startDate))
                        {
                            return ValidationResult.Error("[red]The end time of session cannot be before the start time, type in Q and press Enter to exit, or try again.[/]");
                        }
                        return ValidationResult.Success();
                    })
                    );
                if (endDate == "q")
                {
                    return;
                }
                switch (orderFieldChoice)
                {
                    case RecordsFilterFieldMenu.Date:
                        sessions = DbConnection.GetSessionsByPeriod(startDate, endDate, $" ORDER BY StartTime {order}");
                        break;
                    default:
                        sessions = DbConnection.GetSessionsByPeriod(startDate, endDate, $" ORDER BY Duration {order}");
                        break;
                }
                TableVisualizationEngine.VisualizeSessions(sessions);
            }
        }

        internal static void DeleteSession()
        {
            ViewSessions();
            string choice = AnsiConsole.Prompt(
            new TextPrompt<string>("Please, enter the id of the session you would like to delete."));
            if (Int32.TryParse(choice, out _))
            {
                if (DbConnection.DeleteSession(choice))
                {
                    SuccessMessage("The session has been removed.");
                }
            }
            else {
                ErrorMessage("There has been a problem while trying to remove the session. The session might not exist, or the id was mistyped.");
            }
        }
    }
}
