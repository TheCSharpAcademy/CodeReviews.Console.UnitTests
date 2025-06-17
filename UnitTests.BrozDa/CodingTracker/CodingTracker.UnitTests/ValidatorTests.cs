using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

using CodingTracker;

namespace CodingTracker.UnitTests;

[TestClass]
public class ValidatorTests
{

    [TestMethod]
    public void IsValidDate_ValidDateFormat_ReturnsTrue()
    {
        string dateFormat = "dd-MM-yyyy HH:mm";

        bool result = Validator.IsValidDate(DateTime.Now.ToString(dateFormat), dateFormat);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsValidDate_InValidDateFormat_ReturnsFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";

        bool result = Validator.IsValidDate(DateTime.Now.ToString(), dateFormat);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValidEndDate_ValidDateFormatAfterStartDate_ReturnTrue() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.AddDays(1).ToString(dateFormat);

        bool result = Validator.IsValidEndDate(endDateString,startDate,dateFormat);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsValidEndDate_ValidDateFormatSameAsStartDate_ReturnFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.ToString(dateFormat);

        bool result = Validator.IsValidEndDate(endDateString, startDate, dateFormat);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValidEndDate_ValidDateFormatBeforeStartDate_ReturnFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.AddHours(-2).ToString(dateFormat);

        bool result = Validator.IsValidEndDate(endDateString, startDate, dateFormat);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValidEndDate_InvalidDateFormatAfterStartDate_ReturnFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.AddDays(1).ToString();

        bool result = Validator.IsValidEndDate(endDateString, startDate, dateFormat);

        Assert.IsFalse(result);

    }
    [TestMethod]
    public void IsValidEndDate_InvalidDateFormatSameAsStartDate_ReturnFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.ToString();

        bool result = Validator.IsValidEndDate(endDateString, startDate, dateFormat);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValidEndDate_InvalidDateFormatBeforeStartDate_ReturnFalse() 
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        DateTime startDate = new DateTime(2025, 06, 16, 08, 30, 0);
        string endDateString = startDate.AddHours(-2).ToString();

        bool result = Validator.IsValidEndDate(endDateString, startDate, dateFormat);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsSessionIdValidOrZero_ValidIdFromSet_ReturnsTrue() 
    { 
        HashSet<int> validIds = new HashSet<int>() { 1,3,8,9,15,27,93};
        int input = 3;

        bool result = Validator.IsSessionIdValidOrZero(input, validIds);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsSessionIdValidOrZero_InvalidIdFromSet_ReturnsFalse() 
    {
        HashSet<int> validIds = new HashSet<int>() { 1, 3, 8, 9, 15, 27, 93 };
        int input = 5;

        bool result = Validator.IsSessionIdValidOrZero(input, validIds);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsSessionIdValidOrZero_ZeroInputSetFull_ReturnsTrue() 
    {
        HashSet<int> validIds = new HashSet<int>() { 1, 3, 8, 9, 15, 27, 93 };
        int input = 0;

        bool result = Validator.IsSessionIdValidOrZero(input, validIds);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsSessionIdValidOrZero_ZeroInputSetEmpty_ReturnsTrue() 
    {
        HashSet<int> validIds = new HashSet<int>();
        int input = 0;

        bool result = Validator.IsSessionIdValidOrZero(input, validIds);

        Assert.IsTrue(result);
    }

    
}