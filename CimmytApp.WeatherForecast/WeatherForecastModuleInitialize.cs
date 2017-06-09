namespace CimmytApp.WeatherForecast
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    internal class WeatherForecastModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public WeatherForecastModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<WeatherForecastPage>();
        }
    }
}