using CodingTrackerV2.Helpers;

namespace CodingTrackerV2.UnitTests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    public void ValidateInt_InputValidNumber_ReturnsNumber()
    {
        //arrange
        string input = "5";
        string message = "Enter a number: ";

        //act
        int result = Validation.ValidateInt(input, message);

        //assert
        Assert.AreEqual(5, result);
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