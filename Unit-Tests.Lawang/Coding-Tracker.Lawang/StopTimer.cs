using System.Diagnostics;
using System.Timers;
using Spectre.Console;
using Timer = System.Timers.Timer;

namespace Lawang.Coding_Tracker;

public static class StopTimer
{
    private static Stopwatch sw = new Stopwatch();
    private static Timer? myTimer;


    public static TimeSpan UseTimer()
    {
        StartTimer();
        Console.ReadLine();
        sw.Stop();
        TimeSpan timerValue = sw.Elapsed;
        sw.Reset();
        if (myTimer != null)
        {
            myTimer.Stop();
            myTimer.Dispose();
        }
        return timerValue;
    }
    private static void StartTimer()
    {
        //Creater a timer and set the interval to 1 second
        myTimer = new Timer(1000);

        //Attach the Tick method to the Elapsed event
        myTimer.Elapsed += Tick;

        myTimer.AutoReset = true;

        // Enable the timer
        myTimer.Enabled = true;

    }

    //This method will execute every 1 second
    private static void Tick(object? sender, ElapsedEventArgs e)
    {
        if(!sw.IsRunning) sw.Start();

        Console.Clear();
        Console.WriteLine("Current Time: {0:hh:mm:ss tt}", e.SignalTime);

        //TO DISPAY TIMER
        Panel timerView = new Panel(new Markup($"[bold]Timer: [green]{sw.Elapsed.ToString("hh\\:mm\\:ss")}[/][/]").Centered())
          .Expand()
          .Padding(1, 1, 1, 1);
        AnsiConsole.Write(timerView);
        AnsiConsole.WriteLine("(Press 'Enter' to stop Timer.)");
        Thread.Sleep(1000);
    }
}
