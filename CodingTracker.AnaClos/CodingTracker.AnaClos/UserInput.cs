using Spectre.Console;

namespace AnaClos.CodingTracker;

public class UserInput
{
    public string Menu()
    {
        string choice = string.Empty;

        Console.Clear();

        AnsiConsole.MarkupLine("[bold blue]Coding Session Menu[/]\n");
        AnsiConsole.MarkupLine("[bold red](I)[/] [blue]Insert Coding Session[/]");
        AnsiConsole.MarkupLine("[bold red](D)[/] [blue]Delete Coding Session[/]");
        AnsiConsole.MarkupLine("[bold red](U)[/] [blue]Update Coding Session[/]");
        AnsiConsole.MarkupLine("[bold red](V)[/] [blue]View All Coding Sessions[/]");
        AnsiConsole.MarkupLine("[bold red](Q)[/] [blue]Quit[/]");
        AnsiConsole.MarkupLine("[bold blue]-----------------------------[/]\n");

        choice = AnsiConsole.Prompt(new TextPrompt<string>($@"[bold blue]Select your choice[/]"));
        choice = choice.ToLower().Trim();
        return choice;
    }
    
    public string GetInput(string message)
    {
        string response = string.Empty;

        response = AnsiConsole.Prompt(new TextPrompt<string>($@"[bold blue]{message} Type 'R' to return to main menu.[/]"));
        response = response.ToLower().Trim();
        return response;
    }   
}