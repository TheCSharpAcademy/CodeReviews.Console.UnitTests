using CodingTracker.Utils;

namespace CodingTracker.Tests;

[TestClass]
public class Validation_IsValidId
{
    [TestMethod]
    public void IsValidId_Id1IsValid_ReturnTrue()
    {
        bool result = Validation.ValidateId("1");

        Assert.IsTrue(result, "1 is valid Id");
    }

    [TestMethod]
    public void IsValidId_IdaIsNotValid_ReturnFalse()
    {
        bool result = Validation.ValidateId("a");

        Assert.IsFalse(result, "a is NOT a valid Id");
    }


}