using Xamarin.Forms;

namespace CimmytApp.Core.Map.Views
{
    using CimmytApp.Core.Map.ViewModels;

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
        }

        public void EnableMyLocation()
        {
            this.map.MyLocationEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = true;
        }
    }
}
