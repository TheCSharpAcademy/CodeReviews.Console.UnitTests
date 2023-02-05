using CodingTracker;

namespace edvaudin.UnitTests
{
    [TestClass]
    public class CodingTrackerTests
    {
        [TestMethod]
        [DataRow("beans")]
        [DataRow("")]
        [DataRow("x")]
        [DataRow("2")]
        public void IsValidFilterOption_WithInvalidInput_ReturnsFalse(string option)
        {
            bool result = Validator.IsValidFilterOption(option);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("a")]
        [DataRow("d")]
        [DataRow("w")]
        [DataRow("m")]
        [DataRow("y")]
        public void IsValidFilterOption_WithValidInput_ReturnsTrue(string option)
        {
            bool result = Validator.IsValidFilterOption(option);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("beans")]
        [DataRow("")]
        [DataRow("x")]
        [DataRow("2")]
        public void IsValidOption_WithInvalidInput_ReturnsFalse(string option)
        {
            bool result = Validator.IsValidOption(option);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("v")]
        [DataRow("a")]
        [DataRow("d")]
        [DataRow("u")]
        [DataRow("srt")]
        [DataRow("stp")]
        [DataRow("0")]
        public void IsValidOption_WithValidInput_ReturnsTrue(string option)
        {
            bool result = Validator.IsValidOption(option);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("40-03-24 23-03-22")]
        [DataRow("12-03-24 25-03-22")]
        [DataRow("12:03:24 04:03:22")]
        [DataRow("12/03/24 04:03:22")]
        public void IsValidDateInput_WithInvalidInput_ReturnsFalse(string input)
        {
            bool result = Validator.IsValidDateInput(input);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("05-02-23 22-31-00")]
        [DataRow("17-01-23 05-24-20")]
        [DataRow("17-12-01 04-24-20")]
        public void IsValidDateInput_WithValidInput_ReturnsTrue(string input)
        {
            bool result = Validator.IsValidDateInput(input);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("05-02-23 22-31-00", "2023-03-04 00:00:00")]
        [DataRow("17-01-23 05-24-20", "2023-02-04 00:00:00")]
        [DataRow("17-12-01 04-24-20", "2002-02-04 00:00:00")]
        public void IsDateAfterStartTime_WithDateOlderThanStartTime_ReturnsFalse(string input, string startTimeStr)
        {
            DateTime startTime = DateTime.Parse(startTimeStr);
            bool result = Validator.IsDateAfterStartTime(input, startTime);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("05-02-23 22-31-00", "2023-02-04 00:00:00")]
        [DataRow("17-01-23 05-24-20", "2023-01-04 00:00:00")]
        [DataRow("17-12-01 04-24-20", "2001-02-04 00:00:00")]
        public void IsDateAfterStartTime_WithDateNewerThanStartTime_ReturnsTrue(string input, string startTimeStr)
        {
            DateTime startTime = DateTime.Parse(startTimeStr);
            bool result = Validator.IsDateAfterStartTime(input, startTime);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("05-02-23 22-31-00")]
        [DataRow("17-01-23 05-24-20")]
        [DataRow("17-12-01 04-24-20")]
        public void ConvertToDate_ReturnsCorrectDateTime(string input)
        {
            DateTime date = Validator.ConvertToDate(input);
            Assert.AreEqual(date.ToString(@"dd-MM-yy HH-mm-ss"), input);
        }

        [TestMethod]
        [DataRow("05-02-23 22-31-00", "05-02-23 23-31-00", "01:00:00")]
        [DataRow("17-01-23 05-24-20", "17-01-23 07-25-20", "02:01:00")]
        [DataRow("17-12-01 04-24-20", "17-12-01 04-26-40", "00:02:20")]
        public void CalculateDuration_ReturnsCorrectDuration(string startTimeStr, string endTimeStr, string expectedTimeSpanStr)
        {
            TimeSpan result = Validator.CalculateDuration(startTimeStr, endTimeStr);
            TimeSpan expectedTimeSpan = TimeSpan.Parse(expectedTimeSpanStr);
            Assert.AreEqual(result, expectedTimeSpan);
        }
    }
}