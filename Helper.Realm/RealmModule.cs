using Prism.Modularity;
using Helper.Realm.Views;
using Helper.Realm.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Helper.Realm
{
    public class RealmModule : IModule
    {
        private IUnityContainer _container;

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
