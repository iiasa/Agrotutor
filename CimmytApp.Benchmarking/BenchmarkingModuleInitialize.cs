namespace CimmytApp.Benchmarking
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;
    using Views;

    public class BenchmarkingModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public BenchmarkingModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LocalBenchmarkingPage>();
        }
    }
}