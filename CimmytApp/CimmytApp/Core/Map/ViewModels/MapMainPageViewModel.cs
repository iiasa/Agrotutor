namespace CimmytApp.Core.Map.ViewModels
{
    using System;
    using Acr.UserDialogs;
    using CimmytApp.Core.Map.Views;
    using CimmytApp.ViewModels;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.Extensions.Localization;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Essentials;
    using Xamarin.Forms.GoogleMaps;

    public class MapMainPageViewModel : ViewModelBase, INavigatedAware
	{
        public MapMainPageViewModel(INavigationService navigationService,
            IStringLocalizer<MapMainPageViewModel> localizer) : base(localizer)
        {
            LocationPermissionAvailable = false;
            NavigationService = navigationService;
        }

        public MapMainPage MapMainPage { get; set; }
        public INavigationService NavigationService { get; set; }
        public DelegateCommand NavigateToMain => new DelegateCommand(() => NavigationService.NavigateAsync("MainPage"));
        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.OpenSettings);

        public bool LocationPermissionAvailable { get; set; }

        public DelegateCommand<MapClickedEventArgs> MapClickedCommand => new DelegateCommand<MapClickedEventArgs>(
            (args) =>
            {

            });

        public void SetView(MapMainPage mapMainPage)
        {
            MapMainPage = mapMainPage;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            EnsureLocationPermissionAvailable();
        }

        private async void EnsureLocationPermissionAvailable()
        {
            try
            {
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (permissionStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await UserDialogs.Instance.AlertAsync("Please allow AgroTutor to use your device's location.", "Request to use location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    if (results.ContainsKey(Permission.Location))
                        permissionStatus = results[Permission.Location];
                }

                if (permissionStatus == PermissionStatus.Granted)
                {
                    LocationPermissionAvailable = true;
                    MapMainPage.EnableMyLocation();
                }
                else if (permissionStatus != PermissionStatus.Unknown)
                {
                    await UserDialogs.Instance.AlertAsync("It seems you denied us to use the GPS location. Please rethink your decision as this app relies on knowing the location of you and your plots.", "Permission denied", "OK");
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}
