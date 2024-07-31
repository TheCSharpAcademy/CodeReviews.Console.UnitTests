using CodingTracker.SamGannon.Utility;

namespace UnitTests.samggannon;

[TestClass]
public class ValidationTest
{
    [TestMethod]
    [DataRow("123", false)]
    [DataRow("10 o clock", false)]
    [DataRow("24:00", false)]
    [DataRow("13:00", true)]
    [DataRow("10:00", true)]
    public void IsValidTimeFormat_IsValid_ReturnsTrue(string time, bool expectedResult)
    {
        // Arrange
        var validation = new Validation();

        // Act
        bool actualResult = validation.IsValidTimeFormat(time);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    //[TestMethod]
    //public void IsValidDate_IsValid_ReturnsTrue()
    //{
    //    // Arrange

    //    // Act

    //    // Assert
    //}

    //[TestMethod]
    //public void IsValidId_IsValid_ReturnsTrue()
    //{
    //    // Arrange

    //    // Act

    //    // Assert
    //}
}