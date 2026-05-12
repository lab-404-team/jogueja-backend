using System.Globalization;

namespace Common.Helpers;

public static class DateTimeHelper
{
    public static string FormatMonthYear(DateTimeOffset date)
    {
        var culture = new CultureInfo("pt-BR");
        var month = date.ToString("MMMM", culture);
        return culture.TextInfo.ToTitleCase(month) + $" {date:yyyy}";
    }

    public static string FormatMonth(DateTimeOffset date)
    {
        var culture = new CultureInfo("pt-BR");
        var month = date.ToString("MMMM", culture);
        return culture.TextInfo.ToTitleCase(month);
    }

    public static DateTimeOffset AddOneBusinessDay(DateTimeOffset date)
    {
        var next = date.AddDays(1);
        if (next.DayOfWeek == DayOfWeek.Saturday)
            next = next.AddDays(2);
        else if (next.DayOfWeek == DayOfWeek.Sunday)
            next = next.AddDays(1);
        return next;
    }
}
