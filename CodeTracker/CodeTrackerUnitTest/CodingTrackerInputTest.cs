using CodeTracker;
using Spectre.Console.Testing;
namespace CodeTrackerUnitTest;

[TestClass]
public class CodingTrackerInputTest
{
    private readonly TestConsole _console;
    private readonly Input _input;
    public CodingTrackerInputTest()
    {
        _console = new TestConsole();
        _input = new Input(_console);
    }
    [TestMethod]
    [DataRow(" ")]
    [DataRow("description")]
    public void TestingDescriptionInput(string input)
    {
        // Arrange
        if (!string.IsNullOrWhiteSpace(input))
        {
            _console.Input.PushTextWithEnter(input);  // Valid input case
            var result = _input.GetDescription();
            Assert.AreEqual(input, result);  // Verify the description matches the input
        }
        else
        {
            _console.Input.PushTextWithEnter(input);  // Invalid input case

            // Act & Assert: Expect an exception due to invalid input
            Assert.ThrowsException<InvalidOperationException>(() => _input.GetDescription());
        }
    }

    [TestMethod]
    [DataRow("test")]
    [DataRow("")]
    [DataRow("22-10-2023")]
    public void TestDateInput(string input)
    {
        var reader = new StringReader(input);
        Console.SetIn(reader);
        DateTime result = _input.GetDay();
        if (!input.Any(char.IsDigit))
        {
            Assert.AreEqual(DateTime.Today,result);
            return;
        }
        Assert.AreEqual(string.IsNullOrEmpty(input) ? DateTime.Today : new DateTime(2023,10,22), result);
    }
    [TestMethod]
    [DataRow("20:30")]
    public void TestStartTimeInput(string input)
    {
        var reader = new StringReader(input);
        Console.SetIn(reader);
        
        DateTime result = _input.GetStartTime(new DateTime(2023,10,22));
        Assert.AreEqual(new DateTime(2023,10,22,20,30,00), result);
    }
    [TestMethod]
    [DataRow("20-10-2023")]
    [DataRow("")]
    public void TestOptionalDateInput(string input)
    {
        var reader = new StringReader(input);
        Console.SetIn(reader);
        var result = _input.GetOptionalDate();
        if (input == "")
        {
            Assert.AreEqual(DateTime.MinValue,result);
        }
        else
        {
            Assert.AreEqual(DateTime.Parse(input), result);
        }
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("12:30")]
    [DataRow("5:00")]
    [DataRow("05:00")]
    public void TestEndTimeInput(string input)
    {
        var reader = new StringReader(input);
        Console.SetIn(reader);
        var result = _input.GetEndTime(new DateTime(2023, 10, 12),new DateTime(2023,10,12,1,30,00));
        if (input == "")
        {
            Assert.AreEqual(new DateTime(2023,10,12,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second),result);
        }
        else
        {
            var date = DateTime.Parse(input);
            date = new DateTime(2023, 10, 12, date.Hour, date.Minute, 00);
            Assert.AreEqual(date, result);
        }
    }
    
}