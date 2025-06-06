using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Configuration;
using Dapper;
using System.Globalization;
using System.Data;
using CodingTracker.Models;

namespace CodingTracker.Controllers
{
    internal class DataTools
    {
        internal static string? ConnectionString { get; set; }

        internal static void DataBaseAndConnectionString()
        {
            try
            {
                if (ConfigurationManager.AppSettings.Get("connectionString") == "")
                {
                    string? appFolderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    int pathLength = appFolderPath.Length - 16;
                    string dbFolderPath = appFolderPath.Substring(0, pathLength);
                    ConfigurationManager.AppSettings.Set("dbPath", dbFolderPath);
                    ConfigurationManager.AppSettings.Set("connectionString", $"Data Source= {dbFolderPath}CodingTracker.db");
                }
                ConnectionString = ConfigurationManager.AppSettings.Get("connectionString");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine(ex.Message);
                Console.Read();
            }
        }

        internal static List<CodingSession> GetProjectData(string project, string ascDesc = "Asc", string option = "All data")
        {
            try
            {
                string? sqlCommand;
                CodingSession currentData = new();
                List<CodingSession> reports = new();
                string[] dateStr = GetOppositeDates(project);
                DateTime firstDate;
                DateTime LastDate;

                bool haveData = DateTime.TryParseExact(dateStr[0], "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out firstDate);
                haveData = DateTime.TryParseExact(dateStr[1], "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out LastDate);
                DateTime secondDate = firstDate;
                DateTime currentDate = firstDate;
                if (option == "All data")
                {
                    if (project.IndexOf("_CGoal") == -1)
                    {
                        try
                        {
                            sqlCommand = $"SELECT *,rowid from {project} ORDER BY StartDate";
                            using (SqliteConnection connection = new(ConnectionString))
                            {
                                return ascDesc == "Asc" ? connection.Query<CodingSession>(sqlCommand).ToList() : ReverseList(connection.Query<CodingSession>(sqlCommand).ToList());
                            }
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
                else if (option == "Weekly")
                {
                    secondDate = firstDate.DayOfWeek.ToString() switch
                    {
                        "Monday" => firstDate,
                        "Tuesday" => firstDate.AddDays(-1),
                        "Wednesday" => firstDate.AddDays(-2),
                        "Thursday" => firstDate.AddDays(-3),
                        "Friday" => firstDate.AddDays(-4),
                        "Saturday" => firstDate.AddDays(-5),
                        "Sunday" => firstDate.AddDays(-6),
                        _ => firstDate,
                    };
                }
                else if (option == "Monthly") { secondDate = firstDate.AddDays(-firstDate.Day + 1); }
                else if (option == "Yearly") { secondDate = firstDate.AddDays(-firstDate.DayOfYear + 1); }

                int sessionCount = 0;
                TimeSpan finalDuration = TimeSpan.ParseExact("00:00:00", "c", CultureInfo.InvariantCulture, TimeSpanStyles.None);
                do
                {
                    try
                    {
                        using (SqliteConnection connection = new(ConnectionString))
                        {
                            // SQLite command to calculate the duration using only SQLite
                            sqlCommand = @$"SELECT 	printf('%.2i',(avg(strftime('%M', Duration)) / 60 + avg(strftime('%H', Duration))) % 24) as avgHours,
	                                     printf('%.2i',avg(strftime('%M',Duration))) as avgMinutes,
                                         printf('%.2i',sum(strftime('%H',Duration)) / 24) as days,
	                                     printf('%.2i',sum(strftime('%M', Duration)) / 60 + sum(strftime('%H', Duration))) % 24 as hours,
                                         printf('%.2i', sum(strftime('%M', Duration)) % 60) as minutes,
                                         count(Duration) as DurationCount
                                         FROM {project} WHERE StartDate <= '{currentDate.ToString("yyyy.MM.dd")}' AND StartDate >= '{secondDate.ToString("yyyy.MM.dd")}'";
                            currentData = connection.Query<CodingSession>(sqlCommand).ToList()[0];
                            IDataReader reader = connection.ExecuteReader(sqlCommand);
                            reader.Read();
                            currentData.TotalDuration = $"{reader["days"]:D2}.{reader["hours"]:D2}:{reader["minutes"]:D2}:00";
                            reader.Close();
                            if (currentData.DurationCount != "0")
                            {
                                currentData.StartDate = secondDate.ToString("yyyy.MM.dd");
                                currentData.EndDate = currentDate.ToString("yyyy.MM.dd");
                                currentData.rowid = secondDate.Day / 7 + 1;
                                sessionCount += int.Parse(currentData.DurationCount);
                                finalDuration = finalDuration.Add(TimeSpan.ParseExact(currentData.TotalDuration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None));
                                reports.Add(currentData);
                            }

                            currentDate = secondDate.AddDays(-1);
                            secondDate = option switch
                            {
                                "Weekly" => secondDate.AddDays(-7),
                                "Monthly" => secondDate.AddMonths(-1),
                                "Yearly" => secondDate.AddYears(-1)
                            };
                        }

                        if (currentDate < LastDate)
                        {
                            reports.Add(new CodingSession
                            {
                                Project = "Final Result",
                                StartDate = firstDate.ToString("yyyy.MM.dd"),
                                EndDate = LastDate.ToString("yyyy.MM.dd"),
                                TotalDuration = finalDuration.ToString(),
                                DurationCount = sessionCount.ToString(),
                                Average = (finalDuration / sessionCount).ToString().Substring(0, 5),
                            });
                            return ascDesc == "Asc" ? reports : ReverseList(reports, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                } while (haveData);

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine(ex.Message);
                Console.Read();
            }
            return null;
        }

        public static void SetGoals(string project)
        {
            try
            {
                using (SqliteConnection connection = new(ConnectionString))
                {
                    string? sqlCommand;
                    sqlCommand = $"CREATE TABLE IF NOT EXISTS \"{project}_CGoal\"(Project TEXT,Start DATE,End DATE,DurationEstimation TIME,DurationPerDay TIME,NumberOfDays INTEGER)";
                    ExecuteQuery(sqlCommand);
                    sqlCommand = $"SELECT * FROM {project}_CGoal";
                    IDataReader reader = connection.ExecuteReader(sqlCommand);
                    bool goalNotSet = reader.Depth > 0 ? false : true;
                    reader.Close();
                    if (goalNotSet)
                    {
                        string start = UserInputs.GetDateTimeInput("Choose a starting date");
                        string end = UserInputs.GetDateTimeInput("Choose an ending date");
                        string durationEstimation = UserInputs.GetDurationEstimation();
                        int days = GetNumberOfDays(start, end);
                        TimeSpan timePerDay = TimeSpan.ParseExact(durationEstimation, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None);
                        timePerDay = timePerDay / days;
                        sqlCommand = $"INSERT INTO {project}_CGoal (Project,Start,End,DurationEstimation,DurationPerDay,NumberOfDays) VALUES('{project}','{start}','{end}','{durationEstimation}','{timePerDay}',{days})";
                        ExecuteQuery(sqlCommand);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("This projec already have goal set");
                        Console.Read();
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine(ex.Message);
                Console.Read();
            }
        }

        public static CodingSession GetGoalToShow(string project)
        {
            try
            {
                using (SqliteConnection connection = new(ConnectionString))
                {
                    string sqlCommand = $"SELECT Start,End,DurationEstimation,DurationPerDay FROM {project}";
                    IDataReader reader = connection.ExecuteReader(sqlCommand);
                    reader.Read();
                    CodingSession session = new CodingSession
                    {
                        Project = project,
                        StartDate = reader["Start"].ToString(),
                        EndDate = reader["End"].ToString(),
                        TotalDuration = reader["DurationEstimation"].ToString(),
                        Duration = reader["DurationPerDay"].ToString()
                    };
                    reader.Close();
                    return session;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"The project [blue bold]{project} is not set as a Coding Goal");
                AnsiConsole.WriteLine(ex.Message);
                return null;
            }
        }

        public static string GetDuration(string StartDate, string StartTime, string EndDate, string EndTime)
        {

            DateTime start = DateTime.Parse($"{StartDate} {StartTime}");
            DateTime end = DateTime.Parse($"{EndDate} {EndTime}");
            string duration = end.Subtract(start).ToString();
            return duration.Substring(0, duration.Length - 3);
        }

        public static int GetNumberOfDays(string Start, string End)
        {
            DateTime start = DateTime.Parse(Start);
            DateTime end = DateTime.Parse(End);
            return end.Subtract(start).Days;
        }

        public static List<string> GetTables()
        {
            using (SqliteConnection connection = new(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sqlCommand = $"SELECT name FROM sqlite_schema WHERE type='table' ORDER BY name";
                    return connection.Query<string>(sqlCommand).ToList();
                }
                catch
                {
                    AnsiConsole.MarkupLine("Database doesn't contain any table/project yet.");
                    Console.Read();
                    return null;
                }
            }
        }

        public static void CreateNewTable(string project)
        {
            using (SqliteConnection connection = new(ConnectionString))
            {
                string sqlCommand = $"CREATE TABLE IF NOT EXISTS \"{project}\"(StartDate DATE,StartTime DATETIME,EndDate DATE,EndTime DATETIME,Duration TIME)";
                connection.Query(sqlCommand);
            }
        }

        private static string[] GetOppositeDates(string project)
        {
            using (SqliteConnection connection = new(ConnectionString))
            {
                string sqlCommand = $"SELECT MAX(StartDate) FROM {project}";
                List<string> firstDate = connection.Query<string>(sqlCommand).ToList();

                sqlCommand = $"SELECT MIN(StartDate) FROM {project}";
                List<string> lastDate = connection.Query<string>(sqlCommand).ToList();
                return [firstDate[0], lastDate[0]];
            }
        }

        private static List<CodingSession> ReverseList(List<CodingSession> list, bool allData = true)
        {
            int r = allData == true ? list.Count - 1 : list.Count - 2;
            CodingSession tempo;
            for (int l = 0; l < r; l++)
            {
                tempo = list[l];
                list[l] = list[r];
                list[r] = tempo;
                r--;
            }
            return list;
        }

        public static void ExecuteQuery(string sqlCommand, string startDate = "", string endDate = "", string startTime = "", string endTime = "", string duration = "", string id = "")
        {
            try
            {
                using (SqliteConnection connection = new(ConnectionString))
                {
                    connection.Query(sqlCommand,
                        new { id, startDate, startTime, endDate, endTime, duration });
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine(ex.Message);
                Console.Read();
            }
        }

        internal static void AddDataToDB()
        {
            try
            {
                using (SqliteConnection connection = new(ConnectionString))
                {
                    CreateNewTable("test_table");
                    string? sqlCommand = "SELECT * FROM test_table";
                    DateTime date = DateTime.Now.AddDays(-50);
                    DateTime startDate = date.AddDays(-1).AddHours(15).AddMinutes(30);
                    CreateNewTable("test_table");
                    object? reader = connection.ExecuteScalar(sqlCommand);
                    bool tableNotFilled = reader == null;
                    if (tableNotFilled)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            sqlCommand = "INSERT INTO test_table (StartDate,EndDate,StartTime,EndTime,Duration) VALUES($startDate,$endDate,$startTime,$endTime,$duration)";
                            string duration = GetDuration(startDate.ToString("yyyy.MM.dd"), startDate.ToString("HH:mm"),
                                date.ToString("yyyy.MM.dd"), date.ToString("HH:mm"));
                            ExecuteQuery(sqlCommand, startDate.ToString("yyyy.MM.dd"), date.ToString("yyyy.MM.dd"),
                                startDate.ToString("HH:mm"), date.ToString("HH:mm"), duration);
                            startDate = startDate.AddDays(1);
                            date = date.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine(ex.Message);
                AnsiConsole.WriteLine(ex.Source);
            }
        }
    }
}