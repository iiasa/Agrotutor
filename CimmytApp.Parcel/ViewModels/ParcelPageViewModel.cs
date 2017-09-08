namespace CimmytApp.Parcel.ViewModels
{
	using System;
	using Helper.BusinessContract;
	using Helper.DatasetSyncEvents.ViewModelBase;
	using Prism;
	using Prism.Events;
	using Prism.Navigation;
	using DTO.Parcel;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;

        public Parcel Parcel
        {
            get => _parcel;
            set => SetProperty(ref _parcel, value);
        }
        bool _editModeActive;

        public bool EditModeActive
        {
            get => _editModeActive;
            set => SetProperty(ref _editModeActive, value);
        }

        public ParcelPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
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

        protected override IDataset GetDataset()
        {
            return Parcel;
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }
    }
}
