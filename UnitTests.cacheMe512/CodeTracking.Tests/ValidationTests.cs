using Microsoft.VisualStudio.TestTools.UnitTesting;
using cacheMe512.CodeTracker;
using Moq;

namespace CodeTracking.Tests;

[TestClass]
public class ValidationTests
{
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
 
        var mockConsole = new Mock<IConsoleWrapper>();
        mockConsole.Setup(c => c.AskInt(It.IsAny<string>())).Returns(10);

        var validation = new Validation(mockConsole.Object);


        int result = validation.GetNumberInput("Enter a number:");


        Assert.AreEqual(10, result);
    }
}
