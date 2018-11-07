namespace CimmytApp.Core.Map.ViewModels
{
    using Acr.UserDialogs;
    using CimmytApp.Core.Map.Views;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
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


        public DelegateCommand AddParcelClicked => new DelegateCommand(() =>
            {
                CurrentMapTask = MapTask.CreatePlotBySelection;
            });

        public bool LocationPermissionAvailable { get; set; }

        public MapTask CurrentMapTask { get; set; }

        public string CurrentMapTaskHint { get; set; }
        public bool ShowCurrentMapTaskHint { get; set; }


        public DelegateCommand<MapClickedEventArgs> MapClicked => new DelegateCommand<MapClickedEventArgs>(
            (args) =>
            {
                switch (CurrentMapTask)
                {
                    case MapTask.CreatePlotBySelection:
                        createPlotAtPosition(args.Point);
                        break;
                }
            });

        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked => new DelegateCommand<MapLongClickedEventArgs>(
            (args) => { createPlotAtPosition(args.Point); });

        public void SetView(MapMainPage mapMainPage)
        {
            MapMainPage = mapMainPage;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GeolocationRequest req = new GeolocationRequest();
            Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            EnableUserLocation();
        }

        private async void EnableUserLocation()
        {
            bool permissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
            if (permissionGiven)
            {
                this.MapMainPage.EnableMyLocation();
            }
        }

        private async void createPlotAtPosition(Position position)
        {
            var confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you want to create a new plot at this location?",
                OkText = "Yes",
                CancelText = "Cancel",
                Title = "Add plot"
            });


            if (confirmPlotCreation)
            {
                var navigaytionParams = new NavigationParameters();
                navigaytionParams.Add("position", position);
                NavigationService.NavigateAsync("AddParcelPage");
            }
        }
    }
}
