﻿namespace CimmytApp.Core.Map.Views
{
    using System.Collections.Generic;
    using CimmytApp.Core.Map.ViewModels;
    using CimmytApp.Core.Persistence.Entities;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public partial class MapMainPage : ContentPage
    {
        public MapMainPageViewModel ViewModel { get; set; }

        public MapMainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = (MapMainPageViewModel)BindingContext;
            ViewModel.SetView(this);
            this.map.UiSettings.CompassEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = false;
            this.map.UiSettings.RotateGesturesEnabled = true;
            this.map.UiSettings.ZoomControlsEnabled = true;
            this.map.UiSettings.TiltGesturesEnabled = true;
            this.map.UiSettings.ZoomGesturesEnabled = true;
            this.map.UiSettings.MapToolbarEnabled = true;
            // remove below
            this.map.UiSettings.MyLocationButtonEnabled = true;
        }

        public void EnableMyLocation()
        {
            this.map.MyLocationEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = true;
        }

        internal void AddPlots(IEnumerable<Plot> plots)
        {
            foreach (Plot plot in plots)
            {
                this.map.Pins.Add(new Pin
                {
                    Anchor = new Point
                    {
                        X=plot.Position.Longitude,
                        Y=plot.Position.Latitude
                    },
                    Label = plot.Name,
                    Tag = plot
                });
            }
        }
    }
}
