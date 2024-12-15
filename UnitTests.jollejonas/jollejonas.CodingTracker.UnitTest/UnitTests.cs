using jollejonas.CodingTracker.Utilities;

namespace jollejonas.CodingTracker.UnitTest;

[TestClass]
public sealed class UnitTests
{
    [TestMethod]
    [DataRow(null, "Start time cannot be empty.")]
    [DataRow("", "Start time cannot be empty.")]
    [DataRow("281024 10:10", "Invalid start time format. Please enter the start time in the format dd-MM-yyyy HH:mm.")]
    [DataRow("28-10-24 10:10", "Invalid start time format. Please enter the start time in the format dd-MM-yyyy HH:mm.")]
    [DataRow("28-10-2025 10:10", "Start time cannot be in the future.")]
    public void CheckStartTime_InvalidInputs_ReturnsErrorMessage(string input, string expectedMessage)
    {
        var result = Validation.CheckStartTime(input);
        Assert.AreEqual(expectedMessage, result);
    }
    [TestMethod]
    [DataRow("28-10-2024 10:10")]
    [DataRow("01-01-2022 00:00")]
    public void CheckStartTime_ValidInputs_ReturnsNull(string input)
    {
        var result = Validation.CheckStartTime(input);
        Assert.IsNull(result);
    }

    [TestMethod]
    [DataRow(null, "28-10-2024 10:10", "End time cannot be empty.")]
    [DataRow("", "28-10-2024 10:10", "End time cannot be empty.")]
    [DataRow("28-10-2024 09:00", "28-10-2024 10:10", "End time must be after the start time.")]
    [DataRow("28-10-2024 10:10", "28-10-2024 10:10", "End time must be after the start time.")]
    [DataRow("28-10-2025 10:10", "28-10-2024 10:10", "End time cannot be in the future.")]
    public void CheckEndTime_InvalidInputs_ReturnsErrorMessage(string endTime, string startTime, string expectedMessage)
    {
        var result = Validation.CheckEndTime(endTime, startTime);
        Assert.AreEqual(expectedMessage, result);
    }
    [TestMethod]
    [DataRow("28-10-2024 11:10", "28-10-2024 10:10")]
    [DataRow("01-01-2022 01:00", "01-01-2022 00:00")]
    public void CheckEndTime_ValidInputs_ReturnsNull(string endTime, string startTime)
    {
        var result = Validation.CheckEndTime(endTime, startTime);
        Assert.IsNull(result);
    }
}
