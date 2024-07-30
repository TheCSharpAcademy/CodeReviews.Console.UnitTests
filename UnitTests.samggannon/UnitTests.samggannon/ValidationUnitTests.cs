using CodingTracker.SamGannon.Utility;

namespace UnitTests.samggannon;

[TestClass]
public class ValidationUnitTests
{
    [TestMethod]
    [DataRow("12:00", true)]
    [DataRow("01:00", true)]
    [DataRow("1:00", false)]
    [DataRow("string input", false)]
    public void TestIsValidTimeFormat_ValidFormat_ReturnsTrueIfValid(string mockInput, bool expectedResult)
    {
        // Arrange
        var validation = new Validation();

        // Act
        bool result = validation.IsValidTimeFormat(mockInput);

        // Assert
        Assert.AreEqual(result, expectedResult);
    }

    [TestMethod]
    [DataRow("07-17-24", true)]
    [DataRow("July 17th, 2024", false)]
    [DataRow("07172024", false)]
    [DataRow("20240717", false)]
    [DataRow("07/17/24", false)]
    public void TestIsValidDate_ValidDate_ReturnsTrueIfValid(string date, bool expectedResult)
    {
        var validation = new Validation();
        bool result = validation.IsValidDate(date);
        Assert.AreEqual(result, expectedResult);
    }

    [TestMethod]
    [DataRow("1", true)]
    [DataRow("12", true)]
    [DataRow("123", true)]
    [DataRow("-1", false)]
    [DataRow("0.12", false)]
    [DataRow("one", false)]
    public void TestIsValidId_ValidId_ReturnsTrueIfValid(string id, bool expectedResult)
    {
        var validation = new Validation();
        bool result = validation.IsValidId(id);
        Assert.AreEqual(result, expectedResult);
    }
}