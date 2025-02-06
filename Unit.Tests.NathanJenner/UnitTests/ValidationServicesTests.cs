using Coding_Tracking_Application.Services;

namespace UnitTests;

[TestClass]
public class ValidationServicesTests
{
    [TestMethod]
    [DataRow("1")]
    [DataRow("4")]
    [DataRow("5556")]
    public void ParseMenuInput_SuccessfulParseOfInput_ReturnsTrue(string value)
    {
        Tuple<int, bool> result = ValidationServices.ParseMenuInput(value);

        Assert.IsTrue(result.Item2);
    }

    [TestMethod]
    [DataRow("A")]
    [DataRow("y")]
    [DataRow("[")]
    [DataRow("1y")]
    public void ParseMenuInput_SuccessfulParseOfInput_ReturnsFalse(string value)
    {
        Tuple<int, bool> result = ValidationServices.ParseMenuInput(value);

        Assert.IsFalse(result.Item2);
    }

    [TestMethod]
    [DataRow("11:52:00 06/02/2025")]
    [DataRow("10:34:00 6/2/2025")]
    [DataRow("11:00 6/2/2025")]
    [DataRow("23:23:00 06/02/25")]
    public void ParseInputDateTime_SuccessfulParseOfInput_ReturnsTrue(string value)
    {
        Tuple<DateTime, bool> result = ValidationServices.ParseInputDateTime(value);

        Assert.IsTrue(result.Item2);
    }

    [TestMethod]
    [DataRow("13:52:00 06/02/")]
    [DataRow("11 06/02/2025")]
    [DataRow(":52:00 06/02/2025")]
    [DataRow("25:52:00 06/02/2025")]
    public void ParseInputDateTime_SuccessfulParseOfInput_ReturnsFalse(string value)
    {
        Tuple<DateTime, bool> result = ValidationServices.ParseInputDateTime(value);

        Assert.IsFalse(result.Item2);
    }

    [TestMethod]
    [DataRow("11:52:00 06/02/2025", "11:57:00 06/02/2025")]
    [DataRow("11:52:00 06/02/2025", "06:57:00 06/02/2036")]
    [DataRow("00:00:00 06/02/2025", "00:00:01 06/02/2025")]
    public void StartDateLessThanEndDate_ConfirmStartDateLessThanEndDate_ReturnsTrue(string startTimeStr, string endTimeStr)
    {
        DateTime startTime = DateTime.Parse(startTimeStr);
        DateTime endTime = DateTime.Parse(endTimeStr);

        bool result = ValidationServices.StartDateLessThanEndDate(startTime, endTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("11:52:00 06/02/2025", "11:57:00 06/02/2024")]
    [DataRow("11:52:00 06/02/2025", "06:57:00 06/02/2025")]
    [DataRow("00:00:00 07/02/2025", "00:00:01 06/02/2025")]
    public void StartDateLessThanEndDate_ConfirmStartDateLessThanEndDate_ReturnsFalse(string startTimeStr, string endTimeStr)
    {
        DateTime startTime = DateTime.Parse(startTimeStr);
        DateTime endTime = DateTime.Parse(endTimeStr);

        bool result = ValidationServices.StartDateLessThanEndDate(startTime, endTime);

        Assert.IsFalse(result);
    }
}