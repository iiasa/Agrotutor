//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CimmytApp.Calendar
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    public class CalenderModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public CalenderModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<CalendarPage>();
        }
    }
}