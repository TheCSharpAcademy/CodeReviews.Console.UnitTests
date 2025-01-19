using System.Data.SQLite;
using System.Configuration;
using Spectre.Console;
using Dapper;

namespace CodingTrack
{
    internal class DatabaseHelpers
    {
        private static string? connectionString = ConfigurationManager.AppSettings.Get("Key0");
        public static void InitializeDatabase()
        {
            var dbFilePath = ConfigurationManager.AppSettings.Get("Key1"); ;
            var dbDirectory = System.IO.Path.GetDirectoryName(dbFilePath);

            if (!System.IO.Directory.Exists(dbDirectory))
            {
                System.IO.Directory.CreateDirectory(dbDirectory);
            }

            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
            CreateTableOfCodingSessions();
        }
        public static void CreateTableOfCodingSessions()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"CREATE TABLE IF NOT EXISTS CodingSessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration TEXT
                    )";
                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void ViewCodingSessions()
        {
            AnsiConsole.Clear();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                string sql = "SELECT * FROM CodingSessions";

                List<CodingSession> sessions = connection.Query<CodingSession>(sql).ToList();

                connection.Close();

                if(sessions.Count() == 0)
                {
                    AnsiConsole.WriteLine("No rows!");
                }
                
                var table = new Table();

                
                table.AddColumn("[yellow]Session ID[/]");
                table.AddColumn("[yellow]Start Time[/]");
                table.AddColumn("[yellow]End Time[/]");
                table.AddColumn("[yellow]Duration(hh:mm:ss)[/]");

                
                table.Alignment(Justify.Center);

                
                foreach (var session in sessions)
                {
                    table.AddRow(
                        session.GetId().ToString(),
                        session.GetStartTime().ToString(),
                        session.GetEndTime().ToString(),
                        session.GetDuration().ToString()
                    );
                }
                AnsiConsole.Write(
                    new Markup("[yellow]List of coding sessions[/]").Centered()
                );
                AnsiConsole.Write(table);

            }
        }
        public static void AddCodingSession()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("\n[red]First enter start time and then enter end time of coding session.[/]\n");
            AnsiConsole.WriteLine();

            DateTime startDateTime = DateTime.UtcNow;
            DateTime endDateTime = DateTime.UtcNow;
            TimeSpan Duration = TimeSpan.Zero;

            UserInput.GetAllTimesInput(ref startDateTime, ref endDateTime, ref Duration);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"INSERT INTO CodingSessions(StartTime, EndTime, Duration) Values('{startDateTime}', '{endDateTime}', '{Duration}')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void UpdateCodingSession()
        {
           
            ViewCodingSessions();
            AnsiConsole.MarkupLine("\n\n[blue]Write id of coding session that you want to update. Enter 0 to return to menu[/]\n");

            string? id = Console.ReadLine();

            if(id == "0")
            {
                Menu.GetUserInput();
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT EXISTS(SELECT 1 FROM CodingSessions WHERE id = '{id}')";

                int rowCount = Convert.ToInt32(tableCmd.ExecuteScalar());

                if (rowCount == 0)
                {
                    AnsiConsole.MarkupLine($"\n\n[blue]Record with Id {id} doesn't exist. Enter any key to continue.\n\n[/]");
                    Console.ReadLine();
                    connection.Close();
                    UpdateCodingSession();
                }

                AnsiConsole.MarkupLine("[blue]First enter start time and then enter end time of coding session:[/]");

                DateTime startDateTime = DateTime.UtcNow;
                DateTime endDateTime = DateTime.UtcNow;
                TimeSpan Duration = TimeSpan.Zero;

                UserInput.GetAllTimesInput(ref startDateTime, ref endDateTime, ref Duration);
                

                var tableCMD = connection.CreateCommand();
                tableCMD.CommandText = $"UPDATE CodingSessions SET startTime = '{startDateTime}', endTime = '{endDateTime}', duration = '{Duration}' WHERE Id = '{id}'";
                tableCMD.ExecuteNonQuery();

                connection.Close();
                AnsiConsole.MarkupLine("\n\n[blue]Updated succsessfuly. Press any key to return to main menu![/]");
                Console.ReadLine();
            }

        }
        public static void DeleteCodingSession()
        {
            ViewCodingSessions();
            AnsiConsole.Markup("[purple]\n\nPlease type Id of the record would like to Delete.Type 0 to return to main menu.\n\n[/]");

            AnsiConsole.WriteLine();
            string? id = Console.ReadLine();
            if(id == "0")
            {
                Menu.GetUserInput();
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE FROM CodingSessions WHERE Id = '{id}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    AnsiConsole.MarkupLine($"\n[purple]No rows were deleted or wrong id[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"\n[purple]{rowCount} rows were deleted[/]");
                }
                connection.Close();
                AnsiConsole.MarkupLine("\n\n[purple] Press any key to return to main menu![/]");
                Console.ReadLine();
            }
        }
    }
}
