namespace CimmytApp.DTO.Parcel
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class ParcelModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ParcelModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
        }
    }
}