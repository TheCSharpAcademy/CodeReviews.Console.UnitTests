using CodingTracker;
using CodingTracker.Model;
using System.Configuration;

Console.Title = "Coding Tracker";

string connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

var repository = new CodingSessionRepository(connection);

repository.CreateTable();
repository.SeedDatabase();

string? menuChoice;

do
{
    menuChoice = Display.PrintMainMenu();

    switch (menuChoice)
    {
        case "Close Application":
            Console.WriteLine("\nGoodbye!");
            Thread.Sleep(2000);
            Environment.Exit(0);
            break;
        case "View All Records":
            Display.PrintAllRecords("View All Records");
            UI.ReturnToMainMenu();
            break;
        case "Add Record":
            RecordsController.AddRecord();
            UI.ReturnToMainMenu();
            break;
        case "Edit Record":
            RecordsController.EditRecord();
            UI.ReturnToMainMenu();
            break;
        case "Delete Record":
            RecordsController.DeleteRecord();
            UI.ReturnToMainMenu();
            break;
        case "View Report":
            repository = new CodingSessionRepository(connection);
            var reportData = repository.GetReportData();
            Display.PrintReport(reportData);
            UI.ReturnToMainMenu();
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
} while (menuChoice != "Close Application");














