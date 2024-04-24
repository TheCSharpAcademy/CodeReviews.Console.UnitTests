namespace CodingTracker.Dejmenek.Tests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(5)]
    public void Should_IsPositive_ReturnSuccess_ForPositiveNumber(int positiveNumber)
    {
        var result = Validation.IsPositiveNumber(positiveNumber);

        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-4)]
    [DataRow(0)]
    public void Should_IsPositive_ReturnError_ForNegativeNumber(int negativeNumber)
    {
        var result = Validation.IsPositiveNumber(negativeNumber);

        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    [DataRow("2024-04-07 12:20", "2024-04-08 13:30")]
    [DataRow("2024-04-08 10:00", "2024-04-08 11:00")]
    [DataRow("2021-04-08 10:01", "2021-04-08 10:05")]
    public void Should_IsChronologicalOrder_ReturnTrue_ForChronologicalOrder(string startDateStr, string endDateStr)
    {
        var startDateTime = DateTime.Parse(startDateStr);
        var endDateTime = DateTime.Parse(endDateStr);

        var result = Validation.IsChronologicalOrder(startDateTime, endDateTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2024-04-08 13:30", "2024-04-07 12:20")]
    [DataRow("2024-04-08 11:00", "2024-04-08 10:00")]
    [DataRow("2021-04-08 10:05", "2021-04-08 10:01")]
    public void Should_IsChronologicalOrder_ReturnFalse_ForNonChronologicalOrder(string startDateStr, string endDateStr)
    {
        var startDateTime = DateTime.Parse(startDateStr);
        var endDateTime = DateTime.Parse(endDateStr);

        var result = Validation.IsChronologicalOrder(startDateTime, endDateTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024-04-08 13:30")]
    [DataRow("2024-04-08 11:00")]
    [DataRow("2021-04-08 10:05")]
    public void Should_IsValidDateTimeFormat_ReturnSuccess_ForValidDateTimeFormat(string validDateTime)
    {
        var result = Validation.IsValidDateTimeFormat(validDateTime);

        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    [DataRow("2024:04:08")]
    [DataRow("2024-04-08 11:00:00")]
    [DataRow("2021-04-08 10:05:000:000")]
    public void Should_IsValidDateTimeFormat_ReturnError_ForInvalidDateTimeFormat(string invalidDateTime)
    {
        var result = Validation.IsValidDateTimeFormat(invalidDateTime);

        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    [DataRow("2024-04-08")]
    [DataRow("2024-04-09")]
    [DataRow("2022-05-18")]
    public void Should_IsValidDateFormat_ReturnSuccess_ForValidDateFormat(string validDate)
    {
        var result = Validation.IsValidDateFormat(validDate);

        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    [DataRow("2024-04-08 12:00")]
    [DataRow("2024-32-11")]
    [DataRow("0000-031-18")]
    public void Should_IsValidDateFormat_ReturnError_ForInvalidDateFormat(string invalidDate)
    {
        var result = Validation.IsValidDateFormat(invalidDate);

        Assert.IsFalse(result.Successful);
    }
}