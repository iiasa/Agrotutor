using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Modularity;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class WelcomePageViewModel : BindableBase, INavigationAware
    {
        public INavigationService _navigationService { get; set; }
        private readonly IModuleManager _moduleManager;
        public ICommand NavigateToMainPageCommand { get; set; }

        public WelcomePageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _moduleManager = moduleManager;
            NavigateToMainPageCommand = new DelegateCommand(NavigateToMainPage);
            _moduleManager.LoadModule("CalenderModuleIntialize");
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