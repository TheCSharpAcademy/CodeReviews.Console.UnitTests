using CodingTrackerLibrary.Views;
using Spectre.Console.Testing; 
using Spectre.Console;
using Spectre.Console.Rendering;
using CodingTrackerLibrary.Models;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace CodingTrackerTests;
[TestClass]
public class DataViewerTest
{
    [TestMethod]
    public void Test_DisplayListAsTableLive_WithEmptyData()
    {
        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayListAsTableLive<String>([], []);

        String consoleOutput = console.Output;
        console.Dispose();

        Spectre.Console.Table table = new Table().Centered();
        String expected = RenderSpectreConsoleComponent(table);

        Assert.AreEqual(expected, consoleOutput);
    }

    //public CodingSession(int id, string startDate, string endDate, float duration, string units)

    [TestMethod]
    public void Test_DisplayListAsTableLive_WithSomeData()
    {
        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        // Data doesn't need to be accurate, just testing displaying works properly
        List<CodingSession> sessions = new()
        {
            new CodingSession(0, "20-02-02", "2022-4-4", 50, "test untis" ),
            new CodingSession(4, "2035-05-02", "2022-234-11", 15, "test untis" ),
            new CodingSession(55, "2011-07-02", "2022-24-66", 2222, "test untis" ),
            new CodingSession(232, "2022-88-02", "2022-01-4", 1, "test untis" ),
            new CodingSession(1, "2023-22-02", "2022-02-34", 18, "test untis" ),
        };


        DataViewer.DisplayListAsTableLive<CodingSession>(CodingSession.headers, sessions);
        string consoleOutput = console.Output;
        console.Dispose();
       
        // building the expected table
        Spectre.Console.Table table = new Table().Centered();
        string[] headers  = { "Id", "StartDate", "EndDate", "Duration", "Units" };
        List<List<string>> rows = new List<List<string>>()
        {
            new List<string>() { "0"    , "20-02-02"  , "2022-4-4"   , "50"  , "test untis" },
            new List<string>() { "4"    , "2035-05-02", "2022-234-11", "15"  , "test untis" },
            new List<string>() { "55"   , "2011-07-02", "2022-24-66" , "2222", "test untis" },
            new List<string>() { "232"  , "2022-88-02", "2022-01-4"  , "1"   , "test untis" },
            new List<string>() { "1"    , "2023-22-02", "2022-02-34" , "18"  , "test untis" },
        };

        string expectedOutput = RenderSpectreConsoleTableLive
            (
                table, 
                headers, 
                rows
            );

        Assert.AreEqual(expectedOutput, consoleOutput);

    }

    [TestMethod]
    public void Test_DisplayListAsTable_WithEmptyData()
    {
        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayListAsTable<string>([], []);
        string consoleOutput = console.Output;
        
        console.Dispose();
        Table table = new Table();
        string expected = RenderSpectreConsoleComponent(table);

        Assert.AreEqual(expected, consoleOutput);
    }

    [TestMethod]
    public void Test_DisplayListAsTable_WithSomeData()
    {
        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        List<CodingSession> sessions = new()
        {
            new CodingSession(0, "20-02-02", "2022-4-4", 50, "test untis" ),
            new CodingSession(4, "2035-05-02", "2022-234-11", 15, "test untis" ),
            new CodingSession(55, "2011-07-02", "2022-24-66", 2222, "test untis" ),
            new CodingSession(232, "2022-88-02", "2022-01-4", 1, "test untis" ),
            new CodingSession(1, "2023-22-02", "2022-02-34", 18, "test untis" ),
        };
        string[] headers = { "Id", "StartDate", "EndDate", "Duration", "Units" };

        DataViewer.DisplayListAsTable<CodingSession>(CodingSession.headers, sessions);
        string consoleOutput = console.Output;

        console.Dispose();

        Table table = new Table();

        foreach(string header in headers)
            table.AddColumn(header);

        List<List<string>> rows = new List<List<string>>()
        {
            new List<string>() { "0"    , "20-02-02"  , "2022-4-4"   , "50"  , "test untis" },
            new List<string>() { "4"    , "2035-05-02", "2022-234-11", "15"  , "test untis" },
            new List<string>() { "55"   , "2011-07-02", "2022-24-66" , "2222", "test untis" },
            new List<string>() { "232"  , "2022-88-02", "2022-01-4"  , "1"   , "test untis" },
            new List<string>() { "1"    , "2023-22-02", "2022-02-34" , "18"  , "test untis" },
        };

        foreach (List<string> row in rows)
            table.AddRow(row.ToArray());

        string expected = RenderSpectreConsoleComponent(table);
        Assert.AreEqual(expected, consoleOutput);

    }


    [TestMethod]
    public void Test_DisplayHeader_JustifyLeft()
    {
        string header = "test";
        string justify = "left";

        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayHeader(header, justify);

        string consoleOutput = console.Output;
        console.Dispose();

        Rule heading = new($"[red]{header}[/]");
        heading.Justification = Justify.Left;

        string expected = RenderSpectreConsoleComponent(heading);
        Assert.AreEqual(expected, consoleOutput);
    }


    [TestMethod]
    public void Test_DisplayHeader_JustifyRight()
    {
        string header = "test";
        string justify = "right";

        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayHeader(header, justify);

        string consoleOutput = console.Output;
        console.Dispose();

        Rule heading = new($"[red]{header}[/]");
        heading.Justification = Justify.Right;

        string expected = RenderSpectreConsoleComponent(heading);
        Assert.AreEqual(expected, consoleOutput);
    }


    [TestMethod]
    public void Test_DisplayHeader_JustifyCenter()
    {
        string header = "test";
        string justify = "center";

        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayHeader(header, justify);

        string consoleOutput = console.Output;
        console.Dispose();

        Rule heading = new($"[red]{header}[/]");
        heading.Justification = Justify.Center;

        string expected = RenderSpectreConsoleComponent(heading);
        Assert.AreEqual(expected, consoleOutput);
    }

    [TestMethod]
    public void Test_DisplayHeader_DefaultJustify_ShouldBeCenter()
    {
        string header = "test";

        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayHeader(header);

        string consoleOutput = console.Output;
        console.Dispose();

        Rule heading = new($"[red]{header}[/]");
        heading.Justification = Justify.Center;

        string expected = RenderSpectreConsoleComponent(heading);
        Assert.AreEqual(expected, consoleOutput);
    }

    [TestMethod]
    public void Test_DisplayHeader_EmptyHeader()
    {
        string header = "";

        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        DataViewer.DisplayHeader(header);

        string consoleOutput = console.Output;
        console.Dispose();

        Rule heading = new($"[red]{header}[/]");
        heading.Justification = Justify.Center;

        string expected = RenderSpectreConsoleComponent(heading);
        Assert.AreEqual(expected, consoleOutput);
    }


    private string RenderSpectreConsoleComponent(IRenderable renderable)
    {
        Spectre.Console.Testing.TestConsole console = new();
        AnsiConsole.Console = console;

        AnsiConsole.Write(renderable);
        string consoleOutput = console.Output;
        
        console.Dispose();
        return consoleOutput;
    }

    private string RenderSpectreConsoleTableLive(Table table, string[] headers, List<List<string>> rows)
    {
        Spectre.Console.Testing.TestConsole console = new();
        
        AnsiConsole.Console = console;
        AnsiConsole.Live(table).Start(ctx =>
        {
            foreach(string header in headers)
            {
                table.AddColumn(header);
                ctx.Refresh();
                Thread.Sleep(350);
            }

            foreach(List<string> row in rows)
            {
                table.AddRow(row.ToArray());
                ctx.Refresh();
                Thread.Sleep(350);
            }
        });

        string consoleOutput = console.Output;
        console.Dispose();

        return consoleOutput;
    }
}
