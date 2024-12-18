using CodingTracker.TwilightSaw;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Tests
{
    [TestClass]
    public class CodingSessionTests
    {
        [TestInitialize]
        public void Setup()
        {
            using var connection = new SqliteConnection("DataSource=:memory:;Mode=Memory;Cache=Shared");
            connection.Open();
        }

        //In this project back then Date part in the DateTime was not intended to include in the actual value, so it's only time values that are checked 
        [TestMethod]
        [DataRow("13.11.2024 13:01:01", "13.11.2024 14:02:01", true)]
        [DataRow("13.11.2024 15:01:01", "13.11.2024 12:02:01", false)]
        public void IsStartTimeIsGreaterThanEndTime(DateTime startTime, DateTime endTime)
        {
            var codingSession = new CodingSession
            {
                StartTime = startTime,
                EndTime = endTime
            };
            var result =  codingSession.GetStartTime();
            Assert.ThrowsException<Exception>(() => new Exception("You are trying to add session that finishes in another day or entered bad Start Time or End Time"));
        }
    }
}