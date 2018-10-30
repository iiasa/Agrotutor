namespace CimmytApp.Core.Calendar.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.Calendar.Types;

    class CalendarPageViewModel
    {
        CalendarMode currentCalendarMode = CalendarMode.Monthly;
        List<CimmytApp.Core.Calendar.Types.CalendarEvent> events = new List<CimmytApp.Core.Calendar.Types.CalendarEvent>();
    }
}
