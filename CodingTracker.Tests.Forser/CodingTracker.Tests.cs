using CodingTracker.Forser;
using System.Drawing;

namespace CodingTracker.Tests.Forser
{
    [TestClass]
    public class CodingTrackerTests
    {
        private readonly Validation _validation;

        public CodingTrackerTests()
        {
            _validation = new Validation("yy-MM-dd HH:mm");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("23-03-24 16:99:20")]
        [DataRow("23-23-03 12:00:00")]
        [DataRow("00-01-59 12:00:00")]
        [DataRow("23-03-30 12:00:99")]
        [DataRow("19/02/02 02:02:02")]
        public void IsValidDateInput_WithInvalidInput_ReturnsFalse(string input)
        {
            bool result = _validation.ValidateFormat(input, input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("05-05-05 12:00", "05-05-05 13:00")]
        [DataRow("23-10-07 17:13", "23-10-07 18:13")]
        [DataRow("23-12-31 23:59", "24-01-01 01:00")]
        public void IsValidDateInput_WithValidInput_ReturnsTrue(string input, string endDate)
        {
            bool result = _validation.ValidateFormat(input, endDate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("24-01-05 12:00", "23-01-01 12:00")]
        [DataRow("23-01-01 12:00", "05-01-05 12:00")]
        [DataRow("05-11-05 12:00", "05-11-05 12:00")]
        public void IsStartDate_BeforeEndDate_WithInvalidInput_ReturnFalse(string input, string endInput)
        {
            DateTime startDate = DateTime.Parse(input);
            DateTime endDate = DateTime.Parse(endInput);

            bool result = _validation.AreDatesValid(startDate, endDate);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("02-02-02 12:00", "02-02-02 13:00")]
        [DataRow("23-10-07 12:00", "23-11-07 12:00")]
        public void IsStartDate_BeforeEndDate_WithValidInput_ReturnTrue(string input, string endInput)
        {
            DateTime startDate = DateTime.Parse(input);
            DateTime endDate = DateTime.Parse(endInput);

            bool result = _validation.AreDatesValid(startDate, endDate);

            Assert.IsTrue(result);
        }
    }
}