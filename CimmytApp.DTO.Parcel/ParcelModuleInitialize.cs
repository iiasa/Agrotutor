namespace CimmytApp.DTO.Parcel
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class ParcelModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ParcelModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<AddParcelInformationPage>();
            _unityContainer.RegisterTypeForNavigation<AddParcelPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelsOverviewPage>();
            _unityContainer.RegisterTypeForNavigation<ViewParcelInformationPage>();
        }
    }
}