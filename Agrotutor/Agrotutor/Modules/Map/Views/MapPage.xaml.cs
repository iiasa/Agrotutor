using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agrotutor.Core.Camera;
using Agrotutor.Core.Cimmyt.HubsContact;
using Agrotutor.Core.Cimmyt.InvestigationPlatforms;
using Agrotutor.Core.Cimmyt.MachineryPoints;
using Agrotutor.Modules.Map.ViewModels;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using ImTools;
using Prism;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using XF.Material.Forms;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;
using Feature = Agrotutor.Core.Cimmyt.HubsContact.Feature;

namespace Agrotutor.Modules.Map.Views
{
    using HubFeature = Feature;
    using IPFeature = Core.Cimmyt.InvestigationPlatforms.Feature;
    using MPFeature = Core.Cimmyt.MachineryPoints.Feature;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            ViewModel = (MapPageViewModel) BindingContext;
            ViewModel.SetView(this);
            Material.PlatformConfiguration.ChangeStatusBarColor(new Color(1, 1, 1, 0.5));
            NavigationPage.SetHasNavigationBar(this, false);
            map.UiSettings.CompassEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.RotateGesturesEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.TiltGesturesEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.MapToolbarEnabled = true;
            PlotPins = new List<Pin>();
            PlotDelineations = new List<Polygon>();
            DelineationPins = new List<Pin>();
            DelineationPositions = new List<Position>();
            HubContactPins = new List<Pin>();
            InvestigationPlatformPins = new List<Pin>();
            MachineryPointPins = new List<Pin>();
        }

        public MapPageViewModel ViewModel { get; set; }

        public IList<Pin> PlotPins { get; set; }
        public IList<Polygon> PlotDelineations { get; set; }

        public IList<Pin> DelineationPins { get; set; }
        public IList<Position> DelineationPositions { get; set; }
        public Polygon DelineationPolygon { get; set; }

        public IList<Pin> HubContactPins { get; set; }
        public IList<Pin> MachineryPointPins { get; set; }
        public IList<Pin> InvestigationPlatformPins { get; set; }

        public void EnableMyLocation()
        {
        }

        internal void AddPlots(IEnumerable<Core.Entities.Plot> plots)
        {
            foreach (var pin in from plot in plots
                where plot.Position != null
                select new Pin
                {
                    Position = plot.Position.ForMap(),
                    Label = plot.Name ?? "",
                    Tag = plot,
                    Icon = BitmapDescriptorFactory.DefaultMarker(
                        (Color) PrismApplicationBase.Current.Resources["PrimaryGreen"])
                })
            {
                PlotPins.Append(pin);
                Device.BeginInvokeOnMainThread(
                    () => { map.Pins.Add(pin); });
            }
        }

        public void ZoomToPosition(Position position)
        {
            map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(position, 16), TimeSpan.FromSeconds(2));
        }

        private void Handle_MapClicked(object sender, MapClickedEventArgs e)
        {
            ViewModel.MapClicked.Execute(e);
        }

        private void Handle_MapLongClicked(object sender, MapLongClickedEventArgs e)
        {
            ViewModel.MapLongClicked.Execute(e);
        }

        private void Handle_PinClicked(object sender, PinClickedEventArgs e)
        {
            ViewModel.PinClicked.Execute(e);
        }

        public void StartDelineation(Core.Entities.Plot plot)
        {
            DelineationPolygon = new Polygon
            {
                StrokeColor = Color.Green,
                StrokeWidth = 2f
            };
            map.Polygons.Clear();
            map.Pins.Clear();

            map.Pins.Add(new Pin
            {
                Position = plot.Position.ForMap(),
                Label = ""
            });
        }

        public void EndDelineation()
        {
            map.Polygons.Clear();
            map.Pins.Clear();

            map.Pins.Append(PlotPins);
            map.Polygons.Append(PlotDelineations);

            RestorePins();
            map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(20, -100), 5), TimeSpan.FromSeconds(2));
        }

        public void SetPlotLayerVisibility(bool visible)
        {
            PlotPins?.All(x => x.IsVisible = visible);
        }

        private void RestorePins()
        {
            foreach (var pin in HubContactPins) Device.BeginInvokeOnMainThread(() => { map.Pins.Add(pin); });
            foreach (var pin in InvestigationPlatformPins) Device.BeginInvokeOnMainThread(() => { map.Pins.Add(pin); });
            foreach (var pin in MachineryPointPins) Device.BeginInvokeOnMainThread(() => { map.Pins.Add(pin); });
            foreach (var pin in PlotPins) Device.BeginInvokeOnMainThread(() => { map.Pins.Add(pin); });
        }

        public void SetPlotDelineationLayerVisibility(bool value)
        {
            // TODO implement
        }

        public void SetHubContactsLayerVisibility(bool visible)
        {
            HubContactPins?.All(x => x.IsVisible = visible);
        }

        public void AddDelineationPoint(Position position)
        {
            DelineationPositions.Add(position);
            var pin = new Pin
            {
                Position = position,
                Label = ""
            };
            DelineationPins.Add(pin);
            map.Pins.Add(pin);

            map.Polygons.Clear();
            DelineationPolygon.Positions.Clear();
            foreach (var delineationPosition in DelineationPositions)
                DelineationPolygon.Positions.Add(delineationPosition);

            if (DelineationPolygon.Positions.Count > 2) map.Polygons.Add(DelineationPolygon);
        }

        public void SetMachineryPointLayerVisibility(bool visible)
        {
            InvestigationPlatformPins?.All(x => x.IsVisible = visible);
        }

        public void SetInvestigationPlatformLayerVisibility(bool visible)
        {
            InvestigationPlatformPins?.All(x => x.IsVisible = visible);
        }

        public void RemoveLastDelineationPoint()
        {
            if (DelineationPins.Count > 0)
            {
                var position = DelineationPins.Count - 1;
                DelineationPins.RemoveAt(position);
                map.Pins.RemoveAt(position);
                DelineationPolygon.Positions.RemoveAt(position);
            }

            map.Polygons.Clear();
            if (DelineationPolygon.Positions.Count > 2) map.Polygons.Add(DelineationPolygon);
        }

        public void SetOfflineLayerVisibility(bool value)
        {
            // TODO: implement
        }

        public void SetHubsContact(HubsContact hubContacts)
        {
            foreach (var hubContact in hubContacts.Features)
            {
                var pin = new Pin
                {
                    Position = new Position(
                        hubContact.Geometry.Coordinates[1],
                        hubContact.Geometry.Coordinates[0]),
                    Tag = hubContact,
                    Label = hubContact.Properties.Hub,
                    Icon = BitmapDescriptorFactory.DefaultMarker(
                        (Color) PrismApplicationBase.Current.Resources["SecondaryOrange"])
                };
                HubContactPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { map.Pins.Add(pin); });
            }
        }

        public void SetInvestigationPlatforms(InvestigationPlatforms investigationPlatforms)
        {
            foreach (var investigationPlatform in investigationPlatforms.Features)
            {
                var pin = new Pin
                {
                    Position = new Position(
                        investigationPlatform.Geometry.Coordinates[1], investigationPlatform.Geometry.Coordinates[0]),
                    Tag = investigationPlatform,
                    Label = investigationPlatform.Properties.Abrviacion,
                    Icon = BitmapDescriptorFactory.DefaultMarker(
                        (Color) PrismApplicationBase.Current.Resources["SecondaryGreenBrown"])
                };
                InvestigationPlatformPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { map.Pins.Add(pin); });
            }
        }

        public void SetMachineryPoints(MachineryPoints machineryPoints)
        {
            foreach (var machineryPoint in machineryPoints.Features)
            {
                var pin = new Pin
                {
                    Position = new Position(
                        machineryPoint.Geometry.Coordinates[1], machineryPoint.Geometry.Coordinates[0]),
                    Tag = machineryPoint,
                    Label = machineryPoint.Properties.Localidad,
                    Icon = BitmapDescriptorFactory.DefaultMarker(
                        (Color) PrismApplicationBase.Current.Resources["SecondaryDarkGreen"])
                };
                MachineryPointPins.Add(pin);
                Device.BeginInvokeOnMainThread(
                    () => { map.Pins.Add(pin); });
            }
        }



        public async Task UpdateImages()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("UpdatingImages"))
            {
                AllImages.Children.Clear();
                if (ViewModel.SelectedPlot?.MediaItems != null && (bool)ViewModel.SelectedPlot?.MediaItems.Any())
                    foreach (var img in ViewModel.SelectedPlot.MediaItems)
                    {
                        var layout = new AbsoluteLayout { Margin = 10 };
                        var imageSource = img.IsVideo
                            ? ImageSource.FromResource("BirdLife.Core.UI.Assets.Images.video.png", typeof(AssetsHelper))
                            : ImageSource.FromFile(img.Path);

                        var closeImage = new CachedImage
                        {
                            HorizontalOptions = LayoutOptions.End,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 20,
                            WidthRequest = 20,
                            DownsampleToViewSize = true,
                            Source = ImageSource.FromResource("BirdLife.Core.UI.Assets.Images.close-cross.png",
                                typeof(AssetsHelper))
                        };

                        var cachedImage = new CachedImage
                        {
                            HeightRequest = 150,
                            WidthRequest = 150,
                            IsVisible = true,
                            Source = imageSource,
                            ClassId = img.Id.ToString(),
                            CacheType = CacheType.Disk,
                            DownsampleToViewSize = true
                        };

                        layout.HeightRequest = 150;
                        layout.WidthRequest = 150;
                        AbsoluteLayout.SetLayoutBounds(closeImage, new Rectangle(1, 0, 30, 30));
                        AbsoluteLayout.SetLayoutFlags(closeImage, AbsoluteLayoutFlags.All);
                        layout.Children.Add(cachedImage);
                        layout.Children.Add(closeImage);
                        layout.RaiseChild(closeImage);

                        closeImage.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(async o =>
                            {
                                ViewModel.SelectedPlot?.MediaItems.Remove(img);
                                await UpdateImages();
                            }),
                            NumberOfTapsRequired = 1
                        });

                        AllImages.Children.Add(layout);
                    }
            }
        }
    }
}