using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTracker.Controllers;

namespace CodingTracker.Tests
{
    [TestClass]
    public class TimeControllerTests
    {
        [TestMethod]
        [DataRow(100, "00:01:40")]
        [DataRow(5, "00:00:05")]
        [DataRow(1, "00:00:01")]
        [DataRow(-123, "error")]
        [DataRow(-1111111, "error")]
        [DataRow(-1, "error")]
        public void ConvertFromSeconds_InputIsCorrect_ReturnsResult(int seconds, string expectedResult)
        {
            string convertedTime = TimeController.ConvertFromSeconds(seconds);

            Assert.AreEqual(expectedResult, convertedTime);
        }

        [TestMethod]
        [DataRow("2024-09-09", "2024-09-09")]
        [DataRow("2021-01-01", "2021-01-01")]
        public void GoalController_InputIsCorrect_ReturnsResult(string date, string expectedDateString)
        {
            DateOnly expectedDate = DateOnly.Parse(expectedDateString);

            DateOnly? result = GoalController.DateChecker(date);

            Assert.AreEqual(expectedDate, result);
        }

        [TestMethod]
        [DataRow("-123")]
        [DataRow("not correct")]
        [DataRow("5435981958")]
        public void GoalController_InputIsIncorrect_ReturnsNull(string date)
        {
            var result = GoalController.DateChecker(date);

            Assert.AreEqual(null, result);
        }
    }
}
