namespace CimmytApp.Core.Calendar.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms;
    using CimmytApp.Core.Calendar.Types;
    using CimmytApp.Core.Calendar.ViewModels;
    using CimmytApp.Core.Map.Views;

    public partial class CalendarPage : ContentPage
    {
        private List<ContentView> dayViews = new List<ContentView>();

        public CalendarPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.ViewModel = (CalendarPageViewModel)BindingContext;
            ViewModel.SetView(this);
        }

        public CalendarPageViewModel ViewModel { get; set; }

        public void ShowPeriod(DateTime firstDay, CalendarMode currentCalendarMode)
        {
            var indexFirstWeekday = Helper.IndexOfWeekday(firstDay);
            var daysInMonth = Helper.DaysInMonth(firstDay);
            var gridElements = this.MonthlyCalendarGrid.Children;
            gridElements.Clear();

            for (var i = 0; i < daysInMonth; i++)
            {
                var date = firstDay.AddDays(i);
                var positionLinear = i + indexFirstWeekday;
                var positionRow = (int)Math.Floor(positionLinear / 7.0);
                var positionColumn = positionLinear - positionRow * 7;
                var calendarEvents = ViewModel.Events?.Where(e => e.StartTime.Date.Equals(date)).ToList();
                MonthlyCalendarDayView monthlyCalendarDayView = new MonthlyCalendarDayView
                {
                    CalendarEvents = calendarEvents,
                    Date = date
                };
                gridElements.Add(monthlyCalendarDayView);
                Grid.SetRow(monthlyCalendarDayView, positionRow);
                Grid.SetColumn(monthlyCalendarDayView, positionColumn);
                this.dayViews.Add(monthlyCalendarDayView);
            }
        }
    }
}
