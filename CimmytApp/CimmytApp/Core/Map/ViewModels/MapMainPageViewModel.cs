namespace CimmytApp.Core.Map.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using CimmytApp.Core.Calendar.ViewModels;
    using CimmytApp.Core.Map.Views;
    using CimmytApp.Core.Parcel.ViewModels;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.StaticContent;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Plugin.Permissions.Abstractions;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Essentials;
    using Xamarin.Forms.GoogleMaps;

    public class MapMainPageViewModel : ViewModelBase, INavigatedAware
    {
        private Persistence.Entities.Position addPlotPosition;
        private MapTask currentMapTask;
        private string currentMapTaskHint;
        private Persistence.Entities.Position currentPosition;
        private bool dimBackground;

        private bool listenForLocation = true;
        private bool showAddParcel;
        private bool showCurrentMapTaskHint;
        private bool showGpsLocationUI;
        private bool showOptions;
        private bool showSelectLocationUI;

        public MapMainPageViewModel(INavigationService navigationService, IAppDataService appDataService,
            IStringLocalizer<MapMainPageViewModel> localizer) : base(localizer)
        {
            AppDataService = appDataService;
            LocationPermissionAvailable = false;
            NavigationService = navigationService;
            CurrentMapTask = MapTask.Default;

            ShowAddParcel = false;
            ShowOptions = false;
        }

        public DelegateCommand AddParcelClicked =>
            new DelegateCommand(() =>
            {
                CurrentMapTask = MapTask.CreatePlotBySelection;
                ShowAddParcel = true;
            });

        public DelegateCommand AddPlot => new DelegateCommand(CreatePlot);

        public IAppDataService AppDataService { get; }

        public DelegateCommand ClickChooseLocation =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocation;
            });

        public DelegateCommand ClickGetLocation =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                AddPlotPosition = CurrentPosition;
                CurrentMapTask = MapTask.CreatePlotByGPS;
            });

        public DelegateCommand HideOverlays =>
            new DelegateCommand(() =>
            {
                CurrentMapTask = MapTask.Default;
                CurrentMapTaskHint = string.Empty;
                DimBackground = false;
            });

        public DelegateCommand<MapClickedEventArgs> MapClicked =>
            new DelegateCommand<MapClickedEventArgs>(args =>
            {
                switch (CurrentMapTask)
                {
                    case MapTask.CreatePlotBySelection:
                        AddPlotPosition = Persistence.Entities.Position.From(args.Point);
                        CreatePlot();
                        break;
                }
            });

        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked =>
            new DelegateCommand<MapLongClickedEventArgs>(args =>
            {
                AddPlotPosition = Persistence.Entities.Position.From(args.Point);
                CreatePlot();
            });

        public DelegateCommand NavigateToMain => new DelegateCommand(() => NavigationService.NavigateAsync("NavigationPage/MainPage"));

        public DelegateCommand<PinClickedEventArgs> PinClicked =>
            new DelegateCommand<PinClickedEventArgs>(args =>
            {
                object data = args.Pin.Tag;
                if (data is Plot)
                {
                    ShowPlotInformation((Plot)data);
                }
            });

        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.OpenSettings);

        public DelegateCommand ShowCalendar => new DelegateCommand(() =>
            {
                var navigationParameters = new NavigationParameters
                {
                    { CalendarPageViewModel.EventsParameterName, Plot.GetCalendarEvents(Plots) },
                    { "Dev", true}
                };
                NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
            });

        public Persistence.Entities.Position AddPlotPosition
        {
            get => this.addPlotPosition;
            set
            {
                this.addPlotPosition = value;
                RefreshWeatherData();
            }
        }

        public MapTask CurrentMapTask
        {
            get => this.currentMapTask;
            set
            {
                this.currentMapTask = value;
                SetUIForMapTask(value);
            }
        }

        public string CurrentMapTaskHint
        {
            get => this.currentMapTaskHint;
            set => SetProperty(ref this.currentMapTaskHint, value);
        }

        public Persistence.Entities.Position CurrentPosition
        {
            get => this.currentPosition;
            set
            {
                this.currentPosition = value;
                RefreshWeatherData();
            }
        }

        public bool DimBackground
        {
            get => this.dimBackground;
            set
            {
                SetProperty(ref this.dimBackground, value);
                if (!this.dimBackground)
                {
                    ShowAddParcel = false;
                    ShowOptions = false;
                }
            }
        }

        public bool ListenForLocation { get; set; }

        public bool LocationPermissionAvailable { get; set; }

        public MapMainPage MapMainPage { get; set; }

        public INavigationService NavigationService { get; set; }

        public IEnumerable<Plot> Plots { get; set; }

        public Plot SelectedPlot { get; set; }

        public bool ShowAddParcel
        {
            get => this.showAddParcel;
            set
            {
                SetProperty(ref this.showAddParcel, value);
                if (this.showAddParcel)
                {
                    ShowOptions = false;
                    DimBackground = true;
                }
            }
        }

        public bool ShowCurrentMapTaskHint
        {
            get => this.showCurrentMapTaskHint;
            set => SetProperty(ref this.showCurrentMapTaskHint, value);
        }

        public bool ShowGPSLocationUI
        {
            get => this.showGpsLocationUI;
            set => SetProperty(ref this.showGpsLocationUI, value);
        }

        public bool ShowOptions
        {
            get => this.showOptions;
            set
            {
                SetProperty(ref this.showOptions, value);
                if (this.showOptions)
                {
                    ShowAddParcel = false;
                    DimBackground = true;
                }
            }
        }

        public bool ShowSelectLocationUI
        {
            get => this.showSelectLocationUI;
            set => SetProperty(ref this.showSelectLocationUI, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            ListenForLocation = false;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            EnableUserLocation();
            LoadPlots();
            LoadMapData();
        }

        public void SetView(MapMainPage mapMainPage)
        {
            MapMainPage = mapMainPage;
        }

        private async void CreatePlot()
        {
            bool confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you want to create a new plot at this location?",
                OkText = "Yes",
                CancelText = "Cancel",
                Title = "Add plot"
            });

            if (confirmPlotCreation)
            {
                NavigationParameters navigationParams = new NavigationParameters
                {
                    { AddParcelPageViewModel.PositionParameterName, AddPlotPosition }
                };
                NavigationService.NavigateAsync("NavigationPage/AddParcelPage", navigationParams);
            }
        }

        private async void EnableUserLocation()
        {
            bool permissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
            if (permissionGiven)
            {
                MapMainPage.EnableMyLocation();
                GetUserLocation();
            }
        }

        private async void GetUserLocation()
        {
            GeolocationRequest geolocationRequest = new GeolocationRequest
            {
                DesiredAccuracy = Constants.MainMapLocationAccuracy
            };
            Location location = await Geolocation.GetLastKnownLocationAsync();

            do
            {
                CurrentPosition = (Persistence.Entities.Position)location;
                if (CurrentMapTask == MapTask.CreatePlotByGPS)
                {
                    AddPlotPosition = CurrentPosition;
                }

                await Task.Delay(Constants.MainMapLocationRefreshPeriod * 1000);
                location = await Geolocation.GetLocationAsync(geolocationRequest);
            }
            while (ListenForLocation);
        }

        private async void LoadMapData()
        {
            // TODO: implement
        }

        private async void LoadPlots()
        {
            Plots = DevHelper.GetTestData(); //TODO dev data!

            // Plots = await AppDataService.GetAllPlots();
            MapMainPage.AddPlots(Plots);
        }

        private async void RefreshWeatherData()
        {
            //TODO: implement
        }

        private void SetUIForMapTask(MapTask value)
        {
            switch (value)
            {
                case MapTask.Default:
                    CurrentMapTaskHint = string.Empty;
                    ShowCurrentMapTaskHint = false;
                    ShowGPSLocationUI = false;
                    ShowSelectLocationUI = false;
                    break;
                case MapTask.SelectLocation:
                    CurrentMapTaskHint = "Click on map to select location";
                    ShowCurrentMapTaskHint = true;
                    ShowSelectLocationUI = true;
                    ShowGPSLocationUI = false;
                    break;
                case MapTask.CreatePlotByGPS:
                    CurrentMapTaskHint = "Accept location when accurate enough";
                    ShowCurrentMapTaskHint = true;
                    ShowGPSLocationUI = true;
                    ShowSelectLocationUI = false;
                    break;
            }
        }

        private void ShowPlotInformation(Plot data)
        {
            SelectedPlot = data;
        }
    }
}