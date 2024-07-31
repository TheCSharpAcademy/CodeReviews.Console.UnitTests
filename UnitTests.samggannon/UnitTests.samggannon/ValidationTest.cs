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

    [TestMethod]
    [DataRow("07312024", false)]
    [DataRow("31-07-2024", false)]
    [DataRow("July 31st, 2024", false)]
    [DataRow("07-31-24", true)]
    public void IsValidDate_IsValid_ReturnsTrue(string date, bool expectedResult)
    {
        // Arrange
        var validation = new Validation();

        // Act
        bool actualResult = validation.IsValidDate(date);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

//    [TestMethod]
//    public void IsValidId_IsValid_ReturnsTrue()
//    {
//        // Arrange

//        // Act

//        // Assert
//    }
}