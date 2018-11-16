namespace CimmytApp.Core.Map.Views
{
    using System.Collections.Generic;
    using CimmytApp.Core.Calendar.Types;
    using Xamarin.Forms;

    public partial class MonthlyCalendarMultipleEventEntries : ContentView
    {
        public MonthlyCalendarMultipleEventEntries()
        {
            InitializeComponent();
        }

        public List<CalendarEvent> CalendarEvents { get; set; }
    }
}