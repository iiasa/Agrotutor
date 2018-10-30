namespace CimmytApp.Core.Calendar.Types
{
    public class CalendarEvent
    {
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }

        public bool AllDayEvent { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public object Data { get; set; }
    }
}