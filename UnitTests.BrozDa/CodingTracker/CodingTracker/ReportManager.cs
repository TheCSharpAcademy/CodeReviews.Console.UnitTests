using CodingTracker.Interfaces;

namespace CodingTracker
{
    /// <summary>
    /// Manages reporting function
    /// Implements <see cref="IReportManager"/>
    /// </summary>
    public class ReportManager : IReportManager
    {
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ICodingSessionRepository _codingSessionRepository;

        /// <summary>
        /// Initiates new object of <see cref="ReportManager"/>
        /// </summary>
        /// <param name="inputManager">Handles user input operations</param>
        /// <param name="outputManager">Handles output operations</param>
        /// <param name="repository">Manages access to the database</param>
        public ReportManager(IInputManager inputManager, IOutputManager outputManager, ICodingSessionRepository repository)
        {
            _codingSessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }
        /// <inheritdoc/>
        public void GetReport()
        {
            ReportTimeFrame timeFrame = _inputManager.GetTimeRangeForReport();
            DateTime start = GetStartDateForReport(timeFrame);
            DateTime end = GetEndDateForReport(timeFrame, start);

            var sessions = _codingSessionRepository.GetSessionsWithinRange(start, end).ToList();
            _outputManager.PrintRecords(sessions);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        /// <summary>
        /// Gets start date for the report based on passed Enum <see cref="ReportTimeFrame"/> value
        /// </summary>
        /// <param name="timeFrame"><see cref="ReportTimeFrame"/> value representing time frame for the report</param>
        /// <returns>Valid <see cref="DateTime"/> value representing start date for the report</returns>
        private DateTime GetStartDateForReport(ReportTimeFrame timeFrame)
        {
            return timeFrame switch
            {
                ReportTimeFrame.ThisYear => new DateTime(DateTime.Now.Year, 1, 1),
                ReportTimeFrame.ThisMonth => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                ReportTimeFrame.ThisWeek => GetFirstDayOfWeek(),
                ReportTimeFrame.Custom => _inputManager.GetStartTime(),
                _ => throw new ArgumentException("Invalid timeframe") //should not ever happen
            };
        }
        /// <summary>
        /// Gets Monday date of current week
        /// </summary>
        /// <returns><see cref="DateTime"/> representing Monday date of current week</returns>
        private DateTime GetFirstDayOfWeek()
        {
            DateTime today = DateTime.Now;
            int offset = today.DayOfWeek - DayOfWeek.Monday;
            today = today.AddDays(offset);
            return today;
        }
        /// <summary>
        /// Gets start end for the report based on passed Enum <see cref="ReportTimeFrame"/> value
        /// Performs validation based on passed <see cref="DateTime"/> value representing the start
        /// </summary>
        /// <param name="timeFrame"><see cref="ReportTimeFrame"/> value representing time frame for the report</param>
        /// <param name="startDate"><see cref="DateTime"/> value representing the start</param>
        /// <returns>Valid <see cref="DateTime"/> value representing start date for the report</returns>
        private DateTime GetEndDateForReport(ReportTimeFrame timeFrame, DateTime startDate)
        {
            DateTime today = DateTime.Now;

            return timeFrame switch
            {
                ReportTimeFrame.ThisYear => today,
                ReportTimeFrame.ThisMonth => today,
                ReportTimeFrame.ThisWeek => today,
                ReportTimeFrame.Custom => _inputManager.GetEndTime(startDate),
                _ => throw new ArgumentException("Invalid timeframe") //should not ever happen
            };
        }
    }
}