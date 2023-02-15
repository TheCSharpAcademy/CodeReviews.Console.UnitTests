namespace CodingTrackerConsole.Test;

[TestFixture]
public class ValidationTest
{
    private Validation validation = new();

    [TestCase("11-16-2022")]
    [TestCase("01-25-2016")]
    [TestCase("07-26-2000")]
    [TestCase("09-30-1999")]
    public void GivenAValidDate_WhenIsValidDateIsCalled_ThenItReturnsTrue(string date)
    {
        var result = validation.IsValidDate(date);

        Assert.IsTrue(result);
    }

    [TestCase("16-11-2022")]
    [TestCase("25-01-2016")]
    [TestCase("26-07-2000")]
    [TestCase("30-09-1999")]
    public void GivenAnInvalidDate_WhenIsValidDateIsCalled_ThenItReturnsFalse(string date)
    {
        var result = validation.IsValidDate(date);

        Assert.IsFalse(result);
    }

    [TestCase("18:30")]
    [TestCase("06:15")]
    [TestCase("01:20")]
    [TestCase("21:57")]
    public void GivenAValidTime_WhenIsValidTimeIsCalled_ThenItReturnsTrue(string time)
    {
        var result = validation.IsValidTime(time);

        Assert.IsTrue(result);
    }

    [TestCase("6:30")]
    [TestCase("9:57")]
    [TestCase("1:20")]
    [TestCase("4:15")]
    public void GivenAnInvalidTime_WhenIsValidTimeIsCalled_ThenItReturnsFalse(string time)
    {
        var result = validation.IsValidTime(time);

        Assert.IsFalse(result);
    }

    [TestCase("06")]
    [TestCase("26")]
    [TestCase("20")]
    [TestCase("15")]
    public void GivenAValidDay_WhenIsValidDayIsCalled_ThenItReturnsTrue(string day)
    {
        var result = validation.IsValidDay(day);

        Assert.IsTrue(result);
    }

    [TestCase("5")]
    [TestCase("0")]
    [TestCase("93")]
    [TestCase("-3")]
    public void GivenAnInvalidDay_WhenIsValidDayIsCalled_ThenItReturnsFalse(string day)
    {
        var result = validation.IsValidDay(day);

        Assert.IsFalse(result);
    }

    [TestCase("06")]
    [TestCase("11")]
    [TestCase("01")]
    [TestCase("05")]
    public void GivenAValidMonth_WhenIsValidMonthIsCalled_ThenItReturnsTrue(string month)
    {
        var result = validation.IsValidMonth(month);

        Assert.IsTrue(result);
    }

    [TestCase("5")]
    [TestCase("0")]
    [TestCase("93")]
    [TestCase("993")]
    [TestCase("-12")]
    [TestCase("  ")]
    [TestCase("")]
    [TestCase("2333333333333333333333333333333")]
    public void GivenAnInvalidMonth_WhenIsValidMonthIsCalled_ThenItReturnsFalse(string month)
    {
        var result = validation.IsValidMonth(month);

        Assert.IsFalse(result);
    }

    [TestCase("2006")]
    [TestCase("1999")]
    [TestCase("2022")]
    [TestCase("2016")]
    public void GivenAValidYear_WhenIsValidYearIsCalled_ThenItReturnsTrue(string year)
    {
        var result = validation.IsValidYear(year);

        Assert.IsTrue(result);
    }

    [TestCase("55")]
    [TestCase("04")]
    [TestCase("0")]
    [TestCase("993")]
    [TestCase("-1993")]
    [TestCase("  ")]
    [TestCase("")]
    [TestCase("2333333333333333333333333333333")]
    public void GivenAnInvalidYear_WhenIsValidYearIsCalled_ThenItReturnsFalse(string year)
    {
        var result = validation.IsValidYear(year);

        Assert.IsFalse(result);
    }

    [TestCase("10:50", "12:30")]
    [TestCase("10:50", "15:30")]
    [TestCase("12:30", "12:57")]
    [TestCase("12:30", "15:30")]
    public void GivenAValidTimeOrder_WhenIsValidTimeOrderIsCalled_ThenItReturnsTrue(string startTime, string endTime)
    {
        var result = validation.IsValidTimeOrder(startTime, endTime);

        Assert.IsTrue(result);
    }

    [TestCase("12:30", "10:50")]
    [TestCase("15:30", "10:50")]
    [TestCase("12:57", "12:30")]
    [TestCase("15:30", "12:30")]
    public void GivenAnInvalidTimeOrder_WhenIsValidTimeOrderIsCalled_ThenItReturnsFalse(string startTime, string endTime)
    {
        var result = validation.IsValidTimeOrder(startTime, endTime);

        Assert.IsFalse(result);
    }
}
