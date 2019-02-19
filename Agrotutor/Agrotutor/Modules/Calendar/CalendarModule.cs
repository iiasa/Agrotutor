namespace Agrotutor.Modules.Calendar
{
    using Prism.Ioc;
    using Prism.Modularity;

    using Views;
    using ViewModels;
    using Agrotutor.Modules.Calendar.Components.Views;

    public class CalendarModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CalendarPage, CalendarPageViewModel>();
            containerRegistry.RegisterForNavigation<EventInfoPopup, EventInfoPopupViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}
