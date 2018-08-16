namespace CimmytApp.StaticContent
{
    using CimmytApp.StaticContent.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class StaticContentModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LinksPage>();
            containerRegistry.RegisterForNavigation<CitationPage>();
            containerRegistry.RegisterForNavigation<WelcomePage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}