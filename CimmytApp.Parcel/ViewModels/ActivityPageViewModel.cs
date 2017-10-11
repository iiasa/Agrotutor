using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace CimmytApp.Parcel.ViewModels
{
    public class ActivityPageViewModel : BindableBase,INavigationAware
    {
        private INavigationService _navigationService;
        public DelegateCommand<string> ActivityClickedCommand { get; set; }
        public ActivityPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ActivityClickedCommand=new DelegateCommand<string>(ExecuteMethod);
        }

        private void ExecuteMethod(string activityType)
        {
            _navigationService.NavigateAsync("ActivityDetail",new NavigationParameters(){{ "activityType", activityType} });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
           
        }
    }
}
