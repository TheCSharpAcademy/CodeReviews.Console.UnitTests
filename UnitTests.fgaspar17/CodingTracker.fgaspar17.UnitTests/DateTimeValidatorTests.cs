namespace CodingTracker.fgaspar17.UnitTests;

[TestClass]
public class DateTimeValidatorTests
{
    private readonly DateTimeValidator _dateTimeValidator = new DateTimeValidator();
    [TestMethod]
    public void DateTimeValidatorValidate_ValidFormat_ReturnsSuccess()
    {
        // Arrange, if you need to instantiate a class

        // Act
        var result = _dateTimeValidator.Validate("1987-09-27 12:35");

        // Assert
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    [DataRow("November 2, 2021")]
    [DataRow("2021/11/02")]
    [DataRow("11/02/2021")]
    [DataRow("2021-11-02")]
    [DataRow("02.11.2021")]
    public void DateTimeValidatorValidate_InvalidFormat_ReturnsError(string date)
    {
        // Act
        var result = _dateTimeValidator.Validate(date);
  
        // Assert
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("11987-09-27 12:35")]
    [DataRow("1987-13-27 12:35")]
    [DataRow("1987-00-27 12:35")]
    [DataRow("1987-09-34 12:35")]
    [DataRow("1987-09-00 12:35")]
    [DataRow("1987-09-00 25:35")]
    [DataRow("1987-09-00 12:72")]
    public void DateTimeValidatorValidate_InvalidDates_ReturnsError(string date)
    {
        // Act
        var result = _dateTimeValidator.Validate(date);

        // Assert
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    [DataRow("2021-09-27 12:35")]
    public void DateTimeValidatorFutureValidate_ValidEndDate_ReturnsSuccess(string endDate)
    {
        // Arrange
        DateTime startDate = new (2021, 09, 27, 12, 34, 0);

        // Act
        var result = _dateTimeValidator.FutureValidate(endDate, startDate);

        // Assert
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    [DataRow("2021-09-27 12:35")]
    public void DateTimeValidatorFutureValidate_EndLowerThanStarts_ReturnsError(string endDate)
    {
        // Arrange
        DateTime startDate = new(2021, 09, 27, 12, 36, 0);

        // Act
        var result = _dateTimeValidator.FutureValidate(endDate, startDate);

        // Assert
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    [DataRow(null)]
    public void DateTimeValidatorFutureValidate_NullValues_ReturnsError(string endDate)
    {
        // Arrange
        DateTime startDate = new();

        // Act
        var result = _dateTimeValidator.FutureValidate(endDate, startDate);

        // Assert
        Assert.IsFalse(result.Successful);
    }
}