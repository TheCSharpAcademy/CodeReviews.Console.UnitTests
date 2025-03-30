using CodingTracker.Utilities;

namespace CodingTracker.Tests;

[TestClass]
public class DateValidationTests
{
    [TestMethod]
    public void IsValidDate_ValidDateFormat_ReturnsTrue()
    {
        // Arrange
        string date = "01/01/2022";

        // Act
        bool result = Validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void IsValidDate_InvalidDateFormat_ReturnsFalse()
    {
        // Arrange
        string date = "invalid";

        // Act
        bool result = Validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidDate_EmptyString_ReturnsFalse()
    {
        // Arrange
        string date = "";

        // Act
        bool result = Validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidDate_NullValue_ReturnsFalse()
    {
        // Arrange
        string date = null;

        // Act
        bool result = Validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidDate_InvalidDay_ReturnsFalse()
    {
        // Arrange
        string date = "32/01/2022";

        // Act
        bool result = Validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(false, result);
    }
}
