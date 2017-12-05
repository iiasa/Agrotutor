namespace CimmytApp.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Telerik.XamarinForms.Input;
    using Xamarin.Forms;

    public class TelerikCalendarPageViewModel : BindableBase, INavigationAware
    {
        private List<Parcel> _parcels;
        private List<Appointment> _events;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcels = new List<Parcel>();
                    Parcels.Add((Parcel)parcel);
                }
            }
            if (parameters.ContainsKey("Parcels"))
            {
                parameters.TryGetValue("Parcels", out var parcels);
                if (parcels != null)
                {
                    Parcels = (List<Parcel>)parcels;
                }
            }
        }

        public List<Parcel> Parcels
        {
            get => _parcels;
            set
            {
                SetProperty(ref _parcels, value);
                PopulateEvents();
            }
        }

        private void PopulateEvents()
        {
            var events = new List<Appointment>();
            foreach (Parcel parcel in Parcels)
            {
                if (parcel.AgriculturalActivities != null)
                {
                    foreach (var activity in parcel.AgriculturalActivities)
                    {
                        events.Add(new Appointment
                        {
                            IsAllDay = true,
                            StartDate = activity.Date,
                            EndDate = activity.Date,
                            Title = activity.Name,
                            Color = Color.PaleGreen
                        });
                    }
                }
            }

            Events = events;
        }

        public List<Appointment> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }
    }

    public class Appointment : IAppointment
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public Color Color { get; set; }

        public bool IsAllDay { get; set; }

        public string Detail { get; set; }
    }
}