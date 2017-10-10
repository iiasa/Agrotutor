namespace CimmytApp.StaticContent
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class StaticContentModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public StaticContentModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LinksPage>();
            _unityContainer.RegisterTypeForNavigation<SplashScreenPage>();
            _unityContainer.RegisterTypeForNavigation<CitationPage>();
        }
    }
}