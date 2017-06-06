using CimmytApp.AgronomicalRecommendations.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

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