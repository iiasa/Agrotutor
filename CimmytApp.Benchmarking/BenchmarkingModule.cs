namespace CimmytApp.Benchmarking
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;
    using Views;

    public class BenchmarkingModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public BenchmarkingModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LocalBenchmarkingPage>();
        }
    }
}