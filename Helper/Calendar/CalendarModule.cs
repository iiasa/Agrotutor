namespace Helper.Calendar
{
    using Prism.Ioc;
    using Prism.Modularity;

    public class CalendarModule : IModule
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