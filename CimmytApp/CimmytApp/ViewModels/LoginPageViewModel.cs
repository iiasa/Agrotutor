namespace CimmytApp.ViewModels
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class LoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(LoginClick);
            NavigateToRegistrationCommand = new DelegateCommand(NavigateToRegistrationClick);
        }

        public ICommand LoginCommand { get; set; }

        public ICommand NavigateToRegistrationCommand { get; set; }

        private void LoginClick()
        {
            //TODO- implement login
        }

        private void NavigateToRegistrationClick()
        {
            _navigationService.NavigateAsync("RegistrationPage");
        }
    }
}