using CodingTrackerV2;
using CodingTrackerV2.Data;

namespace CodingTracker;
public class CodingTracker
{
    public static void Main(string[] args)
    {
        var dataAcess = new DataAccess();
        dataAcess.CreateDatabase();
        UInterface.MainMenu();
    }
}