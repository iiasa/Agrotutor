using Xamarin.Forms;

namespace CimmytApp.Core.Map.Views
{
    using CimmytApp.Core.Map.ViewModels;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;

    public partial class MapMainPage : ContentPage
    {
        public MapMainPageViewModel ViewModel { get; set; }

        public MapMainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = (MapMainPageViewModel)BindingContext;
            initializeMap();
        }

        private async void initializeMap()
        {
            var perm = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            this.map.UiSettings.CompassEnabled = true;
            this.map.UiSettings.MyLocationButtonEnabled = true;
            this.map.UiSettings.RotateGesturesEnabled = true;
            this.map.UiSettings.ZoomControlsEnabled = true;
            this.map.UiSettings.TiltGesturesEnabled = true;
            this.map.UiSettings.ZoomGesturesEnabled = true;
            this.map.UiSettings.MapToolbarEnabled = true;

        }
    }
}
