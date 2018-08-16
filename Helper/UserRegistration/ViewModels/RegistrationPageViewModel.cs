namespace Helper.UserRegistration.ViewModels
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class RegistrationPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        public RegistrationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            RegistrationCommand = new DelegateCommand(RegisterClick);
        }

        public ICommand RegistrationCommand { get; set; }

        private void RegisterClick()
        {
            //TODO- implement registration
        }
    }
}