using CodingTracker.Models;
using Spectre.Console;
using System.Diagnostics;

namespace CodingTracker
{
    internal static class UserInterface
    {
        internal static string DisplayMenu()
        {
            Console.Clear();

            // Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("Operation");
            table.AddColumn("Description");
            table.Title = new TableTitle("[blue]Coding Session Main Menu[/]");
            // Add some rows
            table.AddRow("Exit", "[white]Exits the application[/]");
            table.AddRow("Add", "[white]Allows the user to add a coding session to the database[/]");
            table.AddRow("Delete", "[white]Allows the user to delete a coding session from the database[/]");
            table.AddRow("Edit", "[white]Allows the user to change the start and/or end time of a coding session[/]");
            table.AddRow("Live", "[white]Starts a live coding session that will be added to the database upon completion[/]");
            table.AddRow("View All Entries", "[white]Allows the user to view all coding sessions stored in the database[/]");
            table.AddRow("View Filtered Entries", "[white]Allows the user to apply filters to view certain coding sessions stored in the database[/]");
            table.AddRow("View Report", "[white]Allows the user to view summaries of the coding sessions stored in the database[/]");

            // Render the table to the console
            AnsiConsole.Write(table);

            // Ask for the user's input
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What [white]operation do you want to perform? Use up or down arrow keys to make selection.[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
            "Exit", "Add", "Delete",
            "Edit", "Live", "View All Entries", "View Filtered Entries", "View Report",
                    }));

            return selection;

        }

        internal static string DisplayFilterMenu()
        {
            Console.Clear();

            // Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("Filter By");
            table.AddColumn("Description");
            table.Title = new TableTitle("[blue]Coding Session Main Menu[/]");
            // Add some rows
            table.AddRow("Day", "[white]Shows all coding sessions that exist for a specified day[/]");
            table.AddRow("Week", "[white]Shows all coding sessions that exist for a specified week[/]");
            table.AddRow("Month", "[white]Shows all coding sessions that exsit for a specified Month[/]");
            table.AddRow("Edit", "[white]Shows all coding sessions that exist for a specified year[/]");

            // Render the table to the console
            AnsiConsole.Write(table);

            // Ask for the user's input
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What [white]operation do you want to perform? Use up or down arrow keys to make selection.[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
            "Day", "Week", "Month",
            "Year"}));

            return selection;

        }

        internal static void AddSession()
        {
            Console.Clear();

            CodingSession codingSession = new CodingSession();

            codingSession.StartTime = Helper.GetDateTimeInput("start date and time");
            codingSession.EndTime = Helper.GetDateTimeInput("end date and time");

            if (DateTime.Compare(Helper.ConvertStringToDateTime(codingSession.EndTime), Helper.ConvertStringToDateTime(codingSession.StartTime)) <= 0)
            {
                Console.WriteLine("\n\nEnd time must be later than start time. Press any key to return to main menu.\n\n");
                Console.ReadLine();
                AddSession();
            }

            codingSession.CalculateDuration();

            codingSession.Focus = Helper.GetFocusInput();

            DBAccess.AddSession(codingSession);

            return;
        }

        internal static void DisplayTable(List<CodingSession> codingSessions)
        {
            Console.Clear();

            // Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("ID");
            table.AddColumn("Start Timestamp");
            table.AddColumn("End Timestamp");
            table.AddColumn("Duration");
            table.AddColumn("Focus");
            table.Title = new TableTitle("[blue]Coding Sessions[/]");

            // Add some rows
            foreach (var session in codingSessions)
            {
                table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, session.Duration, session.Focus);
            }

            AnsiConsole.Write(table);

        }

        internal static void DeleteSession()
        {
            Console.Clear();

            List<CodingSession> codeSessions = DBAccess.GetAllSessions();
            UserInterface.DisplayTable(codeSessions);

            DBAccess.DeleteSession(Helper.GetSessionIdInput());

            codeSessions = DBAccess.GetAllSessions();
            UserInterface.DisplayTable(codeSessions);
        }

        internal static void EditSession()
        {
            Console.Clear();

            List<CodingSession> codeSessions = DBAccess.GetAllSessions();
            UserInterface.DisplayTable(codeSessions);


            CodingSession codingSession = new CodingSession();

            codingSession.Id = Helper.GetSessionIdInput();

            codingSession.StartTime = Helper.GetDateTimeInput("start date and time");
            codingSession.EndTime = Helper.GetDateTimeInput("end date and time");

            if (DateTime.Compare(Helper.ConvertStringToDateTime(codingSession.EndTime), Helper.ConvertStringToDateTime(codingSession.StartTime)) <= 0)
            {
                Console.WriteLine("\n\nEnd time must be later than start time. Press any key to return to main menu.\n\n");
                Console.ReadLine();
                AddSession();
            }

            codingSession.CalculateDuration();

            codingSession.Focus = Helper.GetFocusInput();

            DBAccess.EditSession(codingSession);

            return;
        }

        internal static void LiveSession()
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Live Session Running")
                .LeftJustified()
                .Color(Color.Blue));


            CodingSession codingSession = new CodingSession();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            codingSession.StartTime = DateTime.Now.ToString("MM/dd/yy hh:mm tt");

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"\n\nCoding session started at {codingSession.StartTime}.\n\n");

            Console.WriteLine("\n\nPress any key to end the session.\n\n");

            // Loop to update the elapsed time display
            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"Elapsed time: {stopwatch.Elapsed:hh\\:mm\\:ss}");
                System.Threading.Thread.Sleep(1000); // Update every second
            }

            // Wait for the user to press any key
            Console.ReadKey(true);

            stopwatch.Stop();

            codingSession.EndTime = DateTime.Now.ToString("MM/dd/yy hh:mm tt");

            codingSession.CalculateDuration();

            codingSession.Focus = Helper.GetFocusInput();

            DBAccess.AddSession(codingSession);

            Console.ForegroundColor = ConsoleColor.Gray;


            return;
        }

        internal static void ViewFilteredSessions()
        {
            Console.Clear();

            string filterSelection = UserInterface.DisplayFilterMenu();

            switch (filterSelection)
            {
                case "Day":
                    List<CodingSession> daySessions = DBAccess.GetSessionsByDay(Helper.GetDateInput());
                    UserInterface.DisplayTable(daySessions);
                    break;
                case "Week":
                    List<CodingSession> weekSessions = DBAccess.GetSessionsByWeek(Helper.GetDateInput());
                    UserInterface.DisplayTable(weekSessions);
                    break;
                case "Month":
                    List<CodingSession> monthSessions = DBAccess.GetSessionsByMonth(Helper.GetMonthAndYearInput());
                    UserInterface.DisplayTable(monthSessions);
                    break;
                case "Year":
                    List<CodingSession> yearSessions = DBAccess.GetSessionsByYear(Helper.GetYearInput());
                    UserInterface.DisplayTable(yearSessions);
                    break;
            }
        }

        internal static void ViewReport()
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("Coding Report")
                .LeftJustified()
                .Color(Color.Red));

            List<CodingSession> codingSessions = DBAccess.GetAllSessions();
            int totalSessions = codingSessions.Count;
            TimeSpan totalDuration = new();
            bool corruptTimeSpan = false;

            for (int i = 0; i < totalSessions; i++)
            {
                if (TimeSpan.TryParse(codingSessions[i].Duration, out TimeSpan duration))
                {
                    totalDuration += duration;
                }
                else
                {
                    corruptTimeSpan = true;
                }
            }

            TimeSpan averageTime = totalDuration / totalSessions;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n\nTotal number of sessions: {totalSessions}");
            Console.WriteLine($"\n\nTotal duration of all sessions: {totalDuration.Days} day(s), {totalDuration.Hours} hour(s), and {totalDuration.Minutes} minute(s).");
            Console.WriteLine($"\n\nAverage duration of each sessions: {averageTime.Days} day(s), {averageTime.Hours} hour(s), and {averageTime.Minutes} minute(s).");

            if (corruptTimeSpan)
            {
                Console.WriteLine("\n\nWarning: One or more sessions have a corrupt duration value.");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }


    }
}
