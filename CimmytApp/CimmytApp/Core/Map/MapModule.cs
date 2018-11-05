namespace CimmytApp.Core.Map
{
    using CimmytApp.Core.Map.ViewModels;
    using Prism.Ioc;
    using Prism.Modularity;

    public class MapModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Map>();
            containerRegistry.RegisterForNavigation<Views.MapMainPage, MapMainPageViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}