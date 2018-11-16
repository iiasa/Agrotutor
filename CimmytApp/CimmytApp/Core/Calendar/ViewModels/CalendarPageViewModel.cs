namespace CimmytApp.Core.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using CimmytApp.Core.Calendar.Types;
    using CimmytApp.Core.Calendar.Views;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.StaticContent;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class CalendarPageViewModel : BindableBase, INavigatedAware
    {
        public static string EventsParameterName = "Events";
        private readonly CalendarMode currentCalendarMode = CalendarMode.Monthly;
        private string currentMonthName;
        private string currentYear;
        private DateTime firstDay;
        private INavigationService navigationService;
        private string dateRangeText;

        private IEnumerable<CalendarEvent> events;

        public CalendarPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public DelegateCommand ShowNextMonth => new DelegateCommand(() =>
        {
            FirstDay = FirstDay.AddMonths(1);
        });
        public DelegateCommand ShowPreviousMonth => new DelegateCommand(() => { FirstDay = FirstDay.AddMonths(-1); });

        public string DateRangeText
        {
            get => this.dateRangeText;
            set => SetProperty(ref this.dateRangeText, value);
        }

        public IEnumerable<CalendarEvent> Events
        {
            get => this.events;
            set
            {
                this.events = value;
                if (FirstDay != null)
                {
                    View?.ShowPeriod(this.firstDay, this.currentCalendarMode);
                }
            }
        }

        public DateTime FirstDay
        {
            get => this.firstDay;
            set
            {
                this.firstDay = value;
                DateRangeText = value.ToString("Y");

                if (Events != null)
                {
                    View?.ShowPeriod(this.firstDay, this.currentCalendarMode);
                }
            }
        }

        public CalendarPage View { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

            if (parameters.ContainsKey("Dev"))
            {
                if (Events == null)
                {
                    Events = GetDevData();
                }
            }

            if (parameters.ContainsKey(CalendarPageViewModel.EventsParameterName))
            {
                parameters.TryGetValue(CalendarPageViewModel.EventsParameterName,
                    out IEnumerable<CalendarEvent> events);
                if (events != null)
                {
                    Events = events;
                }
            }
        }

        public void SetView(CalendarPage calendarPage)
        {
            View = calendarPage;
            FirstDay = Helper.FirstDayInMonth(DateTime.Today);
        }

        private List<CalendarEvent> GetDevData()
        {
            List<CalendarEvent> eventsDev = new List<CalendarEvent>();
            List<Plot> plots = DevHelper.GetTestData();
            foreach (Plot plot in plots)
            {
                eventsDev.AddRange(plot.GetCalendarEvents());
            }

            return eventsDev;
        }
    }
}