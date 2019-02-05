namespace Agrotutor.Modules.Benchmarking
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;

    public class BenchmarkingModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BenchmarkingPage, BenchmarkingPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewCostPage, ViewCostPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewIncomePage, ViewIncomePageViewModel>();
            containerRegistry.RegisterForNavigation<ViewProfitPage, ViewProfitPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewYieldPage, ViewYieldPageViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}
