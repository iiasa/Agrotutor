using CimmytApp.BusinessContract;

namespace CimmytApp.DTO.Parcel.ViewModels
{
    using Helper.PublishSubscriberEvents;
    using Prism;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System;

    public class AddParcelInformationPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private readonly IEventAggregator _eventAggregator;
        private bool isActive;
        private Parcel _parcel;

        public AddParcelInformationPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetSyncEvent>().Subscribe(ReadParcelData);
            _eventAggregator.GetEvent<DatasetAvailableForSyncEvent>().Subscribe(OnDatasetAvailableForSync);
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Publish();
        }

        private void OnDatasetAvailableForSync()
        {
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Publish();
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive && !value)
                {
                    _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(_parcel);
                }
                isActive = value;
            }
        }

        private void ReadParcelData(IDataset parcelObj)
        {
            _parcel = (Parcel)parcelObj;
        }

        public event EventHandler IsActiveChanged;

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