using System.Globalization;
using CodingTracker.SamGannon.Utility;

namespace CodingTracker.SamGannon.Utility;

internal class UserInputHelper
{
    Validation validator = new();
    public string GetStartTime()
    {
        Console.WriteLine("Please enter the start time of your session in the following format: (HH:mm).");
        Console.WriteLine("Use 24-hour format (e.g., 17:45 represents 5:45pm).\n");

        string startTime = Console.ReadLine();

        while(!validator.IsValidTimeFormat(startTime))
        {
            Console.WriteLine($"\n{startTime} is not a valid time format.");
            Console.WriteLine("\nFormat time HH:mm (hour-hour:minute-minute as a 24 hour clock.");
            Console.WriteLine("\n ie Military time. One in in the afternoon (1:00pm) would be 13:00 ");
            startTime = Console.ReadLine();
        }
        
        return startTime;
    }

    public string GetEndTime()
    {
        Console.WriteLine("Please enter the ending time of your session in the following format: (HH:mm).");
        Console.WriteLine("Use 24-hour format (e.g., 17:45).");

        string endTime = Console.ReadLine();

        while (!validator.IsValidTimeFormat(endTime))
        {
            Console.WriteLine($"\n{endTime} is not a valid time format.");
            Console.WriteLine("\nFormat time HH:mm (hour-hour:minute-minute as a 24 hour clock.");
            Console.WriteLine("\n ie Military time. One in in the afternoon (1:00pm) would be 13:00 ");
            endTime = Console.ReadLine();
        }

        return endTime;
    }

    public string GetDateInput()
    {
        Console.WriteLine("Please enter the date in the following format: (mm-dd-yy).");

        string userDateInput = Console.ReadLine();

        while (!validator.IsValidDate(userDateInput))
        {
            Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: mm-dd-yy.\n\n");
            userDateInput = Console.ReadLine();
        }

        return userDateInput;
    }

    public string CalculateDuration(string startTime, string endTime)
    {
        TimeSpan tsStartTime = TimeSpan.ParseExact(startTime, "h\\:mm", CultureInfo.InvariantCulture);
        TimeSpan tsEndTime = TimeSpan.ParseExact(endTime, "hh\\:mm", CultureInfo.InvariantCulture);

        while (tsEndTime < tsStartTime)
        {
            Console.WriteLine("\nEnd time cannot occur before the start time. Please try again.\n");

            startTime = GetStartTime();
            endTime = GetEndTime();

            tsStartTime = TimeSpan.ParseExact(startTime, "h\\:mm", CultureInfo.InvariantCulture);
            tsEndTime = TimeSpan.ParseExact(endTime, "hh\\:mm", CultureInfo.InvariantCulture);
        }

        TimeSpan duration = tsEndTime - tsStartTime;
        return $"{(int)duration.TotalHours:D2}:{duration.Minutes:D2}";
    }

    public string CalculateSleepType(string duration)
    {
        TimeSpan sleepDuration = TimeSpan.ParseExact(duration, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);

        if (sleepDuration.TotalHours > 4)
        {
            return "Long";
        }
        else
        {
            return "Short";
        }
    }

    public int ValidateIdInput(string? commandInput)
    {
        while (!validator.IsValidId(commandInput))
        {
            Console.WriteLine("\n You have to type a valid Id\n");
            commandInput = Console.ReadLine();
        }

        var id = int.Parse(commandInput);

        return id;
    }
}
