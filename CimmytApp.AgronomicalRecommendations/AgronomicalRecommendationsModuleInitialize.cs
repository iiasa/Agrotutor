using CimmytApp.AgronomicalRecommendations.Views;

namespace CimmytApp.AgronomicalRecommendations
{
    public class AgronomicalRecommendationsModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public AgronomicalRecommendationsModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LocalAgronomicalRecommendationsPage>();
        }
    }
}