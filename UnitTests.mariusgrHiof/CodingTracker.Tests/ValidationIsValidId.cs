using CodingTracker.Utils;

namespace CodingTracker.Tests;

[TestClass]
public class ValidationIsValidId
{

    [TestMethod]
    [DataRow("1")]
    [DataRow("2")]
    [DataRow("3")]
    [DataRow("123")]
    [DataRow("243")]
    public void IsValidId_IdIsValid_ReturnTrue(string value)
    {
        bool result = Validation.ValidateId(value);

        Assert.IsTrue(result, $"{value} is valid Id");
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("-1")]
    [DataRow("0")]
    [DataRow("@")]
    [DataRow("%")]
    [DataRow("")]
    [DataRow(" ")]
    public void IsValidId_IdIsNotValid_ReturnFalse(string value)
    {
        bool result = Validation.ValidateId(value);

        Assert.IsFalse(result, $"{value} is NOT a valid Id");
    }


}