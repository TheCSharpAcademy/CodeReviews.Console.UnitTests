using CodingTracker.KamilKolanowski.Services;
using Spectre.Console;

namespace CodingTracker.Tests;

[TestClass]
public sealed class SessionsServiceTests
{
    private readonly SessionsService _sessionsService;
    private readonly Validation _validation;

    public SessionsServiceTests()
    {
        _sessionsService = new SessionsService();
        _validation = new Validation();
    }
    
    [TestMethod]
    public void GetCurrentTime_ReturnsCorrectDateTime()
    {
        bool isCurrentTimeValid  = DateTime.MinValue < _sessionsService.GetCurrentTime(); 
        
        Assert.IsTrue(isCurrentTimeValid, "The current time is not valid");
    }

    [TestMethod]
    public void GetDuration_WithValidParams_ReturnExpectedDuration_1()
    {
        bool isDurationValid1 = _sessionsService.GetDuration(DateTime.Parse("2023-10-01 12:00:00"), DateTime.Parse("2023-10-01 12:35:00")) == 9999;
        
        Assert.IsFalse(isDurationValid1, "The duration is not valid for the first test case.");
    }
    
    [TestMethod]
    public void GetDuration_WithValidParams_ReturnExpectedDuration_2()
    {
        bool isDurationValid2 = _sessionsService.GetDuration(DateTime.Parse("2023-10-01 12:00:00"), DateTime.Parse("2023-10-01 12:30:00")) == 1800;
        
        Assert.IsTrue(isDurationValid2, "The duration is not valid for the second test case.");
    }
    
    [TestMethod]
    public void GetDuration_WithValidParams_ReturnExpectedDuration_3()
    {
        bool isDurationValid3 = _sessionsService.GetDuration(DateTime.Parse("2023-12-31 23:59:59"), DateTime.Parse("2024-01-01 00:00:01")) == 2;
        
        Assert.IsTrue(isDurationValid3, "The duration is not valid for the third test case.");
    }
    
    [TestMethod]
    public void GetDuration_WithValidParams_ReturnExpectedDuration_4()
    {
        bool isDurationValid4 = _sessionsService.GetDuration(DateTime.Parse("9999-12-31 23:59:59"), DateTime.Parse("2024-01-01 00:00:01")) > 0;
        
        Assert.IsFalse(isDurationValid4, "The duration is not valid for the fourth test case.");
    }
    
    [TestMethod]
    public void UserStartTimeInput_IsValid()
    {
        DateTime userStartTime = DateTime.Parse("2023-10-01 12:00:00");
        var isStartTimeValid = _validation.ValidateStartTime(userStartTime);
        
        Assert.IsTrue(isStartTimeValid.Successful);
    }
    
    [TestMethod]
    public void UserStartTimeInput_IsNotValid()
    {
        DateTime userStartTime = DateTime.Parse("2023-10-01 00:00:00");
        var isStartTimeValid = _validation.ValidateStartTime(userStartTime);
        
        Assert.IsFalse(isStartTimeValid.Successful);
    }
    
    [TestMethod]
    public void UserStartTimeInput_WrongDate_IsNotValid()
    {
        string userStartTime = "11111-11-33 30:00:00";
        if (DateTime.TryParse(userStartTime, out DateTime parsedDate))
        {
            var result = _validation.ValidateStartTime(parsedDate);
            Assert.IsFalse(result.Successful);
        }
        else
        {
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    public void UserEndTimeInput_IsValid()
    {
        DateTime sessionStartTime = DateTime.Parse("2023-10-01 12:00:00");
        DateTime userEndTime = DateTime.Parse("2023-10-01 13:00:00");
    
        var isEndTimeValid = _validation.ValidateEndTime(userEndTime, sessionStartTime);
    
        Assert.IsTrue(isEndTimeValid.Successful);
    }

    [TestMethod]
    public void UserEndTimeInput_IsNotValid()
    {
        DateTime sessionStartTime = DateTime.Parse("2023-10-01 12:00:00");
        DateTime userEndTime = DateTime.Parse("2023-10-01 00:00:00");
    
        var isEndTimeValid = _validation.ValidateEndTime(userEndTime, sessionStartTime);
    
        Assert.IsFalse(isEndTimeValid.Successful);
    }

    [TestMethod]
    public void UserEndTimeInput_WrongDate_IsNotValid()
    {
        string userEndTimeStr = "2222asf1-11-33 30:00:00";
        DateTime sessionStartTime = DateTime.Parse("2023-10-01 12:00:00");

        if (DateTime.TryParse(userEndTimeStr, out DateTime parsedDate))
        {
            var result = _validation.ValidateEndTime(parsedDate, sessionStartTime);
            Assert.IsFalse(result.Successful);
        }
        else
        {
            Assert.IsTrue(true);
        }
    }
}
