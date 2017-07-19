namespace CimmytApp.DTO.BEM
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    public class BEMModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public BEMModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
        }
    }
}