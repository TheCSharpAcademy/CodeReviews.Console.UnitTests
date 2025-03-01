using Spectre.Console;
namespace CodingTrackerLibrary.Views;

internal static class DataViewer
{
    public static void DisplayListAsTableLive<T>(string[] headers, List<T> data)
    {
        var table = new Table().Centered();
        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                foreach (string header in headers)
                {
                    table.AddColumn(header);
                    ctx.Refresh();
                    Thread.Sleep(350);
                }

                foreach (var item in data)
                {
                    var row = new List<string>();

                    foreach (var header in headers)
                    {
                        var property = typeof(T).GetProperty(header);
                        row.Add(property is null ? "N/A" : property.GetValue(item).ToString());
                    }

                    table.AddRow(row.ToArray());
                    ctx.Refresh();
                    Thread.Sleep(350);
                }
            });
        System.Console.WriteLine();
    }

    public static void DisplayListAsTable<T>(string[] headers, List<T> data)
    {
        var table = new Table();
        
        foreach (string header in headers)
            table.AddColumn(header);

        foreach (var item in data)
        {
            var row = new List<string>();

            foreach (var header in headers)
            {
                var property = typeof(T).GetProperty(header);
                row.Add(property is null ? "N/A" : property.GetValue(item).ToString());
            }

            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
        System.Console.WriteLine();
    }

    public static void DisplayHeader(string header, string justify="center")
    {
        var heading = new Rule($"[red]{header}[/]");
        switch(justify)
        {
            case "left":
                heading.Justification = Justify.Left;
                AnsiConsole.Write(heading);
                break;

            case "right":
                heading.Justification = Justify.Right;
                AnsiConsole.Write(heading);
                break;
            
            default:
                AnsiConsole.Write(heading);
                break;
        }
        System.Console.WriteLine();
    }
}