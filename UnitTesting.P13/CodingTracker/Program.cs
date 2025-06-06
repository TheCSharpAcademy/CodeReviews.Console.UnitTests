using CodingTracker.Controllers;
using CodingTracker.Views;

namespace CodingTracker

{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTools.DataBaseAndConnectionString();
            MainMenu mainmenu = new();
        }
    }
}