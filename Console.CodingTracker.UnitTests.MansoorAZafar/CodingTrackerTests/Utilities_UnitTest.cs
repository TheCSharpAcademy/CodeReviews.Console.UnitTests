using CodingTrackerLibrary.Controllers;

namespace CodingTrackerTests;

[TestClass]
public class Utilities_UnitTest
{

    [TestMethod]
    public void Test_CalculateDurationBetweenStartAndEndTime()
    {
        // Testing DateTime Duration Calculator, DateTime is put in (yyyy-MM-dd-HH) format
        DateTime start = new DateTime(2004, 02, 02, 02, 0, 0), end = new DateTime(2004, 02, 08, 15, 0, 0);
        double expectedDuration = 157; // Expects 157 Hrs

        Assert.AreEqual(
            Utilities.CalculateDurationBetweenStartAndEndTime(
                start, 
                end).TotalHours, 
            expectedDuration);

        end = end.AddYears(20); // add 20 years
        expectedDuration = 175477; // Expects this many hrs

        Assert.AreEqual(
            Utilities.CalculateDurationBetweenStartAndEndTime(
                start,
                end).TotalHours,
            expectedDuration
        );


        end = end.AddHours(5000);
        double BadDuration = 175477;
        Assert.AreNotEqual(
            Utilities.CalculateDurationBetweenStartAndEndTime(
                    start,
                    end).TotalHours,
            BadDuration
        );

    }

    [TestMethod]
    public void Test_GetValidDateInyyMMddHHFormatWithBadYear_ShouldPrintErrorMessageInConsole()
    {
        //Setup for capturing User Input
        //  - Need a Good Input to end it
        StringReader reader = new StringReader("bad Input\n-2000-15-15\n2000-02-15-00\n");
        System.Console.SetIn(reader);

        // Capturing Console Output
        StringWriter writer = new StringWriter();
        System.Console.SetOut(writer);

        DateTime dateTime = new();
        Utilities.GetValidDateInyyMMddHHFormat(ref dateTime);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        DateTime expectedDateTime = new(2000, 02, 15, 00, 00, 00);
        const String expectedOutput = "> Invalid Date, Please enter in yy-MM-dd Format\n> Invalid Date, Please enter in yy-MM-dd Format\n> ";
        Assert.AreEqual( expectedDateTime, dateTime );
        Assert.AreEqual( expectedOutput, consoleOutput );

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidDateInyyMMddHHFormat_Should_NOT_PrintErrorMessage()
    {
        StringReader reader = new StringReader("2025-12-24-15\n");
        System.Console.SetIn(reader);

        StringWriter writer = new StringWriter();
        System.Console.SetOut(writer);

        DateTime dateTime = new();
        DateTime expectedDateTime = new(2025, 12, 24, 15, 00, 00);

        Utilities.GetValidDateInyyMMddHHFormat(ref dateTime);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();
        

        Assert.AreEqual( "> ", consoleOutput );
        Assert.AreEqual( expectedDateTime, dateTime );
        
        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidStringInput_ShouldPrintErrorMessageInConsole()
    {
        //Setup for capturing User Input
        //  - Need a Good Input to end it
        StringReader reader = new StringReader("\nThis one should pass\n");
        System.Console.SetIn(reader);

        // Capturing Console Output
        StringWriter writer = new StringWriter();
        System.Console.SetOut(writer);

        String input = "";
        Utilities.GetValidStringInput(ref input);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        const String expectedOutput = "> Invalid Input\n Please Enter the data\n> ";
        Assert.AreEqual("This one should pass", input);
        Assert.AreEqual(expectedOutput, consoleOutput);

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidStringInput_Should_NOT_PrintErrorMessage()
    {
        StringWriter writer = new();
        System.Console.SetOut(writer);

        StringReader reader = new("This test should print no Error in the console\n");
        System.Console.SetIn(reader);

        String? input = "";
        Utilities.GetValidStringInput(ref input);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        Assert.AreEqual( "This test should print no Error in the console", input );
        Assert.AreEqual( "> ", consoleOutput );

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidIntegerInput_ShouldPrintErrorMessageInConsole()
    {
        const int expectedNumber = 2;

        StringWriter writer = new();
        System.Console.SetOut(writer);

        StringReader reader = new($"\nabc\n{expectedNumber}\n");
        System.Console.SetIn(reader);

        int number = 0;
        Utilities.GetValidIntegerInput(ref number);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        Assert.AreEqual(expectedNumber, number);
        Assert.AreEqual( "> Invalid Value\n> Invalid Value\n> ", consoleOutput );

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidIntegerInput_Should_NOT_PrintErrorMessage()
    {
        const int expectedNumber = 2;
        StringWriter writer = new();
        System.Console.SetOut(writer);

        StringReader reader = new($"{expectedNumber}\n");
        System.Console.SetIn(reader);

        int input = 0;
        Utilities.GetValidIntegerInput(ref input);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        Assert.AreEqual(expectedNumber, input);
        Assert.AreEqual( "> ", consoleOutput );

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidIntegerInput_WithRange_ShouldPrintErrorMessageInConsole()
    {
        const int expectedNumber = 18;
        StringWriter writer = new();
        System.Console.SetOut(writer);

        StringReader reader = new($"abc\n14\n21\n{expectedNumber}\n");
        System.Console.SetIn(reader);

        int input = 0;
        Utilities.GetValidIntegerInput(ref input, lowRange: 15, maxRange: 20);

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        Assert.AreEqual( expectedNumber, input );
        Assert.AreEqual("> Invalid Value\n> Invalid Value\n> Invalid Value\n> ", consoleOutput );

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetValidIntegerInput_WithRangeAndCustomErrorMessage_ShouldPrintErrorMessageInConsole()
    {
        const int expectedNumber = 18;
        StringWriter writer = new();
        System.Console.SetOut(writer);

        StringReader reader = new($"abc\n14\n21\n{expectedNumber}\n");
        System.Console.SetIn(reader);

        int input = 0;
        Utilities.GetValidIntegerInput(
            ref input, 
            lowRange: 15, 
            maxRange: 20,
            errorMessage: "Not in Range or Invalid Input\n> "
        );

        writer.Flush();
        String consoleOutput = writer.ToString();
        writer.DisposeAsync();

        Assert.AreEqual(expectedNumber, input);
        Assert.AreEqual("> Not in Range or Invalid Input\n> Not in Range or Invalid Input\n> Not in Range or Invalid Input\n> ", consoleOutput);

        reader.Dispose();
    }

    [TestMethod]
    public void Test_GetConnectionString()
    {
        const string testConfigurationFile = "appsettings.json";

        //Create the temporary appsettings.json file
        File.WriteAllText(testConfigurationFile, 
            "{ \"ConnectionString\": \"TestConnectionString\" }");

        try
        {
            String? connectionString = Utilities.GetConnectionString();
            Assert.AreEqual("TestConnectionString", connectionString);

        } finally
        {
            if(File.Exists(testConfigurationFile))
            {
                File.Delete(testConfigurationFile);
            }
        }
    }

    [TestMethod]
    public void Test_GetConnectionString_WrongResult()
    {
        const string testConfigurationFile = "appsettings.json";
        File.WriteAllText(testConfigurationFile,
            "{\"ConnectionString\": \"TestConnectionString\"}");

        try
        {
            String? connectionString = Utilities.GetConnectionString();
            Assert.AreNotEqual("wrong connection string", connectionString);
        } finally
        {
            if(File.Exists(testConfigurationFile))
            {
                File.Delete(testConfigurationFile);
            }
        }
    }

}