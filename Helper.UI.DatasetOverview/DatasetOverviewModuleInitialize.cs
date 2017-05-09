namespace Helper.UI.DatasetOverview
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    public class DatasetOverviewModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public DatasetOverviewModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<Views.DatasetOverview>();
        }
    }
}