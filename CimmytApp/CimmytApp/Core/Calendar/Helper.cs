

namespace CimmytApp.Core.Calendar
{
    using System;

    public static class Helper
    {
        public static DateTime FirstDayInMonth(DateTime dateTime)
        {
            return dateTime.AddDays((double)(dateTime.Day - 1) * -1).Date;
        }

        public static int IndexOfWeekday(DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            return (dayOfWeek == DayOfWeek.Sunday) ? 6 : ((int)dayOfWeek - 1); // TODO: implement flexible start of week
        }

        public static int DaysInMonth(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }
    }
}
