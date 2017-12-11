namespace CimmytApp.WeatherForecast.ViewModels
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
                parameters.TryGetValue<List<DailySummary>>("DailyForecast", out var forecast);
                if (forecast != null) DailySummaries = forecast;
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}