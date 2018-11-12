namespace CimmytApp.Core.Map.ViewModels
{
    using System.Threading;
    using System.Threading.Tasks;
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
        private MapTask currentMapTask;

        public MapMainPageViewModel(INavigationService navigationService,
            IStringLocalizer<MapMainPageViewModel> localizer) : base(localizer)
        {
            LocationPermissionAvailable = false;
            NavigationService = navigationService;
            CurrentMapTask = MapTask.Default;
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

        public MapTask CurrentMapTask
        {
            get => this.currentMapTask;
            set
            {
                this.currentMapTask = value;
                SetUIForMapTask(value);
            }
        }

        public string CurrentMapTaskHint { get; set; }
        public bool ShowCurrentMapTaskHint { get; set; }

        public Persistence.Entities.Position CurrentPosition { get; set; }

        private bool ListenForLocation = true;
        private CancellationToken locationCancellationToken;


        public DelegateCommand<MapClickedEventArgs> MapClicked => new DelegateCommand<MapClickedEventArgs>(
            (args) =>
            {
                switch (CurrentMapTask)
                {
                    case MapTask.CreatePlotBySelection:
                        CreatePlotAtPosition(Persistence.Entities.Position.From(args.Point));
                        break;
                }
            });

        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked => new DelegateCommand<MapLongClickedEventArgs>(
            (args) => { CreatePlotAtPosition(Persistence.Entities.Position.From(args.Point)); });

        public void SetView(MapMainPage mapMainPage)
        {
            MapMainPage = mapMainPage;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            this.ListenForLocation = false;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            EnableUserLocation();
        }

        private async void EnableUserLocation()
        {
            bool permissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
            if (permissionGiven)
            {
                this.MapMainPage.EnableMyLocation();
                GetUserLocation();
            }
        }

        private async void GetUserLocation()
        {
            var geolocationRequest = new GeolocationRequest
            {
                DesiredAccuracy = Constants.MainMapLocationAccuracy
            };
            var location = await Geolocation.GetLastKnownLocationAsync();

            do
            {
                CurrentPosition = (Persistence.Entities.Position)location;
                await Task.Delay(Constants.MainMapLocationRefreshPeriod * 1000);
                location = await Geolocation.GetLocationAsync(geolocationRequest);
            }
            while (this.ListenForLocation);
        }

        private async void CreatePlotAtPosition(Persistence.Entities.Position position)
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
                var navigationParams = new NavigationParameters
                {
                    { "position", position }
                };
                NavigationService.NavigateAsync("AddParcelPage", navigationParams);
            }
        }

        private void SetUIForMapTask(MapTask value)
        {
            switch (value)
            {
                case MapTask.Default:
                    CurrentMapTaskHint = string.Empty;
                    ShowCurrentMapTaskHint = false;
                    break;
            }
        }
    }
}
