using CimmytApp.Calendar.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;


//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CimmytApp.Calendar
{
    public class CalenderModuleIntialize : IModule
    {
        private readonly IUnityContainer _unityContainer;
        public CalenderModuleIntialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
          _unityContainer.RegisterTypeForNavigation<CalendarPage>();

        }
    }
}
