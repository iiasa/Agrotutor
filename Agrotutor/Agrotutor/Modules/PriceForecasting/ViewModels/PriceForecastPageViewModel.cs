using System.Collections.Generic;
using Agrotutor.Core;
using Agrotutor.Modules.PriceForecasting.Types;
using Microsoft.Extensions.Localization;
using Prism.Navigation;

namespace Agrotutor.Modules.PriceForecasting.ViewModels
{
    public class PriceForecastPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PriceForecastParameterName = "PRICE_FORECAST_PARAMETER";
        private List<PriceForecast> _priceForecasts;

        public List<PriceForecast> PriceForecasts
        {
            get => _priceForecasts;
            set => SetProperty(ref _priceForecasts, value);
        }

        public PriceForecastPageViewModel(INavigationService navigationService, IStringLocalizer<PriceForecastPageViewModel> stringLocalizer) 
            : base(navigationService, stringLocalizer)
        {

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(PriceForecastParameterName))
            {
                parameters.TryGetValue<List<PriceForecast>>(PriceForecastParameterName, out var forecast);
                if (forecast != null)
                {
                    PriceForecasts = forecast;
                }
            }
        }
    }
}
