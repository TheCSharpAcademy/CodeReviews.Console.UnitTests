using Doc415.CodingTracker;
namespace CodintTracker.UnitTests
{
    [TestClass]
    public class TestValidators
    {
        [TestMethod]
        [DataRow("13/11/24 13:01", false)]
        [DataRow("13-11-24 12-00", false)]
        [DataRow("12-11-24 00:00", true)]
        [DataRow("01-11-23", false)]
        [DataRow("01-13-24 11:00", false)]
        [DataRow("01-11-23 12:61", false)]
        [DataRow("", false)]
        [DataRow(" ", false)]
        [DataRow("1234", false)]
        [DataRow("dfdsfs", false)]
        public void IsDateInputValid(string date,bool expected)
        {
            var result = Validator.IsValidDate(date);
            Assert.AreEqual(expected, result.Item1);
        }

        [TestMethod]
        [DataRow("-1", false)]
        [DataRow("0", false)]
        [DataRow("12", true)]
        [DataRow("-12", false)]
        public void IsHourInputValid(string hour,bool expected)
        {
            var result=Validator.IsValidHour(hour);
            Assert.AreEqual(expected, result.Item1);
        }
    }
}