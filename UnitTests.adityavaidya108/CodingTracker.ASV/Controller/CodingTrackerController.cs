using System.Reflection;
using Spectre.Console;

namespace CodingTracker;

public class CodingTrackerController
{
    public static void Menu()
    {
        var isTrackerOn = true;
        do
        {
            Display.DisplayMenu();
            var selection = Console.ReadLine().ToLower().Trim();
            switch(selection)
            {
                case "0":
                    HandleViewRecords();
                    break;
                case "1":
                    HandleInsertRecord();
                    break;
                case "2":
                    HandleDeleteRecord();
                    break;
                case "3":
                    HandleUpdateRecord();
                    break;
                case "4":
                    AnsiConsole.Markup("[aqua]Thanks for using Goodbye.Press any key to close.[/]");
                    Console.ReadLine();
                    isTrackerOn = false;
                    break;
                default:
                    AnsiConsole.Markup("[red]Invalid input. Press any key to continue[/]");
                    Console.ReadLine();
                    break;
            }

        }while(isTrackerOn);
    }

    private static void HandleInsertRecord()
    {
        IEnumerable<CodingSessionModel> results;
        CodingSessionModel userSessionInput= new CodingSessionModel();
        string CodingDate;
        do
        {
            AnsiConsole.Markup("[yellow3]Enter a date in the format yyyy-MM-dd (e.g., 2024-03-14)\n[/]");
            CodingDate = Console.ReadLine();
        } while (!Validation.TryParseDate(CodingDate, out DateOnly date));
        string timeDateInput;
        userSessionInput.SessionCodingDate = CodingDate;
        do
        {
            Display.GetCodingSessionStartTimeConsoleMessage();
            timeDateInput = Console.ReadLine();
        } while (!Validation.TryParseTime(timeDateInput, out TimeOnly Time));
        userSessionInput.SessionStartTime = timeDateInput;
        do
        {
            Display.GetCodingSessionEndTimeConsoleMessage();
            timeDateInput = Console.ReadLine();
        } while (!Validation.ValidateEndSessionTime(timeDateInput, userSessionInput.SessionStartTime));
        userSessionInput.SessionEndTime = timeDateInput;
        userSessionInput.SessionDuration = Validation.CalculateTimeSpan(TimeOnly.Parse(userSessionInput.SessionStartTime), TimeOnly.Parse(userSessionInput.SessionEndTime)).ToString();
        Database.InsertRecord(userSessionInput);
        AnsiConsole.Markup("[green4]Record inserted sucessfully.Press any key to continue.\n[/]");
        Console.ReadLine();
    }

    private static void HandleViewRecords()
    {
        IEnumerable<CodingSessionModel> results = Database.ViewSessionRecords();
        if (results.Count() == 0)
        {
            AnsiConsole.Markup("[yellow3]No Records inserted. Kindly Insert some records .Press any key to continue[/]\n");
            Console.ReadLine();
        }
        foreach (var session in results)
        {
            AnsiConsole.Markup("[aqua]Session ID: {0} , Start Time: {1}, End Time: {2}, SessionDuration: {3}, Date: {4}\n[/]", session.SessionId, session.SessionStartTime, session.SessionEndTime, session.SessionDuration, session.SessionCodingDate);
        }
        AnsiConsole.Markup("[orange1]Press any key to continue\n[/]");
        Console.ReadLine();
    }

    private static void HandleDeleteRecord()
    {
        AnsiConsole.Markup("[yellow3]Enter the session id you want to delete: [/]");
        var input = Console.ReadLine();
        bool intVal = int.TryParse(input, out int id);
        while (!intVal)
        {
            AnsiConsole.Markup("[red]Invalid input pls enter integer value.[/]");
            input = Console.ReadLine();
            intVal = int.TryParse(input, out int id1);
        }
        int sessionDeleteId = int.Parse(input.ToString());
        bool IsGivenIdPresent = Database.IsGivenSessionIdPresent(sessionDeleteId);
        Database.DeleteRecord(sessionDeleteId, IsGivenIdPresent);
        if (!IsGivenIdPresent)
        {
            AnsiConsole.Markup("[yellow3]The session id : {0} you entered is not present in the database.Cannot delete. Press any key to continue[/]", sessionDeleteId);
            Console.ReadLine();
        }
        else
        {
            AnsiConsole.Markup("[green4]The record with session id : {0} was deleted successfully. Press any key to continue.[/]", sessionDeleteId);
            Console.ReadLine();
        }
    }

    private static void HandleUpdateRecord() {
        IEnumerable<CodingSessionModel> results;
        string CodingDate;
        do
        {
            AnsiConsole.Markup("[yellow3]Enter a date in the format yyyy-MM-dd (e.g., 2024-03-14)\n[/]");
            CodingDate = Console.ReadLine();
        } while (!Validation.TryParseDate(CodingDate, out DateOnly date));
        results = Database.GetRecordsByDate(CodingDate);
        if (results.Count() != 0)
        {
            AnsiConsole.Markup("[aqua]The following records are already present for the specified date:\n[/]");
            foreach (var session in results)
            {
                AnsiConsole.Markup("[aqua]Session ID: {0} , Start Time: {1}, End Time: {2}, SessionDuration: {3}, Date: {4}\n[/]", session.SessionId, session.SessionStartTime, session.SessionEndTime, session.SessionDuration, session.SessionCodingDate);
            }
            AnsiConsole.Markup("[orange1]Press any key to continue\n[/]");
            Console.ReadLine();
            AnsiConsole.Markup("[yellow3]Enter the session id you want to update: \n[/]");
            var input = Console.ReadLine();
            bool intVal = int.TryParse(input, out int id);
            while (!intVal)
            {
                AnsiConsole.Markup("[red]Invalid input pls enter integer value.[/]");
                input = Console.ReadLine();
                intVal = int.TryParse(input, out int id1);
            }
            int SessionUpdateId = int.Parse(input.ToString());
            bool IsGivenIdToUpdatePresent = Database.IsGivenSessionIdPresentForInputDate(SessionUpdateId, CodingDate);
            if (IsGivenIdToUpdatePresent)
            {
                string timeDateInput;
                CodingSessionModel model = new CodingSessionModel();
                model.SessionCodingDate = CodingDate;
                do
                {
                    Display.GetCodingSessionStartTimeConsoleMessage();
                    timeDateInput = Console.ReadLine();
                } while (!Validation.TryParseTime(timeDateInput, out TimeOnly Time));
                model.SessionStartTime = timeDateInput;
                do
                {
                    Display.GetCodingSessionEndTimeConsoleMessage();
                    timeDateInput = Console.ReadLine();
                } while (!Validation.ValidateEndSessionTime(timeDateInput, model.SessionStartTime));
                model.SessionEndTime = timeDateInput;
                model.SessionDuration = Validation.CalculateTimeSpan(TimeOnly.Parse(model.SessionStartTime), TimeOnly.Parse(model.SessionEndTime)).ToString();
                Database.UpdateRecord(IsGivenIdToUpdatePresent, model, SessionUpdateId);
            }
            if (!IsGivenIdToUpdatePresent)
            {
                AnsiConsole.Markup("[yellow3]The session id : {0} you entered is not present in the database for the given input date: {1}.Cannot update. Press any key to continue[/]", SessionUpdateId, CodingDate);
                Console.ReadLine();
            }
            else
            {
                AnsiConsole.Markup("[green4]The record with session id : {0} was updated successfully. Press any key to continue.[/]", SessionUpdateId);
                Console.ReadLine();
            }
        }
        else
        {
            AnsiConsole.Markup("[yellow3]There are no records with the date : {0} . Nothing to Update.[/]", CodingDate);
            Console.ReadLine();
        }
    }
}