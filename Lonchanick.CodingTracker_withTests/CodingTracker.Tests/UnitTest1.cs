using Lonchanick.CodingTracker;

namespace CodingTracker.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

[TestClass]
public class DateTimeInputTests
{
    private StringWriter stringWriter;
    private TextWriter originalConsoleOut;

    [TestInitialize]
    public void Initialize()
    {
        stringWriter = new StringWriter();
        originalConsoleOut = Console.Out;
        Console.SetOut(stringWriter);
    }

    [TestCleanup]
    public void Cleanup()
    {
        Console.SetOut(originalConsoleOut);
        stringWriter.Dispose();
    }

    [TestMethod]
    public void GetValidDateTime_ValidInput_ReturnsDateTime()
    {
        string userInput = "2024-04-26";
        DateTime expectedDateTime = new DateTime(2024, 04, 26);

        using (StringReader stringReader = new StringReader(userInput))
        {
            Console.SetIn(stringReader);

            DateTime result = Controller.GetValidDateTime("Enter a valid date");

            Assert.AreEqual(expectedDateTime, result);
        }

        string expectedConsoleOutput = "Enter a valid date: ";
        Assert.AreEqual(expectedConsoleOutput, stringWriter.ToString());
    }

    [TestMethod]
    public void GetValidInteger_ValidInput_ReturnsInteger()
    {
        int userInput = 22;
        int expectedInteger = 22;

        using (StringReader stringReader = new StringReader(userInput.ToString()))
        {
            Console.SetIn(stringReader);

            int result = Controller.GetValidInteger("Enter a valid integer");

            Assert.AreEqual(expectedInteger, result);
        }
    }
 
}
