
namespace CimmytApp.Core.Map.Views
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.Core.Calendar.Types;
    using Xamarin.Forms;

    public partial class MonthlyCalendarDayView : ContentView
    {
        public MonthlyCalendarDayView()
        {
            InitializeComponent();
            calendarEvents = new List<CalendarEvent>();
        }

        private DateTime date;
        private List<CalendarEvent> calendarEvents;

        public DateTime Date
        {
            get => this.date;
            set
            {
                this.date = value;
                this.LblDay.Text = this.date.Day.ToString();
            }
        }

        public List<CalendarEvent> CalendarEvents
        {
            get => this.calendarEvents;
            set
            {
                var newValue = value ?? new List<CalendarEvent>();
                this.calendarEvents = newValue;
                SetEventsUI();
            }
        }

        private void SetEventsUI()
        {
            this.layoutEvents.Children.Clear();

            if (CalendarEvents.Count <= 3)
            {
                foreach (CalendarEvent calendarEvent in CalendarEvents)
                {
                    this.layoutEvents.Children.Add(new MonthlyCalendarEventEntry
                    {
                        CalendarEvent = calendarEvent
                    });
                }
            }
            else
            {
                this.layoutEvents.Children.Add(new MonthlyCalendarEventEntry
                {
                    CalendarEvent = CalendarEvents[0]
                });
                this.layoutEvents.Children.Add(new MonthlyCalendarEventEntry
                {
                    CalendarEvent = CalendarEvents[1]
                });
                this.layoutEvents.Children.Add(new MonthlyCalendarMultipleEventEntries
                {
                    CalendarEvents = CalendarEvents.GetRange(2, CalendarEvents.Count - 2)
                });

            }
        }
    }
}
