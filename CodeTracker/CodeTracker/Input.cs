using System.Globalization;
using CodeTrackerLibrary;
using Spectre.Console;

namespace CodeTracker;

public class Input
{
    private readonly IAnsiConsole _console;
    public Input(IAnsiConsole? console = null)
    {
        _console = console ?? AnsiConsole.Console;
    }
    public CodingSession GetCodingSession()
    {
        DateTime day = GetDay();
        DateTime startTime = GetStartTime(day);
        DateTime endTime = GetEndTime(day,startTime);
        CodingSession codingSession = new CodingSession(startTime,endTime,GetDescription());
        Console.WriteLine($"Session Details: {codingSession}");
        Console.WriteLine("press enter to continue...");
        Console.ReadLine();
        return codingSession;
    }

    public string GetDescription()
    {
        string description =  _console.Prompt(new TextPrompt<string>("Enter description (Language, Task, Project):"));
        return description;
    }
    
    public DateTime GetEndTime(DateTime day , DateTime startTime)
    {
        DateTime endTime;
        Console.WriteLine("Enter ending time (HH:MM) or press enter for current time:");
        do
        {
            var readline = Console.ReadLine();
            if (String.IsNullOrEmpty(readline))
                endTime = DateTime.Now;
            else
            {
                DateTime.TryParse(readline, out endTime);
            }
            endTime = new DateTime(day.Year, day.Month, day.Day, endTime.Hour, endTime.Minute, endTime.Second);
            if (endTime.Hour < startTime.Hour)
            {
                endTime = new DateTime(day.Year, day.Month, day.Day+1, endTime.Hour, endTime.Minute, endTime.Second);
            }

        } while (endTime <= startTime);
        
         return endTime;
    }
    public DateTime GetStartTime(DateTime day)
    {
        DateTime startTime;
        Console.WriteLine("Enter starting time (HH:MM):");
        do
        {
            var readline = Console.ReadLine();
            var success = DateTime.TryParse(readline, out startTime);
            if (!success)
                Console.WriteLine("please make sure the format is correct");
        } while ((startTime > DateTime.Now && day.Date == DateTime.Today) || startTime <= DateTime.MinValue );
        startTime = new DateTime(day.Year, day.Month, day.Day, startTime.Hour, startTime.Minute, startTime.Second);
          return startTime;
    }
    public DateTime GetFullDate()
    {

        DateTime date;

        do
        {
            var readLine = Console.ReadLine();
            if (String.IsNullOrEmpty(readLine))
            {
                date = DateTime.MinValue;
                break;
            }

            if (DateTime.TryParseExact(readLine, CodingSession.DayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {

                break;
            }
            else
            {
                Console.WriteLine($"It appears that format is incorrect please enter correct format {CodingSession.DayFormat}");
            }
        } while (true);

        return date;
    }
    public DateTime GetOptionalDate()
    {
        
        DateTime date;
                    
        do
        {
            var readLine = Console.ReadLine();
            if (String.IsNullOrEmpty(readLine))
            {
                date = DateTime.MinValue;
                break;
            }

            if (DateTime.TryParseExact(readLine, CodingSession.DayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {

                break;
            }
            else
            {
                Console.WriteLine($"It appears that format is incorrect please enter correct format {CodingSession.DayFormat} or press enter to skip");
            }
        } while (true);

        return date;
    }
    public DateTime GetDay()
    {
        DateTime day;
        Console.WriteLine("Enter date (dd-mm-yyyy) or press any key to get current date.");
        do
        {
            var readline = Console.ReadLine();
            if (String.IsNullOrEmpty(readline))
            {
                day = DateTime.Today;
                break;
            }

            bool success = DateTime.TryParseExact(readline, "d-M-yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out day);
            if (!success)
                day = DateTime.MinValue;
        } while (day <= DateTime.MinValue || day > DateTime.Now);

        return day;
    }

   
    
}