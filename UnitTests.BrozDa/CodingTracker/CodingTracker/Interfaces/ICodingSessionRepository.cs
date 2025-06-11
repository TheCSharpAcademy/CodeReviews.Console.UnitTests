namespace CodingTracker
{
    /// <summary>
    /// Defines handling of CRUD operations against repository
    /// </summary>
    public interface ICodingSessionRepository
    {
        /// <summary>
        /// String variable containing path to the database file
        /// </summary>
        string RepositoryPath { get; }
        /// <summary>
        /// Inserts single entity into the repository
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        void Insert(CodingSession entity);
        /// <summary>
        /// Retrieves all records from the repository
        /// </summary>
        /// <returns>Collection of all records</returns>
        IEnumerable<CodingSession> GetAll();
        /// <summary>
        /// Updates existing entity in the repository with new values
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        void Update(CodingSession entity);
        /// <summary>
        /// Deletes single entity from the repository
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(CodingSession entity);
        /// <summary>
        /// Ensures the repository exists, creating it if necessary
        /// </summary>
        void CreateRepository();
        /// <summary>
        /// Inserts multiple entities to the repository
        /// </summary>
        /// <param name="entities">Collection of entities to be inserted</param>
        void InsertBulk(IEnumerable<CodingSession> entities);
        /// <summary>
        /// Retrieves all records from repository which are within defined time range 
        /// </summary>
        /// <param name="startDate">DateTime value defining start - inclusive</param>
        /// <param name="endDate">DateTime value defining end - inclusive</param>
        /// <returns>Collection of entities withing spefified range</returns>
        IEnumerable<CodingSession> GetSessionsWithinRange(DateTime startDate, DateTime endDate);
    }
}