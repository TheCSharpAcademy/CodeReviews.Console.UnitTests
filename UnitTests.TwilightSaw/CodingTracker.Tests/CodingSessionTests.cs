using CodingTracker.TwilightSaw;
using Microsoft.Data.Sqlite;
using Moq;
using Newtonsoft.Json.Linq;

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

        [TestMethod]
        [DataRow("13.11.2024 23:01:01", "13.11.2024 01:02:01")]
        [DataRow("13.11.2024 15:01:01", "12.11.2024 12:02:01")]
        public void IsStartTimeIsGreaterThanEndTime(string startTime, string endTime)
        {
            var codingSession = new CodingSession
            {
                StartTime = DateTime.Parse(startTime),
                EndTime = DateTime.Parse(endTime)
            };
            var exception = Assert.ThrowsException<Exception>(() => codingSession.GetStartTime());
            Assert.AreEqual("You are trying to add session that finishes in another day or entered bad Start Time or End Time", exception.Message);
        }

        [TestMethod]
        [DataRow("13.11.2024 02:01:01", "13.11.2024 07:32:56")]
        [DataRow("13.11.2024 12:27:41", "14.11.2024 06:05:03")]
        public void IsEndTimeIsGreaterThanStartTime(string startTime, string endTime)
        {
            var codingSession = new CodingSession
            {
                StartTime = DateTime.Parse(startTime),
                EndTime = DateTime.Parse(endTime)
            };
            Assert.AreEqual(DateTime.Parse(startTime), codingSession.GetStartTime());
        }


        [TestMethod]
        [DataRow(10, 7)]
        [DataRow(20, 15)]
        public void IsCreateSpecifiedIntReturnsCorrectly(int bound, int input)
        {
            var inputs = new Queue<string>(["invalid", "-1", input.ToString()]);
            var mockInputProvider = new Mock<IUserInputProvider>();
            mockInputProvider
                .SetupSequence(provider => provider.ReadInput())
                .Returns(inputs.Dequeue);

            var result = new UserInput(mockInputProvider.Object).CreateSpecifiedInt(bound, "Test 1");

            Assert.AreEqual(input, result);
        }
    }
}