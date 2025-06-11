namespace CodingTracker
{
    /// <summary>
    /// Defines handling of CRUD operations for application and preparing of the repository
    /// </summary>
    public interface ICodingSessionManager
    {
        /// <summary>
        /// Prepares the repository
        /// Ensures that the repository exists by creating and populating it if necessary."
        /// </summary>
        void PrepareAndFillRepository();
        /// <summary>
        /// Displays stored records to the user
        /// </summary>
        void HandleView();
        /// <summary>
        /// Handles insertion of new record to the repository
        /// </summary>
        void HandleInsert();
        /// <summary>
        /// Handles updating of the record in the repository
        /// </summary>
        void HandleUpdate();
        /// <summary>
        /// Handles removing of the record from the repository
        /// </summary>
        void HandleDelete();
    }
}