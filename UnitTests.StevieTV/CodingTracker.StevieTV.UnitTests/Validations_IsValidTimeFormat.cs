namespace CodingTracker.StevieTV.UnitTests;

[TestClass]
public class Validations_IsValidTimeFormat
{
    [TestMethod]
    public void IsValidTimeFormat_EmptyString_ShouldReturnFalse()
    {
        var result = Validations.IsValidTimeFormat("");
        Assert.IsFalse(result, "Empty String should return false");
    }

    [TestMethod]
    [DataRow("00:00")]
    [DataRow("01:30")]
    [DataRow("11:15")]
    [DataRow("12:00")]
    public void IsValidTimeFormat_ValidTimesBeforeNoon_ShouldReturnTrue(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
        Assert.IsTrue(result, "Valid Time strings should be accepted");
    }    
    
    [TestMethod]
    [DataRow("13:15")]
    [DataRow("17:55")]
    [DataRow("19:16")]
    public void IsValidTimeFormat_ValidTimesAfterNoon24HourClock_ShouldReturnTrue(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
        Assert.IsTrue(result, "Valid Time strings after noon in 24 hour clock should be accepted");
    }
    
    [TestMethod]
    [DataRow("24:00")]
    [DataRow("24:01")]
    public void IsValidTimeFormat_ValidTimesGreaterThan24HourClock_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
        Assert.IsFalse(result, "Times after 24:00 should be rejected");
    }
    
    [TestMethod]
    [DataRow("17:69")]
    [DataRow("19:99")]
    public void IsValidTimeFormat_InvalidTimes_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
        Assert.IsFalse(result, "Strings in right format but not valid time should be rejected");
    }  

    [TestMethod]
    [DataRow("HelloWorld")]
    [DataRow("This Test Should Fail")]
    public void IsValidTimeFormat_NotTimeFormat_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
        Assert.IsFalse(result, "Strings in incorrect format should be rejected");
    }  
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    [DataRow(1300)]
    [DataRow(true)]
    public void IsValidTimeFormat_InvalidDataTypes_ShouldThrowException(string input)
    {
        var result = Validations.IsValidTimeFormat(input);
    }  

}