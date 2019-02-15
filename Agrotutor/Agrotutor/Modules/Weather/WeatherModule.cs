namespace Agrotutor.Modules.Weather
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;

    public class WeatherModule : IModule
    {
        void IModule.OnInitialized(IContainerProvider containerProvider)
        {
        }

        void IModule.RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherForecastPage, WeatherForecastPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherHistoryPage, WeatherHistoryPageViewModel>();
        }
    }
}
