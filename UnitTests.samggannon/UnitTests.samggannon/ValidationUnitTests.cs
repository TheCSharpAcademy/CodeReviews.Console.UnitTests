using CodingTracker.SamGannon.Utility;

namespace UnitTests.samggannon;

[TestClass]
public class ValidationUnitTests
{
    [TestMethod]
    [DataRow("A", false)]
    [DataRow("-1", false)]
    [DataRow("24:00", false)]
    [DataRow("13:00", true)]
    [DataRow("01:00", true)]
    public void IsValid24HourFormat_IsValidFormat_ReturnTrue(string hour, bool expected)
    {
        // Arrange
        var validation = new Validation();

        // Act
        var result = validation.IsValid24HourFormat(hour);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow("A", false)]
    [DataRow("-1", false)]
    [DataRow("24:00", false)]
    [DataRow("13:00", true)]
    [DataRow("01:00", true)]
    public void ValidateIdInput_IsLongI(string input, bool expected)
    {
        var validation = new Validation();

        int result = validation.ValidateIdInput(input);

        Assert.AreEqual(input, result);
    }
}