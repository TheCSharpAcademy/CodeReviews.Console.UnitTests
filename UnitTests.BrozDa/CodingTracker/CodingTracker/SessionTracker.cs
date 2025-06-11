using CodingTracker.Interfaces;
using System.Diagnostics;

namespace CodingTracker
{
    /// <summary>
    /// Manages tracking of coding session
    /// Implements <see cref="ISessionTracker"/>
    /// </summary>
    internal class SessionTracker : ISessionTracker
    {
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ICodingSessionRepository _codingSessionRepository;

        /// <summary>
        /// Initializes new object of <see cref="SessionTracker"/>
        /// </summary>
        /// <param name="inputManager">Handles user input operations</param>
        /// <param name="outputManager">Handles output operations</param>
        /// <param name="repository">Manages access to the database</param>
        public SessionTracker(IInputManager inputManager, IOutputManager outputManager, ICodingSessionRepository repository)
        {
            _codingSessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }
        /// <inheritdoc/>
        public void TrackSession()
        {
            if (!_inputManager.ConfirmTrackingStart())
            { return; }

            DateTime start = InitializeDate();

            PerformTracking();

            DateTime end = InitializeDate();

            CodingSession trackedSession = new CodingSession { Start = start, End = end };

            _outputManager.PrintSingleSession(trackedSession);
            _codingSessionRepository.Insert(trackedSession);
        }
        /// <summary>
        /// Initializes date in desired format
        /// </summary>
        /// <returns><see cref="DateTime"/> value in desired format</returns>
        private DateTime InitializeDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }
        /// <summary>
        /// Tracks the session 
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="cursorPosition"></param>
        private void PerformTracking()
        {
            var cursorPosition = Console.GetCursorPosition();
            Console.CursorVisible = false;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);
                _outputManager.PrintTrackingPanel(stopwatch.Elapsed);
                Thread.Sleep(1000);
            }

            stopwatch.Stop();

            Console.ReadKey(false);
        }
    }
}