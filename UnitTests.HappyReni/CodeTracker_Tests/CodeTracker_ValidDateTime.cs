namespace CodeTracker.Tests
{
    [TestClass]
    public class CodeTrackerValidation
    {
        [TestMethod]
        [DataRow("2022-10-10 10:20:14")]
        [DataRow("2022-10-10")]
        [DataRow("2022-10-33")]
        [DataRow("2022-13-33")]
        [DataRow("3949-11-22 01:22:30")]
        public void ValidDateTime(string value)
        {
            try
            {
                Validation.ValidDateTime(value);
            }
            catch
            {
                Assert.Fail($"{value} is not valid DateTime type value.");
            }
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(202)]
        [DataRow(2023)]
        [DataRow(9999)]
        [DataRow(10002)]
        public void ValidYear(int value)
        {
            try
            {
                Validation.ValidYear(value);
            }
            catch
            {
                Assert.Fail($"{value} is not valid DateTime type value.");
            }
        }

        [TestMethod]
        [DataRow("2022-13")]
        [DataRow("202-2")]
        [DataRow("1999-60")]
        [DataRow("1234520-22")]
        [DataRow("hello")]
        public void ValidWeek(string week)
        {
            try
            {
                Validation.ValidWeek(week);
            }
            catch
            {
                Assert.Fail($"{week} is not a valid week.");
            }
        }

        [TestMethod]
        [DataRow("2022-10-10 10:20:14")]
        [DataRow("2022-10-10")]
        [DataRow("2022-10-33")]
        [DataRow("2022-22-95")]
        [DataRow("2022-13-33")]
        [DataRow("1993-05-22")]
        [DataRow("3949-11-22 01:22:30")]
        [DataRow("date")]
        public void ValidDate(string value)
        {
            try
            {
                Validation.ValidDate(value);
            }
            catch
            {
                Assert.Fail($"{value} is not valid date.");
            }
        }
    }
}
