using System;
using System.ComponentModel;
using Helper.BusinessContract;
using Helper.DatasetSyncEvents.ViewModelBase;
using Prism;
using Prism.Events;
using Prism.Navigation;

namespace CimmytApp.WeatherForecast.ViewModels
{
    public class WeatherDataViewViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
	{

        protected WeatherDataViewViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
