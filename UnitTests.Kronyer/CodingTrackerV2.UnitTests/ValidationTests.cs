using CodingTrackerV2.Helpers;

namespace CodingTrackerV2.UnitTests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    public void IsValidInt_InputValidNumber_ReturnsTrue()
    {
        //arrange
        string input = "5";

        //act
        bool result = Validation.IsValidInt(input);

        //assert
        Assert.AreEqual(true, result);
    }

    public void IsValidInt_InputInvalidNumber_ReturnsFalse()
    {
        //arrange
        string input = "x";

        //act
        bool result = Validation.IsValidInt(input);

        //assert
        Assert.AreEqual(false, result);
    }

    public void IsPositiveInt_InputPositiveNumber_ReturnsTrue()
    {
        string input = "3";

        bool result = Validation.IsPositiveInt(input);

        Assert.AreEqual(true, result);
    }

    public void IsPositiveInt_InputNegativeNumber_ReturnsFalse()
    {
        string input = "-3";

        bool result = Validation.IsPositiveInt(input);

        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void ValidateStartDate_ValidDate_ReturnsDateTime()
    {
        //arrange
        string input = "01-01-01 00:00";

        //act
        DateTime result = Validation.ValidateStartDate(input);

        //assert
        Assert.AreEqual(new DateTime(2001, 1, 1, 0, 0, 0), result);
    }

    [TestMethod]
    public void ValidateEndDate_ValidDate_ReturnsDateTime()
    {
        string input = "01-01-01 00:00";
        DateTime dateStart = new DateTime(2001, 1, 1, 0, 0, 0);

        DateTime result = Validation.ValidateEndDate(dateStart, input);

        Assert.AreEqual(new DateTime(2001, 1, 1, 0, 0, 0), result);
    }
}