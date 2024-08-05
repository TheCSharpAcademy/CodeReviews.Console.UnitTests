using CodingTracker.kwm0304.Enums;
using CodingTracker.kwm0304.Utils;

namespace UnitTests.kwm0304;

[TestClass]
public class CodingTrackerValidationTests
{
    [TestMethod]
    public void ConvertDateTimeToString_ReturnsFormattedString()
    {
        DateTime dateTime = new(2024, 8, 5, 9, 20, 24);
        string result = Validator.ConvertDateTimeToString(dateTime);
        Assert.AreEqual("2024-08-05 09:20:24", result);
    }

    [TestMethod]
    public void ConvertTimeToInt_ReturnsTotalSeconds()
    {
        TimeSpan timeSpan = new(2, 30, 45);
        int result = Validator.ConvertTimeToInt(timeSpan);
        Assert.AreEqual(9045, result);
    }

    [TestMethod]
    public void ConvertTextToDateTime_ReturnsDateTime()
    {
        string dateStr = "2024-08-05 14:30:45";
        DateTime result = Validator.ConvertTextToDateTime(dateStr);
        Assert.AreEqual(new DateTime(2024, 8, 5, 14, 30, 45), result);
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ConvertTextToDateTime_ThrowsFormatException()
    {
        string dateStr = "Notadate";
        Validator.ConvertTextToDateTime(dateStr);
    }

    [TestMethod]
    public void ToDays_ReturnsCorrectDay()
    {
        Assert.AreEqual(7, Validator.ToDays(DateRange.Week));
        Assert.AreEqual(30, Validator.ToDays(DateRange.Month));
        Assert.AreEqual(365, Validator.ToDays(DateRange.Year));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ToDays_ThrowsArgumentOutOfRangeException()
    {
        DateRange invalidRange = (DateRange)999;
        Validator.ToDays(invalidRange);
    }

    [TestMethod]
    public void ToDateRange_ReturnsCorrectDateRange()
    {
        Assert.AreEqual(DateRange.Week, Validator.ToDateRange(7));
        Assert.AreEqual(DateRange.Month, Validator.ToDateRange(30));
        Assert.AreEqual(DateRange.Year, Validator.ToDateRange(365));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ToDateRange_ThrowsArgumentOutOfRangeException()
    {
        Validator.ToDateRange(999);
    }
}