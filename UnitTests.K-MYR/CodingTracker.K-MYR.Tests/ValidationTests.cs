using System.Globalization;

namespace CodingTracker.K_MYR.Tests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    [DataRow("09:00 12-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("00:00 01-01-0001", "HH:mm dd-MM-yyyy")]
    [DataRow("23:59 31-12-9999", "HH:mm dd-MM-yyyy")]
    [DataRow("12-12-2023", "dd-MM-yyyy")]
    [DataRow("01-01-0001", "dd-MM-yyyy")]
    public void ValidateDate_ValidInputs_ReturnTrue(string input, string format)
    {
        bool result = UserInput.ValidateDate(input, format);
        Assert.IsTrue(result, "Validation should be true (sucessfull)");
    }

    [TestMethod]
    [DataRow("01:00 12-13-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01:00 32-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("99:00 13-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01:99 13-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01-01-0001", "HH:mm dd-MM-yyyy")]
    [DataRow("01-15-0000", "dd-MM-yyyy")]
    [DataRow("00-15-00", "dd-MM-yyyy")]
    [DataRow("00:00 01-01-0001", "dd-MM-yyyy")]
    [DataRow(@"qwerwqeer\\\", "dd-MM-yyyy")]
    [DataRow(@"13-12-2023", @"\\\\dd-MM-yyyy")]
    public void ValidateDate_InvalidInputs_ReturnFalse(string input, string format)
    {
        bool result = UserInput.ValidateDate(input, format);
        Assert.IsFalse(result, "Validation should be false (sucessfull)");
    }

    [TestMethod]
    [DataRow("01:00 12-12-2023", "01:01 12-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01:00 12-12-2023", "01:01 13-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("12-12-2023", "13-12-2023", "dd-MM-yyyy")]
    public void ValidateDates_ValidInputs_ReturnTrue(string date1, string date2, string format)
    {
        var start = DateTime.ParseExact(date1, format, new CultureInfo("de-DE"), DateTimeStyles.None);
        var end = DateTime.ParseExact(date2, format, new CultureInfo("de-DE"), DateTimeStyles.None);

        bool result = UserInput.ValidateDates(start, end);

        Assert.IsTrue(result, "Validation should be true (sucessfull)");
    }

    [TestMethod]
    [DataRow("01:00 12-12-2023", "00:59 12-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01:00 12-12-2023", "01:00 11-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("01:00 12-12-2023", "01:00 12-12-2023", "HH:mm dd-MM-yyyy")]
    [DataRow("12-12-2023", "11-12-2022", "dd-MM-yyyy")]
    [DataRow("11-12-2023", "11-12-2022", "dd-MM-yyyy")]
    public void ValidateDates_InvalidInputs_ReturnFalse(string date1, string date2, string format)
    {
        var start = DateTime.ParseExact(date1, format, new CultureInfo("de-DE"), DateTimeStyles.None);
        var end = DateTime.ParseExact(date2, format, new CultureInfo("de-DE"), DateTimeStyles.None);

        bool result = UserInput.ValidateDates(start, end);

        Assert.IsFalse(result, "Validation should be false (sucessfull)");
    }
}
