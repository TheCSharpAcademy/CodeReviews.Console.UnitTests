using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Coding_Tracker.Utils;

public static class EnumExtensions
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()
            ?.GetCustomAttribute<DisplayAttribute>()
            ?.Name;
    }
}
