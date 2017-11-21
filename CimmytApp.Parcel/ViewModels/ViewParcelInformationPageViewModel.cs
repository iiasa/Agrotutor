namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.ComponentModel;
    using CimmytApp.DTO.Parcel;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;

    public class ViewParcelInformationPageViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware,
        INotifyPropertyChanged
    {
        private Parcel _parcel;

        public ViewParcelInformationPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            TestCommand = new DelegateCommand(Test);
        }

        public event EventHandler IsActiveChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsActive { get; set; }

        public Parcel Parcel
        {
            get
            {
                return _parcel;
            }
            set
            {
                _parcel = value;
                OnPropertyChanged("Parcel");
            }
        }

        public DelegateCommand TestCommand { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            PropertyChangedEventHandler iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private void Test()
        {
            //Parcel.Submit();
        }
    }
}