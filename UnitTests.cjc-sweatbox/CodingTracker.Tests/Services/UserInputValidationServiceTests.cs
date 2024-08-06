using System.Globalization;
using CodingTracker.Services;

namespace CodingTracker.Tests.Services;

[TestClass]
public class UserInputValidationServiceTests
{
    [TestMethod]
    [DataRow(1.0)]
    [DataRow(10.0)]
    [DataRow(100.5)]
    public void IsValidCodingGoalDuration_ShouldReturnTrue_WhenPositive(double test)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidCodingGoalDuration(test);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void IsValidCodingGoalDuration_ShouldReturnTrue_WhenZero()
    {
        // Arrange.
        double test = 0.0;

        // Act.
        var result = UserInputValidationService.IsValidCodingGoalDuration(test);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow(-1.0)]
    [DataRow(-10.0)]
    [DataRow(-100.5)]
    public void IsValidCodingGoalDuration_ShouldReturnFalse_WhenNegative(double test)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidCodingGoalDuration(test);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("2024-01-01", "yyyy-MM-dd")]
    [DataRow("2024-01-01 01:01", "yyyy-MM-dd HH:mm")]
    [DataRow("2024", "yyyy")]
    [DataRow("2024-01", "yyyy-MM")]
    public void IsValidReportStartDate_ShouldReturnTrue_WhenDateAsStringMatchesFormat(string test, string format)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidReportStartDate(test, format);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void IsValidReportStartDate_ShouldReturnFalse_WhenDateAsStringIsNull()
    {
        // Arrange.
        string? test = null;
        string format = "";

        // Act.
        var result = UserInputValidationService.IsValidReportStartDate(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("\t")]
    [DataRow("\n")]
    public void IsValidReportStartDate_ShouldReturnFalse_WhenDateAsStringIsWhitespace(string test)
    {
        // Arrange.
        string format = "";

        // Act.
        var result = UserInputValidationService.IsValidReportStartDate(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("01-01-2024", "yyyy-MM-dd")]
    [DataRow("2024/01/01", "yyyy-MM-dd")]
    [DataRow("Bob", "yyyy-MM-dd")]
    [DataRow(";#'[*()", "yyyy-MM-dd")]
    public void IsValidReportStartDate_ShouldReturnFalse_WhenDateAsStringDoesNotMatchFormat(string test, string format)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidReportStartDate(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("2024-01-01", "2024-01-01", "yyyy-MM-dd")]
    [DataRow("2024-01-01 01:01", "2024-01-01 01:01", "yyyy-MM-dd HH:mm")]
    [DataRow("2024", "2024", "yyyy")]
    [DataRow("2024-01", "2024-01", "yyyy-MM")]
    [DataRow("2024-01-01", "2024-01-02", "yyyy-MM-dd")]
    [DataRow("2024-01-01 01:01", "2024-01-01 02:02", "yyyy-MM-dd HH:mm")]
    [DataRow("2024", "2025", "yyyy")]
    [DataRow("2024-01", "2024-02", "yyyy-MM")]

    public void IsValidReportEndDate_ShouldReturnTrue_WhenDateAsStringMatchesFormatAndIsEqualToOrAfterStartDate(string startDateString, string test, string format)
    {
        // Arrange.
        DateTime startDate = DateTime.ParseExact(startDateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // Act.
        var result = UserInputValidationService.IsValidReportEndDate(test, format, startDate);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow("2024-01-01", "2023-12-31", "yyyy-MM-dd")]
    [DataRow("2024-02-02 02:02", "2024-01-01 00:00", "yyyy-MM-dd HH:mm")]
    [DataRow("2024", "2023", "yyyy")]
    [DataRow("2024-01", "2023-12", "yyyy-MM")]
    public void IsValidReportEndDate_ShouldReturnFalse_WhenEndDateIsBeforeStartDate(string startDateString, string test, string format)
    {
        // Arrange.
        DateTime startDate = DateTime.ParseExact(startDateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // Act.
        var result = UserInputValidationService.IsValidReportEndDate(test, format, startDate);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void IsValidReportEndDate_ShouldReturnFalse_WhenDateAsStringIsNull()
    {
        // Arrange.
        string? test = null;
        string format = "";
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidReportEndDate(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("\t")]
    [DataRow("\n")]
    public void IsValidReportEndDate_ShouldReturnFalse_WhenDateAsStringIsWhitespace(string test)
    {
        // Arrange.
        string format = "";
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidReportEndDate(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("01-01-2024", "yyyy-MM-dd")]
    [DataRow("2024/01/01", "yyyy-MM-dd")]
    [DataRow("Bob", "yyyy-MM-dd")]
    [DataRow(";#'[*()", "yyyy-MM-dd")]
    public void IsValidReportEndDate_ShouldReturnFalse_WhenDateAsStringDoesNotMatchFormat(string test, string format)
    {
        // Arrange.
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidReportEndDate(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("2023-01-01 00:00", "yyyy-MM-dd HH:mm")]
    [DataRow("2023-08-18 22:19", "yyyy-MM-dd HH:mm")]
    [DataRow("2023-06-05 12:34", "yyyy-MM-dd HH:mm")]
    [DataRow("2022-03-03 13:13", "yyyy-MM-dd HH:mm")]
    public void IsValidCodingSessionStartDateTime_ShouldReturnTrue_WhenDateAsStringMatchesFormat(string test, string format)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidCodingSessionStartDateTime(test, format);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void IsValidCodingSessionStartDateTime_ShouldReturnFalse_WhenDateAsStringIsNull()
    {
        // Arrange.
        string? test = null;
        string format = "";

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionStartDateTime(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("\t")]
    [DataRow("\n")]
    public void IsValidCodingSessionStartDateTime_ShouldReturnFalse_WhenDateAsStringIsWhitespace(string test)
    {
        // Arrange.
        string format = "";

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionStartDateTime(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("01-01-2024", "yyyy-MM-dd")]
    [DataRow("2024/01/01", "yyyy-MM-dd")]
    [DataRow("Bob", "yyyy-MM-dd")]
    [DataRow(";#'[*()", "yyyy-MM-dd")]
    public void IsValidCodingSessionStartDateTime_ShouldReturnFalse_WhenDateAsStringDoesNotMatchFormat(string test, string format)
    {
        // Arrange.
        // Act.
        var result = UserInputValidationService.IsValidCodingSessionStartDateTime(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);

    }

    [TestMethod]
    public void IsValidCodingSessionStartDateTime_ShouldReturnFalse_WhenStartDateIsInTheFuture()
    {
        // Arrange.
        string format = "yyyy-MM-dd HH:mm";
        string test = DateTime.Now.AddHours(10).ToString(format);

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionStartDateTime(test, format);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("2024-01-01 02:02", "2024-01-01 03:03", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-02-11 12:12", "2024-02-11 12:13", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-07-13 18:59", "2024-07-14 01:01", "yyyy-MM-dd HH:mm")]
    [DataRow("2023-01-01 00:00", "2024-01-01 00:00", "yyyy-MM-dd HH:mm")]
    public void IsValidCodingSessionEndDateTime_ShouldReturnTrue_WhenDateAsStringMatchesFormatAndIsAfterStartDateTime(string startDateTimeString, string test, string format)
    {
        // Arrange.
        DateTime startDate = DateTime.ParseExact(startDateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, startDate);

        // Assert.
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow("2024-01-01 02:02", "2024-01-01 02:02", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-02-11 12:12", "2024-02-11 12:12", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-07-13 18:59", "2024-07-13 18:59", "yyyy-MM-dd HH:mm")]
    [DataRow("2023-01-01 00:00", "2023-01-01 00:00", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-01-01 03:03", "2024-01-01 02:02", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-02-11 12:13", "2024-02-11 12:12", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-07-14 01:01", "2024-07-13 18:59", "yyyy-MM-dd HH:mm")]
    [DataRow("2024-01-01 00:00", "2023-01-01 00:00", "yyyy-MM-dd HH:mm")]
    public void IsValidCodingSessionEndDateTime_ShouldReturnFalse_WhenEndDateIsEqualToOrBeforeStartDateTime(string startDateTimeString, string test, string format)
    {
        // Arrange.
        DateTime startDate = DateTime.ParseExact(startDateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, startDate);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void IsValidCodingSessionEndDateTime_ShouldReturnFalse_WhenDateAsStringIsNull()
    {
        // Arrange.
        string? test = null;
        string format = "";
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("\t")]
    [DataRow("\n")]
    public void IsValidCodingSessionEndDateTime_ShouldReturnFalse_WhenDateAsStringIsWhitespace(string test)
    {
        // Arrange.
        string format = "";
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("01-01-2024", "yyyy-MM-dd")]
    [DataRow("2024/01/01", "yyyy-MM-dd")]
    [DataRow("Bob", "yyyy-MM-dd")]
    [DataRow(";#'[*()", "yyyy-MM-dd")]
    public void IsValidCodingSessionEndDateTime_ShouldReturnFalse_WhenDateAsStringDoesNotMatchFormat(string test, string format)
    {
        // Arrange.
        DateTime dateTime = DateTime.Now;

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void IsValidCodingSessionEndDateTime_ShouldReturnFalse_WhenEndDateTimeIsInTheFuture()
    {
        // Arrange.
        string format = "yyyy-MM-dd HH:mm";
        string test = DateTime.Now.AddHours(10).ToString(format);
        DateTime dateTime = DateTime.Now.AddHours(-10);

        // Act.
        var result = UserInputValidationService.IsValidCodingSessionEndDateTime(test, format, dateTime);

        // Assert.
        Assert.IsFalse(result.IsValid);
    }
}