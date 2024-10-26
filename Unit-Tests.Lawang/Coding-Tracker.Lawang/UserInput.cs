using System.Globalization;
using Spectre.Console;

namespace Lawang.Coding_Tracker;

public class UserInput
{
    //Takes user Input -> Validate whether the Inputted time is in "12 hr" format or not
    //(user's input) 12:30 am -> (Valid) Since it is in "12 hr" format it will return DateTime object.
    //(user's input) 14:00 -> (Invalid) Since it is not in "12 hr" format, method will prompt user to input time again.
    public DateTime GetUserTime(Rule? rule = null)
    {
        // Define the expected time formats
        DateTime time;
        //loop until user inputs the right format for time or presses "0" to exit to the menu
        while (true)
        {
            if (rule != null)
                AnsiConsole.Write(rule);

            // Create the panel with the time prompt inside
            var panel = new Panel(new Markup("Please enter a [green]time[/] (e.g., 12:30 [cyan]AM[/] or 02:30 [cyan]PM[/]) in 12 hr format:\n\t\t[grey bold](press '0' to go back to Menu.)[/]"))
                .Header("[bold cyan]Time Input[/]", Justify.Center)
                .Padding(1, 1, 1, 1)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Blue3);

            // Render the panel
            AnsiConsole.Write(panel);

            //Ask the user for time input as a string
            string input = AnsiConsole.Ask<string>("[green]Time[/]: ");

            // Try parse the user input with the 12 hr format
            if(Validator.ValidateUserTime(input))
            {
                time = DateTime.Parse(input);
                break;
            }
            else
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red bold]Invalid time format! Please try again.[/]\n");
                AnsiConsole.MarkupLine("[grey](Tips: Don't Forget to add 'AM' or 'PM' after the input time) [/]\n");
            }

        }
        Console.Clear();
        return time;
    }

    //Takes User Input -> Validates the user Input.
    // "two" (user's input) -> (Invalid) Input is string value, so this method will prompt user to enter the correct value.
    // 235 (user's input) -> (Invalid) If there isn't any record in the database with the given record Id,
    //                      User is propmted again to enter the correct value.
    // 0 (user's input) -> (Valid) Exits to Main Menu.
    public CodingSession GetCodingSession(List<CodingSession> codingSessions, string operation)
    {
        CodingSession? codingSessionRecord;
        do
        {
            //Prompts the user to enter the integer value.
            int Application = AnsiConsole.Ask<int>($"[bold]Enter the Id of the record you want to {operation}: [/]");
            //Returns to the Main menu.
            if (Application == 0)
            {
                throw new ExitOutOfOperationException("");
            }
            codingSessionRecord = codingSessions.FirstOrDefault(session => session.Id == Application);
            if (codingSessionRecord == null)
            {
                AnsiConsole.MarkupLine($"[red underline]Record with specified ID {Application} is not present[/]");
            }
        } while (codingSessionRecord == null);

        return codingSessionRecord;
    }
    public CodingGoals GetCodingGoal(List<CodingGoals> codingGoals)
    {
        CodingGoals? codingGoal;
        do
        {
            //Prompts the user to enter the integer value.
            int Application = AnsiConsole.Ask<int>($"[bold]Enter the Id of the record you want to Delete: [/]");
            //Returns to the Main menu.
            if (Application == 0)
            {
                throw new ExitOutOfOperationException("");
            }
            codingGoal = codingGoals.FirstOrDefault(session => session.Id == Application);
            if (codingGoal == null)
            {
                AnsiConsole.MarkupLine($"[red underline]Record with specified ID {Application} is not present[/]");
            }
        } while (codingGoal == null);

        return codingGoal;
    }
    public Option GetMenuOption()
    {
        AnsiConsole.Write(new Rule("[blue3]Menu Options[/]").LeftJustified().RuleStyle("red"));

        //All the main menu options 
        List<Option> options = new List<Option>()
        {
            new Option() {Display = "View all the Records.", SelectedValue = 1},
            new Option() {Display = "Add a Record.", SelectedValue = 2},
            new Option() {Display = "Update a Record.", SelectedValue = 3},
            new Option() {Display = "Delete a Record.", SelectedValue = 4},
            new Option() {Display = "Use Timer to record session", SelectedValue = 6},
            new Option() {Display = "Filter the Records and Show the Report", SelectedValue = 7},
            new Option() {Display = "Manage Goals", SelectedValue = 8},
            new Option() {Display = "Exit the Application.", SelectedValue = 0}


        };

        //for showing the user all the menu options which user can select from.
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Option>()
            .Title("\n[bold cyan underline]What [green]operation[/] do you want to perform?[/]\n")
            .UseConverter<Option>(c => c.Display)
            .MoreChoicesText("[grey](Press 'up' and 'down' key to navigate.[/])")
            .AddChoices(options)
            .HighlightStyle(Color.Blue3)
            .WrapAround()
        );

        return selection;
    }
    public Option GetGoalOption()
    {
        AnsiConsole.Write(new Rule("[blue3]Goal Options[/]").LeftJustified().RuleStyle("red"));

        //All the Goal options

        List<Option> options = new List<Option>()
        {
            new Option() {Display = "Show all Goals", SelectedValue = 1},
            new Option() {Display = "Set Goals", SelectedValue = 2},
            new Option() {Display = "Delete Goal", SelectedValue = 3},
            new Option() {Display = "Exit", SelectedValue = 0}
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Option>()
            .Title("\n[bold cyan underline]What [green]OPTION[/] do you want to choose?[/]\n")
            .UseConverter<Option>(c => c.Display)
            .MoreChoicesText("[grey(Press 'up' and 'down' key to navigate.[/])]")
            .AddChoices(options)
            .HighlightStyle(Color.Aqua)
            .WrapAround()
        );

        return selection;
    }

    public Option GetFilterOption()
    {
        AnsiConsole.Write(new Rule("[blue3]Filter Options[/]").LeftJustified().RuleStyle("red"));

        //All the filter options
        List<Option> options = new List<Option>()
        {
            new Option() {Display = "Today", SelectedValue = 1},
            new Option() {Display = "Yesterday", SelectedValue = 2},
            new Option() {Display = "Last 7 days", SelectedValue = 3},
            new Option() {Display = "Last 14 days", SelectedValue = 4},
            new Option() {Display = "This Month", SelectedValue = 5},
            new Option() {Display = "This Year", SelectedValue = 6},
            new Option() {Display = "Last Year", SelectedValue = 7},
            new Option() {Display = "In Ascending Order", SelectedValue = 8},
            new Option() {Display = "In Descending Order", SelectedValue = 9},
            new Option() {Display = "Exit", SelectedValue = 0}

        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Option>()
            .Title("\n[bold cyan underline]What [green]filter[/] do you want to choose?[/]\n")
            .UseConverter<Option>(c => c.Display)
            .MoreChoicesText("[grey(Press 'up' and 'down' key to navigate.[/])]")
            .AddChoices(options)
            .HighlightStyle(Color.Aqua)
            .WrapAround()
        );
        return selection;
    }

    public TimeSpan SetTimeDuration()
    {
        AnsiConsole.MarkupLine("[green bold]Write the Total amount of time for your [cyan1]coding goals[/][/]");
        AnsiConsole.MarkupLine("[grey](press '0' to to go back to menu)[/]");
        AnsiConsole.Markup("[grey]Enter the time in format (D.hh:mm:ss), where 'D' is day, 'hh' is hour, 'mm' is minutes and 'ss' is seconds[/] (eg. 12:00:00, 1.10:30:00): ");
        string? Application = Console.ReadLine()?.Trim();
        TimeSpan totalCodingTime;

        do
        {
            if(Validator.ValidateTimeDuration(Application ?? ""))
            {
                totalCodingTime = TimeSpan.Parse(Application ?? "");
                return totalCodingTime;
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please enter the time in correct format[/]: (eg. 23:12:24, 12:00:00)");
                Application = Console.ReadLine()?.Trim();
            }
        }while(true);

    }

    public DateTime GetStartDate()
    {
        AnsiConsole.MarkupLine("[green bold]Give the Starting Date for your [cyan1]Coding Goals[/]?.[/]");
        AnsiConsole.MarkupLine("[grey](press '0' to go back to menu)[/]");
        AnsiConsole.Markup("[grey]Enter the date in format (dd/MM/yy) where 'dd' is day, 'MM' is month, 'yy' is year (eg.22/12/24, 10/09/22): [/]");
        string? Application = Console.ReadLine()?.Trim();

        DateTime startingDate;

        do
        {
            if(Validator.ValidateDate(Application ??""))
            {
                startingDate = DateTime.Parse(Application ?? "");
                return startingDate;
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please enter the date in correct format[/]: (eg. 02/05/24, 12/09/11)");
                Application = Console.ReadLine()?.Trim();
            }
        }while(true);
    }

    public DateTime ValidateSessionDate()
    {
        AnsiConsole.MarkupLine("[green bold]Give the Date for your [cyan1]Coding Session[/]?.[/]");
        AnsiConsole.MarkupLine("[grey](press '0' to go back to menu)[/]");
        AnsiConsole.Markup("[grey]Enter the date in format (dd/MM/yy) where 'dd' is day, 'MM' is month, 'yy' is year (eg.22/12/24, 10/09/22): [/]");
        string? Application = Console.ReadLine()?.Trim();

        DateTime startingDate;

        do
        {
            if(Validator.ValidateDate(Application ?? ""))
            {
                startingDate = DateTime.Parse(Application ?? "");
                return startingDate;
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please enter the date in correct format[/]: (eg. 02/05/24, 12/09/11)");
                Application = Console.ReadLine()?.Trim();
            }
        }while(true);
    }

}
