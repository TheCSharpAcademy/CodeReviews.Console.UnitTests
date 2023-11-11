namespace CodeTracker.Tests
{
    [TestClass]
    public class CodeTrackerValidation
    {
        [TestMethod]
        [DataRow(" ")]
        [DataRow("2022-10-10")]
        [DataRow("2022:10:10 12:00:00")]
        [DataRow("2022-10-33 12-00-00")]
        [DataRow("2022-13-33 12:00:00")]
        [DataRow("1-12-25 12:00:00")]
        [DataRow("-1-12-25 13:33:99")]
        public void WhenValidDateTimeReturnsFalse(string value)
        {
            var result = Validation_Test.ValidDateTime(value);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("2022-10-10 12:00:00")]
        [DataRow("2022-12-25 23:55:59")]
        [DataRow("3455-12-31 12:34:56")]
        public void WhenValidDateTimeReturnsTrue(string value)
        {
            var result = Validation_Test.ValidDateTime(value);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-2424)]
        [DataRow(29290)]
        [DataRow(10002)]
        public void WhenValidYearReturnsFalse(int value)
        {
            var result = Validation_Test.ValidYear(value);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(2023)]
        [DataRow(100)]
        [DataRow(2929)]
        [DataRow(9999)]
        public void WhenValidYearReturnsTrue(int value)
        {
            var result = Validation_Test.ValidYear(value);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("2022:13")]
        [DataRow("2022-00")]
        [DataRow("2022-53")]
        [DataRow("5505_13")]
        [DataRow("0000/22")]
        [DataRow("0000 52th")]
        public void WhenValidWeekReturnsFalse(string week)
        {
            var result = Validation_Test.ValidWeek(week);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("2022-13")]
        [DataRow("2022-52")]
        [DataRow("5505-01")]
        [DataRow("0000-22")]
        [DataRow("0000-45")]
        public void WhenValidWeekReturnsTrue(string week)
        {
            var result = Validation_Test.ValidWeek(week);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("2022-10-10 10:20:14")]
        [DataRow("2022-10-33")]
        [DataRow("2022-22-95")]
        [DataRow("2022-13-33")]
        [DataRow("3949-11-22 01:22:30")]
        [DataRow("date")]
        public void WhenValidDateReturnsFalse(string value)
        {
            var result = Validation_Test.ValidDate(value);
            Assert.IsFalse(result);
        }
        [TestMethod]
        [DataRow("2022-10-10")]
        [DataRow("1993-05-22")]
        [DataRow("9999-12-31")]
        [DataRow("0001-01-20")]
        public void WhenValidDateReturnsTrue(string value)
        {
            var result = Validation_Test.ValidDate(value);
            Assert.IsTrue(result);
        }
    }
}
