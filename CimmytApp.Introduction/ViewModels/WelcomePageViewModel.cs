namespace CimmytApp.Introduction.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Windows.Input;
    using Prism.Modularity;
    using Prism.Navigation;

    public class WelcomePageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private readonly IModuleManager _moduleManager;
        public ICommand NavigateToMainPageCommand { get; set; }

        public WelcomePageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _moduleManager = moduleManager;
            NavigateToMainPageCommand = new DelegateCommand(NavigateToMainPage);
            _moduleManager.LoadModule("CalenderModuleIntialize");
            _moduleManager.LoadModule("MapModuleIntialize");
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