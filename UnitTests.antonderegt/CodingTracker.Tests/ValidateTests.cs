using System;

namespace CodingTracker.Tests;

[TestClass]
public class ValidateTests
{
    [TestMethod]
    public void CanEndSession_StartTimeIsCurrentTime_ReturnsTrue()
    {
        // Arrange
        CodingSession codingSession = new()
        {
            StartTime = DateTime.Now
        };

        // Act
        bool actual = Validate.CanEndSession(codingSession);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void CanEndSession_StartTimeIsMinValue_ReturnsFalse()
    {
        // Arrange
        CodingSession codingSession = new()
        {
            StartTime = DateTime.MinValue
        };

        // Act
        bool actual = Validate.CanEndSession(codingSession);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void CanEndSession_StartTimeIsDefault_ReturnsFalse()
    {
        // Arrange
        CodingSession codingSession = new();

        // Act
        bool actual = Validate.CanEndSession(codingSession);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void EndTimeIsAfterStartTime_EndTimeIsAfterStartTime_ReturnsTrue()
    {
        // Arrange
        CodingSession codingSession = new()
        {
            StartTime = DateTime.Now
        };
        DateTime endTime = DateTime.MaxValue;

        // Act
        bool actual = Validate.EndTimeIsAfterStartTime(codingSession, endTime);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void EndTimeIsAfterStartTime_EndTimeIsBeforeStartTime_ReturnsFalse()
    {
        // Arrange
        CodingSession codingSession = new()
        {
            StartTime = DateTime.MaxValue
        };
        DateTime endTime = DateTime.Now;

        // Act
        bool actual = Validate.EndTimeIsAfterStartTime(codingSession, endTime);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void EndTimeIsAfterStartTime_EndTimeIsStartTime_ReturnsFalse()
    {
        // Arrange
        DateTime dateTime = DateTime.Now;
        CodingSession codingSession = new()
        {
            StartTime = dateTime
        };
        DateTime endTime = dateTime;

        // Act
        bool actual = Validate.EndTimeIsAfterStartTime(codingSession, endTime);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    [DataRow("abc", -1)]
    [DataRow("-8", -1)]
    [DataRow("1", 1)]
    [DataRow("15", 15)]
    public void ParseSessionId_GivenInput_ReturnsExpectedResult(string input, int expected)
    {
        // Act
        int actual = Validate.ParseSessionId(input);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TryParseDateTime_InputIsValidDate_ReturnsTrue()
    {
        // Arrange
        string input = "01-01-2000";
        DateTime expected = DateTime.Parse(input);

        // Act
        bool actual = Validate.TryParseDateTime(input, out DateTime actualDate);

        // Assert
        Assert.IsTrue(actual);
        Assert.AreEqual(expected, actualDate);
    }

    [TestMethod]
    public void TryParseDateTime_InputIsInvalidDate_ReturnsFalse()
    {
        // Arrange
        string input = "abc";
        DateTime expected = DateTime.MinValue;

        // Act
        bool actual = Validate.TryParseDateTime(input, out DateTime actualDate);

        // Assert
        Assert.IsFalse(actual);
        Assert.AreEqual(expected, actualDate);
    }
}