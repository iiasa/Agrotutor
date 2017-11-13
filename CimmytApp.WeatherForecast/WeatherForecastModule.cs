namespace CimmytApp.WeatherForecast
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class WeatherForecastModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public WeatherForecastModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<WeatherDataSelection>();
            _unityContainer.RegisterTypeForNavigation<DailyWeatherDataPage>();
            _unityContainer.RegisterTypeForNavigation<HourlyWeatherDataPage>();
            _unityContainer.RegisterTypeForNavigation<WeatherMainPage>();
            _unityContainer.RegisterTypeForNavigation<DailyForecastPage>();
        }
    }
}