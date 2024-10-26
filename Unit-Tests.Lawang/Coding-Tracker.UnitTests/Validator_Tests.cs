using System.Data;
using Lawang.Coding_Tracker;

namespace Coding_Tracker.UnitTests;

[TestClass]
public class Validator_Tests
{
    [TestMethod]
    public void ValidateUserTime_Input0_ThrowsExitOutOfOperationException()
    {
        var invalidTime = "0";
        Assert.ThrowsException<ExitOutOfOperationException>(() => Validator.ValidateUserTime(invalidTime));
    }

    [TestMethod]
    [DataRow("12:00")]
    [DataRow("01:00")]
    [DataRow("14:00 pm")]
    [DataRow("13:00 am")]
    public void ValidateUserTime_InvalidTimeFormat_ReturnFalse(string invalidTime)
    {
        bool result = Validator.ValidateUserTime(invalidTime);
        Assert.IsFalse(result, $"{result} is an invalid time format.");
    }

    [TestMethod]
    [DataRow("12:00 pm")]
    [DataRow("01:00 pm")]
    [DataRow("06:30 am")]
    [DataRow("1:00 pm")]
    public void ValidateUserTime_ValidTimeFormat_ReturnTrue(string validTime)
    {
        bool result = Validator.ValidateUserTime(validTime);
        Assert.IsTrue(result, $"{result} is a valid time format");
    }

    // ValidateTimeDuration

    [TestMethod]
    public void ValidateTimeDuration_Input0_ThrowsExitOutOfOperationException()
    {
        var invalid = "0";
        Assert.ThrowsException<ExitOutOfOperationException>(() => Validator.ValidateTimeDuration(invalid));
    }

    [TestMethod]
    [DataRow("1:0.0:00")]
    [DataRow("1:00:00:00")]
    [DataRow("12:00:00:00")]
    public void ValidateTimeDuration_InvalidTime_ReturnFalse(string input)
    {
        bool result = Validator.ValidateTimeDuration(input);
        Assert.IsFalse(result, $"{input} is invalid format");
    }

    [TestMethod]
    [DataRow("1.12:00")]
    [DataRow("13:00:00")]
    [DataRow("2.15:00:00")]
    public void ValidateTimeDuration_ValidTime_ReturnTrue(string input)
    {
        bool result = Validator.ValidateTimeDuration(input);
        Assert.IsTrue(result, $"{input} is valid format");
    }

    [TestMethod]
    public void ValidateDate_Input0_ThrowsExitOutOfOperationException()
    {
        var invalid = "0";
        Assert.ThrowsException<ExitOutOfOperationException>(() => Validator.ValidateDate(invalid));
    }

    [TestMethod]
    [DataRow("10-04-24")]
    [DataRow("13 November 2024")]
    [DataRow("2024/12/11")]
    public void ValidateDate_InvalidDate_ReturnFalse(string input)
    {
        bool result = Validator.ValidateDate(input);
        Assert.IsFalse(result, $"{input} is an invalid format.");
    }

    [TestMethod]
    [DataRow("10/12/2024")]
    [DataRow("01/01/2001")]
    [DataRow("12/01/2001")]
    public void ValidateDate_ValidDate_ReturnTrue(string input)
    {
        //Valid format = dd/mm/yyyy
        bool result = Validator.ValidateDate(input);
        Assert.IsTrue(result, $"{input} is valid date format");
    }
    
    
}