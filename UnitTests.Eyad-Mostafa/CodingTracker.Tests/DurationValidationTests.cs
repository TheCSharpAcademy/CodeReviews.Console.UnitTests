using CodingTracker.Utilities;

namespace CodingTracker.Tests;

[TestClass]
public class DurationValidationTests
{
    [TestMethod]
    public void IsValidDuration_DurationGreaterThanZero_ReturnsTrue()
    {
        // Arrange
        int duration = 1;

        // Act
        bool result = Validation.IsValidDuration(duration);

        // Assert
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void IsValidDuration_DurationLessThanZero_ReturnsFalse()
    {
        // Arrange
        int duration = -1;

        // Act
        bool result = Validation.IsValidDuration(duration);

        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void IsValidDuration_DurationEqualToZero_ReturnsFalse()
    {
        // Arrange
        int duration = 0;

        // Act
        bool result = Validation.IsValidDuration(duration);

        // Assert
        Assert.AreEqual(false, result);
    }
}