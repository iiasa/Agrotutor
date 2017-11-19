using Helper.Map;

namespace CimmytApp.WeatherForecast.ViewModels
{
    using BusinessContract;
    using DTO;
    using DTO.Parcel;
    using Helper.DTO.SkywiseWeather.Historical;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="WeatherDataSelectionViewModel" />
    /// </summary>
    public class WeatherDataSelectionViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _datasetNames
        /// </summary>
        private List<string> _datasetNames;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        /// Defines the _selectedDataset
        /// </summary>
        private int _selectedDataset;

        /// <summary>
        /// Defines the _weatherData
        /// </summary>
        private WeatherData _weatherData;

        /// <summary>
        /// Defines the _weatherDbOperations
        /// </summary>
        private IWeatherDbOperations _weatherDbOperations;

        /// <summary>
        /// Defines the _weatherDataAvailable
        /// </summary>
        private bool _weatherDataAvailable = true;

        /// <summary>
        /// Defines the isActive
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// Defines the position
        /// </summary>
        private GeoPosition _position;

        /// <summary>
        /// Defines the refreshedFromServer
        /// </summary>
        private bool _refreshedFromServer = false;

        /// <summary>
        /// Defines the _downloading
        /// </summary>
        private bool _downloading;

        /// <summary>
        /// Gets or sets a value indicating whether Downloading
        /// </summary>
        public bool Downloading { get => _downloading; set => SetProperty(ref _downloading, value); }

        /// <summary>
        /// Gets a value indicating whether ViewInactive
        /// </summary>
        public bool ViewInactive => !Downloading;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherDataSelectionViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="weatherDbOperations">The <see cref="IWeatherDbOperations"/></param>
        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator, INavigationService navigationService, IWeatherDbOperations weatherDbOperations)
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
            ShowWeatherDataCommand = new DelegateCommand(ShowWeatherData);
            RefreshWeatherDataCommand = new DelegateCommand(RefreshWeatherData);

            _navigationService = navigationService;
            _weatherDbOperations = weatherDbOperations;
        }

        /// <summary>
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// Gets or sets the DatasetNames
        /// </summary>
        public List<string> DatasetNames { get => _datasetNames; set => SetProperty(ref _datasetNames, value); }

        /// <summary>
        /// Gets or sets the MyWeatherData
        /// </summary>
        public WeatherData MyWeatherData
        {
            get => _weatherData;

            set
            {
                var data = value;
                if (data == null)
                {
                    if (!_refreshedFromServer) return;
                    _refreshedFromServer = false;
                    Downloading = false;
                    return;
                }
                WeatherDataAvailable = true;
                data.ParcelId = Parcel.ParcelId;
                SetProperty(ref _weatherData, data);
                if (_refreshedFromServer)
                {
                    _weatherDbOperations.UpdateWeatherData(data);
                    _refreshedFromServer = false;
                    Downloading = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
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
                catch { }
            }
        }

        /// <summary>
        /// Gets or sets the RefreshWeatherDataCommand
        /// </summary>
        public DelegateCommand RefreshWeatherDataCommand { get; set; }

        /// <summary>
        /// Gets or sets the SelectedDataset
        /// </summary>
        public int SelectedDataset { get => _selectedDataset; set => SetProperty(ref _selectedDataset, value); }

        /// <summary>
        /// Gets or sets the ShowWeatherDataCommand
        /// </summary>
        public DelegateCommand ShowWeatherDataCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether WeatherDataAvailable
        /// </summary>
        public bool WeatherDataAvailable { get => _weatherDataAvailable; set => SetProperty(ref _weatherDataAvailable, value); }

        /// <summary>
        /// Gets a value indicating whether ShowRefreshText
        /// </summary>
        public bool ShowRefreshText => !_weatherDataAvailable;

        /// <summary>
        /// Gets a value indicating whether ParcelLocationNotSet
        /// </summary>
        public bool ParcelLocationNotSet
        {
            get
            {
                if (Parcel == null) return true;
                return (Parcel.Latitude == 0 && Parcel.Longitude == 0);
            }
        }

        /// <summary>
        /// The LoadWeatherDataAsync
        /// </summary>
        private async void LoadWeatherDataAsync()
        {
            _refreshedFromServer = true;
            Downloading = true;
            MyWeatherData = await WeatherService.GetWeatherData(_position);
        }

        /// <summary>
        /// The LoadWeatherFromDb
        /// </summary>
        /// <param name="parcelId">The <see cref="int"/></param>
        private void LoadWeatherFromDb(int parcelId)
        {
            MyWeatherData = _weatherDbOperations.GetWeatherData(parcelId);
        }

        /// <summary>
        /// The RefreshWeatherData
        /// </summary>
        private void RefreshWeatherData()
        {
            if (_parcel.Latitude != 0 || _parcel.Longitude != 0)
            {
                _position = new GeoPosition
                {
                    Latitude = Parcel.Latitude,
                    Longitude = Parcel.Longitude
                };
            }
            else
            {
                _position = new GeoPosition
                {
                    Latitude = 21.798344,
                    Longitude = -101.667537
                };
            }
            LoadWeatherDataAsync();
        }

        /// <summary>
        /// The ShowWeatherData
        /// </summary>
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
                {"Series", series},
                {"VariableName", DatasetNames.ElementAt(SelectedDataset)}
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null) Parcel = (Parcel)parcel;
            }
        }
    }
}