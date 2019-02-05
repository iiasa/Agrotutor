namespace Agrotutor.Modules.Map
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;

    public class MapModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}
