using CodingTracker.Views;

namespace CodingTracker;

internal class Program
{
    private static void Main(string[] args)
    {
        var appMenu = new AppMenu();
        appMenu.Show();
    }
}