namespace CimmytApp.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Telerik.XamarinForms.Input;
    using Xamarin.Forms;

    public class TelerikCalendarPageViewModel : BindableBase, INavigationAware
    {
        private List<Appointment> _events;
        private List<Parcel> _parcels;

        public List<Appointment> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcels = new List<Parcel>
                    {
                        parcel
                    };
                }
            }
            if (parameters.ContainsKey("Parcels"))
            {
                parameters.TryGetValue<List<Parcel>>("Parcels", out var parcels);
                if (parcels != null)
                {
                    Parcels = parcels;
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private void PopulateEvents()
        {
            var events = (from parcel in Parcels
                          where parcel.AgriculturalActivities != null
                          from activity in parcel.AgriculturalActivities
                          select new Appointment
                          {
                              IsAllDay = true,
                              StartDate = activity.Date.UtcDateTime,
                              EndDate = activity.Date.UtcDateTime,
                              Title = activity.Name,
                              Color = Color.PaleGreen
                          }).ToList();

            foreach (var windows in Parcels.Select(parcel => parcel.GetWindowsForFertilization()))
            {
                events.AddRange(windows.Select(window => new Appointment
                {
                    IsAllDay = true,
                    StartDate = window.Date,
                    EndDate = window.Date,
                    Title = "Oportunidad para la fertilización",
                    Color = Color.BurlyWood
                }));
            }
            Events = events;
        }
    }

    public class Appointment : IAppointment
    {
        public Color Color { get; set; }

        public string Detail { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsAllDay { get; set; }

        public DateTime StartDate { get; set; }

        public string Title { get; set; }
    }
}