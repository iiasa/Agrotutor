namespace Agrotutor.Modules.Calendar.Components.Views
{
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using Types;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthlyCalendarMultipleEventsEntry : ContentView
    {
        public List<CalendarEvent> CalendarEvents { get; set; }

        public MonthlyCalendarMultipleEventsEntry()
        {
            InitializeComponent();
        }
    }
}