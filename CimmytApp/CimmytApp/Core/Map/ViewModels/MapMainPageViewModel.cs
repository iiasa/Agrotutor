﻿namespace CimmytApp.Core.Map.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Acr.UserDialogs;

    using CimmytApp.Core.Calendar.ViewModels;
    using CimmytApp.Core.Datatypes.HubsContact;
    using CimmytApp.Core.Datatypes.InvestigationPlatforms;
    using CimmytApp.Core.Datatypes.MachineryPoints;
    using CimmytApp.Core.Map.Views;
    using CimmytApp.Core.Parcel.ViewModels;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.StaticContent;
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    using Plugin.Media.Abstractions;
    using Plugin.Permissions.Abstractions;

    using Prism.Commands;
    using Prism.Navigation;

    using Xamarin.Essentials;
    using Xamarin.Forms.GoogleMaps;

    public class MapMainPageViewModel : ViewModelBase, INavigatedAware
    {
        private bool addParcelIsVisible;

        private Persistence.Entities.Position addPlotPosition;

        private Datatypes.HubsContact.Feature currentHubContact;

        private Datatypes.InvestigationPlatforms.Feature currentInvestigationPlatform;

        private Datatypes.MachineryPoints.Feature currentMachineryPoint;

        private MapTask currentMapTask;

        private string currentMapTaskHint;

        private bool currentMapTaskHintIsVisible;

        private Persistence.Entities.Position currentPosition;

        private bool dimBackground;

        private bool gpsLocationUIIsVisible;

        private HubsContact hubsContact;

        private bool hubsContactUIIsVisible;

        private InvestigationPlatforms investigationPlatforms;

        private bool investigationPlatformUIIsVisible;

        private bool listenForLocation = true;

        private bool loadingSpinnerIsVisible;

        private MachineryPoints machineryPoints;

        private bool machineryPointUIIsVisible;

        private bool optionsIsVisible;

        private bool plotDetailIsVisible;

        private Plot selectedPlot;

        private bool selectLocationUIIsVisible;

        public MapMainPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            IStringLocalizer<MapMainPageViewModel> localizer)
            : base(localizer)
        {
            AppDataService = appDataService;
            LocationPermissionAvailable = false;
            NavigationService = navigationService;
            CurrentMapTask = MapTask.Default;
            AddParcelIsVisible = false;
            OptionsIsVisible = false;
            LoadingSpinnerIsVisible = true;
        }

        public DelegateCommand AddParcelClicked =>
            new DelegateCommand(
                () =>
                {
                    CurrentMapTask = MapTask.CreatePlotBySelection;
                    AddParcelIsVisible = true;
                });

        public DelegateCommand AddPlot => new DelegateCommand(CreatePlot);

        public IAppDataService AppDataService { get; }

        public DelegateCommand ClickChooseLocation =>
            new DelegateCommand(
                () =>
                {
                    DimBackground = false;
                    CurrentMapTask = MapTask.SelectLocation;
                });

        public DelegateCommand ClickGetLocation =>
            new DelegateCommand(
                () =>
                {
                    DimBackground = false;
                    AddPlotPosition = CurrentPosition;
                    CurrentMapTask = MapTask.CreatePlotByGPS;
                });

        public DelegateCommand DelineateSelectedPlot =>
            new DelegateCommand(
                async () =>
                {
                    if (this.selectedPlot.Delineation?.Count > 0)
                    {
                        bool confirmDelineation = await UserDialogs.Instance.ConfirmAsync(
                                                      new ConfirmConfig
                                                      {
                                                          Message =
                                                              "The plot already has a delineation. Do you really want to overwrite the old one?",
                                                          OkText = "Yes",
                                                          CancelText = "Cancel",
                                                          Title = "Delineation already exists"
                                                      });

                        if (confirmDelineation)
                        {
                            MapMainPage.StartDelineation(this.selectedPlot);
                            CurrentMapTask = MapTask.DelineationNotEnoughPoints;
                        }
                    }
                    else
                    {
                        MapMainPage.StartDelineation(this.selectedPlot);
                        CurrentMapTask = MapTask.DelineationNotEnoughPoints;
                    }

                    DimBackground = false;
                });

        public DelegateCommand NavigateToCurrentMachineryPoint =>
            new DelegateCommand(() => NavigateToLocation(CurrentMachineryPoint.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentHubContact =>
            new DelegateCommand(() => NavigateToLocation(CurrentHubContact.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentInvestigationPlatform =>
            new DelegateCommand(() => NavigateToLocation(CurrentInvestigationPlatform.Geometry.ToLocation()));

        private void NavigateToLocation(Xamarin.Essentials.Location location)
        {
            var mapOptions = new MapsLaunchOptions { MapDirectionsMode = MapDirectionsMode.Driving };
            Maps.OpenAsync(location, mapOptions);
        }

        public DelegateCommand HideOverlays =>
            new DelegateCommand(
                () =>
                {
                    CurrentMapTask = MapTask.Default;
                    CurrentMapTaskHint = string.Empty;
                    DimBackground = false;
                });

        public DelegateCommand<MapClickedEventArgs> MapClicked =>
            new DelegateCommand<MapClickedEventArgs>(
                args =>
                {
                    switch (CurrentMapTask)
                    {
                        case MapTask.CreatePlotBySelection:
                            AddPlotPosition = Persistence.Entities.Position.From(args.Point);
                            CreatePlot();
                            break;

                        case MapTask.DelineationNotEnoughPoints:
                        case MapTask.DelineationEnoughPoints:
                            if (CurrentDelineation == null)
                            {
                                CurrentDelineation = new List<Persistence.Entities.Position>();
                            }

                            CurrentDelineation.Add(Persistence.Entities.Position.From(args.Point));
                            MapMainPage.AddDelineationPoint(args.Point);
                            break;
                    }
                });

        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked =>
            new DelegateCommand<MapLongClickedEventArgs>(
                args =>
                {
                    AddPlotPosition = Persistence.Entities.Position.From(args.Point);
                    CreatePlot();
                });

        public DelegateCommand NavigateToMain =>
            new DelegateCommand(() => NavigationService.NavigateAsync("NavigationPage/MainPage"));

        public DelegateCommand<MediaFile> OnParcelPictureTaken =>
            new DelegateCommand<MediaFile>(
                mediaFile =>
                {
                    int i = 0;
                    i++;
                });

        public DelegateCommand<PinClickedEventArgs> PinClicked =>
            new DelegateCommand<PinClickedEventArgs>(
                args =>
                {
                    object data = args.Pin.Tag;
                    if (data is Plot plot)
                    {
                        ShowPlotInformation(plot);
                    }
                    else if (data is Datatypes.HubsContact.Feature hubContact)
                    {
                        ShowHubContactInformation(hubContact);
                    }
                    else if (data is Datatypes.InvestigationPlatforms.Feature investigationPlatform)
                    {
                        ShowInvestigationPlatformInformation(investigationPlatform);
                    }
                    else if (data is Datatypes.MachineryPoints.Feature machineryPoint)
                    {
                        ShowMachineryPointInformation(machineryPoint);
                    }
                });

        public DelegateCommand ShowCalendar =>
            new DelegateCommand(
                () =>
                {
                    NavigationParameters navigationParameters = new NavigationParameters
                                                                {
                                                                    {
                                                                        CalendarPageViewModel.EventsParameterName,
                                                                        Plot.GetCalendarEvents(Plots)
                                                                    },
                                                                    { "Dev", true }
                                                                };
                    NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowCalendarForSelectedPlot =>
            new DelegateCommand(
                () =>
                {
                    NavigationParameters navigationParameters = new NavigationParameters
                                                                {
                                                                    {
                                                                        CalendarPageViewModel.EventsParameterName,
                                                                        SelectedPlot.GetCalendarEvents()
                                                                    }
                                                                };
                    NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowOptions => new DelegateCommand(() => { OptionsIsVisible = true; });

        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.OpenSettings);

        public DelegateCommand StartPlanner =>
            new DelegateCommand(() => CurrentMapTask = MapTask.SelectLocationForPlanner);

        public DelegateCommand<string> PhoneCall =>
            new DelegateCommand<string>((number) => PhoneDialer.Open(number));

        public DelegateCommand<string> WriteEmail =>
            new DelegateCommand<string>(
                async(emailAddress) =>
                {
                    var message = new EmailMessage
                                  {
                                      To = new List<string> { emailAddress },
                                      Subject = "Agrotutor enquiry"
                                  };
                    await Email.ComposeAsync(message);
                });

        public bool AddParcelIsVisible
        {
            get => this.addParcelIsVisible;
            set
            {
                SetProperty(ref this.addParcelIsVisible, value);
                if (this.addParcelIsVisible)
                {
                    OptionsIsVisible = false;
                    PlotDetailIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public Persistence.Entities.Position AddPlotPosition
        {
            get => this.addPlotPosition;
            set
            {
                this.addPlotPosition = value;
                RefreshWeatherData();
            }
        }

        public List<Persistence.Entities.Position> CurrentDelineation { get; set; }

        public Datatypes.HubsContact.Feature CurrentHubContact
        {
            get => this.currentHubContact;
            set => SetProperty(ref this.currentHubContact, value);
        }

        public Datatypes.InvestigationPlatforms.Feature CurrentInvestigationPlatform
        {
            get => this.currentInvestigationPlatform;
            set => SetProperty(ref this.currentInvestigationPlatform, value);
        }

        public Datatypes.MachineryPoints.Feature CurrentMachineryPoint
        {
            get => this.currentMachineryPoint;
            set => SetProperty(ref this.currentMachineryPoint, value);
        }

        public MapTask CurrentMapTask
        {
            get => this.currentMapTask;
            set
            {
                MapTask oldValue = this.currentMapTask;
                this.currentMapTask = value;
                SetUIForMapTask(value, oldValue);
            }
        }

        public string CurrentMapTaskHint
        {
            get => this.currentMapTaskHint;
            set => SetProperty(ref this.currentMapTaskHint, value);
        }

        public bool CurrentMapTaskHintIsVisible
        {
            get => this.currentMapTaskHintIsVisible;
            set => SetProperty(ref this.currentMapTaskHintIsVisible, value);
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
                    AddParcelIsVisible = false;
                    OptionsIsVisible = false;
                    PlotDetailIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                }
            }
        }

        public bool GPSLocationUIIsVisible
        {
            get => this.gpsLocationUIIsVisible;
            set => SetProperty(ref this.gpsLocationUIIsVisible, value);
        }

        public HubsContact HubsContact
        {
            get => this.hubsContact;
            private set
            {
                this.hubsContact = value;
                MapMainPage.SetHubsContact(value);
            }
        }

        public bool HubsContactUIIsVisible
        {
            get => this.hubsContactUIIsVisible;
            set
            {
                SetProperty(ref this.hubsContactUIIsVisible, value);
                if (this.hubsContactUIIsVisible)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    OptionsIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public InvestigationPlatforms InvestigationPlatforms
        {
            get => this.investigationPlatforms;
            private set
            {
                this.investigationPlatforms = value;
                MapMainPage.SetInvestigationPlatforms(value);
            }
        }

        public bool InvestigationPlatformUIIsVisible
        {
            get => this.investigationPlatformUIIsVisible;
            set
            {
                SetProperty(ref this.investigationPlatformUIIsVisible, value);
                if (this.investigationPlatformUIIsVisible)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    OptionsIsVisible = false;
                    HubsContactUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public bool ListenForLocation { get; set; }

        public bool LoadingSpinnerIsVisible
        {
            get => this.loadingSpinnerIsVisible;
            set => SetProperty(ref this.loadingSpinnerIsVisible, value);
        }

        public bool LocationPermissionAvailable { get; set; }

        public MachineryPoints MachineryPoints
        {
            get => this.machineryPoints;
            private set
            {
                this.machineryPoints = value;
                MapMainPage.SetMachineryPoints(value);
            }
        }

        public bool MachineryPointUIIsVisible
        {
            get => this.machineryPointUIIsVisible;
            set
            {
                SetProperty(ref this.machineryPointUIIsVisible, value);
                if (this.machineryPointUIIsVisible)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    OptionsIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public MapMainPage MapMainPage { get; set; }

        public INavigationService NavigationService { get; set; }

        public bool OptionsIsVisible
        {
            get => this.optionsIsVisible;
            set
            {
                SetProperty(ref this.optionsIsVisible, value);
                if (this.optionsIsVisible)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public bool PlotDetailIsVisible
        {
            get => this.plotDetailIsVisible;
            set
            {
                SetProperty(ref this.plotDetailIsVisible, value);
                if (this.plotDetailIsVisible)
                {
                    OptionsIsVisible = false;
                    AddParcelIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public IEnumerable<Plot> Plots { get; set; }

        public Plot SelectedPlot
        {
            get => this.selectedPlot;
            set => SetProperty(ref this.selectedPlot, value);
        }

        public bool SelectLocationUIIsVisible
        {
            get => this.selectLocationUIIsVisible;
            set => SetProperty(ref this.selectLocationUIIsVisible, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            ListenForLocation = false;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Task.Run(() => EnableUserLocation());
            Task.Run(() => LoadPlots());
            Task.Run(() => LoadMapData());
        }

        public void SetView(MapMainPage mapMainPage)
        {
            MapMainPage = mapMainPage;
        }

        private async void CreatePlot()
        {
            bool confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(
                                           new ConfirmConfig
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
                                                            {
                                                                AddParcelPageViewModel.PositionParameterName,
                                                                AddPlotPosition
                                                            }
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
            Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync();

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
            HubsContact = await HubsContact.FromEmbeddedResource();
            InvestigationPlatforms = await InvestigationPlatforms.FromEmbeddedResource();
            MachineryPoints = await MachineryPoints.FromEmbeddedResource();
        }

        private async void LoadPlots()
        {
            Plots = DevHelper.GetTestData(); // TODO dev data!

            // Plots = await AppDataService.GetAllPlots();
            MapMainPage.AddPlots(Plots);
        }

        private async void RefreshWeatherData()
        {
            // TODO: implement
        }

        private void SetUIForMapTask(MapTask value, MapTask oldValue)
        {
            switch (value)
            {
                case MapTask.Default:
                    CurrentMapTaskHint = string.Empty;
                    CurrentMapTaskHintIsVisible = false;
                    GPSLocationUIIsVisible = false;
                    SelectLocationUIIsVisible = false;
                    break;
                case MapTask.SelectLocation:
                    CurrentMapTaskHint = "Click on map to select location";
                    CurrentMapTaskHintIsVisible = true;
                    SelectLocationUIIsVisible = true;
                    GPSLocationUIIsVisible = false;
                    break;
                case MapTask.CreatePlotByGPS:
                    CurrentMapTaskHint = "Accept location when accurate enough";
                    CurrentMapTaskHintIsVisible = true;
                    GPSLocationUIIsVisible = true;
                    SelectLocationUIIsVisible = false;
                    break;
                case MapTask.SelectLocationForPlanner:
                    CurrentMapTaskHint = "Click on map to select location";
                    CurrentMapTaskHintIsVisible = true;
                    SelectLocationUIIsVisible = true;
                    GPSLocationUIIsVisible = false;
                    break;
                case MapTask.DelineationNotEnoughPoints:
                    if (oldValue != MapTask.DelineationEnoughPoints)
                    {
                        MapMainPage.ZoomToPosition(this.selectedPlot.Position.ForMap());
                    }

                    break;
            }
        }

        private void ShowHubContactInformation(Datatypes.HubsContact.Feature hubContact)
        {
            CurrentHubContact = hubContact;
            HubsContactUIIsVisible = true;
        }

        private void ShowInvestigationPlatformInformation(
            Datatypes.InvestigationPlatforms.Feature investigationPlatform)
        {
            CurrentInvestigationPlatform = investigationPlatform;
            InvestigationPlatformUIIsVisible = true;
        }

        private void ShowMachineryPointInformation(Datatypes.MachineryPoints.Feature machineryPoint)
        {
            CurrentMachineryPoint = machineryPoint;
            MachineryPointUIIsVisible = true;
        }

        private void ShowPlotInformation(Plot data)
        {
            SelectedPlot = data;
            PlotDetailIsVisible = true;
        }
    }
}