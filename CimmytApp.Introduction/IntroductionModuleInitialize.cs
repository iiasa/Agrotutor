using CimmytApp.Introduction.Views;
using CimmytApp.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace CimmytApp.Introduction
{
    public class IntroductionModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public IntroductionModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<WelcomePage>();
            _unityContainer.RegisterTypeForNavigation<WelcomeContentPage>();
        }
    }
}