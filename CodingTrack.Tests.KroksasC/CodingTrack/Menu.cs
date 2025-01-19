using Spectre.Console;

namespace CodingTrack
{
    internal class Menu
    {
        public static void GetUserInput()
        {
            bool endApp = true;
            while (endApp)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Markup("[bold green]Welcome to the Coding Tracker![/]").Centered()
                    );
                
                AnsiConsole.WriteLine();
                
                AnsiConsole.MarkupLine("Chose an option: ");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[red]a. Add coding session[/]");
                AnsiConsole.MarkupLine("[yellow]b. View coding sessions[/]");
                AnsiConsole.MarkupLine("[blue]c. Update coding session[/]");
                AnsiConsole.MarkupLine("[purple]d. Delete coding session[/]");

                string? option = Console.ReadLine();

                switch (option) 
                {
                    case "a":
                        DatabaseHelpers.AddCodingSession();
                        break;
                    case "b":
                        DatabaseHelpers.ViewCodingSessions();
                        AnsiConsole.MarkupLine("\n\n[yellow]Press any key to continue![/]");
                        Console.ReadLine();
                        break;
                    case "c":
                        DatabaseHelpers.UpdateCodingSession();
                        break;
                    case "d":
                        DatabaseHelpers.DeleteCodingSession();
                        break;
                    case "e":
                        AnsiConsole.WriteLine("GoodBye");
                        endApp = false;
                        Environment.Exit(0);
                        break;
                    default:
                        AnsiConsole.Clear();
                        AnsiConsole.WriteLine("\n\nInvalid option. Press any key to return to main menu!");
                        Console.ReadLine();
                        break;
                }

            }
            


        }
        
    }
}
