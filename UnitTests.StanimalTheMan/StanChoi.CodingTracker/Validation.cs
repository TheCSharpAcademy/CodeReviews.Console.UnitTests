using System.Globalization;
using System;

namespace StanChoi.CodingTracker
{
	public static class Validation
	{
		public static bool IsValidId(string id)
		{
			return Int32.TryParse(id, out _) && !string.IsNullOrEmpty(id) && Int32.Parse(id) >= 0;
		}

		public static bool IsValidDateTime(string dateTimeInputString)
		{
			return DateTime.TryParseExact(dateTimeInputString, "yyyy-MM-dd HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _);
		}
	}
}
