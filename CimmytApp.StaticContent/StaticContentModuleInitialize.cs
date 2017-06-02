using CimmytApp.StaticContent.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace CimmytApp.StaticContent
{
    public class StaticContentModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public StaticContentModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LinksPage>();
        }
    }
}