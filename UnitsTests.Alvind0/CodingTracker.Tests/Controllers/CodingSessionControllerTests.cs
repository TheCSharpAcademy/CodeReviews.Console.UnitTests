using Alvind0.CodingTracker.Controllers;
using Alvind0.CodingTracker.Data;
using Alvind0.CodingTracker.Views;
using Moq;

namespace CodingTracker.Tests;

[TestClass]
public class CodingSessionControllerTests
{
    private Mock<CodingSessionRepository> _mockRepository;
    private Mock<TableRenderer> _mockRenderer;
    CodingSessionController _controller;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new Mock<CodingSessionRepository>("");
        _mockRenderer = new Mock<TableRenderer>();
        _controller = new(_mockRepository.Object, _mockRenderer.Object);
    }

    [TestMethod]
    public void ValidateDateTime_ValidInput_ReturnsTrue()
    {
        var validDateTime = "01-15-25 0:00";

        var result = _controller.ValidateDateTime(validDateTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidateDateTime_InputHasWhiteSpacesInStart_ReturnsTrue()
    {
        var validDateTime = "  01-15-25 0:00";

        var result = _controller.ValidateDateTime(validDateTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidateDateTime_InputHasWhiteSpacesInEnd_ReturnsTrue()
    {
        var validDateTime = "01-15-25 0:00   ";

        var result = _controller.ValidateDateTime(validDateTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidateDateTime_DateOnlyInput_ReturnsFalse()
    {
        var invalidDateTime = "01-15-25";

        var result = _controller.ValidateDateTime(invalidDateTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateDateTime_DateTimeWithSeconds_ReturnsFalse()
    {
        var invalidDateTime = "01-15-25 0:00:00";

        var result = _controller.ValidateDateTime(invalidDateTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateDateTime_StringIsNull_ReturnsFalse()
    {
        var invalidDateTime = string.Empty;

        var result = _controller.ValidateDateTime(invalidDateTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateStartAndEndTime_StartIsEarlierThanEnd_ReturnsTrue()
    {
        var startTime = new DateTime(2024, 12, 24);
        var endTime = new DateTime(2025, 1, 15);

        var result = _controller.ValidateStartAndEndTime(startTime, endTime);

        Assert.IsTrue(result);
    }
}
