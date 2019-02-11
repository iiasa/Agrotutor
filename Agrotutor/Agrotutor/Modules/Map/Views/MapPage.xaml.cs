namespace Agrotutor.Modules.Map.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ImTools;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;
    using Xamarin.Forms.Xaml;

    using Core.Entities;
    using Core.Cimmyt.HubsContact;
    using Core.Cimmyt.InvestigationPlatforms;
    using Core.Cimmyt.MachineryPoints;
    using HubFeature = Core.Cimmyt.HubsContact.Feature;
    using IPFeature = Core.Cimmyt.InvestigationPlatforms.Feature;
    using MPFeature = Core.Cimmyt.MachineryPoints.Feature;
    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPageViewModel ViewModel { get; set; }

        public IList<Pin> PlotPins { get; set; }
        public IList<Polygon> PlotDelineations { get; set; }

        public IList<Pin> DelineationPins { get; set; }
        public IList<Xamarin.Forms.GoogleMaps.Position> DelineationPositions { get; set; }
        public Polygon DelineationPolygon { get; set; }

        public IList<Pin> HubContactPins { get; set; }
        public IList<Pin> MachineryPointPins { get; set; }
        public IList<Pin> InvestigationPlatformPins { get; set; }

        public MapPage()
        {
            InitializeComponent();
            ViewModel = (MapPageViewModel)BindingContext;
            ViewModel.SetView(this);
            XF.Material.Forms.Material.PlatformConfiguration.ChangeStatusBarColor(new Color(1, 1, 1, 0.5));
            NavigationPage.SetHasNavigationBar(this, false);
            this.map.UiSettings.CompassEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = false;
            this.map.UiSettings.RotateGesturesEnabled = true;
            this.map.UiSettings.ZoomControlsEnabled = true;
            this.map.UiSettings.TiltGesturesEnabled = true;
            this.map.UiSettings.ZoomGesturesEnabled = true;
            this.map.UiSettings.MapToolbarEnabled = true;
            this.map.MyLocationEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = true;

            PlotPins = new List<Pin>();
            PlotDelineations = new List<Polygon>();
            DelineationPins = new List<Pin>();
            DelineationPositions = new List<Xamarin.Forms.GoogleMaps.Position>();

            HubContactPins = new List<Pin>();
            InvestigationPlatformPins = new List<Pin>();
            MachineryPointPins = new List<Pin>();
        }

        public void EnableMyLocation()
        {
            
        }

        internal void AddPlots(IEnumerable<Plot> plots)
        {
            foreach (Pin pin in from plot in plots
                                where plot.Position != null
                                select new Pin
                                {
                                    Position = plot.Position.ForMap(),
                                    Label = plot.Name ?? "",
                                    Tag = plot,
                                    Icon = BitmapDescriptorFactory.DefaultMarker((Color) App.Current.Resources["PrimaryGreen"])
                                })
            {
                PlotPins.Append(pin);
                Device.BeginInvokeOnMainThread(
                    () => { this.map.Pins.Add(pin); });
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
                StrokeColor = Color.Green,
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

            RestorePins();
            this.map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Xamarin.Forms.GoogleMaps.Position(20, -100), 5), TimeSpan.FromSeconds(2));
        }

        public void SetPlotLayerVisibility(bool visible)
        {
            this.PlotPins?.All(x => x.IsVisible = visible);
        }

        private void RestorePins()
        {
            foreach (Pin pin in HubContactPins)
            {
                Device.BeginInvokeOnMainThread(() => { this.map.Pins.Add(pin); });
            }
            foreach (Pin pin in InvestigationPlatformPins)
            {
                Device.BeginInvokeOnMainThread(() => { this.map.Pins.Add(pin); });
            }
            foreach (Pin pin in MachineryPointPins)
            {
                Device.BeginInvokeOnMainThread(() => { this.map.Pins.Add(pin); });
            }
            foreach (Pin pin in PlotPins)
            {
                Device.BeginInvokeOnMainThread(() => { this.map.Pins.Add(pin); });
            }
        }

        public void SetPlotDelineationLayerVisibility(bool value)
        {
            // TODO implement
        }

        public void SetHubContactsLayerVisibility(bool visible)
        {
            this.HubContactPins?.All(x => x.IsVisible = visible);
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

            this.map.Polygons.Clear();
            DelineationPolygon.Positions.Clear();
            foreach (var delineationPosition in DelineationPositions)
            {
                DelineationPolygon.Positions.Add(delineationPosition);
            }

            if (DelineationPolygon.Positions.Count > 2)
            {
                this.map.Polygons.Add(DelineationPolygon);
            }
        }

        public void SetMachineryPointLayerVisibility(bool visible)
        {
            var map = this.map;
            var pins = map.Pins.Where(x => (x.Tag is MPFeature));
            pins.All(x => x.IsVisible = visible);
        }

        public void SetInvestigationPlatformLayerVisibility(bool visible)
        {
            this.InvestigationPlatformPins?.All(x => x.IsVisible = visible);
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

        public void SetOfflineLayerVisibility(bool value)
        {
            // TODO: implement
        }

        public void SetHubsContact(HubsContact hubContacts)
        {
            foreach (HubFeature hubContact in hubContacts.Features)
            {
                Pin pin = new Pin
                {
                    Position = new Xamarin.Forms.GoogleMaps.Position(
                                  hubContact.Geometry.Coordinates[1],
                                  hubContact.Geometry.Coordinates[0]),
                    Tag = hubContact,
                    Label = hubContact.Properties.Hub,
                    Icon = BitmapDescriptorFactory.DefaultMarker((Color)App.Current.Resources["SecondaryOrange"])
                };
                this.HubContactPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { this.map.Pins.Add(pin); });
            }
        }

        public void SetInvestigationPlatforms(InvestigationPlatforms investigationPlatforms)
        {
            foreach (IPFeature investigationPlatform in investigationPlatforms.Features)
            {
                Pin pin = new Pin
                {
                    Position = new Xamarin.Forms.GoogleMaps.Position(
                                                investigationPlatform.Geometry.Coordinates[1], investigationPlatform.Geometry.Coordinates[0]),
                    Tag = investigationPlatform,
                    Label = investigationPlatform.Properties.Abrviacion,
                    Icon = BitmapDescriptorFactory.DefaultMarker((Color)App.Current.Resources["SecondaryGreenBrown"])
                };
                this.InvestigationPlatformPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { this.map.Pins.Add(pin); });
            }
        }

        public void SetMachineryPoints(MachineryPoints machineryPoints)
        {
            foreach (MPFeature machineryPoint in machineryPoints.Features)
            {
                Pin pin = new Pin
                {
                    Position = new Xamarin.Forms.GoogleMaps.Position(
                                                machineryPoint.Geometry.Coordinates[1], machineryPoint.Geometry.Coordinates[0]),
                    Tag = machineryPoint,
                    Label = machineryPoint.Properties.Localidad,
                    Icon = BitmapDescriptorFactory.DefaultMarker((Color)App.Current.Resources["SecondaryDarkGreen"])
                };
                this.MachineryPointPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { this.map.Pins.Add(pin); });
            }
        }
    }
}