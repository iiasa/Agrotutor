using Helper.Map.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Helper.Map
{
    public class PictureModuleIntialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public PictureModuleIntialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<GenericMap>();
        }
    }
}