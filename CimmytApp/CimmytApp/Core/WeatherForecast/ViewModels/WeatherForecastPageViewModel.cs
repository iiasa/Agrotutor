namespace CimmytApp.WeatherForecast.ViewModels
{
    using CimmytApp.Core.Persistence.Entities;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.DTO.SkywiseWeather.Historical.Temperature;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class WeatherForecastPageViewModel : BindableBase, INavigationAware
    {
        private DailyHighTemperature _dailyHighTemperature;
        private Plot _plot;
        private WeatherData _weatherData;
        private Position position;

        public DailyHighTemperature DailyHighTemperature
        {
            get => _dailyHighTemperature;
            private set => SetProperty(ref _dailyHighTemperature, value);
        }

        public Plot Plot
        {
            get => _plot;
            set
            {
                SetProperty(ref _plot, value);
                if (_plot.Position.Latitude != null && _plot.Position.Longitude != null
                ) // TODO - check if undefined - ==0.0?
                {
                    if (_plot.Position.Latitude == 0 && _plot.Position.Longitude == 0)
                    {
                        return;
                    }

                    position = new Position
                    {
                        Latitude = Plot.Position.Latitude,
                        Longitude = Plot.Position.Longitude
                    };
                    LoadWeatherDataAsync();
                }
            }
        }

        public WeatherData WeatherData
        {
            get => _weatherData;

            set => SetProperty(ref _weatherData, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private async void LoadWeatherDataAsync()
        {
            //WeatherData = await WeatherService.GetWeatherData(position);
            if (WeatherData != null)
            {
                DailyHighTemperature = WeatherData.DailyHighTemperature;
            }
        }
    }
}