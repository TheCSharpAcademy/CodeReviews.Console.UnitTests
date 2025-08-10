namespace CodingTracker.Interfaces
{
    /// <summary>
    /// Defines input and input validation operations
    /// </summary>
    public interface IInputManager
    {
        /// <summary>
        /// Readonly property for DateTime format
        /// </summary>
        public string DateTimeFormat { get; init; }
        /// <summary>
        /// Gets <see cref="UserChoice"/> value based on user input
        /// </summary>
        /// <returns><see cref="UserChoice"/> value based on user input</returns>
        UserChoice GetMenuInput();
        /// <summary>
        /// Returns new <see cref="CodingSession"/> based on user input
        /// </summary>
        /// <returns><see cref="CodingSession"/> based on user input</returns>
        CodingSession GetNewSession();
        /// <summary>
        /// Gets <see cref="DateTime"/> value representing start of the coding session
        /// </summary>
        /// <returns><see cref="DateTime"/> value representing start of the coding session</returns>
        DateTime GetStartTime();

        /// <summary>
        /// Gets <see cref="DateTime"/> value representing end of the coding session
        /// </summary>
        /// <param name="startDate"><see cref="DateTime"/> value representing start of the coding session</param>
        /// <returns><see cref="DateTime"/> value representing end of the coding session</returns>
        DateTime GetEndTime(DateTime startDate);
        /// <summary>
        /// Prints Coding session to the user along with the operation being performed with the record and gets confirmation from user before 
        /// </summary>
        /// <param name="session"><see cref="CodingSession"/> record being adjusted</param>
        /// <param name="operation"><see cref="string"/> value representing the operation - eg: "update", "delete"</param>
        /// <returns>true if user confirms the operation, false otherwise</returns>
        bool ConfirmOperation(CodingSession session, string operation);
        /// <summary>
        /// Prints original and updated <see cref="CodingSession"/> records and asks user for confirmation before it's being reflected in the repository 
        /// </summary>
        /// <param name="original"><see cref="CodingSession"/> record being adjusted</param>
        /// <param name="updated"><see cref="CodingSession"/> record with new values</param>
        /// <returns>true if user confirms the operation, false otherwise</returns>
        bool ConfirmUpdate(CodingSession original, CodingSession updated);
        /// <summary>
        /// Gets session from user
        /// </summary>
        /// <param name="sessions">Collection of session which which should user select</param>
        /// <param name="operation"><see cref="string"/> value representing the operation - eg: "update", "delete"</param>
        /// <returns>null if user decided to abort the opration, valid <see cref="CodingSession?"/> otherwise</returns>
        CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation);
        /// <summary>
        /// Gets user confirmation to start tracking
        /// </summary>
        /// <returns>true if user confirms tracking, false otherwise</returns>
        bool ConfirmTrackingStart();
        /// <summary>
        /// Gets <see cref="ReportTimeFrame"/> for Report functionalities
        /// </summary>
        /// <returns><see cref="ReportTimeFrame"/> value</returns>
        public ReportTimeFrame GetTimeRangeForReport();
    }
}