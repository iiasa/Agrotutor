namespace CimmytApp.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.RestfulClient;

    using DTO;
    using DTO.Parcel;
    using BusinessContract;
    using CimmytApp.MockData;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private Parcel _parcel;

        private List<WeatherData> _weatherData;

        private IWeatherDbOperations _weatherDbOperations;

        public ParcelPageViewModel(IEventAggregator eventAggregator, IWeatherDbOperations weatherDbOperations) : base(eventAggregator)
        {
            _weatherDbOperations = weatherDbOperations;
            ReadDataAsync();
        }

        public event EventHandler IsActiveChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsActive
        {
            get;
            set;
        }

        public Parcel Parcel
        {
            get { return _parcel; }
            set
            {
                SetProperty(ref _parcel, value);
                OnPropertyChanged("Parcel");
                if (value != null) PublishDataset(value);
            }
        }

        public List<WeatherData> WeatherData
        {
            get { return _weatherData; }
            set { SetProperty(ref _weatherData, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var id = (int)parameters["id"];
            Parcel = new TestParcels().ElementAt(id);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected override IDataset GetDataset()
        {
            return Parcel;
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private async System.Threading.Tasks.Task ReadDataAsync()
        {
            var restfulClient = new RestfulClient<WeatherData>();
            var response = await restfulClient.RefreshDataAsync($"https://wsgi.geo-wiki.org/skywise_weather?lat={Parcel.GeoPosition.Latitude}&lng={Parcel.GeoPosition.Longitude}");
            if (response != null)
            {
                _weatherDbOperations.AddWeatherData(response);
                var returnData = _weatherDbOperations.GetAllWeatherData();
                WeatherData = returnData;
            }
        }
    }
}