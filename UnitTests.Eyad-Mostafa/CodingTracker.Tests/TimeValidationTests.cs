using CodingTracker.Utilities;

namespace CodingTracker.Tests;

[TestClass]
public class TimeValidationTests
{
    [TestMethod]
    public void IsValidTime_ValidTimeFormat_ReturnsTrue()
    {
        Assert.IsTrue(Validation.IsValidTime("12:30"));
        Assert.IsTrue(Validation.IsValidTime("00:00"));
        Assert.IsTrue(Validation.IsValidTime("23:59"));
    }

    [TestMethod]
    public void IsValidTime_InvalidTimeFormat_ReturnsFalse()
    {
        Assert.IsFalse(Validation.IsValidTime("invalid")); // Non-time string
        Assert.IsFalse(Validation.IsValidTime(""));        // Empty string
        Assert.IsFalse(Validation.IsValidTime(null));      // Null value
        Assert.IsFalse(Validation.IsValidTime("25:00"));   // Invalid hour
        Assert.IsFalse(Validation.IsValidTime("12:60"));   // Invalid minute
    }
}
