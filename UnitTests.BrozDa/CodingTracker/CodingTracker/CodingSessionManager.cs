using CodingTracker.Interfaces;

namespace CodingTracker
{
    public class CodingSessionManager : ICodingSessionManager
    {
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ICodingSessionRepository _sessionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingSessionManager"/> class.
        /// </summary>
        /// <param name="inputManager">Handles user input operations</param>
        /// <param name="outputManager">Handles output operations</param>
        /// <param name="repository">Manages access to the database</param>
        public CodingSessionManager(IInputManager inputManager, IOutputManager outputManager, ICodingSessionRepository repository)
        {
            _sessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }
        /// <inheritdoc/>
        public void PrepareAndFillRepository()
        {
            if (!File.Exists(_sessionRepository.RepositoryPath))
            {
                _sessionRepository.CreateRepository();

                IEnumerable<CodingSession> sessions = GenerateRecords(100);
                _sessionRepository.InsertBulk(sessions);
            }
        }
        /// <inheritdoc/>
        public void HandleView()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        /// <inheritdoc/>
        public void HandleInsert()
        {
            CodingSession session = _inputManager.GetNewSession();

            if (_inputManager.ConfirmOperation(session, "insert"))
            {
                _sessionRepository.Insert(session);
            }
        }
        /// <inheritdoc/>
        public void HandleUpdate()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            CodingSession? originalSession = _inputManager.GetSessionFromUserInput(sessions, "update");

            if (originalSession is null)
            { return; }

            CodingSession updatedSession = _inputManager.GetNewSession();
            updatedSession.Id = originalSession.Id;

            if (_inputManager.ConfirmUpdate(originalSession, updatedSession))
            {
                _sessionRepository.Update(updatedSession);
            }
        }
        /// <inheritdoc/>
        public void HandleDelete()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            CodingSession? session = _inputManager.GetSessionFromUserInput(sessions, "delete");

            if (session is null)
            {  return; }

            if (_inputManager.ConfirmOperation(session, "delete"))
            {
                _sessionRepository.Delete(session);
            }
        }
        /// <summary>
        /// Generates records for the database
        /// </summary>
        /// <param name="count">Number of records to be generated</param>
        /// <returns>Collection containing generated records</returns>
        private IEnumerable<CodingSession> GenerateRecords(int count)
        {
            DateTime start = new DateTime(2025, 02, 01, 00, 00, 00);
            DateTime end;
            Random random = new Random();

            List<CodingSession> records = new List<CodingSession>();

            for (int i = 0; i < count; i++)
            {
                end = start.AddHours(random.Next(24));
                records.Add(new CodingSession { Start = start, End = end });
                start = end;
            }

            return records;
        }
    }
}