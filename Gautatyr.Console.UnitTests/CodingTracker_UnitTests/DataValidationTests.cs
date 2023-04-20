using static CodingTracker.DataValidation;

namespace CodingTracker_UnitTests;

[TestClass]
public class DataValidationTests
{
    [TestMethod]
    public void IsPositiveNumber_PositiveNumber_ReturnsTrue()
    {
        string input = "23";

        var result = IsPositiveNumber(input);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsPositiveNumber_NegativeNumber_ReturnsFalse()
    {
        string input = "-23";

        var result = IsPositiveNumber(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsPositiveNumber_Zero_ReturnsTrue()
    {
        string input = "0";

        var result = IsPositiveNumber(input);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsPositiveNumber_NotANumber_ReturnsFalse()
    {
        string input = "I am not a number";

        var result = IsPositiveNumber(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsPositiveNumber_Empty_ReturnsFalse()
    {
        string input = string.Empty;

        var result = IsPositiveNumber(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidDate_ValidDate_ReturnsTrue()
    {
        string input = "22-10-98";

        var result = IsValidDate(input);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidDate_NotADate_ReturnsFalse()
    {
        string input = "this is not a date";

        var result = IsValidDate(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidDate_InvalidDateFormat_ReturnsFalse()
    {
        string input = "10-22-98";

        var result = IsValidDate(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidDate_Empty_ReturnsFalse()
    {
        string input = string.Empty;

        var result = IsValidDate(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidTime_ValidTime_ReturnsTrue()
    {
        string input = "22:10";

        var result = IsValidTime(input);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidTime_InvalidTime_ReturnsFalse()
    {
        string input = "2:2";

        var result = IsValidTime(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidTime_NotADateTime_ReturnsFalse()
    {
        string input = "This is not a datetime";

        var result = IsValidTime(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidTime_Empty_ReturnsTrue()
    {
        string input = string.Empty;

        var result = IsValidTime(input);

        Assert.IsFalse(result);
    }
}