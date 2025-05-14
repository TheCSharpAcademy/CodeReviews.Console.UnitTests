using CodingTracker.Models;
using System.Globalization;


namespace CodingTracker;

public static class Helper
{

    private static IDBAccessWrapper _dbAccessWrapper;

    public static void SetDBAccessWrapper(IDBAccessWrapper dbAccessWrapper)
    {
        _dbAccessWrapper = dbAccessWrapper;
    }

    private static bool IsTestEnvironment()
    {
        // Check for an environment variable or configuration setting
        return Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") == "true";
    }
    public static void SeedDatabase()
    {
        string[] startTimes = { "01/01/21 09:00 AM", "01/02/21 09:00 AM", "01/03/21 09:00 AM",
            "01/04/21 09:00 AM", "01/05/21 09:00 AM", "01/06/21 09:00 AM", "01/07/21 09:00 AM", "01/08/21 09:00 AM", "01/09/21 09:00 AM" };
        string[] endTimes = { "01/01/21 10:00 AM", "01/02/21 10:00 AM", "01/03/21 10:00 AM",
            "01/04/21 10:00 AM", "01/05/21 10:00 AM", "01/06/21 10:00 AM", "01/07/21 10:00 AM", "01/08/21 10:00 AM", "01/09/21 10:00 AM" };

        string[] focuses = { "C#", "Python", "Java", "C++", "JavaScript", "Ruby", "Swift", "Kotlin", "Go" };

        for (int i = 0; i < startTimes.Length; i++)
        {
            CodingSession codingSession = new CodingSession();
            codingSession.StartTime = startTimes[i];
            codingSession.EndTime = endTimes[i];
            codingSession.Focus = focuses[i];
            codingSession.CalculateDuration();

            DBAccess.AddSession(codingSession);
        }
    }
    public static DateTime ConvertStringToDateTime(string dateTime)
    {
        return DateTime.ParseExact(dateTime, "MM/dd/yy hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None);
    }

    public static string GetDateTimeInput(string message)
    {
        Console.WriteLine($"\n\nEnter {message} (Format: MM/dd/yy hh:mm tt) where tt AM or PM, or type 0 to return to main menu.\n\n");
        string dateInput = Console.ReadLine();

        if (dateInput == "0")
        {
            throw new MenuExitException();
        }

        while (!Validation.ValidateDateTimeInput(dateInput))
        {
            Console.WriteLine($"\n\nInvalid format. Enter {message} (Format: MM/dd/yy hh:mm tt) where tt AM or PM\n\n");
            dateInput = Console.ReadLine();
            if (dateInput == "0")
            {
                throw new MenuExitException();
            }
        }

        return dateInput;
    }

    public static string GetFocusInput()
    {
        Console.WriteLine($"\n\nEnter the focus of the coding session or type 0 to return to main menu. (Short description of what you were working on.)\n\n");
        string focusInput = Console.ReadLine();

        if (focusInput == "0")
        {
            throw new MenuExitException();
        }
        while (!Validation.ValidateFocusInput(focusInput))
        {
            Console.WriteLine("\n\nInvalid input. Please enter a focus for the coding session or type 0 to return to main menu.\n\n");
            focusInput = Console.ReadLine();
            if (focusInput == "0")
            {
                throw new MenuExitException();
            }
        }

        return focusInput;
    }

    public static int GetSessionIdInput()
    {
        Console.WriteLine("\n\nEnter the ID of the session you would like to delete or type 0 to return to main menu.\n\n");
        string idInput = Console.ReadLine();

        if (idInput == "0")
        {
            throw new MenuExitException();
        }

        List<CodingSession> sessions;

        sessions = DBAccess.GetAllSessions();

        int id = Validation.ValidateSessionIdInput(idInput);

        while (id == -1 || !SessionExistsById(id, sessions))
        {
            Console.WriteLine("\n\nInvalid ID. Please enter a valid ID or type 0 to return to main menu.\n\n");
            idInput = Console.ReadLine();
            if (idInput == "0")
            {
                throw new MenuExitException();
            }
            id = Validation.ValidateSessionIdInput(idInput);
        }

        return id;
    }

    public static bool SessionExistsById(int id, List<CodingSession> codingSessions)
    {
        foreach (var session in codingSessions)
        {
            if (session.Id == id)
            {
                return true;
            }
        }
        return false;
    }


    public static DateTime GetDateInput()
    {
        Console.WriteLine($"\n\nEnter date input (Format: MM/dd/yy), or type 0 to return to main menu.\n\n");
        string dateInput = Console.ReadLine();

        if (dateInput == "0")
        {
            throw new MenuExitException();
        }

        while (!Validation.ValidateDateInput(dateInput))
        {
            Console.WriteLine($"\n\nInvalid format. Enter date input (Format: MM/dd/yy), or type 0 to return to main menu.\n\n");
            dateInput = Console.ReadLine();
            if (dateInput == "0")
            {
                throw new MenuExitException();
            }
        }

        DateTime inputDate = DateTime.ParseExact(dateInput, "MM/dd/yy", new CultureInfo("en-US"), DateTimeStyles.None);

        // Convert the DateTime object to the desired format string
        string formattedDateString = inputDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        // Parse the formatted string back to a DateTime object
        DateTime formattedDateTime = DateTime.ParseExact(formattedDateString, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);


        return formattedDateTime;
    }

    public static DateTime GetMonthAndYearInput()
    {
        Console.WriteLine($"\n\nEnter month and year input (Format: MM/yy), or type 0 to return to main menu.\n\n");
        string dateInput = Console.ReadLine();

        if (dateInput == "0")
        {
            throw new MenuExitException();
        }

        while (!Validation.ValidateMonthAndYearInput(dateInput))
        {
            Console.WriteLine($"\n\nInvalid format. Enter month and year input (Format: MM/yy), or type 0 to return to main menu.\n\n");
            dateInput = Console.ReadLine();
            if (dateInput == "0")
            {
                throw new MenuExitException();
            }
        }

        DateTime inputDate = DateTime.ParseExact(dateInput, "MM/yy", new CultureInfo("en-US"), DateTimeStyles.None);

        // Convert the DateTime object to the desired format string
        string formattedDateString = inputDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        // Parse the formatted string back to a DateTime object
        DateTime formattedDateTime = DateTime.ParseExact(formattedDateString, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);


        return formattedDateTime;
    }

    public static DateTime GetYearInput()
    {
        Console.WriteLine($"\n\nEnter year input (Format: 20XX ex: enter 24 for the year 2024), or type 0 to return to main menu.\n\n");
        string dateInput = Console.ReadLine();

        if (dateInput == "0")
        {
            throw new MenuExitException();
        }

        while (!Validation.ValidateYearInput(dateInput))
        {
            Console.WriteLine($"\n\nInvalid input. Enter year input (Format: 20XX ex: enter 24 for the year 2024), or type 0 to return to main menu.\n\n");
            dateInput = Console.ReadLine();
            if (dateInput == "0")
            {
                throw new MenuExitException();
            }
        }

        DateTime inputDate = DateTime.ParseExact(dateInput, "yy", new CultureInfo("en-US"), DateTimeStyles.None);

        // Convert the DateTime object to the desired format string
        string formattedDateString = inputDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        // Parse the formatted string back to a DateTime object
        DateTime formattedDateTime = DateTime.ParseExact(formattedDateString, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);

        return formattedDateTime;
    }


}
