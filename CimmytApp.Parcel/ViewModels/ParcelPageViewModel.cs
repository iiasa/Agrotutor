using System;
using Helper.BusinessContract;
using Helper.DatasetSyncEvents.ViewModelBase;
using Prism;
using Prism.Events;
using Prism.Navigation;

namespace CimmytApp.Parcel.ViewModels
{
    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
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
            throw new NotImplementedException();
        }

        protected override void ReadDataset(IDataset dataset)
        {
            throw new NotImplementedException();
        }
    }
}
