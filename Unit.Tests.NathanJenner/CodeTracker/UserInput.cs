using Coding_Tracking_Application.DataModels;
using Coding_Tracking_Application.Services;
using Spectre.Console;

namespace Coding_Tracking_Application;

public class UserInput
{
    public static string MainMenuOptions()
    {
        AnsiConsole.Markup("[springgreen4]Welcome to the Coding Tracker!\nPlease select from the following options\n\n[/]");
        AnsiConsole.Markup(
            "[turquoise4]\t - Type 0 to Close the application.\n" +
            "\t - Type 1 to View your Coding Session Info.\n" +
            "\t - Type 2 to Create your Coding Tracker.\n" +
            "\t - Type 3 to Delete your Coding Tracker.\n" +
            "\t - Type 4 to Add Coding Session.\n[/]" +
            "\n--------------------------------------------------------\n\n"
            );

        string userMenuInput = Console.ReadLine();
        return userMenuInput;
    }

    public static void CreateEntryInput()
    {
        AnsiConsole.Markup("[springgreen4]** LETS GET CODING! **[/]");
        AnsiConsole.Markup("[turquoise4]\n\nPlease enter your first coding start and end time & date (in the following format - 00:00:00 31/07/2024)\n\n[/]");
        AnsiConsole.Markup("Start time & date: ");
        string startTime = Console.ReadLine();

        Thread.Sleep(1000); //adding 1sec pause for UX

        AnsiConsole.Markup("\nEnd time & date: ");
        string endTime = Console.ReadLine();

        Thread.Sleep(1000); //adding 1sec pause for UX

        Tuple<DateTime, bool> startTimeTuple = ValidationServices.ParseInputDateTime(startTime);
        Tuple<DateTime, bool> endTimeTuple = ValidationServices.ParseInputDateTime(endTime);

        if ((startTimeTuple.Item2 && endTimeTuple.Item2) && ValidationServices.StartDateLessThanEndDate(startTimeTuple.Item1, endTimeTuple.Item1))
        {
            CodingSession session = new CodingSession();
            session.StartTime = startTimeTuple.Item1;
            session.EndTime = endTimeTuple.Item1;

            TimeSpan timeAndDateDifference = endTimeTuple.Item1 - startTimeTuple.Item1;
            session.CodingTime = timeAndDateDifference.ToString();

            DatabaseServices.CreateEntry(session);
            AnsiConsole.Markup("[green]\n\nYour coding entry has been added.\n\n\n[/]");
            Worker.Execute();
        }
        else
        {
            AnsiConsole.Markup("[red]\n\nPlease re-enter your Start and End date & time with the correct format and/or ensure End date & time are not before Start date & time. .\n\n\n\n[/]");
            CreateEntryInput();
        }
    }

    public static void AddSessionInput()
    {
        AnsiConsole.Markup("[turquoise4]\n\nPlease enter details of your latest coding session (in the following format - 00:00:00 31/07/2024)\n\n[/]");
        AnsiConsole.Markup("Start time & date: ");
        string startTime = Console.ReadLine();

        Thread.Sleep(1000); //adding 1sec pause for UX

        AnsiConsole.Markup("\nEnd time & date: ");
        string endTime = Console.ReadLine();

        Thread.Sleep(1000); //adding 1sec pause for UX

        Tuple<DateTime, bool> startTimeTuple = ValidationServices.ParseInputDateTime(startTime);
        Tuple<DateTime, bool> endTimeTuple = ValidationServices.ParseInputDateTime(endTime);

        if ((startTimeTuple.Item2 && endTimeTuple.Item2) && ValidationServices.StartDateLessThanEndDate(startTimeTuple.Item1, endTimeTuple.Item1))
        {
            CodingSession session = new CodingSession();
            session.StartTime = startTimeTuple.Item1;
            session.EndTime = endTimeTuple.Item1;

            TimeSpan timeAndDateDifference = endTimeTuple.Item1 - startTimeTuple.Item1;
            session.CodingTime = timeAndDateDifference.ToString();

            DatabaseServices.CreateEntry(session);
            AnsiConsole.Markup("[green]\n\nYour coding entry has been added.\n\n\n[/]");
            Worker.Execute();
        }
        else
        {
            AnsiConsole.Markup("[red]\n\nPlease re-enter your Start and End date & time with the correct format and/or ensure End date & time are not before Start date & time. .\n\n\n\n[/]");
            AddSessionInput();
        }
    }
}
