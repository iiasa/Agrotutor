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
        }
    }
}