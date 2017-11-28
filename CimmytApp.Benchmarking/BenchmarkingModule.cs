namespace CimmytApp.Benchmarking
{
    using CimmytApp.Benchmarking.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    public class BenchmarkingModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public BenchmarkingModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<BenchmarkingPage>();
            _unityContainer.RegisterTypeForNavigation<LocalBenchmarkingSelectionPage>();
            _unityContainer.RegisterTypeForNavigation<ViewCostoPage>();
            _unityContainer.RegisterTypeForNavigation<ViewIngresoPage>();
            _unityContainer.RegisterTypeForNavigation<ViewRendimientoPage>();
            _unityContainer.RegisterTypeForNavigation<ViewUtilidadPage>();
        }
    }
}