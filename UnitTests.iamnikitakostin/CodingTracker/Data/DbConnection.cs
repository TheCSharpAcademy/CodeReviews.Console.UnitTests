using CodingTracker.Controllers;
using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Data
{
    public class DbConnection : ConsoleController
    {
        private static SqliteConnection _connection;
        private static string? defaultConnection;

        public static SqliteConnection StartConnection()
        {
            if (_connection == null)
            {
                try
                {
                    var connectionStringSettings = System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"];
                    defaultConnection = connectionStringSettings;
                    _connection = new SqliteConnection(defaultConnection);
                    _connection.Open();
                    if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                    {
                        CreateTable("Sessions");
                        CreateTable("Goals");
                        DbConnection.SeedData();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage("An error has been received:" + ex.Message);
                }
                finally
                {
                    SuccessMessage("The database has been connected.");
                    _connection?.Close();
                }
            }
            return _connection;
        }

        public static bool CheckIfCurrentSessionExists()
        {
            using (_connection)
            {
                try
                {
                    string query = "SELECT * FROM Sessions ORDER BY ID DESC LIMIT 1";
                    CodingSession result = (CodingSession)_connection.Query<CodingSession>(query).FirstOrDefault();
                    if (result.EndTime == null) return true;
                    return false;
                }
                catch (Exception ex) {
                    ErrorMessage($"There has been an error while getting the current session: {ex.Message}");
                    return false;
                }
            }
        }

        public static List<CodingSession>? GetSessions(string order = "")
        {
            using (_connection)
            {
                try
                {
                    string query = $"SELECT * FROM Sessions{order};";
                    List<CodingSession> results = _connection.Query<CodingSession>(query).ToList();
                    return results;
                }
                catch (Exception ex) {
                    ErrorMessage($"There has been an error while showing all the records {ex.Message}");
                    return null;
                }
            }
        }

        public static int GetSessionsDurationsByPeriod(string startDate, string endDate)
        {
            using (_connection)
            {
                List<CodingSession> results = new List<CodingSession>();
                try
                {
                    string query = @"SELECT
                        Duration, EndTime FROM Sessions
                        WHERE StartTime BETWEEN @startDate and @endDate;
                    ";
                    results = _connection.Query<CodingSession>(query, new { startDate, endDate }).ToList();
                    int totalDurationInSecondsInPeriod = 0;
                    foreach (CodingSession session in results) {
                        if (session.EndTime == null) continue;
                        totalDurationInSecondsInPeriod += session.Duration;
                    }
                    return totalDurationInSecondsInPeriod;
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message);
                    return 0;
                }
            }
        }

        public static List<CodingSession> GetSessionsByPeriod(string startDate, string endDate, string order="")
        {
            using (_connection)
            {
                List<CodingSession> results = new List<CodingSession>();
                try
                {
                    string query = @"
                        SELECT Id, Duration, StartTime, EndTime 
                        FROM Sessions
                        WHERE StartTime BETWEEN @startDate AND @endDate
                    ";
                    if (!string.IsNullOrWhiteSpace(order))
                    {
                        query += order;
                    }
                    results = _connection.Query<CodingSession>(query, new { startDate, endDate }).ToList();
                    return results;
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message);
                    return results;
                }
            }
        }

        public static void GetSessionsByPeriod(string period)
        {
            using (_connection)
            {
                try
                {
                    string query;
                    switch (period)
                    {
                        case "week":
                            query = @"
                            SELECT 
                                strftime('%Y', StartTime) AS Year,
                                strftime('%W', StartTime) AS WeekNumber,
                                date(StartTime, 'weekday 0') AS WeekStart,
                                date(StartTime, 'weekday 0', '+6 days') AS WeekEnd,
                                COUNT(id) AS Count,
                                SUM(Duration) AS TotalDuration
                            FROM Sessions
                            GROUP BY Year, WeekNumber
                            ORDER BY TotalDuration DESC;                            
                            ";
                            List<WeekReportModel> results = new List<WeekReportModel>();
                            results = _connection.Query<WeekReportModel>(query).ToList();
                            Table table = new Table();
                            table.AddColumn(new TableColumn("Year/Week Number").Centered());
                            table.AddColumn(new TableColumn("Week Start").Centered());
                            table.AddColumn(new TableColumn("Week End").Centered());
                            table.AddColumn(new TableColumn("Duration").Centered());
                            table.AddColumn(new TableColumn("Sessions Count").Centered());
                            foreach (WeekReportModel item in results)
                            {
                                table.AddRow($"{item.Year.ToString()}/{item.WeekNumber.ToString()}", item.WeekStart, item.WeekEnd,
                                             TimeController.ConvertFromSeconds(item.TotalDuration), item.Count.ToString());
                            }
                            AnsiConsole.Write(table);
                            break;
                        case "month":
                            query = @"
                            SELECT 
                                strftime('%Y.%m.%d %H:%M:%S', StartTime) AS Date,
                                strftime('%m', StartTime) AS Month,
                                strftime('%Y', StartTime) AS Year,
                                COUNT(id) AS Count,
                                SUM(Duration) AS TotalDuration
                            FROM Sessions
                            GROUP BY Year, Month
                            ORDER BY TotalDuration DESC
                            ";
                            List<MonthReportModel> monthResults = new List<MonthReportModel>();
                            monthResults = _connection.Query<MonthReportModel>(query).ToList();
                            Table monthTable = new Table();
                            monthTable.AddColumn(new TableColumn("Month").Centered());
                            monthTable.AddColumn(new TableColumn("Total Duration").Centered());
                            monthTable.AddColumn(new TableColumn("Sessions Count").Centered());
                            foreach (MonthReportModel item in monthResults)
                            {
                                monthTable.AddRow(item.Month.ToString(), TimeController.ConvertFromSeconds(item.TotalDuration), item.Count.ToString());
                            }
                            AnsiConsole.Write(monthTable);
                            break;
                        case "year":
                            query = @"
                            SELECT 
                                strftime('%Y.%m.%d %H:%M:%S', StartTime) AS Date,
                                strftime('%Y', StartTime) AS Year,
                                COUNT(id) AS Count,
                                SUM(Duration) AS TotalDuration
                            FROM Sessions
                            GROUP BY Year
                            ORDER BY TotalDuration DESC;
                            ";
                            List<YearReportModel> yearResults = new List<YearReportModel>();
                            yearResults = _connection.Query<YearReportModel>(query).ToList();
                            Table yearTable = new Table();
                            yearTable.AddColumn(new TableColumn("Month").Centered());
                            yearTable.AddColumn(new TableColumn("Total Duration").Centered());
                            yearTable.AddColumn(new TableColumn("Sessions Count").Centered());
                            foreach (YearReportModel item in yearResults)
                            {
                                yearTable.AddRow(item.Year.ToString(), TimeController.ConvertFromSeconds(item.TotalDuration), item.Count.ToString());
                            }
                            AnsiConsole.Write(yearTable);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while showing all the records {ex.Message}");
                }
            }
        }

        public static CodingSession? GetCurrentSession()
        {
            using (_connection)
            {
                try
                {
                    string query = "SELECT * FROM Sessions ORDER BY ID DESC LIMIT 1";
                    CodingSession result = (CodingSession)_connection.Query<CodingSession>(query).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while showing all the records {ex.Message}");
                    return null;
                }
            }
        }

        public static CodingSession? GetSession(string id)
        {
            using (_connection)
            {
                try
                {
                    string query = "SELECT * FROM Sessions WHERE id=@id";
                    CodingSession result = (CodingSession)_connection.QuerySingleOrDefault<CodingSession>(query, new {id});
                    return result;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while getting the record {ex.Message}");
                    return null;
                }
            }
        }

        public static bool AddSession(CodingSession codingSession) {
            using (_connection)
            {
                try
                {
                    string query = "INSERT INTO Sessions (StartTime) VALUES (@StartTime)";
                    _connection.Execute(query, new { codingSession.StartTime });
                    return true;
                } catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while adding a record: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool UpdateSession(CodingSession codingSession) {
            using (_connection) {
                try 
                {
                    string query = "UPDATE Sessions SET startTime = @StartTime, endTime = @EndTime, duration = @Duration WHERE id=@Id";
                    int rowAffected = _connection.Execute(query, new
                    {
                        codingSession.StartTime,
                        codingSession.EndTime,
                        codingSession.Duration,
                        codingSession.Id
                    });
                    if (rowAffected > 0) return true;
                    return false;
                } catch (Exception ex) {
                    ErrorMessage($"There has been an error while executing a command: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool CheckIfSessionExists(string id)
        {
            using (_connection)
            {
                try
                {
                    string query = "SELECT * FROM Sessions WHERE id = @id";
                    int rowAffected = _connection.ExecuteScalar<int>(query, new { id });
                    if (rowAffected != 0) return true;
                    return false;
                } catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while executing a command: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool DeleteSession(string id) {
            using (_connection)
            {
                if (!CheckIfSessionExists(id)) return false;
                try
                {
                    string query = "DELETE FROM Sessions WHERE id = @id";
                    int rowAffected = _connection.Execute(query, new { id });
                    if (rowAffected != 0) return true;

                    return true;
                } catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while executing a command: {ex.Message}");
                    return false;
                }
            }
        }

        public static void CreateTable(string name)
        {
            using (_connection)
            {
                try
                {
                    var query = "";
                    if (name == "Sessions")
                    {
                        query = @"
                        CREATE TABLE IF NOT EXISTS Sessions (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            StartTime TEXT NOT NULL,
                            EndTime TEXT,
                            Duration INTEGER
                        )
                        ";
                        _connection.Execute(query);
                    } else
                    {
                        query =
                       @"
                        CREATE TABLE IF NOT EXISTS Goals (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            PeriodInDays INTEGER NOT NULL,
                            StartDate TEXT NOT NULL,
                            DesiredLengthInSeconds INTEGER NOT NULL,
                            IsActive INTEGER NOT NULL
                        )
                    ";
                        _connection.Execute(query);
                    }

                }
                catch (Exception ex)
                {
                    ErrorMessage("Error creating table: " + ex.Message);
                }
            }
        }

        public static void SeedData()
        {
            using (_connection)
            {
                string query = "SELECT COUNT(*) FROM Sessions";
                int sessionsCount = _connection.ExecuteScalar<int>(query);
                if (sessionsCount == 0)
                {
                    AnsiConsole.WriteLine("Seeding data into the Sessions table.");
                    Random random = new Random();
                    for (int i = 0; i < 20; i++)
                    {
                        DateTime startTime = DateTime.Now.AddDays(-random.Next(0, 100));
                        DateTime endTime = (startTime.AddMinutes(random.Next(15, 250)));
                        double duration = endTime.Subtract(startTime).TotalSeconds;
                        object session = new { startTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime = endTime.ToString("yyyy-MM-dd HH:mm:ss"), duration };
                        query =
                        @"
                            INSERT INTO Sessions (StartTime, EndTime, Duration)
                            VALUES (@startTime, @endTime, @duration)
                        ";
                        _connection.Execute(query, session);
                    }
                }

                query = "SELECT COUNT(*) FROM Goals";
                int goalsCount = _connection.ExecuteScalar<int>(query);
                if (goalsCount == 0)
                {
                    AnsiConsole.WriteLine ("Seeding data into the Goals table.");
                    Random random = new Random();
                    for (int i = 1; i < 5; i++)
                    {
                        DateTime startTime = DateTime.Now;
                        int periodInDays = i * random.Next(10, 30);
                        int desiredDuration = random.Next(3000,300000);
                        bool isActive = true;
                        query =
                        @"
                            INSERT INTO Goals (PeriodInDays, StartDate, DesiredLengthInSeconds, IsActive)
                            VALUES (@periodInDays, @startTime, @desiredDuration, @isActive)
                        ";
                        _connection.Execute(query, new { periodInDays, startTime, desiredDuration, isActive });
                    }
                }
            }
        }

        public static bool AddGoal(Goal goal)
        {
            using (_connection)
            {
                try
                {
                    string query = "INSERT INTO Goals (PeriodInDays, DesiredLengthInSeconds, StartDate, IsActive) VALUES (@PeriodInDays, @DesiredLengthInSeconds, @StartDate, @IsActive)";
                    _connection.Execute(query, new { goal.PeriodInDays, goal.StartDate, goal.DesiredLengthInSeconds, goal.IsActive });
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while adding a goal: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool DeleteGoal(string id)
        {
            using (_connection)
            {
                try
                {
                    string query = "DELETE FROM Goals WHERE id = @id";
                    int rowAffected = _connection.Execute(query, new { id });
                    if (rowAffected != 0) return true;
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while executing a command: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool UpdateGoal(Goal goal)
        {
            using (_connection)
            {
                try
                {
                    string query = "UPDATE Goals SET StartDate = @StartDate, PeriodInDays = @PeriodInDays, IsActive = @IsActive, desiredLengthInSeconds = @DesiredLengthInSeconds WHERE id=@Id";
                    int rowAffected = _connection.Execute(query, new
                    {
                        goal.StartDate,
                        goal.PeriodInDays,
                        goal.DesiredLengthInSeconds,
                        goal.IsActive,
                        goal.Id
                    });
                    if (rowAffected > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while executing a command: {ex.Message}");
                    return false;
                }
            }
        }

        public static List<Goal>? GetGoals()
        {
            using (_connection)
            {
                try
                {
                    string query = "SELECT * FROM Goals";
                    List<Goal> results = _connection.Query<Goal>(query).ToList();
                    return results;
                }
                catch (Exception ex)
                {
                    ErrorMessage($"There has been an error while showing all the goals: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
