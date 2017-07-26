namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Forms;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;

    public class ViewParcelInformationPageViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;
        private bool isActive;
        public ICommand TestCommand { get; set; }

        public Parcel Parcel
        {
            get { return _parcel; }
            set { _parcel = value; }
        }

        public ViewParcelInformationPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            TestCommand = new Command(Test);
        }

        private void Test()
        {
            Parcel.Submit();
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
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