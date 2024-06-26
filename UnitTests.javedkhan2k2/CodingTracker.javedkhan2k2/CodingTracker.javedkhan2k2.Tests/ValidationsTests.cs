using static CodingTracker.Validation;
namespace CodingTracker.UnitTests;

[TestClass]
public class ValidationTests
{

    #region IsValidDateInput_Tests

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("2024")]
    [DataRow("2024-01")]
    [DataRow("2024-01-01")]
    [DataRow("2024-01-01 13:")]
    [DataRow("2024-01-01 13:00")]
    [DataRow("2024-01-01 13:00:0")]
    [DataRow("2024-01-01 24:00:00")]
    [DataRow("2024-01-01 13:00:60")]
    [DataRow("123")]
    public void IsValidDateInput_InvalidDateInputs_ReturnsFalse(string inputDate)
    {
        bool result = IsValidDateInput(inputDate, "yyyy-MM-dd HH:mm:ss");

        Assert.IsFalse(result);
    }
    [TestMethod]
    [DataRow("0")]
    [DataRow("2024-01-01 13:00:00")]
    [DataRow("2024-01-01 23:59:59")]
    [DataRow("2024-01-01 00:00:00")]
    public void IsValidDateInput_ValidDateInputs_ReturnsTrue(string inputDate)
    {
        bool result = IsValidDateInput(inputDate, "yyyy-MM-dd HH:mm:ss");

        Assert.IsTrue(result);
    }

    #endregion

    #region IsValidDateTimeInputs_Tests

    [TestMethod]
    [DataRow("", "")]
    [DataRow("2024-01-01 13:00:00", "")]
    [DataRow("", "2024-01-01 13:00:00")]
    [DataRow(null, null)]
    [DataRow(null, "2024-01-01 13:00:00")]
    [DataRow("2024-01-01 13:00:00", null)]
    [DataRow("2024-01-01 14:00:00", "2024-01-01 14:00:00")]
    [DataRow("2024-01-01 14:00:00", "2024-01-01 13:00:00")]
    [DataRow("2024-01-01-12:00:00", "2024-01-01 13:00:00")]
    [DataRow("2024-01-01", "2024-01-02")]
    [DataRow("2024-01-01-12:00:00", "2024-01-01-13:00:00")]
    [DataRow("2024-01-01 12:00:00", "2024-01-01-13:00:00")]
    public void IsValidDateTimeInputs_InvalidDatesInput_ReutrnsFalse(string startDate, string endDate)
    {
        bool result = IsValidDateTimeInputs(startDate, endDate, "yyyy-MM-dd HH:mm:ss");

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2024-01-01 12:00:00", "2024-01-01 13:00:00")]
    [DataRow("2024-01-01 12:00:00", "2024-01-01 12:00:01")]
    public void IsValidDateTimeInputs_ValidDatesInput_ReutrnsTrue(string startDate, string endDate)
    {
        bool result = IsValidDateTimeInputs(startDate, endDate, "yyyy-MM-dd HH:mm:ss");

        Assert.IsTrue(result);
    }

    #endregion

    #region AreValidYearInputs_Tests

    [TestMethod]
    [DataRow(null, null)]
    [DataRow(null, "2024")]
    [DataRow("2024", null)]
    [DataRow("0", "0")]
    [DataRow(null, "0")]
    [DataRow("0", null)]
    [DataRow("0", "2024")]
    [DataRow("2024", "0")]
    [DataRow("", "")]
    [DataRow("", "0")]
    [DataRow("0", "")]
    [DataRow("", "2024")]
    [DataRow("2024", "")]
    [DataRow("2025", "2024")]
    [DataRow("2020", "2010")]
    [DataRow("-2020", "2025")]
    [DataRow("2010", "-2020")]
    public void AreValidYearInputs_InvalidYearInputs_ReturnFalse(string startYear, string endYear)
    {
        bool result = AreValidYearInputs(startYear, endYear);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2020", "2024")]
    [DataRow("2020", "2020")]
    [DataRow("2020", "2021")]
    public void AreValidYearInputs_ValidYearInputs_ReturnTrue(string startYear, string endYear)
    {
        bool result = AreValidYearInputs(startYear, endYear);

        Assert.IsTrue(result);
    }

    #endregion

    #region AreDatesEmptyOrZeroTests

    [TestMethod]
    [DataRow(null, null)]
    [DataRow(null, "2024-01-01 13:00:00")]
    [DataRow("2024-01-01 13:00:00", null)]
    [DataRow("0", "0")]
    [DataRow(null, "0")]
    [DataRow("0", null)]
    [DataRow("0", "2024-01-01 13:00:00")]
    [DataRow("2024-01-01 13:00:00", "0")]
    [DataRow("", "")]
    [DataRow("", "0")]
    [DataRow("0", "")]
    [DataRow("", "2024-01-01 13:00:00")]
    [DataRow("2024-01-01 13:00:00", "")]
    public void AreDatesEmptyOrZero_NullOrZeroInputs_ReturnTrue(string input1, string input2)
    {
        bool result = AreDatesEmptyOrZero(input1, input2);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2024-01-01 13:00:00", "2024-01-01 14:00:00")]
    [DataRow("I am also Not Empty", "NotEmpty")]
    public void AreDatesEmptyOrZero_NonEmptyInputs_ReturnFalse(string input1, string input2)
    {
        bool result = AreDatesEmptyOrZero(input1, input2);

        Assert.IsFalse(result);
    }


    #endregion

    #region IsValidIntegerInput_Tests

    [TestMethod]
    public void IsValidIntegerInput_NullUserInput_ReturnsFalse()
    {
        // Arrange
        string? input = null;

        // Act
        bool result = IsValidIntegerInput(input);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("Abc")]
    [DataRow("123Abc")]
    [DataRow("A123bc")]
    [DataRow("-1")]
    [DataRow("-123")]
    [DataRow("-4560")]
    [DataRow("-2147483648")]
    public void IsValidIntegerInput_InvalidUserInput_ReturnsFalse(string input)
    {
        bool result = IsValidIntegerInput(input);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("1")]
    [DataRow("123")]
    [DataRow("4560")]
    [DataRow("2147483647")]
    public void IsValidIntegerInput_ValidPositiveIntegerString_ReturnsTrue(string input)
    {
        bool result = IsValidIntegerInput(input);

        Assert.IsTrue(result);
    }

    #endregion

    #region IsPostiveIntegerInput_Tests

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(123)]
    [DataRow(42344)]
    [DataRow(2147483647)]
    public void IsPostiveIntegerInput_PositiveIntegerInput_ReturnsTrue(int input)
    {
        bool result = IsPostiveIntegerInput(input);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-123)]
    [DataRow(-42344)]
    [DataRow(-2147483648)]
    public void IsPostiveIntegerInput_NegativeIntegerInput_ReturnsTrue(int input)
    {
        bool result = IsPostiveIntegerInput(input);

        Assert.IsFalse(result);
    }

    #endregion

}
