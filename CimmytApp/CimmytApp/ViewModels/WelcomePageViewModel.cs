using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class WelcomePageViewModel : BindableBase, INavigationAware
    {
        public INavigationService _navigationService { get; set; }
        public ICommand NavigateToMainPageCommand { get; set; }

        public WelcomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToMainPageCommand = new DelegateCommand(NavigateToMainPage);
        }

        private void NavigateToMainPage()
        {
            _navigationService.NavigateAsync("MainPage");
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