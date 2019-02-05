namespace Agrotutor.Modules.Calendar.Components.Views
{
    using Xamarin.Forms;
    using Types;

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
                this.Text = CalendarEvent.Title;
                if (CalendarEvent.Color != null)
                {
                    this.BackgroundColor = (Color)CalendarEvent.Color;
                }
                else
                {
                    this.BackgroundColor = Color.LightSkyBlue;
                }

            }
        }
    }
}