namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism;
    using Prism.Events;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;
    using System.ComponentModel;

    public class ViewParcelInformationPageViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private Parcel _parcel;
        private bool isActive;

        public Parcel Parcel
        {
            get { return _parcel; }
            set
            {
                _parcel = value;
                OnPropertyChanged("Parcel");
            }
        }

        public DelegateCommand TestCommand {get; set;}

        public ViewParcelInformationPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            TestCommand = new DelegateCommand(Test);
        }

        private void Test()
        {
            //Parcel.Submit();
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
            Parcel = (Parcel)dataset;
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}