using Spectre.Console;

namespace CodingTracker
{
    /// <summary>
    /// Manages output of the data 
    /// Implements <see cref="IOutputManager"/>
    /// </summary>
    public class OutputManager : IOutputManager
    {
        /// <inheritdoc/>
        public string DateTimeFormat { get; init; }

        /// <summary>
        /// Initiates new object of <see cref="OutputManager"/>
        /// </summary>
        /// <param name="dateTimeFormat"><see cref="string"/> value representing date and time format</param>
        public OutputManager(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        /// <inheritdoc/>
        public void ConsoleClear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        /// <inheritdoc/>
        public void PrintMenuHeader()
        {
            Console.WriteLine("Welcome to Coding Tracker application");
            Console.WriteLine("This application allow you to manually add sessions from past or track a session as it happen");
            Console.WriteLine();
        }
        /// <inheritdoc/>
        public void PrintRecords(IEnumerable<CodingSession> sessions)
        {
            ConsoleClear();

            List<CodingSession> sessionsList = sessions.ToList() ?? new List<CodingSession>();
            
            if (sessionsList.Count == 0)
            {
                Console.WriteLine("No records in database");
                return;
            }

            var table = new Table();
            table.Title = new TableTitle("Coding Sessions");
            table.AddColumns("ID", "Start", "End", "Duration").Centered();

            foreach (var session in sessions)
            {
                table.AddRow(
                        session.Id.ToString(),
                        session.Start.ToString(DateTimeFormat),
                        session.End.ToString(DateTimeFormat),
                        $"{(int)session.Duration.TotalMinutes} min"
                    );
            }
            AnsiConsole.Write(table);
        }
        /// <inheritdoc/>
        public void PrintTrackingPanel(TimeSpan elapsed)
        {
            var panel = new Panel($"Session is being tracked\n\n" +
                                  $"Current time: {elapsed.ToString(@"hh\:mm\:ss")}\n\n" +
                                  $"[red]Press any key to stop tracking[/]")
                            .Padding(2, 1, 2, 1);

            AnsiConsole.Write(panel);
        }
        /// <inheritdoc/>
        public void PrintSingleSession(CodingSession session)
        {
            Console.WriteLine();
            Console.WriteLine($"Session started {session.Start.ToString(DateTimeFormat)}\n" +
                $"Session ended: {session.End.ToString(DateTimeFormat)}\n" +
                $"Session duration: {session.Duration.ToString(@"hh\:mm\:ss")}" +
                $"\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}