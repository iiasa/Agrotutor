namespace Agrotutor.Modules.Plot
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;

    public class PlotModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AddPlotPage, AddPlotPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityPage, ActivityPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityDetail, ActivityDetailViewModel>();
            containerRegistry.RegisterForNavigation<PlotMainPage, PlotMainPageViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}
