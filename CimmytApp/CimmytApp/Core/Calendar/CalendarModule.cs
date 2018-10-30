namespace CimmytApp.Core.Calendar
{
    using Prism.Ioc;
    using Prism.Modularity;

    public class CalendarModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.CalendarPage>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}