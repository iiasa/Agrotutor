using CimmytApp.BusinessContract;

namespace CimmytApp.DTO.Parcel.ViewModels
{
    using Helper.PublishSubscriberEvents;
    using Prism;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System;

    public class ViewParcelInformationPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;
        private readonly IEventAggregator _eventAggregator;
        private bool isActive;

        public Parcel Parcel
        {
            get { return _parcel; }
            set { _parcel = value; }
        }

        public ViewParcelInformationPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ParcelInSyncEvent>().Subscribe(ReadParcelData);
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
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