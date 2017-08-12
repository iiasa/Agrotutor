namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.DTO.SkywiseWeather.Historical;

    using DTO;
    using DTO.Parcel;

    public class WeatherDataSelectionViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private List<string> _datasetNames;
        private Parcel _parcel;
        private int _selectedDataset;
        private WeatherData _weatherData;
        private bool isActive;
        private GeoPosition position;

        public WeatherDataSelectionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DatasetNames = new List<string>
            {
            };
            ShowWeatherDataCommand = new DelegateCommand(ShowWeatherData);
        }

        private void ShowWeatherData()
        {
            throw new NotImplementedException();
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
                else
                {
                    //Show msg - no location
                }
            }
        }

        public int SelectedDataset
        {
            get => _selectedDataset;
            set => SetProperty(ref _selectedDataset, value);
        }

        public DelegateCommand ShowWeatherDataCommand { get; set; }

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

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private async void LoadWeatherDataAsync()
        {
            WeatherData = await WeatherService.GetWeatherData(position);
        }
    }
}