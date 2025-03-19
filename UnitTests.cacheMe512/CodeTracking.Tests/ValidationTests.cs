using Microsoft.VisualStudio.TestTools.UnitTesting;
using cacheMe512.CodeTracker;
using Moq;

namespace CodeTracking.Tests;

[TestClass]
public class ValidationTests
{
    private Mock<IConsoleWrapper> mockConsole;
    private Validation validation;

    [TestInitialize]
    public void Setup()
    {
        mockConsole = new Mock<IConsoleWrapper>();
        validation = new Validation(mockConsole.Object);
    }

    [TestMethod]
    public void DateTimeInSequence_ValidDates()
    {
        DateTime startTime = new DateTime(2025, 3, 1, 12, 0, 0);
        DateTime endTime = new DateTime(2025, 3, 1, 14, 0, 0);

        bool result = Validation.DateTimeInSequence(startTime, endTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DateTimeInSequence_EndBeforeStart()
    {
        DateTime startTime = new DateTime(2025, 3, 1, 14, 0, 0);
        DateTime endTime = new DateTime(2025, 3, 1, 12, 0, 0);

        bool result = Validation.DateTimeInSequence(startTime, endTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DateTimeInSequence_SameDateTime()
    {
        DateTime dateTime = new DateTime(2025, 3, 1, 12, 0, 0);

        bool result = Validation.DateTimeInSequence(dateTime, dateTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetNumberInput_ValidNumber()
    {
        mockConsole.Setup(c => c.AskInt(It.IsAny<string>())).Returns(10);

        int result = validation.GetNumberInput("Enter a number:");

        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void GetNumberInput_NegativeThenValid()
    {
        var sequence = new Queue<int>(new[] { -5, -2, 10 });
        mockConsole.Setup(c => c.AskInt(It.IsAny<string>())).Returns(() => sequence.Dequeue());

        int result = validation.GetNumberInput("Enter a number:");

        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void GetStringInput_ValidString()
    {
        mockConsole.Setup(c => c.AskString(It.IsAny<string>())).Returns("Test input");

        string result = validation.GetStringInput("Enter a string:");

        Assert.AreEqual("Test input", result);
    }
}
