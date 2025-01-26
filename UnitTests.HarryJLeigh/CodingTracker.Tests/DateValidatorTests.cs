using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTracker.Helpers;

namespace CodingTracker.Tests;

[TestClass]
public class CodingTrackerTests
{
    [TestMethod]
    public void IsValidEndDate_ShouldReturnTrue_WhenEndDateIsGreaterThanStartDate()
    {
        DateTime endDate = new DateTime(2025,1, 12, 14, 00,00);
        DateTime startDate = new DateTime(2025,1, 11, 14, 00,00);
        bool isValid = DateValidator.IsValidEndDate(startDate, endDate);
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void IsValidEndDate_ShouldReturnFalse_WhenEndDateIsLessThanStartDate()
    {
        DateTime endDate = new DateTime(2025,1, 11, 14, 00,00);
        DateTime startDate = new DateTime(2025,1, 12, 14, 00,00);
        bool isValid = DateValidator.IsValidEndDate(startDate, endDate);
        Assert.IsFalse(isValid);
    }
}