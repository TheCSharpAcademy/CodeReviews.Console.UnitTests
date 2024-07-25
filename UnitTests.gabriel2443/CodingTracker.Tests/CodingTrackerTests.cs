using coding_tracker;

namespace CodingTracker.Tests;

[TestClass]
public class UserInputTests
{
    private UserInput userInput;

    [TestInitialize]
    public void Setup()
    {
        userInput = new UserInput();
    }

    [TestMethod]
    public void GetStartEndTime_ValidTime_ReturnsTime()
    {
        // Arrange
        string message = "Please insert the start time in format (hh:mm)";
        string inputTime = "14:30";
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            using (var sr = new StringReader(inputTime))
            {
                Console.SetIn(sr);

                // Act
                var result = userInput.GetStartEndTime(message);

                // Assert
                Assert.AreEqual(inputTime, result);
            }
        }
    }

    [TestMethod]
    public void GetDuration_ValidTimes_ReturnsDuration()
    {
        // Arrange
        string startTime = "14:00";
        string endTime = "15:30";
        string expectedDuration = "01 hours : 30 minutes";

        // Act
        var result = userInput.GetDuration(startTime, endTime);

        // Assert
        Assert.AreEqual(expectedDuration, result);
    }
}