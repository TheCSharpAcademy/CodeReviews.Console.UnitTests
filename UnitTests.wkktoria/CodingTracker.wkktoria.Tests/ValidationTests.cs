namespace CodingTracker.wkktoria.Tests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    [DataRow("10-01-21")]
    [DataRow("10-01-21 16:83")]
    [DataRow("10-13-21 12:00")]
    [DataRow("10-13-21 25:20")]
    [DataRow("10.01.21")]
    [DataRow("10.01.21 16:83")]
    [DataRow("10.13.21 12:00")]
    [DataRow("10.13.21 25:00")]
    [DataRow("10/01/21")]
    [DataRow("10/01/21 16:83")]
    [DataRow("10/01/21 12:00")]
    [DataRow("10/13/21 25:00")]
    [DataRow("10:00 10-01-21")]
    public void ValidateDateTime_IncorrectDateTimeFormat_ReturnFalse(string dateTimeStr)
    {
        var result = Validation.ValidateDateTime(dateTimeStr);

        Assert.IsFalse(result, $"{dateTimeStr} should be invalid date time.");
    }

    [TestMethod]
    [DataRow("10-01-21 12:00")]
    [DataRow("13-10-21 16:00")]
    [DataRow("10-01-21 12:30")]
    [DataRow("24-12-23 18:15")]
    public void ValidateDateTime_CorrectDateTimeFormat_ReturnTrue(string dateTimeStr)
    {
        var result = Validation.ValidateDateTime(dateTimeStr);

        Assert.IsTrue(result, $"{dateTimeStr} should be valid date time.");
    }


    [TestMethod]
    public void ValidateTwoDates_StartDateTimeAfterEndDateTime_ReturnFalse()
    {
        var startDateTime = DateTime.Now.AddHours(8);
        var endDateTime = DateTime.Now;

        var result = Validation.ValidateTwoDates(startDateTime, endDateTime);

        Assert.IsFalse(result, $"{startDateTime} which is after {endDateTime} should be invalid.");
    }

    [TestMethod]
    public void ValidateTwoDates_StartDateTimeBeforeEndTime_ReturnTrue()
    {
        var startDateTime = DateTime.Now;
        var endDateTime = DateTime.Now.AddHours(8);

        var result = Validation.ValidateTwoDates(startDateTime, endDateTime);

        Assert.IsTrue(result, $"{startDateTime} which is before {endDateTime} should be valid.");
    }
}