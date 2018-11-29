namespace CimmytApp.Core.Map.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.Core.Map.ViewModels;
    using CimmytApp.Core.Persistence.Entities;
    using ImTools;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;
    using Xamarin.Forms.Xaml;

    public partial class MapMainPage : ContentPage
    {
        public MapMainPageViewModel ViewModel { get; set; }

        public IList<Pin> PlotPins { get; set; }
        public IList<Polygon> PlotDelineations { get; set; }

        public IList<Pin> DelineationPins { get; set; }
        public IList<Xamarin.Forms.GoogleMaps.Position> DelineationPositions { get; set; }
        public Polygon DelineationPolygon { get; set; }

        public MapMainPage()
        {
            InitializeComponent();
            XF.Material.Forms.Material.PlatformConfiguration.ChangeStatusBarColor(new Color(1, 1, 1, 0.5));
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

            PlotPins = new List<Pin>();
            PlotDelineations = new List<Polygon>();
            DelineationPins = new List<Pin>();
            DelineationPositions = new List<Xamarin.Forms.GoogleMaps.Position>();
        }

        public void EnableMyLocation()
        {
            this.map.MyLocationEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = true;
        }

        internal void AddPlots(IEnumerable<Plot> plots)
        {
            foreach (Pin pin in plots.Select(plot => new Pin
            {
                Position = plot.Position.ForMap(),
                Label = plot.Name,
                Tag = plot
            }))
            {
                PlotPins.Append(pin);
                this.map.Pins.Add(pin);
            }
        }

        public void ZoomToPosition(Xamarin.Forms.GoogleMaps.Position position)
        {
            this.map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(position, 16), TimeSpan.FromSeconds(2));
        }

        private void Handle_MapClicked(object sender, MapClickedEventArgs e) =>
            ViewModel.MapClicked.Execute(e);

        private void Handle_MapLongClicked(object sender, MapLongClickedEventArgs e) =>
            ViewModel.MapLongClicked.Execute(e);

        private void Handle_PinClicked(object sender, PinClickedEventArgs e) =>
            ViewModel.PinClicked.Execute(e);

        public void StartDelineation(Plot plot)
        {
            DelineationPolygon = new Polygon
            {
                StrokeColor = Color.Green, // TODO!!
                StrokeWidth = 2f
            };
            this.map.Polygons.Clear();
            this.map.Pins.Clear();

            this.map.Pins.Add(new Pin
            {
                Position = plot.Position.ForMap(),
                Label = ""
            });
        }

        public void EndDelineation()
        {
            this.map.Polygons.Clear();
            this.map.Pins.Clear();

            this.map.Pins.Append(PlotPins);
            this.map.Polygons.Append(PlotDelineations);
        }

        public void AddDelineationPoint(Xamarin.Forms.GoogleMaps.Position position)
        {
            DelineationPositions.Add(position);
            var pin = new Pin
            {
                Position = position,
                Label = ""
            };
            DelineationPins.Add(pin);
            this.map.Pins.Add(pin);
            DelineationPolygon.Positions.Clear();
            foreach (var delineationPosition in DelineationPositions)
            {
                DelineationPolygon.Positions.Add(delineationPosition);
            }

            this.map.Polygons.Clear();
            if (DelineationPolygon.Positions.Count > 2)
            {
                this.map.Polygons.Add(DelineationPolygon);
            }
        }

        public void RemoveLastDelineationPoint()
        {
            if (DelineationPins.Count > 0)
            {
                var position = DelineationPins.Count - 1;
                DelineationPins.RemoveAt(position);
                this.map.Pins.RemoveAt(position);
                this.DelineationPolygon.Positions.RemoveAt(position);
            }

            this.map.Polygons.Clear();
            if (DelineationPolygon.Positions.Count > 2)
            {
                this.map.Polygons.Add(DelineationPolygon);
            }
        }
    }
}
