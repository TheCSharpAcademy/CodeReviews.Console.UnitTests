using Dapper;
using System.Configuration;
using System.Data.SQLite;
using System.Globalization;

namespace CodingTracker.Model
{
    public class CodingSessionRepository : ICodingSessionRepository
    {
        private readonly string _connection;

        string? dbPath = ConfigurationManager.ConnectionStrings["connection"]?.ConnectionString;

        public CodingSessionRepository(string connection)
        {
            _connection = connection;
        }

        public void CreateTable()
        {
            using (var dbConnection = new SQLiteConnection(_connection))
            {
                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS CodingSessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date DATE NOT NULL,
                        StartTime DATETIME NOT NULL,
                        EndTime DATETIME NOT NULL                
                    );";

                dbConnection.Execute(createTableSql);
            }
        }

        public List<CodingSession> GetAllCodingSessions()
        {
            var sessions = new List<CodingSession>();

            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                string query = @"SELECT * FROM CodingSessions ORDER BY Date DESC, StartTime DESC";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var session = new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            StartTime = reader.GetDateTime(2),
                            EndTime = reader.GetDateTime(3)
                        };
                        sessions.Add(session);
                    }
                }
            }

            return sessions;
        }

        public (int TotalDistinctDays, int TotalSessions, string TotalDuration, int LongestStreak) GetReportData()
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                string query = @"
            SELECT 
                COUNT(DISTINCT Date) AS TotalDistinctDays,
                COUNT(*) AS TotalSessions,
                SUM(julianday(EndTime) - julianday(StartTime)) * 24 * 60 * 60 AS TotalDurationInSeconds
            FROM 
                CodingSessions;";

                var result = connection.QuerySingle(query);

                // Handle potential null values and cast to int
                int totalSeconds = (int)(result.TotalDurationInSeconds ?? 0); // Default to 0 if null
                int totalDistinctDays = (int)result.TotalDistinctDays;
                int totalSessions = (int)result.TotalSessions;

                TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
                string formattedDuration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    (int)timeSpan.TotalHours,
                    timeSpan.Minutes,
                    timeSpan.Seconds);

                var repository = new CodingSessionRepository(ConfigurationManager.AppSettings.Get("dbPath"));
                int longestStreak = repository.CalculateStreak();

                return (
                    TotalDistinctDays: totalDistinctDays,
                    TotalSessions: totalSessions,
                    TotalDuration: formattedDuration,
                    LongestStreak: longestStreak
                );
            }
        }

        public int CalculateStreak()
        {
            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            int currentStreak = 0;
            int longestStreak = 0;
            DateTime? lastDate = null;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string query = @"SELECT DISTINCT Date FROM CodingSessions ORDER BY Date;";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = DateTime.Parse(reader["Date"].ToString(), CultureInfo.InvariantCulture);

                        if (lastDate.HasValue && (date - lastDate.Value).Days == 1)
                            currentStreak++;
                        else
                            currentStreak = 1;

                        longestStreak = Math.Max(longestStreak, currentStreak);

                        lastDate = date;
                    }
                }
            }

            return longestStreak;
        }

        public void SeedDatabase()
        {
            using (var dbConnection = new SQLiteConnection(_connection))
            {
                dbConnection.Open();

                var existingSessions = dbConnection.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM CodingSessions;");

                if (existingSessions == 0)
                {
                    List<CodingSession> sessions = new List<CodingSession>();
                    HashSet<DateTime> usedDates = new HashSet<DateTime>();
                    Random random = new Random();
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-30);

                    for (int i = 0; i < 10; i++)
                    {
                        DateTime randomDate;

                        do
                        {
                            randomDate = startDate.AddDays(random.Next(0, 31));
                        } while (usedDates.Contains(randomDate));

                        usedDates.Add(randomDate);

                        int startHour = random.Next(6, 24);
                        int startMinute = random.Next(0, 4) * 15;
                        DateTime startTime = randomDate.Date.AddHours(startHour).AddMinutes(startMinute);

                        int durationMinutes = random.Next(30, 301);
                        TimeSpan duration = TimeSpan.FromMinutes(durationMinutes);
                        DateTime endTime = startTime.Add(duration);

                        int endMinute = endTime.Minute;
                        if (endMinute % 15 != 0)
                        {
                            endMinute = (endMinute / 15) * 15 + 15;
                            if (endMinute >= 60)
                            {
                                endMinute = 0;
                                endTime = endTime.AddHours(1);
                            }
                        }
                        endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endMinute, 0);

                        if (endTime.Date > randomDate.Date)
                            endTime = randomDate.Date.AddHours(23).AddMinutes(59);

                        sessions.Add(new CodingSession { Date = randomDate, StartTime = startTime, EndTime = endTime });
                    }

                    sessions.Sort((s1, s2) => s1.Date.CompareTo(s2.Date));

                    foreach (var session in sessions)
                    {
                        string insertSql = @"
                            INSERT INTO CodingSessions (Date, StartTime, EndTime)
                            VALUES (@Date, @StartTime, @EndTime);";

                        dbConnection.Execute(insertSql, new { Date = session.Date, StartTime = session.StartTime, EndTime = session.EndTime });
                    }
                }
            }
        }

        public int GetRecordIdCount(int recordId)
        {
            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");
            int count;
            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string checkCodingSessionIdQuery = "SELECT COUNT(*) FROM CodingSessions WHERE Id = @recordId";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = checkCodingSessionIdQuery;
                    command.Parameters.AddWithValue("@recordId", recordId);

                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }
    }
}