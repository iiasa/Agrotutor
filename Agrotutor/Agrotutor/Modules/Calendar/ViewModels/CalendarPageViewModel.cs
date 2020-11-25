namespace Agrotutor.Modules.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Core.Persistence;
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
        private IAppDataService _appDataService;
        public CalendarPageViewModel(INavigationService navigationService, IStringLocalizer<CalendarPageViewModel> stringLocalizer, IAppDataService appDataService)
        :base(navigationService, stringLocalizer)
        {
            Title = "CalendarPage";
            NavigationService = navigationService;
            _appDataService = appDataService;
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
                var strings = value.ToString("Y").Split(' ');
                strings[0] = strings[0].ToUpper();
                DateRangeText = string.Join(" ", strings);

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
                base.OnNavigatedTo(parameters);
            }
            else
            {
 
                var plots = _appDataService.GetAllPlotsAsync().Result;
                Events = Core.Entities.Plot.GetCalendarEvents(plots);
                base.OnNavigatedTo(parameters);
          

            }
          
        }

        public void SetView(CalendarPage calendarPage)
        {
            View = calendarPage;
            FirstDay = Helper.FirstDayInMonth(DateTime.Today);
        }
    }
}
