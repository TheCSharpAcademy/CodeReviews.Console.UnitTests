namespace CodingTracker.Models
{
    public interface IDBAccessWrapper
    {
        List<CodingSession> GetAllSessions();
    }
}
