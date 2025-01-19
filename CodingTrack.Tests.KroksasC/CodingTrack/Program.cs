namespace CodingTrack
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            DatabaseHelpers.InitializeDatabase();
            Menu.GetUserInput();
        }
    }
}
