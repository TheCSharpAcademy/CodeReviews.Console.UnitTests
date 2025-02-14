using CodingTracker.Data;

namespace CodingTracker
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbConnection.StartConnection();
            UserInterface.MainMenu();
        }
    }
}