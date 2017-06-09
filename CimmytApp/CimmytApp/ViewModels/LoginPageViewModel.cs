namespace CimmytApp.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Windows.Input;
    using Prism.Navigation;

    public class LoginPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand LoginCommand { get; set; }
        public ICommand NavigateToRegistrationCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(LoginClick);
            NavigateToRegistrationCommand = new DelegateCommand(NavigateToRegistrationClick);
        }

        private void LoginClick()
        {
            //TODO: implement login
        }

        private void NavigateToRegistrationClick()
        {
            _navigationService.NavigateAsync("RegistrationPage");
        }
    }
}