using Agrotutor.Core.Entities;

namespace Agrotutor.Modules.Calendar.Types
{
    using System;
    using Xamarin.Forms;

    public class CalendarEvent
    {
        public bool AllDayEvent { get; set; }

        public Color? Color { get; set; }

        public string Content { get; set; }

        public Activity Data { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string Title { get; set; }
    }
}
