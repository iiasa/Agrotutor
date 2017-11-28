namespace CimmytApp.StaticContent.ViewModels
{
    using System.Threading.Tasks;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class SplashScreenPageViewModel : BindableBase, INavigationAware
    {
        private const int SecondsActive = 5;

        private readonly INavigationService _navigationService;
        private string _nextPage = "MainPage";

        public SplashScreenPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("ShowGuide") && (bool)parameters["ShowGuide"])
            {
                _nextPage = "WelcomePage";
            }
            GoToNextPage();
        }

        private async void GoToNextPage()
        {
            await Task.Delay(SplashScreenPageViewModel.SecondsActive * 1000);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            _navigationService.NavigateAsync(_nextPage);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}