namespace CodingTracker.StevieTV.UnitTests;

[TestClass]
public class Validations_IsValidDateFormat
{
    [TestMethod]
    public void IsValidDateFormat_EmptyString_ShouldReturnFalse()
    {
        var result = Validations.IsValidDateFormat("");
        Assert.IsFalse(result, "Empty String should return false");
    }

    [TestMethod]
    [DataRow("01-01-24")]
    [DataRow("31-01-01")]
    [DataRow("31-12-23")]
    [DataRow("15-08-23")]
    public void IsValidDateFormat_ValidDatesAfter2000_ShouldReturnTrue(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsTrue(result, "Valid Date strings after 2000 should be accepted");
    }    
    
    [TestMethod]
    [DataRow("01-01-99")]
    [DataRow("31-01-84")]
    [DataRow("31-12-56")]
    [DataRow("15-08-77")]
    public void IsValidDateFormat_ValidTimesBefore2000_ShouldReturnTrue(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsTrue(result, "Valid Time strings before 2000 should be accepted");
    }
    
    [TestMethod]
    [DataRow("44-01-99")]
    [DataRow("39-01-84")]
    [DataRow("00-12-56")]
    public void IsValidDateFormat_InvalidDaysOver31_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsFalse(result, "Days greater than 31");
    }
    
    [TestMethod]
    [DataRow("29-02-23")]
    public void IsValidDateFormat_29FebruaryNonLeapYear_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsFalse(result, "29 February in non leap year should be false");
    }
    
    [TestMethod]
    [DataRow("29-02-24")]
    public void IsValidDateFormat_29FebruaryLeapYear_ShouldReturnTrue(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsTrue(result, "29 February in Leap Year should be true");
    }

    
    [TestMethod]
    [DataRow("01-13-99")]
    [DataRow("31-14-84")]
    [DataRow("28-15-56")]
    public void IsValidDateFormat_InvalidMonthsOver12_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsFalse(result, "Days greater than 31");
    }

    [TestMethod]
    [DataRow("HelloWorld")]
    [DataRow("This Test Should Fail")]
    public void IsValidDateFormat_NotDateFormat_ShouldReturnFalse(string input)
    {
        var result = Validations.IsValidDateFormat(input);
        Assert.IsFalse(result, "Strings in incorrect format should be rejected");
    }  
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    [DataRow(1300)]
    [DataRow(true)]
    public void IsValidDateFormat_InvalidDataTypes_ShouldThrowException(string input)
    {
        var result = Validations.IsValidDateFormat(input);
    }  
}