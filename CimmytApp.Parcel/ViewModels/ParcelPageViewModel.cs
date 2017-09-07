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
        Parcel parcel;

        public Parcel Parcel
        {
            get => parcel;
            set => SetProperty(ref parcel, value);
        }

        public bool EditModeActive { get; set; }

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
