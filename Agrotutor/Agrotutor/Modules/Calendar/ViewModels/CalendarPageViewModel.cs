namespace Agrotutor.Modules.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Types;
    using Views;

    public class CalendarPageViewModel : ViewModelBase
    {
        public static string EventsParameterName = "Events";
        private string currentMonthName;
        private string currentYear;
        private DateTime firstDay;
        private string dateRangeText;
        private IEnumerable<CalendarEvent> events;
        public INavigationService NavigationService { get; set; }
        public CalendarPageViewModel(INavigationService navigationService, IStringLocalizer<CalendarPageViewModel> stringLocalizer)
        :base(navigationService, stringLocalizer)
        {

            NavigationService = navigationService;
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
                    View?.ShowPeriod(this.firstDay);
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
                    View?.ShowPeriod(this.firstDay);
                }
            }
        }

        public CalendarPage View { get; set; }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if (parameters.ContainsKey(CalendarPageViewModel.EventsParameterName))
            {
                parameters.TryGetValue(CalendarPageViewModel.EventsParameterName,
                    out IEnumerable<CalendarEvent> events);
                if (events != null)
                {
                    Events = events;
                }
            }
            base.OnNavigatedTo(parameters);
        }

        public void SetView(CalendarPage calendarPage)
        {
            View = calendarPage;
            FirstDay = Helper.FirstDayInMonth(DateTime.Today);
        }
    }
}
