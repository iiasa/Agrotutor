namespace Helper.UserRegistration
{
    using Helper.UserRegistration.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class UserRegistrationModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RegistrationPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}