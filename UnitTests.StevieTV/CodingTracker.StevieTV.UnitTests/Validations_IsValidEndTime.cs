namespace CodingTracker.StevieTV.UnitTests;

[TestClass]
public class Validations_IsValidEndTime
{
    // Valid input strings do not need testing here, they are already tested during input before checking
    // if end time is greater than or equal to start time.
    
    [TestMethod]
    [DataRow("01:00", "00:00")]
    [DataRow("13:00",  "00:00")]
    [DataRow("23:59", "23:00")]
    public void IsValidEndTime_EndTimesGreaterThanStartTime_ShouldReturnTrue(string timeInput, string startTime)
    {
        var result = Validations.IsValidEndTime(timeInput, startTime);
        Assert.IsTrue(result, "Valid end times after start time should be accepted");
    }
    
    [TestMethod]
    [DataRow("00:00", "00:00")]
    [DataRow("13:00", "13:00")]
    [DataRow("23:00", "23:00")]
    public void IsValidEndTime_EndTimesEqualToStartTime_ShouldReturnTrue(string timeInput, string startTime)
    {
        var result = Validations.IsValidEndTime(timeInput, startTime);
        Assert.IsTrue(result, "Valid end times equal to start time should be accepted");
    }
        
    [TestMethod]
    [DataRow("00:00", "01:00")]
    [DataRow("13:00", "14:00")]
    [DataRow("23:00", "23:59")]
    public void IsValidEndTime_EndTimesBeforeStartTime_ShouldReturnFalse(string timeInput, string startTime)
    {
        var result = Validations.IsValidEndTime(timeInput, startTime);
        Assert.IsFalse(result, "Valid end times before to start time should be rejected");
    }    
}