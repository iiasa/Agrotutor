namespace CimmytApp.Core.Map.Views
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
            XF.Material.Forms.Material.PlatformConfiguration.ChangeStatusBarColor(new Color(1, 1, 1, 0.5));
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = (MapMainPageViewModel)BindingContext;
            ViewModel.SetView(this);
            // this.map.UiSettings.CompassEnabled = true;
            // this.map.UiSettings.MyLocationButtonEnabled = false;
            // this.map.UiSettings.RotateGesturesEnabled = true;
            // this.map.UiSettings.ZoomControlsEnabled = true;
            // this.map.UiSettings.TiltGesturesEnabled = true;
            // this.map.UiSettings.ZoomGesturesEnabled = true;
            // this.map.UiSettings.MapToolbarEnabled = true;
           
            // remove below
            //this.map.UiSettings.MyLocationButtonEnabled = true;
        }

        public void EnableMyLocation()
        {
            //this.map.MyLocationEnabled = true;
            //this.map.UiSettings.MyLocationButtonEnabled = true;
        }

        internal void AddPlots(IEnumerable<Plot> plots)
        {
            foreach (Plot plot in plots)
            {
                // this.map.Pins.Add(new Pin
                // {
                //     Position = plot.Position.ForMap(),
                //     Label = plot.Name,
                //     Tag = plot
                // });
            }
        }

        void Handle_MapClicked(object sender, MapClickedEventArgs e) =>
            ViewModel.MapClicked.Execute(e);

        void Handle_MapLongClicked(object sender, MapLongClickedEventArgs e) =>
            ViewModel.MapLongClicked.Execute(e);

        void Handle_PinClicked(object sender, PinClickedEventArgs e) =>
            ViewModel.PinClicked.Execute(e);
    }
}
