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

#pragma warning disable CS0067 // The event 'ViewParcelInformationPageViewModel.IsActiveChanged' is never used
        public event EventHandler IsActiveChanged;
#pragma warning restore CS0067 // The event 'ViewParcelInformationPageViewModel.IsActiveChanged' is never used

#pragma warning disable CS0108 // 'ViewParcelInformationPageViewModel.PropertyChanged' hides inherited member 'BindableBase.PropertyChanged'. Use the new keyword if hiding was intended.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // 'ViewParcelInformationPageViewModel.PropertyChanged' hides inherited member 'BindableBase.PropertyChanged'. Use the new keyword if hiding was intended.

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

#pragma warning disable CS0114 // 'ViewParcelInformationPageViewModel.OnPropertyChanged(string)' hides inherited member 'BindableBase.OnPropertyChanged(string)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        protected virtual void OnPropertyChanged(string aName)
#pragma warning restore CS0114 // 'ViewParcelInformationPageViewModel.OnPropertyChanged(string)' hides inherited member 'BindableBase.OnPropertyChanged(string)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
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