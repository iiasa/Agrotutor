namespace CimmytApp.StaticContent
{
    using CimmytApp.StaticContent.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

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
            _unityContainer.RegisterTypeForNavigation<CitationPage>();
            _unityContainer.RegisterTypeForNavigation<WelcomePage>();
        }
    }
}