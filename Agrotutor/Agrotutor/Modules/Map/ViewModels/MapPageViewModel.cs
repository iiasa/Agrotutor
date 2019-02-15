using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Agrotutor.Core;
using Agrotutor.Core.Camera;
using Agrotutor.Core.Cimmyt.HubsContact;
using Agrotutor.Core.Cimmyt.InvestigationPlatforms;
using Agrotutor.Core.Cimmyt.MachineryPoints;
using Agrotutor.Core.Entities;
using Agrotutor.Core.Persistence;
using Agrotutor.Core.Rest.Bem;
using Agrotutor.Dev;
using Agrotutor.Modules.Benchmarking.ViewModels;
using Agrotutor.Modules.Calendar.ViewModels;
using Agrotutor.Modules.Ciat;
using Agrotutor.Modules.Map.Types;
using Agrotutor.Modules.Map.Views;
using Agrotutor.Modules.Plot.ViewModels;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.Modules.Weather;
using Agrotutor.Modules.Weather.Types;
using Castle.Core.Internal;
using Microsoft.Extensions.Localization;
using Plugin.Permissions.Abstractions;
using Prism;
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
        private CameraPosition _cameraPositionUpdated;

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
        private bool _locationEnabled;
        private bool _machineryPointsLayerVisible;
        private bool _offlineBasemapLayerVisible;

        private int _pickerCropTypesSelectedIndex;
        private ObservableCollection<Pin> _pins;

        private bool _plannerUIIsVisible;
        private bool _plotDelineationsLayerVisible;
        private bool _plotsLayerVisible;
        private ObservableCollection<Polygon> _polygons;
        private MapSpan _region;

        private bool _showWeatherWidget;

        private Position currentPosition;

        private WeatherForecast currentWeather;

        private bool delineationUIIsVisible;

        private bool dimBackground;

        private bool gpsLocationUIIsVisible;

        private bool hubsContactUIIsVisible;

        private bool investigationPlatformUIIsVisible;

        private bool listenForLocation = true;

        private bool locationPermissionGiven;

        private bool machineryPointUIIsVisible;

        private bool optionsIsVisible;

        private bool plotDetailIsVisible;

        private Core.Entities.Plot selectedPlot;

        private bool selectLocationUIIsVisible;

        private Location weatherLocation;
        private string _currentPlotCost;
        private string _currentPlotProfit;
        private string _currentPlotIncome;
        private string _currentPlotYield;

        private readonly ICameraService _cameraService;
        private string _currentPlotPotentialYield;
        private string _currentPlotNitrogen;
        private string _currentPlotPriceForecast;
        private bool _showTileLayer;

        public MapPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            ICameraService cameraService,
            IStringLocalizer<MapPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            _cameraService = cameraService;
            LocationPermissionGiven = false;
            ShowWeatherWidget = false;
            AppDataService = appDataService;
            CurrentMapTask = MapTask.Default;
            AddParcelIsVisible = false;
            OptionsIsVisible = false;
            PlannerUIIsVisible = false;
            DelineationUIIsVisible = false;
            Plots = new List<Core.Entities.Plot>();

            Polygons = new ObservableCollection<Polygon>();
            Pins = new ObservableCollection<Pin>();
            ShowTileLayer = true;
        }

        public ObservableCollection<Polygon> Polygons
        {
            get => _polygons;
            set => SetProperty(ref _polygons, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public bool ShowTileLayer
        {
            get => _showTileLayer;
            set => SetProperty(ref _showTileLayer, value);
        }

        public bool PlotsLayerVisible
        {
            get => _plotsLayerVisible;
            set
            {
                SetProperty(ref _plotsLayerVisible, value);
                Preferences.Set(Constants.PlotsLayerVisiblePreference, value);
                //MapPage?.SetPlotLayerVisibility(value);
            }
        }

        public DelegateCommand ShowCurrentPlotCost => new DelegateCommand(() => 
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Cost.IsNullOrEmpty())
            {
                // TODO: show toast
                return;
            }

            NavigationService.NavigateAsync("ViewCostPage", new NavigationParameters
            {
                {ViewCostPageViewModel.CostsParameterName, SelectedPlot.BemData.Cost}
            });
        });

        public DelegateCommand ShowCurrentPlotIncome => new DelegateCommand(() =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Income.IsNullOrEmpty())
            {
                // TODO: show toast
                return;
            }

            NavigationService.NavigateAsync("ViewIncomePage", new NavigationParameters
            {
                {ViewIncomePageViewModel.IncomesParameterName, SelectedPlot.BemData.Income}
            });
        });

        public DelegateCommand ShowCurrentPlotProfit => new DelegateCommand(() =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Profit.IsNullOrEmpty())
            {
                // TODO: show toast
                return;
            }

            NavigationService.NavigateAsync("ViewProfitPage", new NavigationParameters
            {
                {ViewProfitPageViewModel.ProfitsParameterName, SelectedPlot.BemData.Profit}
            });

        });

        public DelegateCommand ShowCurrentPlotYield => new DelegateCommand(() =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Yield.IsNullOrEmpty())
            {
                // TODO: show toast
                return;
            }

            NavigationService.NavigateAsync("ViewYieldPage", new NavigationParameters
            {
                {ViewYieldPageViewModel.YieldsParameterName, SelectedPlot.BemData.Yield}
            });

        });

        public string CurrentPlotCost
        {
            get => _currentPlotCost;
            set => SetProperty(ref _currentPlotCost, value);
        }

        public string CurrentPlotProfit
        {
            get => _currentPlotProfit;
            set => SetProperty(ref _currentPlotProfit, value);
        }

        public string CurrentPlotIncome
        {
            get => _currentPlotIncome;
            set => SetProperty(ref _currentPlotIncome, value);
        }

        public string CurrentPlotYield
        {
            get => _currentPlotYield;
            set => SetProperty(ref _currentPlotYield, value);
        }

        public bool LocationEnabled
        {
            get => _locationEnabled;
            set => SetProperty(ref _locationEnabled, value);
        }

        public bool PlotDelineationsLayerVisible
        {
            get => _plotDelineationsLayerVisible;
            set
            {
                SetProperty(ref _plotDelineationsLayerVisible, value);
                Preferences.Set(Constants.PlotDelineationsLayerVisiblePreference, value);
                //MapPage?.SetPlotDelineationLayerVisibility(value);
            }
        }

        public bool HubContactsLayerVisible
        {
            get => _hubContactsLayerVisible;
            set
            {
                SetProperty(ref _hubContactsLayerVisible, value);
                Preferences.Set(Constants.HubContactsLayerVisiblePreference, value);
                //MapPage?.SetHubContactsLayerVisibility(value);
            }
        }

        public bool MachineryPointsLayerVisible
        {
            get => _machineryPointsLayerVisible;
            set
            {
                SetProperty(ref _machineryPointsLayerVisible, value);
                Preferences.Set(Constants.MachineryPointsLayerVisiblePreference, value);
            }
        }



        public Command<bool> PlotsSelectionChangedCommand =>
            new Command<bool>(async e => await PlotsSelectionChanged(e));

        public Command<bool> PlotDelineationsSelectionChangedCommand =>
            new Command<bool>(async e => await PlotDelineationsSelectionChanged(e));

        private async Task PlotDelineationsSelectionChanged(bool b)
        {
            if (b)
                await RenderPlotDelineations();
            else
                RemoveDelineationPolygon();
            await Task.CompletedTask;
        }


        private async Task RenderPlotDelineations()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Rendering delineations..."))
            {
                var plots = await AppDataService.GetAllPlotsAsync();

                foreach (var plot in plots)
                {
                    if (plot.Delineation == null) continue;
                    
                    var positions = plot.Delineation;
                    if (positions.Count > 3)
                    {
                        var polygon = new Polygon
                        {
                            Tag = plot.Position
                        };
                        foreach (var position in positions) polygon.Positions.Add(position.ForMap());
                        Polygons.Add(polygon);
                    }
                }
            }
        }

        public Command<bool> HubContactsSelectionChangedCommand =>
            new Command<bool>(async e => await HubContactsSelectionChanged(e));

        public Command<bool> MachineryPointsSelectionChangedCommand =>
            new Command<bool>(async e => await MachineryPointsSelectionChanged(e));

        public Command<bool> InvestigationPlatformsSelectionChangedCommand =>
            new Command<bool>(async e => await InvestigationPlatformsSelectionChanged(e));

        public bool InvestigationPlatformsLayerVisible
        {
            get => _investigationPlatformsLayerVisible;
            set
            {
                SetProperty(ref _investigationPlatformsLayerVisible, value);
                Preferences.Set(Constants.InvestigationPlatformsLayerVisiblePreference, value);
            }
        }

        public bool OfflineBasemapLayerVisible
        {
            get => _offlineBasemapLayerVisible;
            set
            {
                SetProperty(ref _offlineBasemapLayerVisible, value);
                Preferences.Set(Constants.OfflineBasemapLayerVisiblePreference, value);
                ShowTileLayer = !ShowTileLayer;
                // MapPage?.SetOfflineLayerVisibility(value);
                //MapPage?.SetOfflineLayerVisibility(value);
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

                    if (confirmDelineation) CurrentMapTask = MapTask.DelineationNotEnoughPoints;
                }
                else
                {
                    //MapPage.StartDelineation(selectedPlot);
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

                            var pos = Position.From(args.Point);
                            CurrentDelineation.Add(pos);
                            var pin = new Pin
                            {
                                Position = Position.From(args.Point).ForMap(),
                                Label = "Delineation point",
                                Tag = pos
                            };
                            CurrentPin = pin;
                            Preferences.Set(Constants.Lat, args.Point.Latitude);
                            Preferences.Set(Constants.Lng, args.Point.Longitude);
                            Pins.Add(pin);
                            RenderDelineationPolygon();
                            break;
                    }
                });

        private void RenderDelineationPolygon()
        {
            if (CurrentDelineation.Count < 3) return;
            RemoveDelineationPolygon();
            var polygon = new Polygon();
            foreach (var position in CurrentDelineation) polygon.Positions.Add(position.ForMap());
            CurrentPolygon = polygon;
            Polygons.Add(polygon);
        }


        public DelegateCommand<MapLongClickedEventArgs> MapLongClicked =>
            new DelegateCommand<MapLongClickedEventArgs>(
                args =>
                {
                    AddPlotPosition = Position.From(args.Point);
                    Preferences.Set(Constants.Lat, args.Point.Latitude);
                    Preferences.Set(Constants.Lng, args.Point.Longitude);
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

        public DelegateCommand AddPictureToSelectedPlot =>
            new DelegateCommand(async () =>
            {
                var pic = await _cameraService.TakePicture();

                var image = new MediaItem
                {
                    Id = Guid.NewGuid(),
                    Path = pic,
                    IsUploaded = false,
                    IsVideo = false
                };
                SelectedPlot.MediaItems.Add(image);
                await AppDataService.UpdatePlotAsync(SelectedPlot);
                await MapPage.UpdateImages();
            });

        public DelegateCommand AddVideoToSelectedPlot =>
            new DelegateCommand(async () =>
            {
                var pic = await _cameraService.TakeVideo();

                var image = new MediaItem
                {
                    Id = Guid.NewGuid(),
                    Path = pic,
                    IsUploaded = false,
                    IsVideo = true
                };
                SelectedPlot.MediaItems.Add(image);
                await AppDataService.UpdatePlotAsync(SelectedPlot);
                await MapPage.UpdateImages();
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
                        ShowHubContactInformation(hubContact);
                    else if (data is IPFeature investigationPlatform)
                        ShowInvestigationPlatformInformation(investigationPlatform);
                    else if (data is MPFeature machineryPoint) ShowMachineryPointInformation(machineryPoint);
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
                if (value == null) return;
                currentPosition = value;
                if (Util.ShouldRefresh(weatherLocation, value)) WeatherLocation = value;
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
                var today = value?.Location?.DailySummaries?.DailySummary?.ElementAt(0);
                if (cur == null) return;
                CurrentWeatherIconSource = cur.TinyWxIcon;
                var text = $"{cur.TempC} °C";
                if (today != null) text += $" | Rain: {today.precipitationProbability} %";
                CurrentWeatherText = text;
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

        public HubsContact HubsContact { get; private set; }

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

        public InvestigationPlatforms InvestigationPlatforms { get; private set; }

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

        public bool LocationPermissionGiven
        {
            get => locationPermissionGiven;
            set
            {
                SetProperty(ref locationPermissionGiven, value);
                if (value) GetUserLocation();
            }
        }

        public MachineryPoints MachineryPoints { get; private set; }

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

        //public MapPage MapPage { get; set; }

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

        public CameraPosition CameraPositionUpdated
        {
            get => _cameraPositionUpdated;
            set => SetProperty(ref _cameraPositionUpdated, value);
        }

        public IEnumerable<Core.Entities.Plot> Plots { get; set; }

        public Core.Entities.Plot SelectedPlot
        {
            get => selectedPlot;
            set
            {
                if (value == null || value == selectedPlot) return;
                SetProperty(ref selectedPlot, value);

                if (value.MediaItems == null) value.MediaItems = new List<MediaItem>();
                LoadPlotData(value);

                var cost = SelectedPlot?.BemData?.AverageCost;
                var yield = SelectedPlot?.BemData?.AverageYield;
                var profit = SelectedPlot?.BemData?.AverageProfit;
                var income = SelectedPlot?.BemData?.AverageIncome;
                var potentialYield = SelectedPlot?.CiatData?.CiatDataIrrigated?.YieldMax;
                var nitrogenNeeded = SelectedPlot?.CiatData?.CiatDataIrrigated?.TotalNitrogen;
                var priceForecast = SelectedPlot?.PriceForecast;
                if (!priceForecast.IsNullOrEmpty())
                {
                    var priceForecastNextMonth = priceForecast.ElementAt(0)?.Price;
                    CurrentPlotPriceForecast = priceForecastNextMonth == null ? "-" : priceForecastNextMonth.ToString();
                }

                CurrentPlotCost = cost == null ? "-" : cost.ToString();
                CurrentPlotYield = yield == null ? "-" : yield.ToString();
                CurrentPlotProfit = profit == null ? "-" : profit.ToString();
                CurrentPlotIncome = income == null ? "-" : income.ToString();
                CurrentPlotPotentialYield = potentialYield == null ? "-" : potentialYield.ToString();
                CurrentPlotNitrogen = nitrogenNeeded == null ? "-" : nitrogenNeeded.ToString();

                MapPage.UpdateImages();
            } 
        }

        public string CurrentPlotPriceForecast
        {
            get => _currentPlotPriceForecast;
            set => SetProperty(ref _currentPlotPriceForecast, value);
        }

        public string CurrentPlotNitrogen
        {
            get => _currentPlotNitrogen;
            set => SetProperty(ref _currentPlotNitrogen, value);
        }

        public string CurrentPlotPotentialYield
        {
            get => _currentPlotPotentialYield;
            set => SetProperty(ref _currentPlotPotentialYield, value);
        }

        public async void LoadPlotData(Core.Entities.Plot plot)
        {
            bool updatedPlot = false;
            if (plot.BemData == null)
            {
                plot.BemData = await BemDataDownloadHelper.LoadBEMData(plot.Position.Latitude,
                    plot.Position.Longitude, plot.CropType);
                updatedPlot = true;
            }
            if (plot.WeatherForecast == null)
            {
                plot.WeatherForecast = await WeatherForecast.Download(plot.Position.Latitude,plot.Position.Longitude);
                updatedPlot = true;
            }

            if (plot.CiatData == null)
            {
                plot.CiatData = await CiatDownloadHelper.LoadData(plot.Position, "Maize");
                updatedPlot = true;
            }

            if (plot.PriceForecast == null)
            {
                // if (plot.CropType == CropType.Corn)
                // {
                    plot.PriceForecast = await PriceForecast.FromEmbeddedResource();
                    updatedPlot = true;
                // }
            }

            if (updatedPlot) await AppDataService.UpdatePlotAsync(plot);
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
                if (value == null) return;
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
                    EndDelineation();
                });

        public DelegateCommand DelineationCancel =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation = new List<Position>();
                    CurrentMapTask = MapTask.Default;
                    EndDelineation();
                });

        public DelegateCommand DelineationUndo =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation.RemoveAt(CurrentDelineation.Count - 1);
                    RemoveLastDelineationPoint();
                });


        //public void RemoveLastDelineationPoint()
        //{
        //    if (DelineationPins.Count > 0)
        //    {
        //        var position = DelineationPins.Count - 1;
        //        DelineationPins.RemoveAt(position);
        //        this.map.Pins.RemoveAt(position);
        //        this.DelineationPolygon.Positions.RemoveAt(position);
        //    }

        //    this.map.Polygons.Clear();
        //    if (DelineationPolygon.Positions.Count > 2)
        //    {
        //        this.map.Polygons.Add(DelineationPolygon);

        //    }
        //}

        public bool DelineationUIIsVisible
        {
            get => delineationUIIsVisible;
            set => SetProperty(ref delineationUIIsVisible, value);
        }

        public MapSpan Region
        {
            get => _region;
            set => SetProperty(ref _region, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ListenForLocation = false;
            base.OnNavigatedFrom(parameters);
        }

        public void RemoveHubsContact()
        {
            foreach (var pin in Pins.ToList().Where(x => x.Tag is HubFeature)) Pins.Remove(pin);
        }

        public void RemoveInvestigationPlatforms()
        {
            foreach (var pin in Pins.ToList().Where(x => x.Tag is IPFeature)) Pins.Remove(pin);
        }


        public void RemoveMachineryPoints()
        {
            foreach (var pin in Pins.ToList().Where(x => x.Tag is MPFeature)) Pins.Remove(pin);
        }

        private async Task PlotsSelectionChanged(bool b)
        {
            if (b)
                await AddPlots();
            else
                RemovePlots();
            await Task.CompletedTask;
        }

        private void RemovePlots()
        {
            foreach (var pin in Pins.ToList().Where(x => x.Tag is Core.Entities.Plot)) Pins.Remove(pin);
        }

        private async Task HubContactsSelectionChanged(bool b)
        {
            if (b)
                await RenderHubsContact();
            else
                RemoveHubsContact();
            await Task.CompletedTask;
        }

        private async Task MachineryPointsSelectionChanged(bool b)
        {
            if (b)
                await RenderMachineryPoints();
            else
                RemoveMachineryPoints();
            await Task.CompletedTask;
        }

        private async Task InvestigationPlatformsSelectionChanged(bool b)
        {
            if (b)
                await RenderInvestigationPlatforms();
            else
                RemoveInvestigationPlatforms();
            await Task.CompletedTask;
        }

        private void EndDelineation()
        {
            RemoveDelineationPins();

            RemoveDelineationPolygon();
        }

        private void RemoveDelineationPins()
        {
            foreach (var pin in Pins.ToList().Where(x => x.Tag is Position))
            {
                Pins.Remove(pin);
            }
        }

        private void RemoveDelineationPolygon()
        {
            if (CurrentPolygon != null) Polygons.Remove(CurrentPolygon);
            foreach (var polygon in Polygons.ToList().Where(x => x.Tag is Position))
            {
                Polygons.Remove(polygon);
            }
        }

        public Pin CurrentPin { get; set; }

        public Polygon CurrentPolygon { get; set; }

        private void RemoveLastDelineationPoint()
        {
            if (CurrentPin != null) Pins.Remove(CurrentPin);
            RenderDelineationPolygon();
        }

        private async Task PageAppearing()
        {
            
            
            Profiler.Start(Constants.MapData);
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading map data..."))
            {
                await LoadMapData();
            }

            Profiler.Stop(Constants.MapData);


            Profiler.Start(Constants.UserLocation);
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Getting user location..."))
            {
                if (Preferences.ContainsKey(Constants.Lat) && Preferences.ContainsKey(Constants.Lng))
                {
                    var lat = Preferences.Get(Constants.Lat, 0.0);
                    var lng = Preferences.Get(Constants.Lng, 0.0);
                    if (lat > 0 && lng > 0)
                    {
                        Region = MapSpan.FromCenterAndRadius(
                            new Xamarin.Forms.GoogleMaps.Position(lat, lng),
                            Distance.FromKilometers(2));
                    }
                    else
                    {
                        await EnableUserLocation();
                    }
                }
                else
                {
                    await EnableUserLocation();
                }
            }

            Profiler.Stop(Constants.UserLocation);


            Profiler.Start(Constants.Plots);
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading plots..."))
            {
                await LoadPlots();
            }

            Profiler.Stop(Constants.Plots);
        }


        public async Task RenderHubsContact()
        {
            if (HubsContact == null) return;
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading hub contact..."))
            {
                foreach (var hubContact in HubsContact.Features)
                {
                    var pin = new Pin
                    {
                        Position = new Xamarin.Forms.GoogleMaps.Position(
                            hubContact.Geometry.Coordinates[1],
                            hubContact.Geometry.Coordinates[0]),
                        Tag = hubContact,
                        Label = hubContact.Properties.Hub,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color) PrismApplicationBase.Current.Resources["SecondaryOrange"])
                    };
                    Pins.Add(pin);
                }
            }
        }

        public async Task RenderInvestigationPlatforms()
        {
            if (InvestigationPlatforms == null) return;

            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading investigation platforms..."))
            {
                foreach (var investigationPlatform in InvestigationPlatforms.Features)
                {
                    var pin = new Pin
                    {
                        Position = new Xamarin.Forms.GoogleMaps.Position(
                            investigationPlatform.Geometry.Coordinates[1],
                            investigationPlatform.Geometry.Coordinates[0]),
                        Tag = investigationPlatform,
                        Label = investigationPlatform.Properties.Abrviacion,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color) PrismApplicationBase.Current.Resources["SecondaryGreenBrown"])
                    };
                    Pins.Add(pin);
                }
            }
        }

        public async Task RenderMachineryPoints()
        {
            if (MachineryPoints == null) return;

            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading machinery points..."))
            {
                foreach (var machineryPoint in MachineryPoints.Features)
                {
                    var pin = new Pin
                    {
                        Position = new Xamarin.Forms.GoogleMaps.Position(
                            machineryPoint.Geometry.Coordinates[1], machineryPoint.Geometry.Coordinates[0]),
                        Tag = machineryPoint,
                        Label = machineryPoint.Properties.Localidad,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color) PrismApplicationBase.Current.Resources["SecondaryDarkGreen"])
                    };
                    Pins.Add(pin);
                }
            }
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

                if (LocationPermissionGiven) await GetUserLocation();
            }
        }

        private async Task GetUserLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                WeatherLocation = location;
                CurrentPosition = Position.FromLocation(location);
                if (CurrentMapTask == MapTask.CreatePlotByGPS) AddPlotPosition = CurrentPosition;
                Region = MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.GoogleMaps.Position(location.Latitude, location.Longitude),
                    Distance.FromKilometers(2));
                LocationEnabled = true;
            }
        }

        private async Task LoadMapData()
        {
            HubsContact = await HubsContact.FromEmbeddedResource();
            //RenderHubsContact();
            MachineryPoints = await MachineryPoints.FromEmbeddedResource();
            //RenderMachineryPoints();
            InvestigationPlatforms = await InvestigationPlatforms.FromEmbeddedResource();
            //RenderInvestigationPlatforms();
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
            await AddPlots();
            foreach (var plot in plots.Where(plot => plot.BemData == null))
            {
                if (plot.Position == null) continue;
                plot.BemData = await BemDataDownloadHelper.LoadBEMData(plot.Position.Latitude, plot.Position.Longitude);
                await AppDataService.UpdatePlotAsync(plot);
            }
        }

        private async Task AddPlots()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Rendering plots..."))
            {
                Plots = await AppDataService.GetAllPlotsAsync();
                var plots = Plots.ToList();
                foreach (var pin in from plot in plots
                    where plot.Position != null
                    select new Pin
                    {
                        Position = plot.Position.ForMap(),
                        Label = plot.Name ?? "",
                        Tag = plot,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color) PrismApplicationBase.Current.Resources["PrimaryGreen"])
                    })

                    Pins.Add(pin);
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
                        //MapPage.ZoomToPosition(selectedPlot.Position.ForMap());

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

        public void SetView(MapPage mapPage)
        {
            this.MapPage = mapPage;
        }

        public MapPage MapPage { get; set; }
    }
}