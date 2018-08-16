namespace CimmytApp.Calendar
{
    using CimmytApp.Calendar.Views;
    using Prism.Ioc;
    using Prism.Modularity;

    public class CalenderModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CalendarPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}