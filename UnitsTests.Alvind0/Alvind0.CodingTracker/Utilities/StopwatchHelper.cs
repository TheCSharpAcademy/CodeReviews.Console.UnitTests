using System.Diagnostics;
using Spectre.Console;
using static Alvind0.CodingTracker.Models.Enums;

namespace Alvind0.CodingTracker.Utilities;

public class StopwatchHelper
{
    private readonly Stopwatch _stopwatch = new();
    private StopwatchState _state = StopwatchState.Default;
    private CancellationTokenSource _cts;
    
    public StopwatchHelper()
    {
        _cts = new CancellationTokenSource();
    }

    public StopwatchState State => _state;

    public void StartStopwatch()
    {
        if (_state == StopwatchState.Running) return;

        _stopwatch.Start();
        _state = StopwatchState.Running;
        _cts = new CancellationTokenSource();

        Task.Run(() => UpdateDisplay(_cts.Token));
    }

    // I had to think of a way to solve discrepancies between automatically calculated
    // duration and the duration of the stopwatch when paused. Abanadoned the idea.
    //public void PauseStopwatch()
    //{
    //    if (_state != StopwatchState.Running) return;

    //    _stopwatch.Stop();
    //    _state = StopwatchState.Paused;
    //    _cts.Cancel();
    //}

    public void StopStopwatch()
    {
        _stopwatch.Stop();
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        _state = StopwatchState.Default;
        _stopwatch.Reset();
    }

    // I don't know why but using an empty catch block for async tasks seems to be standard practice
    private async Task UpdateDisplay(CancellationToken token)
    {
        try
        {
            Console.WriteLine("Start");
            while (!token.IsCancellationRequested)
            {
                AnsiConsole.Clear();
                AnsiConsole.WriteLine($"{_stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}");

                await Task.Delay(1000, token);
            }
        }
        catch (TaskCanceledException) { }
    }
}
