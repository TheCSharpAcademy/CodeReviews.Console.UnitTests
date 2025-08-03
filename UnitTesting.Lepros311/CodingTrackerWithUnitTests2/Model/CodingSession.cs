using System.Data.SQLite;
using System.Globalization;
using Dapper;
using System;

namespace CodingTracker
{
    public class CodingSession
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;

        public CodingSession() { }

        public CodingSession(DateTime date, DateTime startTime, DateTime endTime)
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int GetRecordsCount(SQLiteConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM CodingSessions";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }  
    }
}