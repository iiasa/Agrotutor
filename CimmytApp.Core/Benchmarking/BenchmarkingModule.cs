namespace CimmytApp.Benchmarking
{
    using CimmytApp.Benchmarking.Views;
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
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}