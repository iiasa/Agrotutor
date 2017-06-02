using CimmytApp.DTO;
using CimmytApp.PublishSubscriberEvents;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CimmytApp.ViewModels
{
    public class AddParcelInformationPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private readonly IEventAggregator _eventAggregator;
        private bool isActive;
        public AddParcelInformationPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool IsActive
        {
            get { return isActive; }
            set {
                if (isActive && !value)
                {
                    _eventAggregator.GetEvent<ParcelInSyncEvent>().Publish(new Parcel() { ID=1,ParcelName="Test"});
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
    }
}
