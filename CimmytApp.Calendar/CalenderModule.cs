namespace CimmytApp.Calendar
{
    using CimmytApp.Calendar.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

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
            _unityContainer.RegisterTypeForNavigation<TelerikCalendarPage>();
        }
    }
}