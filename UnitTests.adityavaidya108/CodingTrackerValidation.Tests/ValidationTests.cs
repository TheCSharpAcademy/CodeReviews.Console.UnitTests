using CodingTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingTrackerValidation.Tests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    [DataRow("11:30")]
    [DataRow("07:59")]
    [DataRow("19:06")]
    public void TryParseTime_ValidTimeOnly_ReturnsTrue(string value)
    {
        var result = Validation.TryParseTime(value, out TimeOnly time);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("11:30:12")]
    [DataRow("07:30:102")]
    [DataRow("some_random_string")]
    [DataRow("1:30")]
    [DataRow("")]
    public void TryParseTime_InvalidTimeOnly_ReturnsFalse(string value)
    {
        var result = Validation.TryParseTime(value, out TimeOnly time);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2025-01-01")]
    [DataRow("2024-12-31")]
    [DataRow("2018-11-30")]
    public void TryParseDate_ValidDatesOnly_ReturnsTrue(string value)
    {
        var result = Validation.TryParseDate(value, out DateOnly date);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2050-01-12")]
    [DataRow("2025-31-31")]
    [DataRow("20240-12-31")]
    [DataRow("some_random_string")]
    public void TryParseDate_InValidDatesOnly_ReturnsFalse(string value)
    {
        var result = Validation.TryParseDate(value, out DateOnly date);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("10:00", "12:30", 2, 30, 0)]
    [DataRow("14:00", "14:00", 0, 0, 0)]
    [DataRow("09:59", "10:00", 0, 1, 0)]
    [DataRow("00:00", "23:59", 23, 59, 0)]
    public void CalculateTimeSpan_ValidCase_ReturnsCorrectDuration(string startTime, string endTime,
        int expectedHours, int expectedMinutes, int expectedSeconds)
    {
        var startSessionTime = TimeOnly.Parse(startTime);
        var endSessionTime = TimeOnly.Parse(endTime);
        var expectedDuration = new TimeSpan(expectedHours, expectedMinutes, expectedSeconds);
        var result = Validation.CalculateTimeSpan(startSessionTime, endSessionTime);
        Assert.AreEqual(expectedDuration, result);
    }

    [TestMethod]
    [DataRow("10:00", "12:30")]
    [DataRow("09:59", "10:00")]
    [DataRow("00:00", "23:59")]
    public void ValidateEndSessionTime_ValidStartAndEndTimes_ReturnsTrue(string startSessionTime, string endSessionTime)
    {
        var result = Validation.ValidateEndSessionTime(endSessionTime, startSessionTime);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("10:00", "some_random_endtime")]
    [DataRow("09:59:15", "10:00:30")]
    [DataRow("10:30", "10:20")]
    public void ValidateEndSessionTime_ValidStartAndEndTimes_ReturnsFalse(string startSessionTime, string endSessionTime)
    {
        var result = Validation.ValidateEndSessionTime(endSessionTime, startSessionTime);
        Assert.IsFalse(result);
    }
}
