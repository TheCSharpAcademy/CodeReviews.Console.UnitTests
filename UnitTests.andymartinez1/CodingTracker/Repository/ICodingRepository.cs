using Coding_Tracker.Models;

namespace Coding_Tracker.Repository;

public interface ICodingRepository
{
    public List<CodingSession> GetAllSessions();

    public CodingSession GetSession(int id);

    public void InsertSession(CodingSession session);

    public void UpdateSession(CodingSession session);

    public void DeleteSession(int id);
}
