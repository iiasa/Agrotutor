namespace Agrotutor.Modules.Calendar.Components.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using Types;
    using Prism.Navigation;

    [XamlCompilation(XamlCompilationOptions.Compile)]
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
        public INavigationService NavigationService { get; set; }
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
                        CalendarEvent = calendarEvent,
                        NavigationService = NavigationService
                    });
                }
            }
            else
            {
                this.layoutEvents.Children.Add(new MonthlyCalendarEventEntry
                {
                    CalendarEvent = CalendarEvents[0],
                    NavigationService= NavigationService

                });
                this.layoutEvents.Children.Add(new MonthlyCalendarEventEntry
                {
                    CalendarEvent = CalendarEvents[1],
                    NavigationService = NavigationService
                });
                this.layoutEvents.Children.Add(new MonthlyCalendarMultipleEventsEntry
                {
                    CalendarEvents = CalendarEvents.GetRange(2, CalendarEvents.Count - 2)
                });

            }
        }
    }
}