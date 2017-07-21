using CimmytApp.DTO.BEM;

namespace CimmytApp.ViewModels
{
    using Prism.Modularity;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        private readonly IModuleManager _moduleManager;
        private INavigationService _navigationService;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainPageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _moduleManager = moduleManager;
            _navigationService = navigationService;
            BemData bd = new BemData();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = "Welcome to this piece of shit!";
        }
    }
}