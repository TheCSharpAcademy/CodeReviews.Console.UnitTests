using CodingTracker.Utilities;

namespace CodingTracker.Tests;

[TestClass]
public class NumberValidationTests
{
    [TestMethod]
    public void IsValidNumber_ValidNumber_ReturnsTrue()
    {
        // Arrange
        string value = "123";

        // Act
        bool result = Validation.IsValidNumber(value);

        // Assert
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void IsValidNumber_InvalidNumber_ReturnsFalse()
    {
        // Arrange
        string value = "invalid";

        // Act
        bool result = Validation.IsValidNumber(value);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidNumber_EmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "";

        // Act
        bool result = Validation.IsValidNumber(value);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidNumber_NullValue_ReturnsFalse()
    {
        // Arrange
        string value = null;

        // Act
        bool result = Validation.IsValidNumber(value);

        // Assert
        Assert.AreEqual(false, result);
    }
}
