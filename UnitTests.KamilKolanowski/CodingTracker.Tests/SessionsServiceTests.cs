using CodingTracker.KamilKolanowski.Services;

namespace CodingTracker.Tests;

[TestClass]
public sealed class SessionsServiceTests
{
    private readonly SessionsService _sessionsService;

    public SessionsServiceTests()
    {
        _sessionsService = new SessionsService();
    }
    
    [TestMethod]
    public void GetCurrentTime_ReturnsCorrectDateTime()
    {
        bool isCurrentTimeValid  = DateTime.MinValue < _sessionsService.GetCurrentTime(); 
        
        Assert.IsTrue(isCurrentTimeValid, "The current time is not valid");
    }

    [TestMethod]
    public void GetDuration_WithValidParams_ReturnExpectedDuration()
    {
        bool isDurationValid1 = _sessionsService.GetDuration(DateTime.Parse("2023-10-01 12:00:00"), DateTime.Parse("2023-10-01 12:35:00")) == 9999;
        bool isDurationValid2 = _sessionsService.GetDuration(DateTime.Parse("2023-10-01 12:00:00"), DateTime.Parse("2023-10-01 12:30:00")) == 1800;
        bool isDurationValid3 = _sessionsService.GetDuration(DateTime.Parse("2023-12-31 23:59:59"), DateTime.Parse("2024-01-01 00:00:01")) == 2;
        bool isDurationValid4 = _sessionsService.GetDuration(DateTime.Parse("9999-12-31 23:59:59"), DateTime.Parse("2024-01-01 00:00:01")) > 0;
        
        Assert.IsFalse(isDurationValid1, "The duration is not valid for the first test case.");
        Assert.IsTrue(isDurationValid2, "The duration is not valid for the second test case.");
        Assert.IsTrue(isDurationValid3, "The duration is not valid for the third test case.");
        Assert.IsFalse(isDurationValid4, "The duration is not valid for the fourth test case.");
    }
}
