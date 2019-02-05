namespace Agrotutor.Modules.Ciat
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;

    public class CiatModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CiatPage, CiatPageViewModel>();
        }
    }
}
