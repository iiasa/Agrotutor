namespace CimmytApp.AgronomicalRecommendations
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class AgronomicalRecommendationsModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public AgronomicalRecommendationsModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LocalAgronomicalRecommendationsPage>();
        }
    }
}