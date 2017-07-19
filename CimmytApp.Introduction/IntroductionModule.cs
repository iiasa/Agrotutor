namespace CimmytApp.Introduction
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;
    using Views;

    public class IntroductionModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public IntroductionModule(IUnityContainer unityContainer)
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