using System.Collections.Generic;
using Prism.Navigation;

namespace CimmytApp.WeatherForecast.ViewModels
{
    using Prism.Mvvm;

    public class DailyForecastPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        public List<DailySummary> DailySummaries { get; set; }

        public DailyForecastPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("DailyForecast"))
            {
                parameters.TryGetValue("DailyForecast", out var forecast);
                if (forecast != null)
                {
                    DailySummaries = (List<DailySummary>)forecast;
                }
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }
    }
}