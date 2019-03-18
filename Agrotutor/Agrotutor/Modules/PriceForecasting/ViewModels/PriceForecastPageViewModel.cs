using System;
using System.Collections.Generic;
using System.Linq;
using Agrotutor.Core;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.ViewModels;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Agrotutor.Modules.PriceForecasting.ViewModels
{
    public class PriceForecastPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PriceForecastParameterName = "PRICE_FORECAST_PARAMETER";
        private List<PriceForecast> _priceForecasts;
        private decimal _nextMax;
        private decimal _nextMin;
        private decimal _nextExpected;
        private List<PriceForecast> _furtherItems;

        public decimal NextMax
        {
            get => _nextMax;
            set => SetProperty(ref _nextMax, value);
        }

        public decimal NextMin
        {
            get => _nextMin;
            set => SetProperty(ref _nextMin, value);
        }

        public decimal NextExpected
        {
            get => _nextExpected;
            set => SetProperty(ref _nextExpected, value);
        }

        public List<PriceForecast> PriceForecasts
        {
            get => _priceForecasts;
            set
            {
                SetProperty(ref _priceForecasts, value);
                if (value != null)
                {
                    SetNextMonth(value.ElementAt(0));
                    var count = value.Count;
                    if (count > 2)
                    {
                        SetFollowingMonths(value.GetRange(1, count - 1));
                    }
                }
            }
        }

        private void SetFollowingMonths(List<PriceForecast> items)
        {
            FurtherItems = items;
        }

        public List<PriceForecast> FurtherItems
        {
            get => _furtherItems;
            set => SetProperty(ref _furtherItems, value);
        }

        private void SetNextMonth(PriceForecast forecast)
        {
            if (forecast == null) return;
            NextExpected = Math.Round((decimal)forecast.Price,0);
            NextMin = Math.Round((decimal)forecast.MinPrice, 0);
            NextMax = Math.Round((decimal)forecast.MaxPrice, 0);
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
            if (PriceForecasts == null)
            {
                if (parameters.ContainsKey(PriceForecastParameterName))
                {
                    parameters.TryGetValue<IEnumerable<PriceForecast>>(PriceForecastParameterName, out var forecast);
                    if (forecast != null)
                    {
                        PriceForecasts = forecast.OrderBy(x => x.Month).ToList();
                    }
                }
            }

            base.OnNavigatedTo(parameters);
        }
        public DelegateCommand ShowAbout => new DelegateCommand(async () =>
        {
            var param = new NavigationParameters { { "page", WebContentPageViewModel.PriceForecasting } };
            await NavigationService.NavigateAsync("WebContentPage", param);
        });
    }
}
