namespace CodingTracker.Tests;

[TestClass]
public sealed class InputValidationTests
{

    [TestMethod]
    [DataRow("01/01/21 09:00 AM")]
    [DataRow("09/15/10 12:55 AM")]
    [DataRow("12/12/25 10:00 PM")]
    public void ValidateDateAndTimeInput_CorrectInput(string input)
    { 
        Assert.IsTrue(Validation.ValidateDateTimeInput(input));
    }

    [TestMethod]
    [DataRow("1/01/21 09:00 AM")]
    [DataRow("12/12/2025 10:00 PM")]
    [DataRow("11/05/10 9:15 AM")]
    [DataRow("10/5/19 12:30 PM")]
    [DataRow("12/12/2025 10:00 PM")]
    [DataRow("15/12/25 10:00 PM")]
    [DataRow("12/32/25 10:00 PM")]
    [DataRow("12/12/25 13:00 PM")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("abc")]
    public void ValidateDateAndTimeInput_IncorrectInput(string input)
    {
        Assert.IsFalse(Validation.ValidateDateTimeInput(input));
    }

    [TestMethod]
    [DataRow("C#")]
    [DataRow("ASP.NET")]
    [DataRow("JavaScript")]
    public void ValidateFocusInput_CorrectInput(string input)
    {
        Assert.IsTrue(Validation.ValidateFocusInput(input));
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void ValidateFocusInput_IncorrectInput(string input)
    {
        Assert.IsFalse(Validation.ValidateFocusInput(input));
    }

    [TestMethod]
    [DataRow("1")]
    [DataRow("20")]
    [DataRow("100")]
    public void ValidateSessionIdInput_CorrectInput(string input)
    {
        Assert.AreEqual(int.Parse(input), Validation.ValidateSessionIdInput(input));
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("-5")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("abc")]
    public void ValidateSessionIdInput_IncorrectInput(string input)
    {
        Assert.AreEqual(-1, Validation.ValidateSessionIdInput(input));
    }

    [TestMethod]
    [DataRow("01/01/21")]
    [DataRow("09/15/10")]
    [DataRow("12/12/25")]
    public void ValidateDateInput_CorrectInput(string input)
    {
        Assert.IsTrue(Validation.ValidateDateInput(input));
    }

    [TestMethod]
    [DataRow("21")]
    [DataRow("abc")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("15/12/25")]
    [DataRow("12/32/25")]
    [DataRow("12/12/2025")]
    public void ValidateDateInput_IncorrectInput(string input)
    {
        Assert.IsFalse(Validation.ValidateDateInput(input));
    }

    [TestMethod]
    [DataRow("01/21")]
    [DataRow("09/10")]
    [DataRow("12/25")]
    public void ValidateMonthAndYearInput_CorrectInput(string input)
    {
        Assert.IsTrue(Validation.ValidateMonthAndYearInput(input));
    }

    [TestMethod]
    [DataRow("21")]
    [DataRow("abc")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("15/25")]
    [DataRow("12/2025")]
    public void ValidateMonthAndYearInput_IncorrectInput(string input)
    {
        Assert.IsFalse(Validation.ValidateMonthAndYearInput(input));
    }

    [TestMethod]
    [DataRow("21")]
    [DataRow("10")]
    [DataRow("25")]
    public void ValidateYearInput_CorrectInput(string input)
    {
        Assert.IsTrue(Validation.ValidateYearInput(input));
    }

    [TestMethod]
    [DataRow("abc")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("15/25")]
    public void ValidateYearInput_IncorrectInput(string input)
    {
        Assert.IsFalse(Validation.ValidateYearInput(input));
    }
}

