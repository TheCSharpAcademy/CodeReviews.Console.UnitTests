namespace CodeTracker;

public enum Options
{
    SessionsHistory,
    AddSession,
    RemoveSession,
    UpdateSession,
    Help,
    Exit
}

public static class Menu
{
    public static void Greeting()
    {
        Console.WriteLine("====================================================================");
        Console.WriteLine("====================== Welcome to CodeTracker ======================");
        Console.WriteLine("====================================================================");
        Console.WriteLine("\npress enter to continue...");
        Console.ReadLine();
    }
    public static void DisplayOptions()
    {
        Console.WriteLine("--MainMenu--");
        Console.WriteLine("1. Sessions History");
        Console.WriteLine("2. Add Session");
        Console.WriteLine("3. Remove Session");
        Console.WriteLine("4. Update Session");
        Console.WriteLine("5. Help");
        Console.WriteLine("6. Exit");
        Console.WriteLine("Enter option's number : ");
    }

    public static Options MainMenu()
    {
        Console.Clear();
        DisplayOptions();
        return GetOptions();
    }

    public static void HelpMenu()
    {
        Console.Clear();
        Console.WriteLine("--Help--");
        Console.WriteLine("1. Sessions History -> Get Reports on your previous coding sessions ");
        Console.WriteLine("2. Add Session -> Add a new Session by providing its start and end time");
        Console.WriteLine("3. Remove Session -> Remove a Session from record by providing its date and time(optional)");
        Console.WriteLine("4. Update Session -> Change a record's Start and End Times by providing its date and time");
        Console.WriteLine("5. Exit -> Close the application");
        Console.WriteLine("press enter to continue...");
        Console.ReadLine();
    }
    public static Options GetOptions()
    {
        int input;
        do
        {
            var readLine = Console.ReadLine();
            int.TryParse(readLine, out input);
            input--;
        } while ((input) < (int)Options.SessionsHistory || input > (int)Options.Exit);

        return (Options)input;
    }
    
}