using Agrotutor.Core;
using Agrotutor.Core.DependencyServices;
using Microsoft.Extensions.Localization;
using Prism.Navigation;
using System;
using System.Threading;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Agrotutor.ViewModels
{
    public class WebContentPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string Activities = "priceforecasting"; // TODO: add activity about page
        public static string CultivarCharacteristics = "cultivarcharacteristics";
        public static string PotentialYield = "potentialyield";
        public static string LocalBenchmarking = "localbenchmarking";
        public static string RecommendedPractices = "recommendedpractices";
        public static string PriceForecasting = "priceforecasting";

        public WebContentPageViewModel(INavigationService navigationService, IStringLocalizer<WebContentPageViewModel> stringLocalizer) 
            : base(navigationService, stringLocalizer)
        {
            _rootUrl = DependencyService.Get<IHtmlBaseUrl>().Get();
            Url = $"{_rootUrl}empty.html";
        }

        private string _url;
        private string _rootUrl;

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public override void OnNavigatedFrom(INavigationParameters navigationParameters)
        {
            base.OnNavigatedFrom(navigationParameters);
        }

        public override void OnNavigatedTo(INavigationParameters navigationParameters)
        {
            var supportedLang = new List<string>
            {
                "en", "es"
            };
            var lang = "en";
            try
            {
                var currentLang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                if (supportedLang.Contains(currentLang))
                {
                    lang = currentLang;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (navigationParameters.ContainsKey("page"))
            {
                navigationParameters.TryGetValue<string>("page", out var page);
                if (page != null) Url = $"{_rootUrl}{lang}/{page}/page.html";
                else NavigationService.GoBackAsync();
            }
            else NavigationService.GoBackAsync();

            base.OnNavigatedTo(navigationParameters);
        }
    }
}
