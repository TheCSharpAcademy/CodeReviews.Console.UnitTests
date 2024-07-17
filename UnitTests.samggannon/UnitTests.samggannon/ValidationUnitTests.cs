using CodingTracker.SamGannon.Utility;

namespace UnitTests.samggannon;

[TestClass]
public class ValidationUnitTests
{
    [TestMethod]
    [DataRow("12:00", true)]
    [DataRow("01:00", true)]
    public void TestIsValidTimeFormat_ValidFormat_ReturnsTrue(string mockInput, bool expectedResult)
    {
        // Arrange
        var validation = new Validation();

        // Act
        bool result = validation.IsValidTimeFormat(mockInput);

        // Assert
        Assert.AreEqual(result, expectedResult);
    }

    [TestMethod]
    [DataRow("1:00", false)]
    [DataRow("string input", false)]
    public void TestIsValidTimeFormat_InvalidFormat_ReturnsFalse(string mockInput, bool expectedResult)
    {
        // Arrange
        var validation = new Validation();

        // Act
        bool result = validation.IsValidTimeFormat(mockInput);

        // Assert
        Assert.AreEqual(result, expectedResult);
    }
}