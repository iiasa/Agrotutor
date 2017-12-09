namespace CimmytApp.StaticContent
{
    using CimmytApp.StaticContent.Views;
    using Prism.Modularity;
    using Prism.Unity;
    using Unity;

    public class StaticContentModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public StaticContentModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer; s
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LinksPage>();
            _unityContainer.RegisterTypeForNavigation<SplashScreenPage>();
            _unityContainer.RegisterTypeForNavigation<CitationPage>();
            _unityContainer.RegisterTypeForNavigation<WelcomePage>();
        }
    }
}