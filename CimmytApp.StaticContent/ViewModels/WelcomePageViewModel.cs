namespace CimmytApp.StaticContent.ViewModels
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Modularity;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class WelcomePageViewModel : BindableBase, INavigationAware
    {
        private readonly IModuleManager _moduleManager;
        private readonly INavigationService _navigationService;

        public WelcomePageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _moduleManager = moduleManager;
            NavigateToMainPageCommand = new DelegateCommand(NavigateToMainPage);

            //_moduleManager.LoadModule("CalenderModuleIntialize");
            //_moduleManager.LoadModule("MapModuleIntialize");
        }

        public ICommand NavigateToMainPageCommand { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private void NavigateToMainPage()
        {
            _navigationService.NavigateAsync("app:///MainPage");
        }
    }
}