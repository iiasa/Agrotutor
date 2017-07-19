namespace CimmytApp.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;
    using CimmytApp.BusinessContract;
    using Helper.RestfulClient;
    using CimmytApp.DTO;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;

        public event EventHandler IsActiveChanged;

        private IWeatherDbOperations _weatherDbOperations;

        public Parcel Parcel
        {
            get { return _parcel; }
            set
            {
                SetProperty(ref _parcel, value);
                PublishDataset(value);
            }
        }

        public bool IsActive
        {
            get;
            set;
        }

        public ParcelPageViewModel(IEventAggregator eventAggregator, IWeatherDbOperations weatherDbOperations) : base(eventAggregator)
        {
            _weatherDbOperations = weatherDbOperations;
            ReadDataAsync();
        }

        private async System.Threading.Tasks.Task ReadDataAsync()
        {
            var x = new RestfulClient<WeatherData>();
            var z = await x.RefreshDataAsync("https://wsgi.geo-wiki.org/skywise_weather?lat=48.16&lng=16.15");
            if (z != null)
            {
                _weatherDbOperations.AddWeatherData(z);
                var res = _weatherDbOperations.GetAllWeatherData();
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Parcel = (Parcel)parameters["parcel"];
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected override void ReadDataset(IDataset dataset)
        {
            _parcel = (Parcel)dataset;
        }

        protected override IDataset GetDataset()
        {
            return _parcel;
        }
    }
}