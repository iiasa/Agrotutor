﻿namespace CimmytApp.WeatherForecast.ViewModels
{
    using System.Collections.Generic;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class DailyForecastPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private List<DailySummary> _dailySummaries;

        public DailyForecastPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public List<DailySummary> DailySummaries
        {
            get => _dailySummaries;
            set => SetProperty(ref _dailySummaries, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("DailyForecast"))
            {
                parameters.TryGetValue("DailyForecast", out object forecast);
                if (forecast != null)
                {
                    DailySummaries = (List<DailySummary>)forecast;
                }
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }
    }
}