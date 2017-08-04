//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

using Helper.Map.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace Helper.Map
{ 

    public class MapModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public MapModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<GenericMap>();
        }
    }
}