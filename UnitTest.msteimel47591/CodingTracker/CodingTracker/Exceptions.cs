
namespace CodingTracker
{
    public class MenuExitException : Exception
    {
        public MenuExitException() : base("User chose to exit the menu.") { }
    }
}
