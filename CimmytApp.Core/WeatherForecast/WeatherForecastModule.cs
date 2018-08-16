namespace CimmytApp.WeatherForecast
{
    using CimmytApp.WeatherForecast.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class WeatherForecastModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WeatherDataSelection>();
            containerRegistry.RegisterForNavigation<DailyWeatherDataPage>();
            containerRegistry.RegisterForNavigation<HourlyWeatherDataPage>();
            containerRegistry.RegisterForNavigation<WeatherMainPage>();
            containerRegistry.RegisterForNavigation<DailyForecastPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}