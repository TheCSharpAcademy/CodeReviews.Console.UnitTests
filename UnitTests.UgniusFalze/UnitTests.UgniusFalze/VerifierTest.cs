namespace UnitTests.UgniusFalze;
using CodingTracker;
[TestClass]
public class VerifierTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "With lower end time, it should throw an exception")]
    [DataRow(2023, 12, 30, 2021, 12, 31)]
    [DataRow(1452, 11, 05, 1452, 05, 20)]
    [DataRow(2003, 08, 04, 2003, 08, 01)]
    public void DateTime_WithInvalidInputThrowsAnException(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
    {
        var startTime = new DateTime(startYear, startMonth, startDay);
        var endTime = new DateTime(endYear, endMonth, endDay);
        Verifier.VerifyDate(startTime, endTime);
    }
    
    [TestMethod]
    [DataRow(1990, 11, 10, 1991, 01, 01)]
    [DataRow(1990, 05, 31, 1990, 06,01)]
    [DataRow(1543, 08, 10, 1543, 08,11)]
    [DataRow(2000, 02, 09, 2000, 02,09)]
    public void DateTime_WithValidInputNoExceptionIsThrown(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
    {
        var startDate = new DateTime(startYear, startMonth, startDay);
        var endDate = new DateTime(endYear, endMonth, endDay);
        Verifier.VerifyDate(startDate, endDate);
    }
}