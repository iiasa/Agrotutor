
namespace Agrotutor.Modules.Map.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using Agrotutor.Modules.Calendar.ViewModels;
    using Microsoft.Extensions.Localization;
    using Plugin.Media.Abstractions;
    using Plugin.Permissions.Abstractions;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;
    using Location = Xamarin.Essentials.Location;
    using MapsApp = Xamarin.Essentials.Map;

    using Core;
    using Core.Cimmyt.HubsContact;
    using Core.Cimmyt.InvestigationPlatforms;
    using Core.Cimmyt.MachineryPoints;
    using Core.Entities;
    using Core.Rest.Bem;
    using Position = Core.Entities.Position;
    using Core.Persistence;
    using Modules.Plot.ViewModels;
    using Modules.Weather;
    using Modules.Weather.Types;
    using Types;
    using Views;
    using HubFeature = Core.Cimmyt.HubsContact.Feature;
    using IPFeature = Core.Cimmyt.InvestigationPlatforms.Feature;
    using MPFeature = Core.Cimmyt.MachineryPoints.Feature;
    using XF.Material.Forms.UI.Dialogs;

    public class MapPageViewModel : ViewModelBase, INavigatedAware
    {
        private string _currentWeatherIconSource;

        private string _currentWeatherText;

        private int _pickerCropTypesSelectedIndex;

        private bool _plannerUIIsVisible;

        private bool _showWeatherWidget;

        private bool _addParcelIsVisible;

        private Position _addPlotPosition;

        private HubFeature _currentHubContact;

        private IPFeature _currentInvestigationPlatform;

        private MPFeature _currentMachineryPoint;

        private MapTask _currentMapTask;

        private string _currentMapTaskHint;

        private bool _currentMapTaskHintIsVisible;

        private Position currentPosition;

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

        private Location weatherLocation;

        private bool delineationUIIsVisible;
        private bool _plotsLayerVisible;
        private bool _plotDelineationsLayerVisible;
        private bool _hubContactsLayerVisible;
        private bool _machineryPointsLayerVisible;
        private bool _investigationPlatformsLayerVisible;
        private bool _offlineBasemapLayerVisible;
        private bool _layerSwitcherIsVisible;

        public MapPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            IStringLocalizer<MapPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            LocationPermissionGiven = false;
            ShowWeatherWidget = false;
            AppDataService = appDataService;
            CurrentMapTask = MapTask.Default;
            AddParcelIsVisible = false;
            OptionsIsVisible = false;
            LoadingSpinnerIsVisible = false;
            PlannerUIIsVisible = false;
            DelineationUIIsVisible = false;
        }

        public bool PlotsLayerVisible
        {
            get => _plotsLayerVisible;
            set
            {
                SetProperty(ref _plotsLayerVisible, value);
                Preferences.Set(Constants.PlotsLayerVisiblePreference, value);
                MapPage?.SetPlotLayerVisibility(value);
            }
        }

        public bool PlotDelineationsLayerVisible
        {
            get => _plotDelineationsLayerVisible;
            set
            {
                SetProperty(ref _plotDelineationsLayerVisible, value);
                Preferences.Set(Constants.PlotDelineationsLayerVisiblePreference, value);
                MapPage?.SetPlotDelineationLayerVisibility(value);
            }
        }

        public bool HubContactsLayerVisible
        {
            get => _hubContactsLayerVisible;
            set
            {
                SetProperty(ref _hubContactsLayerVisible, value);
                Preferences.Set(Constants.HubContactsLayerVisiblePreference, value);
                MapPage?.SetHubContactsLayerVisibility(value);
            }
        }

        public bool MachineryPointsLayerVisible
        {
            get => _machineryPointsLayerVisible;
            set
            {
                SetProperty(ref _machineryPointsLayerVisible, value);
                Preferences.Set(Constants.MachineryPointsLayerVisiblePreference, value);
                MapPage?.SetMachineryPointLayerVisibility(value);
            }
        }

        public bool InvestigationPlatformsLayerVisible
        {
            get => _investigationPlatformsLayerVisible;
            set
            {
                SetProperty(ref _investigationPlatformsLayerVisible, value);
                Preferences.Set(Constants.InvestigationPlatformsLayerVisiblePreference, value);
                MapPage?.SetInvestigationPlatformLayerVisibility(value);
            }
        }

        public bool OfflineBasemapLayerVisible
        {
            get => _offlineBasemapLayerVisible;
            set
            {
                SetProperty(ref _offlineBasemapLayerVisible, value);
                Preferences.Set(Constants.OfflineBasemapLayerVisiblePreference, value);
                MapPage?.SetOfflineLayerVisibility(value);
            }
        }

        public DelegateCommand AddActivityToSelectedPlot =>
            new DelegateCommand(() => {
                NavigationParameters param = new NavigationParameters
                {
                    { "Plot", SelectedPlot }
                };
                NavigationService.NavigateAsync("NavigationPage/ActivityPage", param);
            });

        public DelegateCommand ShowLayerSwitcher =>
            new DelegateCommand(() => {
                LayerSwitcherIsVisible = true;
            });

        public DelegateCommand AddParcelClicked =>
            new DelegateCommand(() => {
                CurrentMapTask = MapTask.CreatePlotBySelection;
                AddParcelIsVisible = true;
            });

        public DelegateCommand AddPlot => new DelegateCommand(CreatePlot);

        public IAppDataService AppDataService { get; }

        public DelegateCommand ClickChooseLocation =>
            new DelegateCommand(() => {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocation;
            });

        public DelegateCommand ClickChooseLocationPlanner =>
            new DelegateCommand(() => {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocationForPlanner;
            });

        public DelegateCommand ClickGetLocation =>
            new DelegateCommand(() => {
                DimBackground = false;
                AddPlotPosition = CurrentPosition;
                CurrentMapTask = MapTask.CreatePlotByGPS;
            });

        public DelegateCommand ClickGetLocationPlanner =>
            new DelegateCommand(() => {
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
            new DelegateCommand(async () => {
                if (this.selectedPlot.Delineation?.Count > 0)
                {
                    bool confirmDelineation = await UserDialogs.Instance.ConfirmAsync(
                        new ConfirmConfig
                        {
                            Message = StringLocalizer.GetString("replace_delineation_prompt_message"),
                            OkText = StringLocalizer.GetString("replace_delineation_prompt_yes"),
                            CancelText = StringLocalizer.GetString("replace_delineation_prompt_cancel"),
                            Title = StringLocalizer.GetString("replace_delineation_prompt_title")
                        });

                    if (confirmDelineation)
                    {
                        MapPage.StartDelineation(this.selectedPlot);
                        CurrentMapTask = MapTask.DelineationNotEnoughPoints;
                    }
                }
                else
                {
                    MapPage.StartDelineation(this.selectedPlot);
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
                            AddPlotPosition = Position.From(args.Point);
                            CreatePlot();
                            break;

                        case MapTask.DelineationNotEnoughPoints:
                        case MapTask.DelineationEnoughPoints:
                            if (CurrentDelineation == null)
                            {
                                CurrentDelineation = new List<Position>();
                            }

                            CurrentDelineation.Add(Position.From(args.Point));
                            MapPage.AddDelineationPoint(args.Point);
                            break;
                    }
                });

        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked =>
            new DelegateCommand<MapLongClickedEventArgs>(
                args =>
                {
                    AddPlotPosition = Position.From(args.Point);
                    CreatePlot();
                });

        public DelegateCommand NavigateToCurrentHubContact =>
            new DelegateCommand(() => NavigateToLocation(CurrentHubContact.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentInvestigationPlatform =>
            new DelegateCommand(() => NavigateToLocation(CurrentInvestigationPlatform.Geometry.ToLocation()));

        public DelegateCommand NavigateToCurrentMachineryPoint =>
            new DelegateCommand(() => NavigateToLocation(CurrentMachineryPoint.Geometry.ToLocation()));

        public DelegateCommand NavigateToGuide =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("NavigationPage/WelcomePage"); });

        public DelegateCommand NavigateToPractices =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("LinksPage"); });

        public DelegateCommand NavigateToProfile =>
            new DelegateCommand(() => { NavigationService.NavigateAsync("ProfilePage"); });

        public DelegateCommand ShowInfoForSelectedPlot =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters { { "Plot", SelectedPlot } };
                NavigationService.NavigateAsync("PlotMainPage", param);
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
                        SelectedPlot = plot;
                        ShowPlotInformation(plot);
                    }
                    else if (data is HubFeature hubContact)
                    {
                        ShowHubContactInformation(hubContact);
                    }
                    else if (data is IPFeature investigationPlatform)
                    {
                        ShowInvestigationPlatformInformation(investigationPlatform);
                    }
                    else if (data is MPFeature machineryPoint)
                    {
                        ShowMachineryPointInformation(machineryPoint);
                    }
                });

        public DelegateCommand ShowCalendar =>
            new DelegateCommand(
                async () =>
                {
                    NavigationParameters navigationParameters = new NavigationParameters
                                                                {
                                                                    {
                                                                        CalendarPageViewModel.EventsParameterName,
                                                                        Plot.GetCalendarEvents(Plots)
                                                                    },
                                                                    { "Dev", true }
                                                                };
                    await NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowCalendarForSelectedPlot =>
            new DelegateCommand(
                async () =>
                {
                    NavigationParameters navigationParameters = new NavigationParameters
                                                                {
                                                                    {
                                                                        CalendarPageViewModel.EventsParameterName,
                                                                        SelectedPlot.GetCalendarEvents()
                                                                    }
                                                                };
                    await NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowOptions => new DelegateCommand(() => { OptionsIsVisible = true; });

        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.ShowSettingsUI);

        public DelegateCommand ShowWeather =>
            new DelegateCommand(
                async () =>
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
                        await MaterialDialog.Instance.AlertAsync(
                            message: "The weather feature needs to know your current location. This can take some time. Make sure you gave permission to use your location. Weather will be available when you see the current weather on the top of the screen.", 
                            title: "Location not available", 
                            acknowledgementText: "OK");
                        return;
                    }

                    await NavigationService.NavigateAsync("WeatherPage", param);
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
                        Subject = StringLocalizer.GetString("email_subject")
                    };
                    await Email.ComposeAsync(message);
                });

        public bool AddParcelIsVisible
        {
            get => this._addParcelIsVisible;
            set
            {
                SetProperty(ref this._addParcelIsVisible, value);
                if (this._addParcelIsVisible)
                {
                    OptionsIsVisible = false;
                    PlotDetailIsVisible = false;
                    PlannerUIIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    PlannerUIIsVisible = false;
                    DimBackground = true;
                    LayerSwitcherIsVisible = false;
                }
            }
        }

        public Position AddPlotPosition
        {
            get => this._addPlotPosition;
            set
            {
                this._addPlotPosition = value;
                RefreshWeatherData();
            }
        }

        public List<Position> CurrentDelineation { get; set; }

        public HubFeature CurrentHubContact
        {
            get => this._currentHubContact;
            set => SetProperty(ref this._currentHubContact, value);
        }

        public IPFeature CurrentInvestigationPlatform
        {
            get => this._currentInvestigationPlatform;
            set => SetProperty(ref this._currentInvestigationPlatform, value);
        }

        public MPFeature CurrentMachineryPoint
        {
            get => this._currentMachineryPoint;
            set => SetProperty(ref this._currentMachineryPoint, value);
        }

        public MapTask CurrentMapTask
        {
            get => this._currentMapTask;
            set
            {
                MapTask oldValue = this._currentMapTask;
                this._currentMapTask = value;
                SetUIForMapTask(value, oldValue);
            }
        }

        public string CurrentMapTaskHint
        {
            get => this._currentMapTaskHint;
            set => SetProperty(ref this._currentMapTaskHint, value);
        }

        public bool CurrentMapTaskHintIsVisible
        {
            get => this._currentMapTaskHintIsVisible;
            set => SetProperty(ref this._currentMapTaskHintIsVisible, value);
        }

        public bool LayerSwitcherIsVisible 
        { 
            get => _layerSwitcherIsVisible;
            set { 
                SetProperty(ref _layerSwitcherIsVisible, value);

                if (value)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    OptionsIsVisible = false;
                    HubsContactUIIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                    PlannerUIIsVisible = false;
                }
            } 
        }

        public Position CurrentPosition
        {
            get => this.currentPosition;
            set
            {
                this.currentPosition = value;
                if (Util.ShouldRefresh(this.weatherLocation, value))
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
                if (value == null) return;
                SetProperty(ref this.currentWeather, value);
                ShowWeatherWidget = true;
                HourlySummary cur = value?.Location?.HourlySummaries?.HourlySummary?.ElementAt(0);
                if (cur == null) return;
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
                    LayerSwitcherIsVisible = false;
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
                MapPage.SetHubsContact(value);
            }
        }

        public bool HubsContactUIIsVisible
        {
            get => this.hubsContactUIIsVisible;
            set
            {
                SetProperty(ref this.hubsContactUIIsVisible, value);
                if (value)
                {
                    AddParcelIsVisible = false;
                    PlotDetailIsVisible = false;
                    OptionsIsVisible = false;
                    InvestigationPlatformUIIsVisible = false;
                    MachineryPointUIIsVisible = false;
                    DimBackground = true;
                    LayerSwitcherIsVisible = false;
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
                MapPage.SetInvestigationPlatforms(value);
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
                    LayerSwitcherIsVisible = false;
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
                    MapPage.EnableMyLocation();
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
                MapPage.SetMachineryPoints(value);
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
                    LayerSwitcherIsVisible = false;
                }
            }
        }

        public MapPage MapPage { get; set; }

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
                    LayerSwitcherIsVisible = false;
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
                    LayerSwitcherIsVisible = false;
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
                    LayerSwitcherIsVisible = false;
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

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ListenForLocation = false;
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Task.Run(() => EnableUserLocation());
            Task.Run(() => LoadPlots());
            Task.Run(() => LoadMapData());
            base.OnNavigatedTo(parameters);
        }

        public void SetView(MapPage View)
        {
            this.MapPage = View;
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
                                               Message = StringLocalizer.GetString("new_plot_prompt_message"),
                                               OkText = StringLocalizer.GetString("new_plot_prompt_yes"),
                                               CancelText = StringLocalizer.GetString("new_plot_prompt_cancel"),
                                               Title = StringLocalizer.GetString("new_plot_prompt_title")
                                           });

            if (confirmPlotCreation)
            {
                NavigationParameters navigationParams = new NavigationParameters
                {
                    {
                        AddPlotPageViewModel.PositionParameterName,
                        AddPlotPosition
                    }
                };
                await NavigationService.NavigateAsync("NavigationPage/AddPlotPage", navigationParams);
            }
        }

        private void EnableUserLocation()
        {
            Device.BeginInvokeOnMainThread(
                async () =>
                {
                    LocationPermissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
                    if (LocationPermissionGiven)
                    {
                        MapPage.EnableMyLocation();
                        GetUserLocation();
                    }
                    else
                    {
                        bool LocationPermissionGiven = await PermissionHelper.CheckAndRequestPermissionAsync(
                                                           Permission.Location,
                                                           StringLocalizer.GetString("location_permission_prompt_title"),
                                                           StringLocalizer.GetString("location_permission_prompt_message"),
                                                           StringLocalizer.GetString("location_permission_prompt_accept"),
                                                           StringLocalizer.GetString("location_permission_prompt_deny"),
                                                           StringLocalizer.GetString(
                                                               "location_permission_prompt_deny_message"));

                        if (LocationPermissionGiven)
                        {
                            MapPage.EnableMyLocation();
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
                CurrentPosition = Position.FromLocation(location);
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

            PlotsLayerVisible = Preferences.Get(Constants.PlotsLayerVisiblePreference, true);
            PlotDelineationsLayerVisible = Preferences.Get(Constants.PlotDelineationsLayerVisiblePreference, true);
            HubContactsLayerVisible = Preferences.Get(Constants.HubContactsLayerVisiblePreference, true);
            MachineryPointsLayerVisible = Preferences.Get(Constants.MachineryPointsLayerVisiblePreference, true);
            InvestigationPlatformsLayerVisible = Preferences.Get(Constants.InvestigationPlatformsLayerVisiblePreference, true);
            OfflineBasemapLayerVisible = Preferences.Get(Constants.OfflineBasemapLayerVisiblePreference, false);
        }

        private async void LoadPlots()
        {
            Plots = await AppDataService.GetAllPlotsAsync();
            MapPage?.AddPlots(Plots);
            foreach (Plot plot in Plots.Where(plot => plot.BemData == null))
            {
                if (plot.Position == null) continue;
                plot.BemData = await BemDataDownloadHelper.LoadBEMData(plot.Position.Latitude, plot.Position.Longitude);
                await AppDataService.UpdatePlotAsync(plot);
            }
        }

        private void NavigateToLocation(Location location)
        {
            MapLaunchOptions mapOptions = new MapLaunchOptions
            {
                NavigationMode = Xamarin.Essentials.NavigationMode.Driving
            };
            MapsApp.OpenAsync(location, mapOptions);
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
                    CurrentMapTaskHint = StringLocalizer.GetString("task_hint_select_location");
                    CurrentMapTaskHintIsVisible = true;
                    SelectLocationUIIsVisible = true;
                    GPSLocationUIIsVisible = false;
                    DelineationUIIsVisible = false;
                    break;

                case MapTask.CreatePlotByGPS:
                case MapTask.GetLocationForPlanner:
                    CurrentMapTaskHint = StringLocalizer.GetString("task_hint_gps_location");
                    CurrentMapTaskHintIsVisible = true;
                    GPSLocationUIIsVisible = true;
                    SelectLocationUIIsVisible = false;
                    DelineationUIIsVisible = false;
                    break;

                case MapTask.DelineationNotEnoughPoints:
                    if (oldValue != MapTask.DelineationEnoughPoints)
                    {
                        MapPage.ZoomToPosition(this.selectedPlot.Position.ForMap());
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
                    AppDataService.UpdatePlotAsync(SelectedPlot);
                    CurrentDelineation = new List<Position>();
                    CurrentMapTask = MapTask.Default;
                    MapPage.EndDelineation();
                });

        public DelegateCommand DelineationCancel =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation = new List<Position>();
                    CurrentMapTask = MapTask.Default;
                    MapPage.EndDelineation();
                });

        public DelegateCommand DelineationUndo =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation.RemoveAt(CurrentDelineation.Count - 1);
                    MapPage.RemoveLastDelineationPoint();
                });

        public bool DelineationUIIsVisible
        {
            get => this.delineationUIIsVisible;
            set => SetProperty(ref this.delineationUIIsVisible, value);
        }

        private void ShowHubContactInformation(HubFeature hubContact)
        {
            CurrentHubContact = hubContact;
            HubsContactUIIsVisible = true;
        }

        private void ShowInvestigationPlatformInformation(
            IPFeature investigationPlatform)
        {
            CurrentInvestigationPlatform = investigationPlatform;
            InvestigationPlatformUIIsVisible = true;
        }

        private void ShowMachineryPointInformation(MPFeature machineryPoint)
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
