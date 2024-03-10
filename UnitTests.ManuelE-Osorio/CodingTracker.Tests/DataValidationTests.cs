using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTracker;

namespace Tests;

[TestClass]
public class DataValidationTests()
{
    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("Text")]
    [DataRow("1.")]
    [DataRow("1,223")]
    public void ValidateInteger_InputIsEmptyNullOrText_ReturnsFalse(string input)
    {
        var result = DataValidation.ValidateInteger(input);
        Assert.IsFalse(result);
        //arrange
        //act
        //assert
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("100")]
    [DataRow("-200")]
    [DataRow("2147483647")]
    [DataRow("-2147483648")]
    [DataRow("2147483648")]
    [DataRow("-2147483649")]
    public void ValidateInteger_InputIsNull_ReturnsTrue(string input)
    {
        var result = DataValidation.ValidateInteger(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("", 5, 10)]
    [DataRow(null, 5, 10)]
    [DataRow("Text", 5, 10)]
    [DataRow("1.", 5, 10)]
    [DataRow("1,223", 5, 10)]
    public void ValidateIntegerMinMaxOverload_InputIsEmptyNullOrText_ReturnsFalse(string input, int min, int max)
    {
        var result = DataValidation.ValidateInteger(input, min, max);
        Assert.IsFalse(result);
    }
}