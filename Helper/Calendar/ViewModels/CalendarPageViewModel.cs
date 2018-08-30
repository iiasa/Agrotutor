namespace Helper.Calendar.ViewModels
{
    using System.Collections.Generic;

    class CalendarPageViewModel
    {
        CalendarMode currentCalendarMode = CalendarMode.Monthly;
        List<CalendarEvent> events = new List<CalendarEvent>();
    }
}
