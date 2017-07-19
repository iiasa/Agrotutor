namespace Helper.UserRegistration
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class UserRegistrationModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public UserRegistrationModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<RegistrationPage>();
        }
    }
}