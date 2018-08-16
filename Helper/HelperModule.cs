using Prism.Ioc;
using Prism.Modularity;

namespace Helper
{
    public class HelperModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        }
    }
}