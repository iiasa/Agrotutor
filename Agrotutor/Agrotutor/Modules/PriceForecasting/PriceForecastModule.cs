using Agrotutor.Modules.PriceForecasting.ViewModels;
using Agrotutor.Modules.PriceForecasting.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Agrotutor.Modules.PriceForecasting
{
    public class PriceForecastModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PriceForecastPage, PriceForecastPageViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}
