namespace Helper.UserRegistration.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Windows.Input;
    using Prism.Navigation;

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