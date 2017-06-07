using Helper.PublishSubscriberEvents;
using Prism.Events;

namespace CimmytApp.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using Prism.Navigation;
    using Prism;

    using DTO.Parcel;

    public class ParcelPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;
        private readonly IEventAggregator _eventAggregator;

        public event EventHandler IsActiveChanged;

        public Parcel Parcel
        {
            get { return _parcel; }
            set
            {
                SetProperty(ref _parcel, value);
                _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(value);
            }
        }

        public bool IsActive
        {
            get;
            set;
        }

        public ParcelPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Subscribe(SyncDataset);
        }

        private void SyncDataset()
        {
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(_parcel);
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
    }
}