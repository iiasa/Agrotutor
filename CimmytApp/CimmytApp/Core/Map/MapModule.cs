namespace CimmytApp.Core.Map
{
    using Prism.Ioc;
    using Prism.Modularity;

    public class MapModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Map>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}