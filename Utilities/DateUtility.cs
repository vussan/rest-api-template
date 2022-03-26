namespace Utilities
{
    public class DateUtility
    {
        public static DateTime DateTime { get { return DateTime.UtcNow; } }
        public static DateTime Date { get { return DateTime.UtcNow.Date; } }
        public static TimeSpan Time { get { return DateTime.UtcNow.TimeOfDay; } }
    }
}
