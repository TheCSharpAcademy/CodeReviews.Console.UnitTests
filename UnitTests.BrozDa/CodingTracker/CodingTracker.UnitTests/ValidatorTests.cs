using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;


namespace CodingTracker.UnitTests;

[TestClass]
public class ValidatorTests
{

    [TestMethod]
    public void IsValidDate_ValidDateFormat_ReturnsTrue()
    {
        string dateFormat = "dd-MM-yyyy HH:mm";

        var result = DateTime.TryParseExact(DateTime.Now.ToString(dateFormat), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        Assert.IsTrue(result);
    }
    public void IsValidDate_InValidDateFormat_ReturnsFalse() { }


    public void IsValidEndDate_ValidDateFormatAfterStartDate_ReturnTrue() { }

    public void IsValidEndDate_ValidDateFormatSameAsStartDate_ReturnFalse() { }

    public void IsValidEndDate_ValidDateFormatBeforeStartDate_ReturnFalse() { }

    public void IsValidEndDate_InvalidDateFormatAfterStartDate_ReturnFalse() { }

    public void IsValidEndDate_InvalidDateFormatSameAsStartDate_ReturnFalse() { }

    public void IsValidEndDate_InvalidDateFormatBeforeStartDate_ReturnFalse() { }

    public void IsSessionIdValidOrZero_ValidIdFromSet_ReturnsTrue() { }
    public void IsSessionIdValidOrZero_InvalidIdFromSet_ReturnsFalse() { }

    public void IsSessionIdValidOrZero_ZeroInputSetEmpty_ReturnsFalse() { }

    public void IsSessionIdValidOrZero_ZeroInputSetFull_ReturnsTrue() { }
}