using CodingLogger.Models;

namespace CodingLogger.Data
{
    public interface ICodingSessionRepository
    {
        Task Create(CodingSession codingSession);
        Task Delete(int key);
        Task<CodingSession?> Retrieve(int key);
        Task<List<CodingSession>> RetrieveAll();
        Task Update(CodingSession codingSession);
    }
}

