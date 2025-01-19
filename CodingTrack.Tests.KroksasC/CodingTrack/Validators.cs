using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrack
{
    public static class Validators
    {
        public static bool IsValidDate(string userDateInput, ref DateTime dateTime)
        {
            return DateTime.TryParse(userDateInput, out dateTime);
        }
        public static bool IsStartTimeLater(DateTime startTime, DateTime endTime)
        {
            return startTime > endTime;
        }
    }
}
