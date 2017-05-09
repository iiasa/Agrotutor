using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class RegistrationPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand RegistrationCommand { get; set; }

        public RegistrationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            RegistrationCommand = new DelegateCommand(RegisterClick);
        }

        private void RegisterClick()
        {
            //TODO: implement registration
        }
    }
}