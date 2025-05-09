using CodingTracker.Models;
using System.Diagnostics;

namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            DBAccess.CreateDatabase();

            Console.WriteLine("Would you like to seed the database? Enter 1 for yes.");
            string seed = Console.ReadLine();

            if (seed == "1")
            {
                Helper.SeedDatabase();
            }

            while (!exit)
            {
                string selection = UserInterface.DisplayMenu();

                try
                {
                    switch (selection)
                    {
                        case "Exit":
                            exit = true;
                            break;
                        case "Add":
                            UserInterface.AddSession();
                            break;
                        case "Delete":
                            UserInterface.DeleteSession();
                            Console.WriteLine("\n\nPress any key to return to main menu.\n\n");
                            Console.ReadLine();
                            break;
                        case "Edit":
                            UserInterface.EditSession();
                            break;
                        case "Live":
                            UserInterface.LiveSession();
                            break;
                        case "View All Entries":
                            List<CodingSession> codeSessions = DBAccess.GetAllSessions();
                            UserInterface.DisplayTable(codeSessions);
                            Console.WriteLine("\n\nPress any key to return to main menu.\n\n");
                            Console.ReadLine();
                            break;
                        case "View Filtered Entries":
                            UserInterface.ViewFilteredSessions();
                            Console.WriteLine("\n\nPress any key to return to main menu.\n\n");
                            Console.ReadLine();
                            break;
                        case "View Report":
                            UserInterface.ViewReport();
                            Console.WriteLine("\n\nPress any key to return to main menu.\n\n");
                            Console.ReadLine();
                            break;

                    }
                }
                catch (MenuExitException)
                {

                    Debug.WriteLine("Exit Menu Exception Caught");
                }
            }

            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
}
