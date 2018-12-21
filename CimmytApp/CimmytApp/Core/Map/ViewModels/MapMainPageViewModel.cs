namespace CimmytApp.Core.Map.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
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
    using CimmytApp.ViewModels;
    using CimmytApp.WeatherForecast;

    using Microsoft.Extensions.Localization;

    using Plugin.Media.Abstractions;
    using Plugin.Permissions.Abstractions;

    using Prism.Commands;
    using Prism.Navigation;

    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class MapMainPageViewModel : ViewModelBase, INavigatedAware
    {
        private string _currentWeatherIconSource;

        private string _currentWeatherText;

        private int _pickerCropTypesSelectedIndex;

        private bool _plannerUIIsVisible;

        private bool _showWeatherWidget;

        private bool addParcelIsVisible;

        private Persistence.Entities.Position addPlotPosition;

        private Datatypes.HubsContact.Feature currentHubContact;

        private Datatypes.InvestigationPlatforms.Feature currentInvestigationPlatform;

        private Datatypes.MachineryPoints.Feature currentMachineryPoint;

        private MapTask currentMapTask;

        private string currentMapTaskHint;

        private bool currentMapTaskHintIsVisible;

        private Persistence.Entities.Position currentPosition;

        private WeatherForecast currentWeather;

        private bool dimBackground;

        private bool gpsLocationUIIsVisible;

        private HubsContact hubsContact;

        private bool hubsContactUIIsVisible;

        private InvestigationPlatforms investigationPlatforms;

        private bool investigationPlatformUIIsVisible;

        private bool listenForLocation = true;

        private bool loadingSpinnerIsVisible;

        private bool locationPermissionGiven;

        private MachineryPoints machineryPoints;

        private bool machineryPointUIIsVisible;

        private bool optionsIsVisible;

        private bool plotDetailIsVisible;

        private Plot selectedPlot;

        private bool selectLocationUIIsVisible;

        private Xamarin.Essentials.Location weatherLocation;

        private bool delineationUIIsVisible;

        public MapMainPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            IStringLocalizer<MapMainPageViewModel> localizer)
            : base(localizer)
        {
            LocationPermissionGiven = false;
            ShowWeatherWidget = false;
            AppDataService = appDataService;
            NavigationService = navigationService;
            CurrentMapTask = MapTask.Default;
            AddParcelIsVisible = false;
            OptionsIsVisible = false;
            LoadingSpinnerIsVisible = false;
            PlannerUIIsVisible = false;
            DelineationUIIsVisible = false;
        }

        public DelegateCommand AddActivityToSelectedPlot =>
            new DelegateCommand(
                () =>
                {
                    NavigationParameters param = new NavigationParameters
                                                 {
                                                     { "Plot", SelectedPlot }
                                                 };
                    NavigationService.NavigateAsync("ActivityPage", param);
                });

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

        public DelegateCommand ClickChooseLocationPlanner =>
            new DelegateCommand(
                () =>
                {
                    DimBackground = false;
                    CurrentMapTask = MapTask.SelectLocationForPlanner;
                });

        public DelegateCommand ClickGetLocation =>
            new DelegateCommand(
                () =>
                {
                    DimBackground = false;
                    AddPlotPosition = CurrentPosition;
                    CurrentMapTask = MapTask.CreatePlotByGPS;
                });

        public DelegateCommand ClickGetLocationPlanner =>
            new DelegateCommand(
                () =>
                {
                    DimBackground = false;
                    AddPlotPosition = CurrentPosition;
                    CurrentMapTask = MapTask.GetLocationForPlanner;
                });

        public List<string> CropTypes { get; } = new List<string>
                                                 {
                                                     "Maíz",
                                                     "Cebada",
                                                     "Frijol",
                                                     "Trigo",
                                                     "Triticale",
                                                     "Sorgo",
                                                     "Alfalfa",
                                                     "Avena",
                                                     "Ajonjolí",
                                                     "Amaranto",
                                                     "Arroz",
                                                     "Canola",
                                                     "Cartamo",
                                                     "Calabacín",
                                                     "Garbanzo",
                                                     "Haba",
                                                     "Soya",
                                                     "Ninguno",
                                                     "Otro"
                                                 };

        public DelegateCommand DelineateSelectedPlot =>
            new DelegateCommand(
                async () =>
                {
                    if (this.selectedPlot.Delineation?.Count > 0)
                    {
                        bool confirmDelineation = await UserDialogs.Instance.ConfirmAsync(
                                                      new ConfirmConfig
                                                      {
                                                          Message = Localizer.GetString(
                                                              "replace_delineation_prompt_message"),
                                                          OkText =
                                                              Localizer.GetString("replace_delineation_prompt_yes"),
                                                          CancelText =
                                                              Localizer.GetString("replace_delineation_prompt_cancel"),
                                                          Title = Localizer.GetString(
                                                              "replace_delineation_prompt_title")
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

        public DelegateCommand NavigateToCurrentHubContact =>
            new DelegateCommand(() => NavigateToLocation(CurrentHubContact.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentInvestigationPlatform =>
            new DelegateCommand(() => NavigateToLocation(CurrentInvestigationPlatform.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentMachineryPoint =>
            new DelegateCommand(() => NavigateToLocation(CurrentMachineryPoint.Geometry.ToLocation()));

        public DelegateCommand NavigateToGuide =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("WelcomePage"); });

        public DelegateCommand NavigateToMain =>
            new DelegateCommand(() => NavigationService.NavigateAsync("NavigationPage/MainPage"));

        public DelegateCommand NavigateToPractices =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("LinksPage"); });

        public DelegateCommand NavigateToProfile =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("ProfilePage"); });

        public DelegateCommand ShowInfoForSelectedPlot =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters{{ "Plot", SelectedPlot }};
                NavigationService.NavigateAsync("ParcelMainPage", param);
            });

        public DelegateCommand<MediaFile> OnParcelPictureTaken =>
            new DelegateCommand<MediaFile>(
                mediaFile =>
                {
                    int i = 0;
                    i++;
                });

        public DelegateCommand<string> PhoneCall => new DelegateCommand<string>(number => PhoneDialer.Open(number));

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

        public DelegateCommand ShowWeather =>
            new DelegateCommand(
                () =>
                {
                    NavigationParameters param = new NavigationParameters();
                    if (CurrentWeather != null)
                    {
                        param.Add("Forecast", CurrentWeather);
                    }

                    if (WeatherLocation != null)
                    {
                        param.Add("Location", WeatherLocation);
                    }
                    else
                    {
                        // TODO put message location missing, or select on map
                        return;
                    }

                    NavigationService.NavigateAsync("WeatherMainPage", param);
                });

        public DelegateCommand StartPlanner => new DelegateCommand(() => { PlannerUIIsVisible = true; });

        public DelegateCommand<string> WriteEmail =>
            new DelegateCommand<string>(
                async emailAddress =>
                {
                    EmailMessage message = new EmailMessage
                                           {
                                               To = new List<string>
                                                    {
                                                        emailAddress
                                                    },
                                               Subject = Localizer.GetString("email_subject")
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
                    PlannerUIIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    PlannerUIIsVisible = false;
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
                if (Core.WeatherForecast.Util.ShouldRefresh(this.weatherLocation, value))
                {
                    this.weatherLocation = value;
                    Task.Run(() => RefreshWeatherData());
                }
            }
        }

        public WeatherForecast CurrentWeather
        {
            get => this.currentWeather;
            set
            {
                SetProperty(ref this.currentWeather, value);
                ShowWeatherWidget = true;
                HourlySummary cur = value.Location.HourlySummaries.HourlySummary.ElementAt(0);
                CurrentWeatherIconSource = cur.TinyWxIcon;
                CurrentWeatherText = cur.WxText;
            }
        }

        public string CurrentWeatherIconSource
        {
            get => this._currentWeatherIconSource;
            set => SetProperty(ref this._currentWeatherIconSource, value);
        }

        public string CurrentWeatherText
        {
            get => this._currentWeatherText;
            set => SetProperty(ref this._currentWeatherText, value);
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
                    PlannerUIIsVisible = false;
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
                    PlannerUIIsVisible = false;
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
                    PlannerUIIsVisible = false;
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

        public bool LocationPermissionGiven
        {
            get => this.locationPermissionGiven;
            set
            {
                SetProperty(ref this.locationPermissionGiven, value);
                if (value)
                {
                    MapMainPage.EnableMyLocation();
                    GetUserLocation();
                }
            }
        }

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
                    PlannerUIIsVisible = false;
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
                    PlannerUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                }
            }
        }

        public int PickerCropTypesSelectedIndex
        {
            get => this._pickerCropTypesSelectedIndex;
            set => SetProperty(ref this._pickerCropTypesSelectedIndex, value);
        }

        public bool PlannerUIIsVisible
        {
            get => this._plannerUIIsVisible;
            set
            {
                SetProperty(ref this._plannerUIIsVisible, value);

                if (this._plannerUIIsVisible)
                {
                    OptionsIsVisible = false;
                    PlotDetailIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    AddParcelIsVisible = false;
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
                    PlannerUIIsVisible = false;
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

        public bool ShowWeatherWidget
        {
            get => this._showWeatherWidget;
            set => SetProperty(ref this._showWeatherWidget, value);
        }

        public Xamarin.Essentials.Location WeatherLocation
        {
            get => this.weatherLocation;
            set
            {
                this.weatherLocation = value;
                Task.Run(() => RefreshWeatherData());
            }
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
            if (AddPlotPosition == null)
            {
                await UserDialogs.Instance.AlertAsync(
                    new AlertConfig
                    {
                        Title = "No position available",
                        Message = "Please make sure the location for a new parcel is set.",
                        OkText = "Ok"
                    });
                return;
            }

            bool confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(
                                           new ConfirmConfig
                                           {
                                               Message = Localizer.GetString("new_plot_prompt_message"),
                                               OkText = Localizer.GetString("new_plot_prompt_yes"),
                                               CancelText = Localizer.GetString("new_plot_prompt_cancel"),
                                               Title = Localizer.GetString("new_plot_prompt_title")
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
                NavigationService.NavigateAsync("AddParcelPage", navigationParams);
            }
        }

        private async void EnableUserLocation()
        {
            Device.BeginInvokeOnMainThread(
                async () =>
                {
                    LocationPermissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
                    if (LocationPermissionGiven)
                    {
                        MapMainPage.EnableMyLocation();
                        GetUserLocation();
                    }
                    else
                    {
                        bool LocationPermissionGiven = await PermissionHelper.CheckAndRequestPermissionAsync(
                                                           Permission.Location,
                                                           Localizer.GetString("location_permission_prompt_title"),
                                                           Localizer.GetString("location_permission_prompt_message"),
                                                           Localizer.GetString("location_permission_prompt_accept"),
                                                           Localizer.GetString("location_permission_prompt_deny"),
                                                           Localizer.GetString(
                                                               "location_permission_prompt_deny_message"));

                        if (LocationPermissionGiven)
                        {
                            MapMainPage.EnableMyLocation();
                            GetUserLocation();
                        }
                    }
                });
        }

        private async void GetUserLocation()
        {
            GeolocationRequest geolocationRequest = new GeolocationRequest
                                                    {
                                                        DesiredAccuracy = Constants.MainMapLocationAccuracy
                                                    };
            Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync();
            WeatherLocation = location;

            do
            {
                CurrentPosition = Persistence.Entities.Position.FromLocation(location);
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
            Plots = await AppDataService.GetAllPlots();
            MapMainPage?.AddPlots(Plots);
            foreach (Plot plot in Plots.Where(plot => plot.BemData == null))
            {
                plot.BemData = await BemData.LoadBEMData(plot.Position.Latitude, plot.Position.Longitude);
                await AppDataService.UpdatePlot(plot);
            }
        }

        private void NavigateToLocation(Xamarin.Essentials.Location location)
        {
            MapsLaunchOptions mapOptions = new MapsLaunchOptions
                                           {
                                               MapDirectionsMode = MapDirectionsMode.Driving
                                           };
            Maps.OpenAsync(location, mapOptions);
        }

        private async void RefreshWeatherData()
        {
            if (WeatherLocation == null)
            {
                return;
            }

            CurrentWeather = await WeatherForecast.Download(WeatherLocation.Latitude, WeatherLocation.Longitude)
                                 .ConfigureAwait(true);
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
                    DelineationUIIsVisible = false;
                    break;

                case MapTask.SelectLocation:
                case MapTask.SelectLocationForPlanner:
                    CurrentMapTaskHint = Localizer.GetString("task_hint_select_location");
                    CurrentMapTaskHintIsVisible = true;
                    SelectLocationUIIsVisible = true;
                    GPSLocationUIIsVisible = false;
                    DelineationUIIsVisible = false;
                    break;

                case MapTask.CreatePlotByGPS:
                case MapTask.GetLocationForPlanner:
                    CurrentMapTaskHint = Localizer.GetString("task_hint_gps_location");
                    CurrentMapTaskHintIsVisible = true;
                    GPSLocationUIIsVisible = true;
                    SelectLocationUIIsVisible = false;
                    DelineationUIIsVisible = false;
                    break;

                case MapTask.DelineationNotEnoughPoints:
                    if (oldValue != MapTask.DelineationEnoughPoints)
                    {
                        MapMainPage.ZoomToPosition(this.selectedPlot.Position.ForMap());
                    }

                    DelineationUIIsVisible = true;
                    break;
            }
        }

        public DelegateCommand DelineationAccept =>
            new DelegateCommand(
                () =>
                {
                    SelectedPlot.Delineation = CurrentDelineation;
                    AppDataService.UpdatePlot(SelectedPlot);
                    CurrentDelineation = new List<Persistence.Entities.Position>();
                    CurrentMapTask = MapTask.Default;
                    MapMainPage.EndDelineation();
                });

        public DelegateCommand DelineationCancel =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation = new List<Persistence.Entities.Position>();
                    CurrentMapTask = MapTask.Default;
                    MapMainPage.EndDelineation();
                });

        public DelegateCommand DelineationUndo =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation.RemoveAt(CurrentDelineation.Count - 1);
                    MapMainPage.RemoveLastDelineationPoint();
                });

        public bool DelineationUIIsVisible
        {
            get => this.delineationUIIsVisible;
            set => SetProperty(ref this.delineationUIIsVisible, value);
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