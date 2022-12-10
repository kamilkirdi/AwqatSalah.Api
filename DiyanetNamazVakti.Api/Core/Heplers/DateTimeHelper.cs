namespace DiyanetNamazVakti.Api.Core.Heplers;

public static class DateTimeHelper
{
    public static DateTime FirstOfCurrentYear(this DateTime dateTime)
    {
        return new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, 0);
    }

    public static DateTime LastOfCurrentYear(this DateTime dateTime)
    {
        return new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59, 999);
    }

    public static DateTime ResetTimeToStartOfDay(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
    }

    public static DateTime ResetTimeToEndOfDay(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
    }

    public static DateTime ResetTimeToStartOfWeek(this DateTime dateTime)
    {
        var weekStart = DateTime.Today.AddDays(-(int)dateTime.DayOfWeek);
        return new DateTime(weekStart.Year, weekStart.Month, weekStart.Day, 0, 0, 0, 0);
    }

    public static DateTime ResetTimeToEndOfWeek(this DateTime dateTime)
    {
        var startOfWeek = dateTime.ResetTimeToStartOfWeek();
        var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);
        return new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59, 999);
    }

    public static DateTime ResetTimeToStartOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0);
    }

    public static DateTime ResetTimeToEndOfMonth(this DateTime dateTime)
    {
        return dateTime.ResetTimeToStartOfMonth().AddMonths(1).AddTicks(-1);
    }

    //public static DateTime FirstOfCurrentMonth(this DateTime dateTime)
    //{
    //    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);
    //}

}
