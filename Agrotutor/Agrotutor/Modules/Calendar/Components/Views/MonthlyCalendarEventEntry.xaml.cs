using System;

namespace Agrotutor.Modules.Calendar.Components.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Types;
    using Prism.Navigation;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthlyCalendarEventEntry : Label
    {
        private CalendarEvent calendarEvent;

        public MonthlyCalendarEventEntry()
        {
            InitializeComponent();
        }

        public CalendarEvent CalendarEvent
        {
            get => this.calendarEvent;
            set
            {
                this.calendarEvent = value;
                this.Text = CalendarEvent.Title ?? "Activity";
                if (CalendarEvent.PlotColor != null)
                {
                    this.BackgroundColor = (Color)CalendarEvent.PlotColor;
                }
                else
                {
                    this.BackgroundColor = Color.LightSkyBlue;
                }

            }
        }

        public INavigationService NavigationService { get; set; }

        private void ClickGestureRecognizer_OnClicked(object sender, EventArgs e)
        {
            NavigationService?.NavigateAsync("EventInfoPopup",new NavigationParameters(){{"event", CalendarEvent } });
        }
    }
}