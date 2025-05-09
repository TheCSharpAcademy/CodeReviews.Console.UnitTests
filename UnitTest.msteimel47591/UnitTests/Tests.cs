using CodingTracker.Models;
using Moq;
using System.Reflection;


namespace CodingTracker.Tests;

[TestClass]
[DoNotParallelize]
public sealed class InputValidationTests
{
    private TextReader _originalConsoleIn;


    [TestInitialize]
    public void TestInitialize()
    {
        _originalConsoleIn = Console.In;
        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "true");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Console.SetIn(_originalConsoleIn);
        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
    }

    [TestMethod]
    public void GetDateTimeInput_CorrectInput()
    {
        var input = "01/01/21 09:00 AM";
        var expectedOutput = "01/01/21 09:00 AM";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetDateTimeInput("start time");

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetDateTimeInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetDateTimeInput("start time");
        }
    }

    [TestMethod]
    public void GetDateTimeInput_IncorrectThenCorrectInput()
    {
        var inputs = "01-01-21 09:00 AM\n01/01/21 09:00 AM";
        var expectedOutput = "01/01/21 09:00 AM";

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetDateTimeInput("start time");

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    public void GetFocusInput_CorrectInput()
    {
        var inputs = "C#";
        var expectedOutput = "C#";

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetFocusInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetFocusInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetFocusInput();
        }
    }

    [TestMethod]
    public void GetFocusInput_IncorrectThenCorrectInput()
    {
        var inputs = "\nC#";
        var expectedOutput = "C#";

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetFocusInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetSessionIDInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetSessionIdInput();
        }
    }

    [TestMethod]
    public void GetSessionIdInput_ValidId_ReturnsId()
    {
        var mockDBAccessWrapper = new Mock<IDBAccessWrapper>();
        mockDBAccessWrapper.Setup(db => db.GetAllSessions()).Returns(new List<CodingSession>
            {
                new CodingSession { Id = 1, StartTime = "01/01/21 09:00 AM", EndTime = "01/01/21 10:00 AM", Focus = "C#" }
            });

        var helperType = typeof(Helper);
        var dbAccessWrapperField = helperType.GetField("_dbAccessWrapper", BindingFlags.NonPublic | BindingFlags.Static);
        dbAccessWrapperField.SetValue(null, mockDBAccessWrapper.Object);

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            using (var sr = new StringReader("1"))
            {
                Console.SetIn(sr);

                int result = Helper.GetSessionIdInput();

                Assert.AreEqual(1, result);
            }
        }
    }

    [TestMethod]
    public void GetDateInput_CorrectInput()
    {
        var input = "01/01/21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetDateInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetDateInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetDateInput();
        }
    }

    [TestMethod]
    public void GetDateInput_IncorrectThenCorrectInput()
    {
        var inputs = "01-01-21\n01/01/21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetDateInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    public void GetMonthAndYearInput_CorrectInput()
    {
        var input = "01/21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetMonthAndYearInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetMonthAndYearInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetMonthAndYearInput();
        }
    }

    [TestMethod]
    public void GetMonthAndYearInput_IncorrectThenCorrectInput()
    {
        var inputs = "01-21\n01/21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetMonthAndYearInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    public void GetYearInput_CorrectInput()
    {
        var input = "21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetYearInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MenuExitException))]
    public void GetYearInput_ExitInput()
    {
        var input = "0";

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            Helper.GetYearInput();
        }
    }

    [TestMethod]
    public void GetYearInput_IncorrectThenCorrectInput()
    {
        var inputs = "2021\n21";
        var expectedOutput = new DateTime(2021, 01, 01);

        using (var stringReader = new StringReader(inputs))
        {
            Console.SetIn(stringReader);

            var result = Helper.GetYearInput();

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
