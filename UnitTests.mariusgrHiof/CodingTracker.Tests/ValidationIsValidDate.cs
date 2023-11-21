using CodingTracker.Utils;

namespace CodingTracker.Tests;

[TestClass]
public class ValidationIsValidDate
{
    [TestMethod]
    [DataRow("01/06/2023 14:43")]
    [DataRow("12/06/2019 09:43")]
    [DataRow("25/11/2003 14:43")]
    public void IsValidDate_DateIsValid_ReturnTrue(string date)
    {
        bool result = Validation.ValidateDate(date);

        Assert.IsTrue(result, $"{date} is valid date");
    }

    [TestMethod]
    [DataRow("01/06/2023 14:4")]
    [DataRow(" ")]
    [DataRow("@")]
    [DataRow("test")]
    public void IsValidDate_DateIsNotValid_ReturnFalse(string date)
    {
        bool result = Validation.ValidateDate(date);

        Assert.IsFalse(result, $"{date} is not a valid date");
    }

    [TestMethod]
    [DataRow("01/06/2023 13:00", "01/06/2023 14:00")]
    [DataRow("05/12/2021 07:40", "05/12/2021 09:00")]
    [DataRow("05/12/2021 07:40", "06/12/2021 03:00")]
    public void IsValidDateRange_DateRangeIsValid_ReturnTrue(string startDate, string endDate)
    {
        bool result = Validation.ValidateDateRange(startDate, endDate);

        Assert.IsTrue(result, $"{startDate} - {endDate} is a valid date range");
    }

    [TestMethod]
    [DataRow("01/06/2023 13:00", "25/01/2022 14:00")]
    [DataRow("05/12/2021 07:40", "05/12/2021 06:00")]
    [DataRow("12/13/2021 07:40", "12/13/2021 09:00")]
    [DataRow("12/1320 0:40", "12/1/201 09:0")]
    [DataRow(" ", "")]
    public void IsValidDateRange_DateRangeIsNotValid_ReturnFalse(string startDate, string endDate)
    {
        bool result = Validation.ValidateDateRange(startDate, endDate);

        Assert.IsFalse(result, $"{startDate} - {endDate} is NOT a valid date range");
    }


}