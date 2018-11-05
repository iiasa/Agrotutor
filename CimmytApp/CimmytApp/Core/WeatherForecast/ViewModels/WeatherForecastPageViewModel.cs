namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Windows.Input;
    using CimmytApp.DTO.Parcel;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.DTO.SkywiseWeather.Historical.Temperature;
    using Helper.Map;
    using Prism;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class WeatherForecastPageViewModel : BindableBase, INavigationAware
    {
        private DailyHighTemperature _dailyHighTemperature;
        private Parcel _parcel;
        private WeatherData _weatherData;
        private Core.Map.GeoPosition position;

        public DailyHighTemperature DailyHighTemperature
        {
            get => _dailyHighTemperature;
            private set => SetProperty(ref _dailyHighTemperature, value);
        }

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                if (_parcel.Position.Latitude != null && _parcel.Position.Longitude != null
                ) // TODO - check if undefined - ==0.0?
                {
                    if (_parcel.Position.Latitude == 0 && _parcel.Position.Longitude == 0)
                    {
                        return;
                    }

                    position = new Core.Map.GeoPosition
                    {
                        Latitude = Parcel.Position.Latitude,
                        Longitude = Parcel.Position.Longitude
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