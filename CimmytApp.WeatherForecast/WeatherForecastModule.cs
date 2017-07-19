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
            _unityContainer.RegisterTypeForNavigation<WeatherForecastPage>();
        }
    }
}