using static Alvind0.CodingTracker.Models.Enums;
namespace Alvind0.CodingTracker.Utilities;

public class SortingHelper
{
    public static readonly Dictionary<SortType, string> SortTypes = new()
    {
        { SortType.Default, "Default" },
        { SortType.Id, "Sort by ID" },
        { SortType.Date, "Sort by Date" },
        { SortType.Duration, "Sort by Duration" }
    };

    public static readonly Dictionary<PeriodFilter, string> PeriodFilters = new()
    {
        { PeriodFilter.None , "None"},
        { PeriodFilter.ThisYear, "This year" },
        { PeriodFilter.ThisMonth, "This month" },
        { PeriodFilter.ThisWeek, "This week" }
    };

    public static List<string> GetSortTypes() => SortTypes.Values.ToList();

    public static List<string> GetPeriodFilters() => PeriodFilters.Values.ToList();

    public static SortType GetSortTypeFromDescription(string description)
    {
        foreach (var sortType in SortTypes)
        {
            if (sortType.Value == description) return sortType.Key;
        }
        throw new NullReferenceException("Invalid sort type: " + description);
    }

    public static PeriodFilter GetPeriodFilterFromDescription(string description)
    {
        foreach (var periodFilter in PeriodFilters)
        {
            if (periodFilter.Value == description) return periodFilter.Key;
        }
        throw new NullReferenceException("Invalid period filter: " + description);
    }
}
