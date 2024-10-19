using CodingTracker.Dates;

namespace CodingTracker.UnitTests;

[TestClass]
public class InputHelperTests
{
    [TestMethod]
    public void ValidateDateFormat_ValidDateFormatLong_ReturnsTrue()
    {
        var results = DateHelper.ValidateDateFormat("02/01/2024");

        Assert.IsTrue(results);
    }

    [TestMethod]
    public void ValidateDateFormat_ValidDateFormatShort_ReturnsTrue()
    {
        var results = DateHelper.ValidateDateFormat("2/1/2024");

        Assert.IsTrue(results);
    }

    [TestMethod]
    public void ValidateDateFormat_IsntDate_ReturnsFalse()
    {
        var results = DateHelper.ValidateDateFormat("Not a date");

        Assert.IsFalse(results);
    }

    [TestMethod]
    public void ValidateDateFormat_InvalidDateFormatDayFirst_ReturnsFalse()
    {
        var results = DateHelper.ValidateDateFormat("24/12/1996");

        Assert.IsFalse(results);
    }

    [TestMethod]
    public void ValidateDateFormat_InvalidDateFormatNoYear_ReturnsFalse()
    {
        var results = DateHelper.ValidateDateFormat("10/19");

        Assert.IsFalse(results);
    }

    [TestMethod]
    public void ValidateTimeFormat_ValidTimeFormat12HourClock_ReturnsTrue()
    {
        var results = DateHelper.ValidateTimeFormat("12:00 PM");

        Assert.IsTrue(results);
    }

    [TestMethod]
    public void ValidateTimeFormat_ValidTimeFormat24HourClock_ReturnsTrue()
    {
        var results = DateHelper.ValidateTimeFormat("23:00");

        Assert.IsTrue(results);
    }

    [TestMethod]
    public void ValidateTimeFormat_IsntTime_ReturnsFalse()
    {
        var results = DateHelper.ValidateTimeFormat("Not a time");

        Assert.IsFalse(results);
    }
}