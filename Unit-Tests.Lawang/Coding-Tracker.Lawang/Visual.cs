using Spectre.Console;

namespace Lawang.Coding_Tracker;

public class Visual
{
    public void TitlePanel(string title)
    {
        //To show the project title "Coding Tracker" in figlet text
        var titlePanel = new Panel(new FigletText($"{title}").Color(Color.Red))
                        .BorderColor(Color.Aquamarine3)
                        .PadTop(1)
                        .PadBottom(1)
                        .Header(new PanelHeader("[blue3 bold]APPLICATION[/]"))
                        .Border(BoxBorder.Double)
                        .Expand();


        AnsiConsole.Write(titlePanel);
    }
    public void RenderTable(List<CodingSession> codingSessions)
    {
        var table = new Table()
                    .Border(TableBorder.Rounded)
                    .Expand()
                    .BorderColor(Color.Aqua);

        table.ShowRowSeparators = true;

        table.AddColumns(new TableColumn[]
            {
                 new TableColumn("[green]ID[/]").Centered(),
                 new TableColumn("[cyan3]Start-Time[/]").Centered(),
                 new TableColumn("[deeppink4_2]End-Time[/]").Centered(),
                 new TableColumn("[darkolivegreen2]Duration[/]").Centered(),
                 new TableColumn("[bold]Date[/]").Centered()
            });

        foreach (var codingSession in codingSessions)
        {
            table.AddRow(
                new Markup($"[green]{codingSession.Id}[/]").Centered(),
                new Markup($"[cyan3]{codingSession.StartTime.ToString("hh:mm tt")}[/]").Centered(),
                new Markup($"[deeppink4_2]{codingSession.EndTime.ToString("hh:mm tt")}[/]").Centered(),
                new Markup($"[darkolivegreen2]{codingSession.Duration.ToString()}[/]").Centered(),
                new Markup($"[bold]{codingSession.Date.ToString("D")}[/]").Centered()
            );
        }

        AnsiConsole.Write(table);
    }

    public void RenderCodingGoals(List<CodingGoals> codingGoals)
    {
        if(codingGoals == null)
        {
            return;
        }
        var table = new Table()
                    .Border(TableBorder.Rounded)
                    .Expand()
                    .BorderColor(Color.Aqua);

        table.ShowRowSeparators = true;

        table.AddColumns(new TableColumn[]
            {
                 new TableColumn("[green]ID[/]").Centered(),
                 new TableColumn("[cyan3]Time_To_Complete[/]").Centered(),
                 new TableColumn("[deeppink4_2]Average_Time_To_Code[/]").Centered(),
                 new TableColumn("[darkolivegreen2]Days_Left[/]").Centered(),
            });

        foreach (var codingGoal in codingGoals)
        {
            table.AddRow(
                new Markup($"[green]{codingGoal.Id}[/]").Centered(),
                new Markup($"[cyan3]{codingGoal.Time_to_complete}[/]").Centered(),
                new Markup($"[deeppink4_2]{codingGoal.Avg_Time_To_Code}[/]").Centered(),
                new Markup($"[darkolivegreen2]{codingGoal.Days_left}[/]").Centered()
            );
        }

        AnsiConsole.Write(table);
    }

    public void RenderResult(int rowsAffected)
    {
        if (rowsAffected == 1)
        {
            ShowResult("green", rowsAffected);
        }
        else
        {
            ShowResult("red", rowsAffected);
        }
    }

    private void ShowResult(string color, int rowsAffected)
    {
        Panel panel = new Panel(new Markup($"[{color} bold]{rowsAffected} rows Affected[/]\n[grey](Press 'Enter' to Continue.)[/]"))
                        .Padding(1, 1, 1, 1)
                        .Header("Result")
                        .Border(BoxBorder.Rounded);

        AnsiConsole.Write(panel);
        Console.ReadLine();
    }

    public void ShowReport(string time, string timeSpan, string color)
    {

        // draw panel
        Panel panel = new Panel(new Markup($"[{color} bold]{time}[/]: {timeSpan}"))
                        .Padding(1, 1, 1, 1)
                        .Header("TIME")
                        .Border(BoxBorder.Rounded)
                        .Expand();

        AnsiConsole.Write(panel);
    }
}
