using CimmytApp.Benchmarking.Views;

namespace CimmytApp.Benchmarking
{
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