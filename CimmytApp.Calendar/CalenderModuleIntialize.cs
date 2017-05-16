using Microsoft.Practices.Unity;
using Prism.Modularity;


//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CimmytApp.Calendar
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
         // _unityContainer.RegisterTypeForNavigation<GenericMap>();

        }
    }
}
