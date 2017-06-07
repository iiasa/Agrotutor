using Helper.BusinessContract;
using Helper.DatasetSyncEvents.ViewModelBase;

namespace CimmytApp.DTO.Parcel.ViewModels
{
    using Prism;
    using Prism.Events;
    using Prism.Navigation;
    using System;

    public class AddParcelInformationPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private bool isActive;
        private Parcel _parcel;

        public AddParcelInformationPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive && !value)
                {
                    PublishDataset(_parcel);
                }
                isActive = value;
            }
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

        protected override void ReadDataset(IDataset dataset)
        {
            _parcel = (Parcel)dataset;
        }
    }
}