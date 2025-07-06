using CodingTracker.Controllers;

namespace UnitTests;

[TestClass]
public sealed class CodingTrackerIsValidatorWork
{
    [TestMethod]
    [DataRow("02-02-2002 12:20")]
    [DataRow("07-06-2025 11:55")]
    public void IsDateValid_ReturnsTrue(string dateToValidate)
    {
        bool result = InputValidator.IsDateValid(dateToValidate);
        Assert.IsTrue(result, $"{dateToValidate} is valid");
    }

    [TestMethod]
    [DataRow("02/02/2002 12:20")]
    [DataRow("07/06/2025 11-55")]
    public void IsDateValid_InvalidValuesFormat_ReturnsFalse(string dateToValidate)
    {
        bool result = InputValidator.IsDateValid(dateToValidate);
        Assert.IsFalse(result, $"{dateToValidate} is invalid");
    }

    [TestMethod]
    [DataRow("1000")]
    [DataRow("12")]
    public void IsDataValid_NoDateFormat_ReturnsFalse(string dateToValidate)
    {
        bool result = InputValidator.IsDateValid(dateToValidate);
        Assert.IsFalse(result, $"{dateToValidate} is invalid");
    }
}