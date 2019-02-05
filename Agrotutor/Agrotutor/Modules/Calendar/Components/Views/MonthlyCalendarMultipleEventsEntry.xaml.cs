namespace Agrotutor.Modules.Calendar.Components.Views
{
    using System.Collections.Generic;
    using Xamarin.Forms;

    using Types;

    public partial class MonthlyCalendarMultipleEventsEntry : ContentView
    {
        public List<CalendarEvent> CalendarEvents { get; set; }

        public MonthlyCalendarMultipleEventsEntry()
        {
            InitializeComponent();
        }
    }
}