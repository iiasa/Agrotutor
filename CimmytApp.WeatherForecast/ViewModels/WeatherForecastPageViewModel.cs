namespace CimmytApp.WeatherForecast.ViewModels
{
    using Prism.Mvvm;
    using System.Windows.Input;
    using Xamarin.Forms;
    using DTO.Parcel;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using System;
    using Helper.BusinessContract;
    using Prism.Events;
    using CimmytApp.DTO;
    using Prism.Navigation;
    using System.ComponentModel;
    using Prism;
    using Helper.DTO.SkywiseWeather.Historical;

    public class WeatherForecastPageViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
	{
		private bool isActive;
        private Parcel _parcel;
        private GeoPosition position;

        public Parcel Parcel
        {
            get { return _parcel; }
            set { 
                SetProperty(ref _parcel, value);
                position = Parcel.GeoPosition;
                LoadWeatherDataAsync();
            }
        }

        private async void LoadWeatherDataAsync()
        {
            WeatherData = await WeatherService.GetWeatherData(position);
        }

        private WeatherData _weatherData;

        public WeatherData WeatherData
        {
            get
            {
                return _weatherData;
            }

            set
            {
                _weatherData = value;
                RaisePropertyChanged("WeatherData");
            }
        }

		public ICommand TestCommand { get; set; }

		public bool IsActive
		{
			get { return isActive; }
			set
			{
				isActive = value;
			}
		}

		public event EventHandler IsActiveChanged;

        public WeatherForecastPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            TestCommand = new Command(Test);
        }

        public void Test()
        {
            var i = 0;
            i++;
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
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
    }
}