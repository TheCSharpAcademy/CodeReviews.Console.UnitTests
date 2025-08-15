using CodingTracker.Model;
using System.Globalization;

namespace CodingTracker
{
    public class Validation
    {
        private readonly ICodingSessionRepository? _repo;

        public Validation() { }

        public Validation(ICodingSessionRepository repo)
        {
            _repo = repo;
        }

        public DateTime? ValidateDate(string dateInput)
        {
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out DateTime date))
            {
                return null;
            }
            else
            {
                return date.Date;
            }
        }

        public DateTime? ValidateTime(string timeInput)
        {
            string format = "h:mm tt";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime time))
            {
                return null;
            }
            else
            {
                return time;
            }
        }

        public bool ValidateStartTimeIsLessThanEndTime(DateTime? start, DateTime? end)
        {
            if (start < end)
                return true;
            else
                return false;
        }

        public (string message, bool validStatus, int recordId) ValidateRecordId(string? recordIdInput)
        {
            if (!int.TryParse(recordIdInput, out int recordId))
                return ("Invalid ID. Please enter a numeric value.", false, recordId);
            else if (_repo.GetRecordIdCount(recordId) <= 0)
                return ("Record not found. Please enter a valid record ID.", false, recordId);
            else
                return ("", true, recordId);
        }

        public string ValidateDeleteConfirmation(string? confirmationInput)
        {
            if (confirmationInput?.ToLower() == "n")
            {
                return "n";
            }
            else if (confirmationInput?.ToLower() != "y")
            {
                return "";
            }

            return "y";
        }
    }
}
