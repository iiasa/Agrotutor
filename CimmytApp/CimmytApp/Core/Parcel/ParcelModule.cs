namespace CimmytApp.Core.Parcel
{
    using CimmytApp.Core.Parcel.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class ParcelModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ActivityPage>();
            containerRegistry.RegisterForNavigation<ActivityDetail>();
            containerRegistry.RegisterForNavigation<ViewActivitiesPage>();
            containerRegistry.RegisterForNavigation<AddParcelPage>();
            containerRegistry.RegisterForNavigation<DeleteParcelPage>();
            containerRegistry.RegisterForNavigation<ParcelsOverviewPage>();
            containerRegistry.RegisterForNavigation<ParcelPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}