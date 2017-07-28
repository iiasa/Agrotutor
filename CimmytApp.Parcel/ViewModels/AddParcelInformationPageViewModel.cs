namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;
    using System.ComponentModel;

    public class AddParcelInformationPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
    {
        private bool isActive;
        public Parcel Parcel {
            get { return _parcel; }
            set {
                _parcel = value;
                OnPropertyChanged("Parcel");
            }
        }
        private Parcel _parcel;

        public AddParcelInformationPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive && !value && _parcel != null)
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
            Parcel = (Parcel)dataset;
        }

        protected override IDataset GetDataset()
        {
            return _parcel;
		}


		protected virtual void OnPropertyChanged(string aName)
		{
			var iHandler = PropertyChanged;
			iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
    }
}