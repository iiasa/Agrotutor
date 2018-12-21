namespace CimmytApp.Core.Benchmarking
{
    using CimmytApp.Core.Benchmarking.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class BenchmarkingModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BenchmarkingPage>();
            containerRegistry.RegisterForNavigation<ViewCostoPage>();
            containerRegistry.RegisterForNavigation<ViewIngresoPage>();
            containerRegistry.RegisterForNavigation<ViewRendimientoPage>();
            containerRegistry.RegisterForNavigation<ViewUtilidadPage>();
            containerRegistry.RegisterForNavigation<CiatSelectionPage>();
            containerRegistry.RegisterForNavigation<CiatContentPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}