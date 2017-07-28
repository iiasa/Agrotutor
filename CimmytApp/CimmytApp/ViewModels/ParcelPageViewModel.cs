namespace CimmytApp.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.RestfulClient;

    using DTO;
    using DTO.Parcel;
    using BusinessContract;
    using CimmytApp.MockData;
    using System.Linq;
    using System.ComponentModel;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
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
                OnPropertyChanged("Parcel");
                if (value!=null) PublishDataset(value);
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
            //ReadDataAsync();
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
            var id = (int)parameters["id"];
            Parcel = new TestParcels().ElementAt(id);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
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
		public event PropertyChangedEventHandler PropertyChanged;
    }
}