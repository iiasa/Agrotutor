using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;

namespace CimmytApp.StaticContent.ViewModels
{
    public class SplashScreenPageViewModel : BindableBase, INavigationAware
    {
        private const int secondsActive = 5;
        private INavigationService _navigationService;
        private bool showGuide = false;

        public SplashScreenPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Task.Delay(secondsActive * 1000);
            _navigationService.NavigateAsync("MainPage");
            /*
            Task.Run(async () =>
            {
                await Task.Delay(secondsActive * 1000);
                if (parameters.ContainsKey("ShowGuide") && (bool)parameters["ShowGuide"])
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateAsync("WelcomePage");
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateAsync("MainPage");
                    });
                }
            });*/
        }
    }
}