using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Agrotutor.Core;
using Agrotutor.Core.Cimmyt.HubsContact;
using Agrotutor.Core.Cimmyt.InvestigationPlatforms;
using Agrotutor.Core.Cimmyt.MachineryPoints;
using Agrotutor.Core.Persistence;
using Agrotutor.Core.Rest.Bem;
using Agrotutor.Dev;
using Agrotutor.Modules.Calendar.ViewModels;
using Agrotutor.Modules.Map.Types;
using Agrotutor.Modules.Map.Views;
using Agrotutor.Modules.Plot.ViewModels;
using Agrotutor.Modules.Weather;
using Agrotutor.Modules.Weather.Types;
using Microsoft.Extensions.Localization;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XF.Material.Forms.UI.Dialogs;
using Feature = Agrotutor.Core.Cimmyt.HubsContact.Feature;
using Location = Xamarin.Essentials.Location;
using NavigationMode = Xamarin.Essentials.NavigationMode;
using Position = Agrotutor.Core.Entities.Position;

namespace Agrotutor.Modules.Map.ViewModels
{
    using MapsApp = Xamarin.Essentials.Map;
    using HubFeature = Feature;
    using IPFeature = Core.Cimmyt.InvestigationPlatforms.Feature;
    using MPFeature = Core.Cimmyt.MachineryPoints.Feature;

    public class MapPageViewModel : ViewModelBase, INavigatedAware
    {
        private bool _addParcelIsVisible;

        private Position _addPlotPosition;

        private HubFeature _currentHubContact;

        private IPFeature _currentInvestigationPlatform;

        private MPFeature _currentMachineryPoint;

        private MapTask _currentMapTask;

        private string _currentMapTaskHint;

        private bool _currentMapTaskHintIsVisible;
        private string _currentWeatherIconSource;

        private string _currentWeatherText;
        private bool _hubContactsLayerVisible;
        private bool _investigationPlatformsLayerVisible;
        private bool _layerSwitcherIsVisible;
        private bool _machineryPointsLayerVisible;
        private bool _offlineBasemapLayerVisible;

        private int _pickerCropTypesSelectedIndex;

        private bool _plannerUIIsVisible;
        private bool _plotDelineationsLayerVisible;
        private bool _plotsLayerVisible;

        private bool _showWeatherWidget;

        private Position currentPosition;

        private WeatherForecast currentWeather;

        private bool delineationUIIsVisible;

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

        private Core.Entities.Plot selectedPlot;

        private bool selectLocationUIIsVisible;

        private Location weatherLocation;

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
            Plots = new List<Core.Entities.Plot>();
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
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters
                {
                    {"Plot", SelectedPlot}
                };
                NavigationService.NavigateAsync("NavigationPage/ActivityPage", param);
            });

        public DelegateCommand ShowLayerSwitcher =>
            new DelegateCommand(() => { LayerSwitcherIsVisible = true; });

        public DelegateCommand PageAppearingCommand =>
            new DelegateCommand(async () => await PageAppearing());

        public DelegateCommand AddParcelClicked =>
            new DelegateCommand(() =>
            {
                CurrentMapTask = MapTask.CreatePlotBySelection;
                AddParcelIsVisible = true;
            });

        public DelegateCommand AddPlot => new DelegateCommand(CreatePlot);

        public IAppDataService AppDataService { get; }

        public DelegateCommand ClickChooseLocation =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocation;
            });

        public DelegateCommand ClickChooseLocationPlanner =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocationForPlanner;
            });

        public DelegateCommand ClickGetLocation =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                AddPlotPosition = CurrentPosition;
                CurrentMapTask = MapTask.CreatePlotByGPS;
            });

        public DelegateCommand ClickGetLocationPlanner =>
            new DelegateCommand(() =>
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
            new DelegateCommand(async () =>
            {
                if (selectedPlot.Delineation?.Count > 0)
                {
                    var confirmDelineation = await UserDialogs.Instance.ConfirmAsync(
                        new ConfirmConfig
                        {
                            Message = StringLocalizer.GetString("replace_delineation_prompt_message"),
                            OkText = StringLocalizer.GetString("replace_delineation_prompt_yes"),
                            CancelText = StringLocalizer.GetString("replace_delineation_prompt_cancel"),
                            Title = StringLocalizer.GetString("replace_delineation_prompt_title")
                        });

                    if (confirmDelineation)
                    {
                        MapPage.StartDelineation(selectedPlot);
                        CurrentMapTask = MapTask.DelineationNotEnoughPoints;
                    }
                }
                else
                {
                    MapPage.StartDelineation(selectedPlot);
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
                            if (CurrentDelineation == null) CurrentDelineation = new List<Position>();

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
                var param = new NavigationParameters {{"Plot", SelectedPlot}};
                NavigationService.NavigateAsync("PlotMainPage", param);
            });

        public DelegateCommand<MediaFile> OnParcelPictureTaken =>
            new DelegateCommand<MediaFile>(
                mediaFile =>
                {
                    var i = 0;
                    i++;
                });

        public DelegateCommand<string> PhoneCall => new DelegateCommand<string>(number => PhoneDialer.Open(number));

        public DelegateCommand<PinClickedEventArgs> PinClicked =>
            new DelegateCommand<PinClickedEventArgs>(
                args =>
                {
                    var data = args.Pin.Tag;
                    if (data is Core.Entities.Plot plot)
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
                    var navigationParameters = new NavigationParameters
                    {
                        {
                            CalendarPageViewModel.EventsParameterName,
                            Core.Entities.Plot.GetCalendarEvents(Plots)
                        },
                        {"Dev", true}
                    };
                    await NavigationService.NavigateAsync("NavigationPage/CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowCalendarForSelectedPlot =>
            new DelegateCommand(
                async () =>
                {
                    var navigationParameters = new NavigationParameters
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
                    var param = new NavigationParameters();
                    if (CurrentWeather != null) param.Add("Forecast", CurrentWeather);

                    if (WeatherLocation != null)
                    {
                        param.Add("Location", WeatherLocation);
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(
                            "The weather feature needs to know your current location. This can take some time. Make sure you gave permission to use your location. Weather will be available when you see the current weather on the top of the screen.",
                            "Location not available",
                            "OK");
                        return;
                    }

                    await NavigationService.NavigateAsync("WeatherPage", param);
                });

        public DelegateCommand StartPlanner => new DelegateCommand(() => { PlannerUIIsVisible = true; });

        public DelegateCommand<string> WriteEmail =>
            new DelegateCommand<string>(
                async emailAddress =>
                {
                    var message = new EmailMessage
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
            get => _addParcelIsVisible;
            set
            {
                SetProperty(ref _addParcelIsVisible, value);
                if (_addParcelIsVisible)
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
            get => _addPlotPosition;
            set
            {
                _addPlotPosition = value;
                RefreshWeatherData();
            }
        }

        public List<Position> CurrentDelineation { get; set; }

        public HubFeature CurrentHubContact
        {
            get => _currentHubContact;
            set => SetProperty(ref _currentHubContact, value);
        }

        public IPFeature CurrentInvestigationPlatform
        {
            get => _currentInvestigationPlatform;
            set => SetProperty(ref _currentInvestigationPlatform, value);
        }

        public MPFeature CurrentMachineryPoint
        {
            get => _currentMachineryPoint;
            set => SetProperty(ref _currentMachineryPoint, value);
        }

        public MapTask CurrentMapTask
        {
            get => _currentMapTask;
            set
            {
                var oldValue = _currentMapTask;
                _currentMapTask = value;
                SetUIForMapTask(value, oldValue);
            }
        }

        public string CurrentMapTaskHint
        {
            get => _currentMapTaskHint;
            set => SetProperty(ref _currentMapTaskHint, value);
        }

        public bool CurrentMapTaskHintIsVisible
        {
            get => _currentMapTaskHintIsVisible;
            set => SetProperty(ref _currentMapTaskHintIsVisible, value);
        }

        public bool LayerSwitcherIsVisible
        {
            get => _layerSwitcherIsVisible;
            set
            {
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
            get => currentPosition;
            set
            {
                currentPosition = value;
                if (Util.ShouldRefresh(weatherLocation, value))
                {
                    weatherLocation = value;
                    Task.Run(() => RefreshWeatherData());
                }
            }
        }

        public WeatherForecast CurrentWeather
        {
            get => currentWeather;
            set
            {
                if (value == null) return;
                SetProperty(ref currentWeather, value);
                ShowWeatherWidget = true;
                var cur = value?.Location?.HourlySummaries?.HourlySummary?.ElementAt(0);
                if (cur == null) return;
                CurrentWeatherIconSource = cur.TinyWxIcon;
                CurrentWeatherText = cur.WxText;
            }
        }

        public string CurrentWeatherIconSource
        {
            get => _currentWeatherIconSource;
            set => SetProperty(ref _currentWeatherIconSource, value);
        }

        public string CurrentWeatherText
        {
            get => _currentWeatherText;
            set => SetProperty(ref _currentWeatherText, value);
        }

        public bool DimBackground
        {
            get => dimBackground;
            set
            {
                SetProperty(ref dimBackground, value);
                if (!dimBackground)
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
            get => gpsLocationUIIsVisible;
            set => SetProperty(ref gpsLocationUIIsVisible, value);
        }

        public HubsContact HubsContact
        {
            get => hubsContact;
            private set
            {
                hubsContact = value;
                MapPage.SetHubsContact(value);
            }
        }

        public bool HubsContactUIIsVisible
        {
            get => hubsContactUIIsVisible;
            set
            {
                SetProperty(ref hubsContactUIIsVisible, value);
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
            get => investigationPlatforms;
            private set
            {
                investigationPlatforms = value;
                MapPage.SetInvestigationPlatforms(value);
            }
        }

        public bool InvestigationPlatformUIIsVisible
        {
            get => investigationPlatformUIIsVisible;
            set
            {
                SetProperty(ref investigationPlatformUIIsVisible, value);
                if (investigationPlatformUIIsVisible)
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
            get => loadingSpinnerIsVisible;
            set => SetProperty(ref loadingSpinnerIsVisible, value);
        }

        public bool LocationPermissionGiven
        {
            get => locationPermissionGiven;
            set
            {
                SetProperty(ref locationPermissionGiven, value);
                if (value)
                {
                    MapPage.EnableMyLocation();
                    GetUserLocation();
                }
            }
        }

        public MachineryPoints MachineryPoints
        {
            get => machineryPoints;
            private set
            {
                machineryPoints = value;
                MapPage.SetMachineryPoints(value);
            }
        }

        public bool MachineryPointUIIsVisible
        {
            get => machineryPointUIIsVisible;
            set
            {
                SetProperty(ref machineryPointUIIsVisible, value);
                if (machineryPointUIIsVisible)
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
            get => optionsIsVisible;
            set
            {
                SetProperty(ref optionsIsVisible, value);
                if (optionsIsVisible)
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
            get => _pickerCropTypesSelectedIndex;
            set => SetProperty(ref _pickerCropTypesSelectedIndex, value);
        }

        public bool PlannerUIIsVisible
        {
            get => _plannerUIIsVisible;
            set
            {
                SetProperty(ref _plannerUIIsVisible, value);

                if (_plannerUIIsVisible)
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
            get => plotDetailIsVisible;
            set
            {
                SetProperty(ref plotDetailIsVisible, value);
                if (plotDetailIsVisible)
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

        public IEnumerable<Core.Entities.Plot> Plots { get; set; }

        public Core.Entities.Plot SelectedPlot
        {
            get => selectedPlot;
            set => SetProperty(ref selectedPlot, value);
        }

        public bool SelectLocationUIIsVisible
        {
            get => selectLocationUIIsVisible;
            set => SetProperty(ref selectLocationUIIsVisible, value);
        }

        public bool ShowWeatherWidget
        {
            get => _showWeatherWidget;
            set => SetProperty(ref _showWeatherWidget, value);
        }

        public Location WeatherLocation
        {
            get => weatherLocation;
            set
            {
                weatherLocation = value;
                Task.Run(() => RefreshWeatherData());
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
            get => delineationUIIsVisible;
            set => SetProperty(ref delineationUIIsVisible, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ListenForLocation = false;
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //Task.Run(() => EnableUserLocation());
            //Task.Run(() => LoadPlots());
            //Task.Run(() => LoadMapData());
            base.OnNavigatedTo(parameters);
        }

        private async Task PageAppearing()
        {
            //Profiler.Start(Constants.Plots);
            //using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading plots..."))
            //{
            //    await LoadPlots();
            //}
            //Profiler.Stop(Constants.Plots);

            

            Profiler.Start(Constants.MapData);
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading map data..."))
            {
                await LoadMapData();
            }
            Profiler.Stop(Constants.MapData);


            Profiler.Start(Constants.UserLocation);
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Getting user location..."))
            {
                await EnableUserLocation();
            }
            Profiler.Stop(Constants.UserLocation);
            //Task.Run(() => EnableUserLocation());
            //Task.Run(() => LoadPlots());
            //Task.Run(() => LoadMapData());
        }

        public void SetView(MapPage View)
        {
            MapPage = View;
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

            var confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(
                new ConfirmConfig
                {
                    Message = StringLocalizer.GetString("new_plot_prompt_message"),
                    OkText = StringLocalizer.GetString("new_plot_prompt_yes"),
                    CancelText = StringLocalizer.GetString("new_plot_prompt_cancel"),
                    Title = StringLocalizer.GetString("new_plot_prompt_title")
                });

            if (confirmPlotCreation)
            {
                var navigationParams = new NavigationParameters
                {
                    {
                        AddPlotPageViewModel.PositionParameterName,
                        AddPlotPosition
                    }
                };
                await NavigationService.NavigateAsync("NavigationPage/AddPlotPage", navigationParams);
            }
        }

        private async Task EnableUserLocation()
        {

            LocationPermissionGiven = await PermissionHelper.HasPermissionAsync(Permission.Location);
            if (LocationPermissionGiven)
            {
                await GetUserLocation();
            }
            else
            {
                LocationPermissionGiven = await PermissionHelper.CheckAndRequestPermissionAsync(
                    Permission.Location,
                    StringLocalizer.GetString("location_permission_prompt_title"),
                    StringLocalizer.GetString("location_permission_prompt_message"),
                    StringLocalizer.GetString("location_permission_prompt_accept"),
                    StringLocalizer.GetString("location_permission_prompt_deny"),
                    StringLocalizer.GetString(
                        "location_permission_prompt_deny_message"));

                if (LocationPermissionGiven)
                {
                    await GetUserLocation();
                }
            }
        }

        private async Task GetUserLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            WeatherLocation = location;


            CurrentPosition = Position.FromLocation(location);
            if (CurrentMapTask == MapTask.CreatePlotByGPS) AddPlotPosition = CurrentPosition;

            //GeolocationRequest geolocationRequest = new GeolocationRequest
            //{
            //    DesiredAccuracy = Constants.MainMapLocationAccuracy
            //};
            //Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync();
            //WeatherLocation = location;

            //do
            //{
            //    CurrentPosition = Position.FromLocation(location);
            //    if (CurrentMapTask == MapTask.CreatePlotByGPS)
            //    {
            //        AddPlotPosition = CurrentPosition;
            //    }

            //    await Task.Delay(Constants.MainMapLocationRefreshPeriod * 1000);
            //    location = await Geolocation.GetLocationAsync(geolocationRequest);
            //}
            //while (ListenForLocation);
        }

        private async Task LoadMapData()
        {
            HubsContact = await HubsContact.FromEmbeddedResource();
            InvestigationPlatforms = await InvestigationPlatforms.FromEmbeddedResource();
            MachineryPoints = await MachineryPoints.FromEmbeddedResource();

            PlotsLayerVisible = Preferences.Get(Constants.PlotsLayerVisiblePreference, true);
            PlotDelineationsLayerVisible = Preferences.Get(Constants.PlotDelineationsLayerVisiblePreference, true);
            HubContactsLayerVisible = Preferences.Get(Constants.HubContactsLayerVisiblePreference, true);
            MachineryPointsLayerVisible = Preferences.Get(Constants.MachineryPointsLayerVisiblePreference, true);
            InvestigationPlatformsLayerVisible =
                Preferences.Get(Constants.InvestigationPlatformsLayerVisiblePreference, true);
            OfflineBasemapLayerVisible = Preferences.Get(Constants.OfflineBasemapLayerVisiblePreference, false);
        }

        private async Task LoadPlots()
        {
            Plots = await AppDataService.GetAllPlotsAsync();
            var plots = Plots.ToList();
            MapPage?.AddPlots(plots);
            foreach (var plot in plots.Where(plot => plot.BemData == null))
            {
                if (plot.Position == null) continue;
                plot.BemData = await BemDataDownloadHelper.LoadBEMData(plot.Position.Latitude, plot.Position.Longitude);
                await AppDataService.UpdatePlotAsync(plot);
            }
        }

        private void NavigateToLocation(Location location)
        {
            var mapOptions = new MapLaunchOptions
            {
                NavigationMode = NavigationMode.Driving
            };
            MapsApp.OpenAsync(location, mapOptions);
        }

        private async void RefreshWeatherData()
        {
            if (WeatherLocation == null) return;

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
                        MapPage.ZoomToPosition(selectedPlot.Position.ForMap());

                    DelineationUIIsVisible = true;
                    break;
            }
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

        private void ShowPlotInformation(Core.Entities.Plot data)
        {
            SelectedPlot = data;
            PlotDetailIsVisible = true;
        }
    }
}