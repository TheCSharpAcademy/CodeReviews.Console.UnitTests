namespace CodingTracker
{
    /// <summary>
    /// Defines presenting output to the user
    /// </summary>
    internal interface IOutputManager
    {
        /// <summary>
        /// Readonly property for DateTime format
        /// </summary>
        public string DateTimeFormat { get; init; }
        /// <summary>
        /// Clears the console and resets the cursor
        /// </summary>
        void ConsoleClear();
        /// <summary>
        /// Prints header of the menu
        /// </summary>
        void PrintMenuHeader();
        /// <summary>
        /// Prints out the records to the user
        /// </summary>
        /// <param name="sessions">Collection of <see cref="CodingSession"/>records</param>
        void PrintRecords(IEnumerable<CodingSession> sessions);
        /// <summary>
        /// Prints out the pannel user during tracking
        /// </summary>
        /// <param name="elapsed"><see cref="TimeSpan"/> value representing duration of recorded session</param>
        void PrintTrackingPanel(TimeSpan elapsed);
        /// <summary>
        /// Presents single session to the user
        /// </summary>
        /// <param name="session"><see cref="CodingSession"/> to be printed</param>
        void PrintSingleSession(CodingSession session);
    }
}