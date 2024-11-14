using Spectre.Console;

namespace CodeTrackerLibrary;

public class CodingSession
{ 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public  static readonly string DayFormat = "d-M-yyyy";
    public static readonly string TimeFormat = "HH:mm";

    public CodingSession(DateTime startTime, DateTime endTime , string description)
    {
        StartTime = startTime;
        EndTime = endTime;
        Description = description;
    }
    //for gatting Data from databse
    private CodingSession(string date,string startTime, string endTime, string description)
    {
           StartTime = DateTime.Parse(startTime);
           EndTime = DateTime.Parse(endTime);
           DateTime day = DateTime.ParseExact(date,DayFormat,System.Globalization.CultureInfo.CurrentCulture);
           StartTime = new DateTime(day.Year, day.Month, day.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);
           EndTime = new DateTime(day.Year, day.Month, day.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
           if(EndTime.Hour <= StartTime.Hour)
           {
                EndTime = EndTime.AddDays(1);
           }
           Description = description;
    }
    public TimeSpan Duration()
    {
        return EndTime - StartTime;
    }

    public override string ToString()
    {
        return $"Date: {StartTime.ToString(DayFormat)}, Time: {StartTime.ToString(TimeFormat)} -> {EndTime.ToString(TimeFormat)}, Duration: {Duration().Hours}h : {Duration().Minutes}m , Description: {Description}";
    }

    public string DurationToString()
    {
        return $"{Duration().Hours.ToString("00")}h : {Duration().Minutes.ToString("00")}m";
    }

    public string Date()
    {
        return StartTime.ToString(DayFormat);
    }
    public static void DisplayerHabitsHistory(CodingSession sessionDetails)
    {
        var sessions = Database.GetCodingSessionRecord(sessionDetails);
        var table = new Table();
        table.AddColumns("No.","Date","Starting Time","Ending Time","Duration","Description");
        int counter = 1;
        Console.Clear();
        foreach (var session in sessions)
        {
            table.AddRow(counter.ToString(),$"[blue]{session.Date()}[/]",
                $"[lightseagreen]{session.StartTime.ToString(TimeFormat)}[/]",$"[lightseagreen]{session.EndTime.ToString(TimeFormat)}[/]",
                $"[skyblue1]{session.DurationToString()}[/]",
                $"[paleturquoise1]{session.Description}[/]");
            counter++;
        }

        table.Centered();
        table.Border = TableBorder.Rounded;
        table.Title = new TableTitle("Coding Sessions History",new Style(decoration:Decoration.Underline));
        for (int i = 1; i < 5; i++)
        {
            table.Columns[i].Alignment = Justify.Center;
        }
        AnsiConsole.Write(table);
        Console.WriteLine("press enter to continue...");
        Console.ReadLine();
    }
   
}