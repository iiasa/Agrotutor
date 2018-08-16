using Prism.Ioc;
using Prism.Modularity;
using CimmytApp.Com.Views;
using CimmytApp.Com.ViewModels;

namespace CimmytApp.Com
{
    public class ComModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        }
    }
}
