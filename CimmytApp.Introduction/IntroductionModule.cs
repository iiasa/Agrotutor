namespace CimmytApp.Introduction
{
    using CimmytApp.Introduction.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

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
        }
    }
}