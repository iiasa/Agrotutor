namespace Helper.Map
{
    using Helper.Map.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class MapModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Map>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}