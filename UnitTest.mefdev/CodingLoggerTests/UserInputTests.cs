using System.Globalization;
using CodingLogger.Controller;
using CodingLogger.Data;
using CodingLogger.Models;
using CodingLogger.Services;
using Moq;
namespace CodingLoggerTests;

[TestClass]
public class UserInputTests
{
    private Mock<ICodingSessionRepository> _mockRepository;
    private readonly UserInput _userInput;
    private readonly CodingService _codingService;
    private readonly CodingController _codingController;

    public UserInputTests()
    {
        _mockRepository = new Mock<ICodingSessionRepository>();
        _userInput = new UserInput(null);
        _codingController = new CodingController();
        _codingService = new CodingService(_mockRepository.Object, _userInput, _codingController);
        
    }

    [TestMethod]
    public void GetDateTimeValue_ThrowsArgumentException_WhenEmptyStringPassed()
    {
        string expectedDateTime = "";
       
        Assert.ThrowsException<ArgumentException>(() => _userInput.GetDateTimeValue(expectedDateTime));
    }

    [TestMethod]
    [DataRow("2024-12-09 12:30")]
    [DataRow("2024-12-10 13:30")]
    [DataRow("2024-12-11 14:30")]
    public void GetDateTimeValue_ReturnsValidDateTime_WhenCorrectFormatProvided(string expectedDateTime)
    {
        
        var input = expectedDateTime;
        var stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        var result = _userInput.GetDateTimeValue(expectedDateTime);

        Assert.AreEqual(DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), result);
    }

    [TestMethod]
    [DataRow("12-09-2024 12:30")]
    [DataRow("12)2-2024 13:30")]
    [DataRow("12-11-2024 14:30")]
    [DataRow("12-11-2024 14:30:30")]
    [DataRow("12-2024-02 -1:40:30")]
    public void GetDateTimeValue_ReturnsFormatException_WhenUnCorrectFormatProvided(string expectedDateTime)
    {
        var input = expectedDateTime;
        var stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        Assert.ThrowsException<FormatException>(()=> _userInput.GetDateTimeValue(expectedDateTime));
    }
}