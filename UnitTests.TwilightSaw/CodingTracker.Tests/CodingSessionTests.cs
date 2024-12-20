using CodingTracker.TwilightSaw;
using Microsoft.Data.Sqlite;
using Moq;
using Newtonsoft.Json.Linq;
using System;

namespace CodingTracker.Tests
{
    [TestClass]
    public class CodingSessionTests
    {
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
        [Timeout(1000)]
        [DataRow(10, 7)]
        [DataRow(20, 15)]
        public void IsCreateSpecifiedIntReturnsCorrectly(int bound, int input)
        {
            var inputs = new List<string>(["invalid", "-1", input.ToString()]);
            var index = 0;

            var mockInputProvider = new Mock<IUserInputProvider>();
            mockInputProvider
                .Setup(provider => provider.ReadInput())
                .Returns(() => inputs[index++]);
                

            var result = new UserInput(mockInputProvider.Object).CreateSpecifiedInt(bound, "Bad value");

            Assert.AreEqual(input, result);
            mockInputProvider.Verify(provider => provider.ReadInput(), Times.AtLeast(1));
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("t")]
        [DataRow("T")]
        public void IsCheckTReturnsTrue(string input)
        {
            var result = UserInput.CheckT(input);
            Assert.AreEqual(DateTime.Now.ToShortDateString(), result);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("sample")]
        [DataRow("Text")]
        public void IsCheckTReturnsFalse(string input)
        {
            var result = UserInput.CheckT(input);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("n")]
        [DataRow("N")]
        public void IsCheckNReturnsTrue(string input)
        {
            var result = UserInput.CheckN(input);
            Assert.AreEqual(DateTime.Now.ToLongTimeString(), result);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("sample")]
        [DataRow("Text")]
        public void IsCheckNReturnsFalse(string input)
        {
            var result = UserInput.CheckN(input);
            Assert.AreEqual(input, result);
        }
    }
}