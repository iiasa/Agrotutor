namespace CimmytApp.Parcel
{
    using CimmytApp.Parcel.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    public class ParcelModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ParcelModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<ActivityPage>();
            _unityContainer.RegisterTypeForNavigation<ViewActivitiesPage>();
            _unityContainer.RegisterTypeForNavigation<ActivityDetail>();
            _unityContainer.RegisterTypeForNavigation<AddParcelPage>();
            _unityContainer.RegisterTypeForNavigation<DeleteParcelPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelsOverviewPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelPage>();
            _unityContainer.RegisterTypeForNavigation<SelectTechnologiesPage>();
        }
    }
}