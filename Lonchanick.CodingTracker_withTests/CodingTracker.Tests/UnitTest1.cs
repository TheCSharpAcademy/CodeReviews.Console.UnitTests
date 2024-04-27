using Lonchanick.CodingTracker;

namespace CodingTracker.Tests;


//[TestClass]
//public class UnitTest1
//{
//    [TestMethod]
//    public void TestMethod1()
//    {
//    }
//}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

[TestClass]
public class DateTimeInputTests
{
    // Define a StringWriter to capture console output
    private StringWriter stringWriter;
    private TextWriter originalConsoleOut;

    [TestInitialize]
    public void Initialize()
    {
        // Redirect Console.Out to capture console output
        stringWriter = new StringWriter();
        originalConsoleOut = Console.Out;
        Console.SetOut(stringWriter);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Restore Console.Out
        Console.SetOut(originalConsoleOut);
        stringWriter.Dispose();
    }

    [TestMethod]
    public void GetValidDateTime_ValidInput_ReturnsDateTime()
    {
        // Arrange
        string userInput = "2024-04-26";
        DateTime expectedDateTime = new DateTime(2024, 04, 26);

        // Mock user input
        using (StringReader stringReader = new StringReader(userInput))
        {
            Console.SetIn(stringReader);

            // Act
            //DateTime result = DateTimeInput.GetValidDateTime("Enter a valid date");
            DateTime result = Controller.GetValidDateTime("Enter a valid date");

            // Assert
            Assert.AreEqual(expectedDateTime, result);
        }

        // Verify console output
        string expectedConsoleOutput = "Enter a valid date: ";
        Assert.AreEqual(expectedConsoleOutput, stringWriter.ToString());
    }

    [TestMethod]
    public void GetValidDateTime_InvalidInputThenValidInput_ReturnsValidDateTime()
    {
        // Arrange
        string[] userInputs = { "invalid date", "2024-04-26" };
        DateTime expectedDateTime = new DateTime(2024, 04, 26);

        // Mock user input
        using (StringReader stringReader = new StringReader(string.Join(Environment.NewLine, userInputs)))
        {
            Console.SetIn(stringReader);

            // Act
            //DateTime result = DateTimeInput.GetValidDateTime("Enter a valid date");
            DateTime result = Controller.GetValidDateTime("Enter a valid date");

            // Assert
            Assert.AreEqual(expectedDateTime, result);
        }

        // Verify console output
        string expectedConsoleOutput = "Enter a valid date: Enter a valid date: ";
        Assert.AreEqual(expectedConsoleOutput, stringWriter.ToString());
    }
}
