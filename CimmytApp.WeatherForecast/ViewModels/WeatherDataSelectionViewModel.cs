namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.DTO.SkywiseWeather.Historical;

    using BusinessContract;
    using DTO;
    using DTO.Parcel;

    public class WeatherDataSelectionViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private List<string> _datasetNames;
        private INavigationService _navigationService;
        private Parcel _parcel;
        private int _selectedDataset;
        private WeatherData _weatherData;
        private IWeatherDbOperations _weatherDbOperations;
        private bool isActive;
        private GeoPosition position;

        private bool refreshedFromServer = false;

        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator, INavigationService navigationService, IWeatherDbOperations weatherDbOperations) : base(eventAggregator)
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

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                LoadWeatherFromDb(value.ParcelId);
            }
        }

        private void LoadWeatherFromDb(int parcelId)
        {
            _weatherDbOperations.GetWeatherData(parcelId);
        }

        public DelegateCommand RefreshWeatherDataCommand { get; set; }

        public int SelectedDataset
        {
            get => _selectedDataset;
            set => SetProperty(ref _selectedDataset, value);
        }

        public DelegateCommand ShowWeatherDataCommand { get; set; }

        public WeatherData WeatherData
        {
            get => _weatherData;

            set
            {
                var data = value;
                data.ParcelId = Parcel.ParcelId;
                SetProperty(ref _weatherData, data);
                if (refreshedFromServer)
                {
                    _weatherDbOperations.UpdateWeatherData(data);
                }
            }
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

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private async void LoadWeatherDataAsync()
        {
            refreshedFromServer = true;
            WeatherData = await WeatherService.GetWeatherData(position);
        }

        private void RefreshWeatherData()
        {
            if (_parcel.Latitude != null && _parcel.Longitude != null) // TODO - check if undefined - ==0.0?
            {
                if (_parcel.Latitude == 0 && _parcel.Longitude == 0) return;
                position = new GeoPosition
                {
                    Latitude = Parcel.Latitude,
                    Longitude = Parcel.Longitude
                };
                LoadWeatherDataAsync();
            }
        }

        private void ShowWeatherData()
        {
            var page = "";
            HistoricalSeries series = null;
            switch (SelectedDataset)
            {
                case 0:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.GrowingDegreeDays;
                    break;

                case 1:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.CoolingDegreeDays;
                    break;

                case 2:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.HeatingDegreeDays;
                    break;

                case 3:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailyPrecipitation;
                    break;

                case 4:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyPrecipitation;
                    break;

                case 5:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyRelativeHumidity;
                    break;

                case 6:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailySolarRadiation;
                    break;

                case 7:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlySolarRadiation;
                    break;

                case 8:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyTemperature;
                    break;

                case 9:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailyHighTemperature;
                    break;

                case 10:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailyLowTemperature;
                    break;

                case 11:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyDewpoint;
                    break;

                case 12:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyWindSpeed;
                    break;

                case 13:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyWindDirection;
                    break;

                case 14:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailyEvapotranspirationShortCrop;
                    break;

                case 15:
                    page = "DailyWeatherDataPage";
                    series = WeatherData.DailyEvapotranspirationTallCrop;
                    break;

                case 16:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyEvapotranspirationShortCrop;
                    break;

                case 17:
                    page = "HourlyWeatherDataPage";
                    series = WeatherData.HourlyEvapotranspirationTallCrop;
                    break;
            }

            var parameters = new NavigationParameters
            {
                {"Series", series},
                {"VariableName", DatasetNames.ElementAt(SelectedDataset)}
            };
            _navigationService.NavigateAsync(page, parameters);
        }
    }
}