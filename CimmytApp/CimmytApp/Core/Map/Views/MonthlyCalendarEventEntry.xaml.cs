﻿namespace CimmytApp.Core.Map.Views
{
    using System;
    using CimmytApp.Core.Calendar.Types;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

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