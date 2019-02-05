namespace Agrotutor.iOS
{
    using Prism;
    using Prism.Ioc;
    using Core.Localization;

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
        }
    }
}