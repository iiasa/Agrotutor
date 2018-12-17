namespace CimmytApp.WeatherForecast.ViewModels
{
    using System.Collections.Generic;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class DailyForecastPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private List<DailySummary> _dailySummaries;

        private WeatherForecast weatherForecast;

        public DailyForecastPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public List<DailySummary> DailySummaries
        {
            get => _dailySummaries;
            set => SetProperty(ref this._dailySummaries, value);
        }

        public WeatherForecast WeatherForecast
        {
            get => this.weatherForecast;
            set
            {
                SetProperty(ref this.weatherForecast, value);
                DailySummaries = value.Location.DailySummaries.DailySummary;
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Forecast"))
            {
                parameters.TryGetValue<WeatherForecast>("Forecast", out var forecast);
                if (forecast != null)
                {
                    WeatherForecast = forecast;
                }
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }
    }
}