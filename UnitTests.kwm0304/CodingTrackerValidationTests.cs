namespace UnitTests.kwm0304;

[TestClass]
public class CodingTrackerValidationTests
{
    [TestMethod]
    public void ConvertDateTimeToString_ReturnsFormattedString()
    {
    }

    [TestMethod]
    public void ConvertTimeToInt_ReturnsTotalSeconds()
    {

    }

    [TestMethod]
    public void ConvertTextToDateTime_ReturnsDateTime()
    {

    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ConvertTextToDateTime_ThrowsFormatException()
    {

    }

    [TestMethod]
    public void ToDays_ReturnsCorrectDay()
    {

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ToDays_ThrowsArgumentOutOfRangeException()
    {

    }

    [TestMethod]
    public void ToDateRange_ReturnsCorrectDateRange()
    {

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ToDateRange_ThrowsArgumentOutOfRangeException()
    {
        
    }
}