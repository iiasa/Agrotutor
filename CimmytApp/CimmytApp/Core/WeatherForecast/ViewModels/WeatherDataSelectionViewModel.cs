namespace CimmytApp.WeatherForecast.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.Core.Persistence.Entities;
    using Helper.DTO.SkywiseWeather.Historical;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class WeatherDataSelectionViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        private List<string> _datasetNames;

        private int _selectedDataset;

        private WeatherData _weatherData;

        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            DatasetNames = new List<string>
            {
                "Días de grado creciente",
                "Días de grado de enfriamiento",
                "Días de grado de calefacción",
                "Precipitación diaria",
                "Precipitaciones por hora",
                "Humedad relativa por hora",
                "Radiación solar diaria",
                "Radiación solar por hora",
                "Temperatura por hora",
                "Temperatura alta diaria",
                "Temperatura baja diaria",
                "Punto de rocío por hora",
                "Velocidad del viento por hora",
                "Dirección del viento por hora",
                "Evapotranspiración diaria de cultivos cortos",
                "Evapotranspiración diaria de cultivos altos",
                "Evapotranspiración horaria de cultivos cortos",
                "Evapotranspiración horaria de cultivos altos"
            };
            DatasetNames = new List<string>
            {
                "Growing degree days",
                "Cooling degree days",
                "Heating degree days",
                "Daily precipitation",
                "Hourly precipitation",
                "Hourly relative humidity",
                "Daily solar radiation",
                "Hourly solar radiation",
                "Hourly temperature",
                "Daily high temperature",
                "Daily low temperature",
                "Hourly dewpoint",
                "Hourly wind speed",
                "Hourly wind direction",
                "Daily evapotranspiration short crop",
                "Daily evapotranspiration tall crop",
                "Hourly evapotranspiration short crop",
                "Hourly evapotranspiration tall crop"
            };

            _navigationService = navigationService;
        }

        public List<string> DatasetNames
        {
            get => _datasetNames;
            set => SetProperty(ref _datasetNames, value);
        }

        public WeatherData MyWeatherData
        {
            get => _weatherData;
            set => SetProperty(ref _weatherData, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("WeatherData"))
            {
                parameters.TryGetValue<WeatherData>("WeatherData", out var weatherData);
                if (weatherData != null)
                {
                    MyWeatherData = weatherData;
                }
            }
        }

        private void ShowWeatherData()
        {
        }
    }
}