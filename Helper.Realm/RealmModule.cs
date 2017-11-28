namespace Helper.Realm
{
    using Helper.Realm.ViewModels;
    using Helper.Realm.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    public class RealmModule : IModule
    {
        private readonly IUnityContainer _container;

        public RealmModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA, ViewAViewModel>();
        }
    }
}