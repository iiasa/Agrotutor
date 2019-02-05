namespace Agrotutor.Modules.Weather.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    using Core;
    using Types;

    public class WeatherForecastPageViewModel : ViewModelBase, INavigatedAware
    {
        private List<DailySummary> _dailySummaries;

        private WeatherForecast weatherForecast;

        public WeatherForecastPageViewModel(INavigationService navigationService, IStringLocalizer<WeatherForecastPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
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
                NavigationService.GoBackAsync();
            }
        }
    }
}
