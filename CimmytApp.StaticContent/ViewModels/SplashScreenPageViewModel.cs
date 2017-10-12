﻿namespace CimmytApp.StaticContent.ViewModels
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
            await Task.Delay(SecondsActive * 1000);
            _navigationService.NavigateAsync(_nextPage);
        }
    }
}