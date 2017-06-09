namespace CimmytApp.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;

        public event EventHandler IsActiveChanged;

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

        public ParcelPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
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