using Library;

namespace CodingTracker.Test.frockett;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    public void IsValidEndAndStartTime_EndIsAfterStart_ReturnsTrue()
    {
        DateTime startTime = new DateTime(2024, 3, 24, 10, 0, 0);
        DateTime endTime = new DateTime(2024, 3, 24, 11, 0, 0);
        bool result = Validation.IsValidEndAndStartTime(startTime, endTime);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidEndAndStartTime_EndIsBeforeStart_ReturnsFalse()
    {
        DateTime startTime = new DateTime(2024, 1, 24, 11, 0, 0);
        DateTime endTime = new DateTime(2024, 1, 24, 10, 0, 0);
        bool result = Validation.IsValidEndAndStartTime(startTime, endTime);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("03-2024", true)]
    [DataRow(null, false)]
    [DataRow("sj-iiio", false)]
    [DataRow("09-1966", true)]
    [DataRow("14-2014", false)]
    public void IsValidMonthAndYear_ReturnsExpected(string sDate, bool expectedIsValid)
    {
        var (isValid, _) = Validation.IsValidMonthAndYear(sDate);

        Assert.AreEqual(expectedIsValid, isValid);
    }

    [TestMethod]
    [DataRow("24-03-2023 14:00", true)]
    [DataRow("31-02-2023 00:00", false)]
    [DataRow("invalid-datetime", false)]
    [DataRow("", false)] 
    [DataRow(null, false)]
    public void IsValidDateTime_ReturnsExpectedIsValid(string sDateTime, bool expectedIsValid)
    {
        var (isValid, _) = Validation.IsValidDateTime(sDateTime);

        Assert.AreEqual(expectedIsValid, isValid, $"Expected IsValidDateTime to return {expectedIsValid} for '{sDateTime}'");
    }
}