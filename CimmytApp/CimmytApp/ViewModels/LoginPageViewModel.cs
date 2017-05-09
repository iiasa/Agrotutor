using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(LoginClick);
        }

        private void LoginClick()
        {
            //TODO: implement login
        }
    }
}