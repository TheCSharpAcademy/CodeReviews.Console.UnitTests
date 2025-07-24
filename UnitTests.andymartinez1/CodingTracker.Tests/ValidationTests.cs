using Coding_Tracker.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Coding_Tracker.Tests;

[TestClass]
public class Tests
{
    [TestMethod]
    [DataRow("2025-01-01 00:00", "yyyy-MM-dd HH:mm")]
    [DataRow("2025-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss")]
    [DataRow("01/01/2025 0:00 AM", "MM/dd/yyyy h:mm tt")]
    [DataRow("Wednesday, January 01, 2025", "dddd, MMMM dd, yyyy")]
    public void IsValidDate_ValidDateFormat_ReturnsTrue(string date, string format)
    {
        bool result = Validation.IsValidDate(date, format);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2025-01-01", "yyyy-MM-dd HH:mm")]
    [DataRow("2025-01-0100:00:00", "yyyy-MM-dd HH:mm:ss")]
    [DataRow("01/01/25 ", "MM/dd/yyyy h:mm tt")]
    [DataRow("Wednesday, January 01", "dddd, MMMM dd, yyyy")]
    [DataRow("Wednesday, January 01, 2025", null)]
    [DataRow(null, "yyyy-MM-dd HH:mm")]
    [DataRow(null, null)]
    public void IsValidDate_InvalidDateFormat_ReturnsFalse(string date, string format)
    {
        bool result = Validation.IsValidDate(date, format);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2025-01-01 09:00", "2025-01-01 13:00")]
    [DataRow("2025-02-02 11:00", "2025-02-02 14:00")]
    [DataRow("2025-03-03 08:00", "2025-03-03 12:00")]
    public void IsStartDateBeforeEndDate_ValidDates_ReturnsTrue(string startDate, string endDate)
    {
        bool result = Validation.IsStartDateBeforeEndDate(startDate, endDate);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2025-01-01 09:00", "2025-01-01 00:00")]
    [DataRow("2025-02-02 11:00", "2025-02-02 10:00")]
    [DataRow("2025-03-03 08:00", "2025-03-03 07:59")]
    public void IsStartDateBeforeEndDate_InvalidDates_ReturnsFalse(string startDate, string endDate)
    {
        bool result = Validation.IsStartDateBeforeEndDate(startDate, endDate);

        Assert.IsFalse(result);
    }
}
