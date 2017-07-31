using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CimmytApp.DTO;
using Helper.BusinessContract;
using Helper.DatasetSyncEvents.ViewModelBase;
using Helper.DTO.SkywiseWeather.Historical;
using Prism;
using Prism.Events;
using Prism.Navigation;
using static CimmytApp.WeatherForecast.WeatherService;

namespace CimmytApp.WeatherForecast.ViewModels
{
    public class WeatherDataViewViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private bool _initialized = false;
        private bool _isActive = false;
        private GeoPosition _location;

        public WeatherData WeatherData {get;set;}

        protected WeatherDataViewViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public bool IsActive 
        { 
            get {
                return _isActive;
            } 
            set {
                _isActive = value;
                if (_isActive && !_initialized){
                    //WeatherData = LoadWeatherData(); TODO load
                }
            } 
        }

        private async Task<WeatherData> LoadWeatherData()
        {
            return await GetWeatherData(_location);
        }

        public event EventHandler IsActiveChanged;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        protected override void ReadDataset(IDataset dataset)
        {
            throw new NotImplementedException();
        }
    }
}
