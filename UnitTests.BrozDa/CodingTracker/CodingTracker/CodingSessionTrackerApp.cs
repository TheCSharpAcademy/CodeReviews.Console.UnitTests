using CodingTracker.Interfaces;

namespace CodingTracker
{
    /// <summary>
    /// Represents application used for tracking coding hours of the user
    /// </summary>
    internal class CodingSessionTrackerApp
    {
        private ICodingSessionManager _codingSessionManager;
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ISessionTracker _sessionTracker;
        private IReportManager _reportManager;

        /// <summary>
        /// Initiates new object of <see cref="CodingSessionTrackerApp"/>
        /// </summary>
        /// <param name="inputManager">Handles user input operations</param>
        /// <param name="outputManager">Handles output operations</param>
        /// <param name="codingSessionManager">Manages actions which needs to be performed for database access</param>
        /// <param name="sessionTracker">Manages tracking of session in real time</param>
        /// <param name="reportManager">Manages generation of reports</param>
        public CodingSessionTrackerApp(IInputManager inputManager, IOutputManager outputManager, ICodingSessionManager codingSessionManager, ISessionTracker sessionTracker, IReportManager reportManager)
        {
            _inputManager = inputManager;
            _outputManager = outputManager;
            _sessionTracker = sessionTracker;
            _codingSessionManager = codingSessionManager;
            _reportManager = reportManager;
        }
        /// <summary>
        /// Main method facilitating application runtime until user decides to exit
        /// </summary>
        public void Run()
        {
            _codingSessionManager.PrepareAndFillRepository();

            UserChoice choice;

            _outputManager.PrintMenuHeader();
            choice = _inputManager.GetMenuInput();

            while (choice != UserChoice.Exit)
            {
                ProcessChoice(choice);

                _outputManager.ConsoleClear();
                _outputManager.PrintMenuHeader();

                choice = _inputManager.GetMenuInput();
            }
        }
        /// <summary>
        /// Perform actions based on user choice in main menu
        /// </summary>
        /// <param name="choice"><see cref="UserChoice"/> value representing chosen option in menu by user</param>
        private void ProcessChoice(UserChoice choice)
        {
            switch (choice)
            {
                case UserChoice.Insert:
                    _codingSessionManager.HandleInsert();
                    break;

                case UserChoice.View:
                    _codingSessionManager.HandleView();
                    break;

                case UserChoice.Update:
                    _codingSessionManager.HandleUpdate();
                    break;

                case UserChoice.Delete:
                    _codingSessionManager.HandleDelete();
                    break;

                case UserChoice.Track:
                    _sessionTracker.TrackSession();
                    break;

                case UserChoice.Report:
                    _reportManager.GetReport();
                    break;

                case UserChoice.Exit:
                    return;
            }
        }
    }
}