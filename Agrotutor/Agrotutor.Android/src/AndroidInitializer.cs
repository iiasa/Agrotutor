using Agrotutor.Core.Components;

namespace Agrotutor.Droid
{
    using Prism;
    using Prism.Ioc;
    using Core.Localization;

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
            containerRegistry.RegisterSingleton<IDocumentViewer, DocumentViewer_Droid>();
        }
    }
}