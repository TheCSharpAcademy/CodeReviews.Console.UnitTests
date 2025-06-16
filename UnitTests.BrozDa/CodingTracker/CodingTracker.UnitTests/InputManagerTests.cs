using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;


namespace CodingTracker.UnitTests;

[TestClass]
public class InputManagerTests
{

    [TestMethod]
    public void GetStartTime_ValidDateFormat_ReturnsTrue()
    {
        string dateFormat = "dd-MM-yyyy HH:mm";

        var result = DateTime.TryParseExact(DateTime.Now.ToString(dateFormat), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void GetStartTime_InvalidDateFormat_ReturnsTrue()
    {
        string dateFormat = "dd-MM-yyyy HH:mm";

        var result = DateTime.TryParseExact(DateTime.Now.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        Assert.IsFalse(result);
    }

    public void GetEndTime_ValidDateFormatAndAfterStartDate_ReturnsTrue() { }
    public void GetEndTime_ValidDateFormatAndBeforeStartDate_ReturnsTrue() { }
    public void GetEndTime_InvalidDateFormatAndAfterStartDate_ReturnsTrue() { }
    public void GetEndTime_InvalidDateFormatAndBeforeStartDate_ReturnsTrue() { }
}