namespace CimmytApp.Benchmarking
{
    using CimmytApp.Benchmarking.Views;
    using CimmytApp.Core.Benchmarking.Views;
    using Prism.Modularity;
    using Prism.Ioc;

    public class BenchmarkingModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BenchmarkingPage>();
            containerRegistry.RegisterForNavigation<LocalBenchmarkingSelectionPage>();
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