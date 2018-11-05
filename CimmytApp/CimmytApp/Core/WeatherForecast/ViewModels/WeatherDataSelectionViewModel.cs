namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.Map;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class WeatherDataSelectionViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private List<string> _datasetNames;

        private bool _downloading;

        private Parcel _parcel;

        private Core.Map.GeoPosition _position;

        private bool _refreshedFromServer;

        private int _selectedDataset;

        private WeatherData _weatherData;

        private bool _weatherDataAvailable = true;

        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            Downloading = false;
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

            ShowWeatherDataCommand = new DelegateCommand(ShowWeatherData);
            RefreshWeatherDataCommand = new DelegateCommand(RefreshWeatherData);

            _navigationService = navigationService;
        }

        public bool ParcelLocationNotSet
        {
            get
            {
                if (Parcel == null)
                {
                    return true;
                }

                return Parcel.Position.Latitude == 0 && Parcel.Position.Longitude == 0;
            }
        }

        public bool ShowRefreshText => !_weatherDataAvailable;

        public bool ViewInactive => !Downloading;

        public List<string> DatasetNames
        {
            get => _datasetNames;
            set => SetProperty(ref _datasetNames, value);
        }

        public bool Downloading
        {
            get => _downloading;
            set => SetProperty(ref _downloading, value);
        }

        public WeatherData MyWeatherData
        {
            get => _weatherData;

            set
            {
                var data = value;
                if (data == null)
                {
                    if (!_refreshedFromServer)
                    {
                        return;
                    }

                    _refreshedFromServer = false;
                    Downloading = false;
                    return;
                }

                WeatherDataAvailable = true;
                data.ParcelId = Parcel.ParcelId;
                SetProperty(ref _weatherData, data);
                if (_refreshedFromServer)
                {
                    //_weatherDbOperations.UpdateWeatherData(data);
                    _refreshedFromServer = false;
                    Downloading = false;
                }
            }
        }

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                try
                {
                    LoadWeatherFromDb(value.ParcelId);
                }
                catch
                {
                }
            }
        }

        public DelegateCommand RefreshWeatherDataCommand { get; set; }

        public int SelectedDataset
        {
            get => _selectedDataset;
            set => SetProperty(ref _selectedDataset, value);
        }

        public DelegateCommand ShowWeatherDataCommand { get; set; }

        public bool WeatherDataAvailable
        {
            get => _weatherDataAvailable;
            set => SetProperty(ref _weatherDataAvailable, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = parcel;
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private async void LoadWeatherDataAsync()
        {
            _refreshedFromServer = true;
            Downloading = true;
            var weatherData = await WeatherService.GetWeatherData(_position);
            //MyWeatherData =
        }

        private void LoadWeatherFromDb(string parcelId)
        {
            //MyWeatherData = _weatherDbOperations.GetWeatherData(parcelId);
        }

        private void RefreshWeatherData()
        {
            if ((bool)_parcel.Position?.IsSet())
            {
                _position = new Core.Map.GeoPosition
                {
                    Latitude = Parcel.Position.Latitude,
                    Longitude = Parcel.Position.Longitude
                };
            }
            else
            {
                _position = new Core.Map.GeoPosition
                {
                    Latitude = 21.798344,
                    Longitude = -101.667537
                };
            }
            LoadWeatherDataAsync();
        }

        private void ShowWeatherData()
        {
            var page = "";
            if (MyWeatherData == null)
            {
                WeatherDataAvailable = false;
                return;
            }

            HistoricalSeries series = null;
            switch (SelectedDataset)
            {
                case 0:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.GrowingDegreeDays;
                    break;

                case 1:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.CoolingDegreeDays;
                    break;

                case 2:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.HeatingDegreeDays;
                    break;

                case 3:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailyPrecipitation;
                    break;

                case 4:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyPrecipitation;
                    break;

                case 5:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyRelativeHumidity;
                    break;

                case 6:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailySolarRadiation;
                    break;

                case 7:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlySolarRadiation;
                    break;

                case 8:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyTemperature;
                    break;

                case 9:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailyHighTemperature;
                    break;

                case 10:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailyLowTemperature;
                    break;

                case 11:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyDewpoint;
                    break;

                case 12:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyWindSpeed;
                    break;

                case 13:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyWindDirection;
                    break;

                case 14:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailyEvapotranspirationShortCrop;
                    break;

                case 15:
                    page = "DailyWeatherDataPage";
                    series = MyWeatherData.DailyEvapotranspirationTallCrop;
                    break;

                case 16:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyEvapotranspirationShortCrop;
                    break;

                case 17:
                    page = "HourlyWeatherDataPage";
                    series = MyWeatherData.HourlyEvapotranspirationTallCrop;
                    break;
            }

            series?.Sort();
            var parameters = new NavigationParameters
            {
                { "Series", series },
                { "VariableName", DatasetNames.ElementAt(SelectedDataset) }
            };
            _navigationService.NavigateAsync(page, parameters);
        }
    }
}