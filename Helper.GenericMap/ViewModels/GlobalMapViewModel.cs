using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Helper.Base.Contract;

namespace Helper.GenericMap.ViewModels
{
  public  class GlobalMapViewModel: BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IEventAggregator _eventAggregator;

        private readonly IPosition _geoLocator;
        public GlobalMapViewModel(IEventAggregator eventAggregator,  INavigationService navigationService)
        {
            _navigationService = navigationService;

            _eventAggregator = eventAggregator;

          //  _geoLocator = geoLocator;
        }
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
