using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTracker;

namespace Tests;

[TestClass]
public class DataValidationTests()
{
    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")]
    [DataRow("1.")]
    [DataRow("1,223")]
    [DataRow("2147483648")]
    [DataRow("-2147483649")]

    public void ValidateInteger_InputIsEmptyNullOrInvalid_ReturnsFalse(string input)
    {
        var result = DataValidation.ValidateInteger(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("100")]
    [DataRow("-200")]
    [DataRow("2147483647")]
    [DataRow("-2147483648")]
    public void ValidateInteger_InputIsInteger_ReturnsTrue(string? input)
    {
        var result = DataValidation.ValidateInteger(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("", -5, 10)]
    [DataRow(null, -5, 10)]
    [DataRow("Text", -5, 10)]
    [DataRow("1.", 0, 5)]
    [DataRow("1,223", 0, 5)]
    [DataRow("20", 0, 5)]
    [DataRow("-10", 0, 5)]
    public void ValidateInteger2_InputIsEmptyNullOrInvalid_ReturnsFalse(string? input, int min, int max)
    {
        var result = DataValidation.ValidateInteger(input, min, max);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("0", 0, 5)]
    [DataRow("5", 0, 5)]
    [DataRow("0", 0, 0)]
    [DataRow("-10", -15, -5)]

    public void ValidateInteger2_InputIsInteger_ReturnsTrue(string? input, int min, int max)
    {
        var result = DataValidation.ValidateInteger(input, min, max);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")]
    [DataRow("24:00")]
    [DataRow("12:61")]
    [DataRow("-00:00")]
    [DataRow("6:30")]
    [DataRow("06:30 ")]
    [DataRow(" 06:30")]
    public void ValidateTime_InputIsNullOrInvalidFormat_ReturnsFalse(string? input)
    {
        var result = DataValidation.ValidateTime(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("00:00")]
    [DataRow("23:59")]
    [DataRow("12:00")]
    [DataRow("12:59")]
    [DataRow("06:30")]
    public void ValidateTime_InputIsValidFormat_ReturnsTrue(string? input)
    {
        var result = DataValidation.ValidateTime(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("", "", "", "")]
    [DataRow(null, null, null, null)]
    [DataRow("", null, null, "")]
    [DataRow(null, "", "", null)]
    [DataRow("Text", "Text2", "Text3", "Text4")]
    [DataRow("2024-02-02", "12:00", "2024/02/02", "14:00")]
    [DataRow("2024/02/02", "24:00", "2024/02/03", "12:00")]
    [DataRow("2024/02/02", "12:00", "2024/02/2", "12:00")]
    [DataRow("2024/02/02", "12:00", "2024/02/02", "3:00")]
    public void ValidateTime2_InputIsNullOrInvalidFormat_ReturnsFalse(string? startDate, string? startTime, 
        string? endDate, string? endTime)
    {
        var result = DataValidation.ValidateTime(startDate, startTime, endDate, endTime);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024/06/20", "12:00", "2024/06/20", "11:00")]
    [DataRow("2024/06/20", "12:00", "2024/06/19", "13:00")]
    [DataRow("2024/06/20", "12:00", "2024/05/22", "13:00")]
    [DataRow("2024/06/20", "12:00", "2023/08/22", "13:00")]
    public void ValidateTime2_EndTimeIsEarlierThanStartTime_ReturnsFalse(string? startDate, string? startTime, 
        string? endDate, string? endTime)
    {
        var result = DataValidation.ValidateTime(startDate, startTime, endDate, endTime);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024/02/02", "12:00", "2024/02/02", "12:01")]
    [DataRow("2024/02/02", "12:00", "2024/02/02", "14:00")]
    [DataRow("2024/02/02", "12:00", "2024/02/04", "06:00")]
    [DataRow("2024/02/02", "12:00", "2024/03/02", "03:00")]
    [DataRow("2024/02/02", "12:00", "2025/01/01", "00:00")]
    public void ValidateTime2_EndTimeIsLaterThanStartTime_ReturnsTrue(string? startDate, string? startTime,
        string? endDate, string? endTime)
    {
        var result = DataValidation.ValidateTime(startDate, startTime, endDate, endTime);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")] 
    [DataRow("2024/02/30")]
    [DataRow("2024/13/05")]
    [DataRow("24/10/05")]
    [DataRow("2024-11-05")]
    public void ValidateDate_InputIsNullOrInvalidFormat_ReturnsFalse(string? input)
    {
        var result = DataValidation.ValidateDate(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024/01/01")]
    [DataRow("2024/12/31")]
    [DataRow("2024/05/22")]
    public void ValidateDate_IsValidFormat_ReturnsTrue(string? input)
    {
        var result = DataValidation.ValidateDate(input);
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, null)]
    [DataRow("Text", "Text2")]
    [DataRow("2024/02/02", "2024/13/05")]
    [DataRow("2024/02/30", "2024/02/05")]
    [DataRow("2024/02/30", "2024/02/5")]
    [DataRow("2024/02/30", "2024/2/05")]
    [DataRow("2024/02/30", "2024-02-05")]
    [DataRow("2024/02/30", "24/02/05")]
    public void ValidateDate2_InputIsNullOrInvalidFormat_ReturnsFalse(string? startDate, string? endDate)
    {
        var result = DataValidation.ValidateDate(startDate, endDate);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024/01/02", "2024/01/01")]
    [DataRow("2024/03/15", "2024/02/15")]
    [DataRow("2025/03/22", "2024/03/22")]
    public void ValidateDate2_EndDateIsEarlierThanStartDate_ReturnsFalse(string? startDate, string? endDate)
    {
        var result = DataValidation.ValidateDate(startDate, endDate);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024/01/01", "2024/01/01")]
    [DataRow("2024/01/01", "2024/01/15")]
    [DataRow("2024/02/11", "2024/03/11")]
    [DataRow("2024/03/11", "2025/03/11")]
    public void ValidateDate2_InputIsValidFormat_ReturnsTrue(string? startDate, string? endDate)
    {
        var result = DataValidation.ValidateDate(startDate, endDate);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")]
    [DataRow("yy")]
    [DataRow(" y ")]
    [DataRow("Y")]
    public void ValidateYesNoQuestion_InputIsNullOrInvalidFormat_ReturnsFalse(string? input)
    {
        var result = DataValidation.ValidateYesNoQuestion(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("y")]
    [DataRow("n")]
    public void ValidateYesNoQuestion_InputIsValidAnswer_ReturnsTrue(string? input)
    {
        var result = DataValidation.ValidateYesNoQuestion(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")]
    [DataRow("12:00")]
    [DataRow("1.24:00")]
    [DataRow("1. 19:00")]
    [DataRow("1.22:00 ")]
    [DataRow("-1.01.30")]
    [DataRow("10675200.00:00")]
    public void ValidateTotalHours_InputIsNullOrInvalidFormat_ReturnsFalse(string? input)
    {
        var result = DataValidation.ValidateTotalHours(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("0.06:00")]
    [DataRow("1.00:00")]
    [DataRow("1.12:00")]
    [DataRow("31.12:30")]

    public void ValidateTotalHours_InputIsValidFormat_ReturnsTrue(string? input)
    {
        var result = DataValidation.ValidateTotalHours(input);
        Assert.IsTrue(result);
    }
}