namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.DTO.SkywiseWeather.Historical;

    using BusinessContract;
    using DTO;
    using DTO.Parcel;

    public class WeatherDataSelectionViewModel : DatasetReceiverBindableBase, IActiveAware
    {
        private List<string> _datasetNames;
        private INavigationService _navigationService;
        private Parcel _parcel;
        private int _selectedDataset;
        private WeatherData _weatherData;
        private IWeatherDbOperations _weatherDbOperations;
        private bool _weatherDataAvailable = true;
        private bool isActive;
        private GeoPosition position;

        private bool refreshedFromServer = false;
        private bool _downloading;

        public bool Downloading
        {
            get => _downloading;
            set => SetProperty(ref _downloading, value);
        }

        public bool ViewInactive
        {
            get { return !Downloading; }
        }

        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator, INavigationService navigationService, IWeatherDbOperations weatherDbOperations) : base(eventAggregator)
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

        public event EventHandler IsActiveChanged;

        public List<string> DatasetNames
        {
            get => _datasetNames;
            set => SetProperty(ref _datasetNames, value);
        }

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public WeatherData MyWeatherData
        {
            get => _weatherData;

            set
            {
                var data = value;
                WeatherDataAvailable = true;
                data.ParcelId = Parcel.ParcelId;
                SetProperty(ref _weatherData, data);
                if (refreshedFromServer)
                {
                    _weatherDbOperations.UpdateWeatherData(data);
                    refreshedFromServer = false;
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
                catch { }
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

        public bool ShowRefreshText => !_weatherDataAvailable;

        public bool ParcelLocationNotSet
        {
            get
            {
                if (Parcel == null) return true;
                return (Parcel.Latitude == 0 && Parcel.Longitude == 0);
            }
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private async void LoadWeatherDataAsync()
        {
            refreshedFromServer = true;
            Downloading = true;
            MyWeatherData = await WeatherService.GetWeatherData(position);
        }

        private void LoadWeatherFromDb(int parcelId)
        {
            MyWeatherData = _weatherDbOperations.GetWeatherData(parcelId);
        }

        private void RefreshWeatherData()
        {
            if (_parcel.Latitude != 0 || _parcel.Longitude != 0)
            {
                position = new GeoPosition
                {
                    Latitude = Parcel.Latitude,
                    Longitude = Parcel.Longitude
                };
            }
            else
            {
                position = new GeoPosition
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
                {"Series", series},
                {"VariableName", DatasetNames.ElementAt(SelectedDataset)}
            };
            _navigationService.NavigateAsync(page, parameters);
        }
    }
}