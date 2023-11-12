using CodingTracker.Utils;

namespace CodingTracker.Tests;

[TestClass]
public class Validation_IsValidDate
{
    [TestMethod]
    public void IsValidDate_DateIsValid_ReturnTrue()
    {
        bool result = Validation.ValidateDate("01/06/2023 14:43");

        Assert.IsTrue(result, "01/06/2023 14:43 is valid date");
    }

    [TestMethod]
    public void IsValidDate_DateIsNotValid_ReturnFalse()
    {
        bool result = Validation.ValidateDate("01/06/2023 14:4");

        Assert.IsFalse(result, "01/06/2023 14:4 is not a valid date");
    }

    [TestMethod]
    public void IsValidDateRange_DateRangeIsValid_ReturnTrue()
    {
        bool result = Validation.ValidateDateRange(DateTime.Parse("01/06/2023 13:00"), DateTime.Parse("01/06/2023 14:00"));

        Assert.IsTrue(result, "01/06/2023 13:00 - 01/06/2023 14:00 is a valid date range");
    }

    [TestMethod]
    public void IsValidDateRange_DateRangeIsNotValid_ReturnFalse()
    {
        bool result = Validation.ValidateDateRange(DateTime.Parse("01/06/2023 13:00"), DateTime.Parse("01/06/2023 12:00"));

        Assert.IsFalse(result, "01/06/2023 13:00 - 01/06/2023 12:00 is NOT a valid date range");
    }


}