namespace CodeTracker;
using CodeTrackerLibrary;
class Program
{
    static void Main()
    {
        Database.InitializeDatabase();
        Menu.Greeting();
        bool isRunning = true;
        var inputManager = new Input();
        while (isRunning)
        {
            switch (Menu.MainMenu())
            {
                case Options.SessionsHistory:
                    Console.WriteLine("--Session History--");
                    Console.WriteLine("EnterDate (D-M-YYYY) or press enter to skip:");
                    DateTime date = inputManager.GetOptionalDate();
                    Console.WriteLine("Enter Description keywords(language or name of project) or press enter to skip");
                    string description = Console.ReadLine();
                    if (date != DateTime.MinValue)
                    {
                        Console.WriteLine($"Date : {date.ToString("d-M-yyyy")}");
                    }
                    if (!String.IsNullOrEmpty(description))
                    {
                        Console.WriteLine($"Description {description}");
                    }
                    CodingSession sessionDetails = new CodingSession(date,date,description);
                    CodingSession.DisplayerHabitsHistory(sessionDetails);
                    break;
                case Options.AddSession:
                    Console.WriteLine("--Add Session --");
                    Database.AddCodingSession(inputManager.GetCodingSession());
                    break;
                case Options.RemoveSession:
                    Console.WriteLine("--Remove Session --");
                    Console.WriteLine("Enter date D-M-YYYY");
                    do
                    {
                        date = inputManager.GetOptionalDate();
                    } while (date == DateTime.MinValue);
                    Console.WriteLine("Enter Description keywords(language or name of project)");
                    do
                    {
                        description = Console.ReadLine();
                    } while (String.IsNullOrEmpty(description));
                    sessionDetails = new CodingSession(date,date,description);
                    Console.WriteLine($"Looking for ->{sessionDetails.StartTime.ToString(CodingSession.DayFormat)} - {sessionDetails.Description}");
                    if (Database.GetCodingSessionRecord(sessionDetails).Count == 0)
                    {
                        Console.WriteLine($"no entries were found");
                        Console.WriteLine("press enter to continue...");
                        Console.ReadLine();
                    }
                   
                    Database.DeleteCodingSession(sessionDetails);
                    break;
                case Options.UpdateSession:
                    Console.WriteLine("--Update Session--");
                    Console.WriteLine("Enter date D-M-YYYY");
                    do
                    {
                        date = inputManager.GetFullDate();
                    } while (date == DateTime.MinValue);
                    Console.WriteLine("Enter Description keywords(language or name of project)");
                    do
                    {
                        description = Console.ReadLine();
                    } while (String.IsNullOrEmpty(description));
                    sessionDetails = new CodingSession(date,date,description);
                    Console.WriteLine($"Looking For :{sessionDetails.StartTime.ToString(CodingSession.DayFormat)} - {sessionDetails.Description}");
                    if (Database.GetCodingSessionRecord(sessionDetails).Count == 0)
                    {
                        Console.WriteLine("No entires were found ");
                        Console.WriteLine("press enter to continue...");
                        Console.ReadLine();
                        break;
                    }
                    DateTime newDate;
                    Console.WriteLine("Enter new date D-M-YYYY");
                    do
                    {
                        newDate = inputManager.GetFullDate();
                    } while (newDate == DateTime.MinValue);
                    Console.WriteLine("Enter new Description ");
                    string newDescription;
                    do
                    {
                        newDescription = Console.ReadLine();
                    } while (String.IsNullOrEmpty(newDescription));
                    CodingSession newSessionDetails = new CodingSession(newDate,newDate,newDescription);
                    Console.WriteLine($"New Session :{newSessionDetails.StartTime.ToString(CodingSession.DayFormat)} - {newSessionDetails.Description}");
                    Console.WriteLine("press enter to continue...");
                    Console.ReadLine();
                    Database.UpdateCodingSession(sessionDetails,newSessionDetails);
                    break;
                case Options.Help:
                    Menu.HelpMenu();
                    break;
                case Options.Exit:
                    Console.WriteLine("----Thanks for using CodeTracker----");
                    isRunning = false;
                    break;
              
            }
            
        }
       
    }
}
