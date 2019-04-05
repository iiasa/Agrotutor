using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Agrotutor.Core;
using Agrotutor.Core.Camera;
using Agrotutor.Core.Cimmyt.HubsContact;
using Agrotutor.Core.Cimmyt.InvestigationPlatforms;
using Agrotutor.Core.Cimmyt.MachineryPoints;
using Agrotutor.Core.Components;
using Agrotutor.Core.Entities;
using Agrotutor.Core.Persistence;
using Agrotutor.Core.Tile;
using Agrotutor.Modules.Benchmarking;
using Agrotutor.Modules.Benchmarking.ViewModels;
using Agrotutor.Modules.Calendar.ViewModels;
using Agrotutor.Modules.Ciat;
using Agrotutor.Modules.Ciat.ViewModels;
using Agrotutor.Modules.Map.Types;
using Agrotutor.Modules.Map.Views;
using Agrotutor.Modules.Plot.ViewModels;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.Modules.Weather;
using Agrotutor.Modules.Weather.Types;
using Microsoft.Extensions.Localization;
using Plugin.Permissions;
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
using MapsPosition = Xamarin.Forms.GoogleMaps.Position;
using NavigationMode = Xamarin.Essentials.NavigationMode;
using Position = Agrotutor.Core.Entities.Position;
using Agrotutor.Modules.Plot.Views;
using Agrotutor.Modules.PriceForecasting.ViewModels;
using Agrotutor.Modules.Weather.ViewModels;
using Agrotutor.Modules.Weather.Views;
using Flurl.Http;

namespace Agrotutor.Modules.Map.ViewModels
{
    using MapsApp = Xamarin.Essentials.Map;
    using HubFeature = Feature;
    using IPFeature = Core.Cimmyt.InvestigationPlatforms.Feature;
    using MPFeature = Core.Cimmyt.MachineryPoints.Feature;
    using Plugin.DownloadManager;
    using Plugin.DownloadManager.Abstractions;
    using Newtonsoft.Json;

    public class MapPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly ICameraService _cameraService;
        public IDocumentViewer DocumentViewer;
        private bool _addParcelIsVisible;

        private Position _addPlotPosition;
        private CameraPosition _cameraPositionUpdated;

        private HubFeature _currentHubContact;

        private IPFeature _currentInvestigationPlatform;

        private MPFeature _currentMachineryPoint;

        private MapTask _currentMapTask;

        private string _currentMapTaskHint;

        private bool _currentMapTaskHintIsVisible;
        private string _currentPlotCost;
        private string _currentPlotIncome;
        private string _currentPlotNitrogen;
        private string _currentPlotPotentialYield;
        private string _currentPlotPriceForecast;
        private string _currentPlotProfit;
        private string _currentPlotYield;
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
        private bool _showTileLayer;

        private bool _showWeatherWidget;

        private Position currentPosition;

        private WeatherForecast currentWeather;

        private bool delineationUIIsVisible;

        private bool dimBackground;

        private bool gpsLocationUIIsVisible;

        private bool hubsContactUIIsVisible;

        private bool investigationPlatformUIIsVisible;

        private bool locationPermissionGiven;

        private bool machineryPointUIIsVisible;

        private bool optionsIsVisible;

        private bool plotDetailIsVisible;

        private Core.Entities.Plot selectedPlot;

        private bool selectLocationUIIsVisible;

        private Location weatherLocation;

        private string _selectedPlotDate;
        private string _selectedPlotIrrigation;
        private string _selectedPlotMaturity;
        private string _selectedPlotClimate;
        private string _currentPlotWeatherIcon;
        private string _currentPlotGdd;
        private bool _showSatelliteLayer;
        private CropType _cropType;
        private string _cacheButtonText;
        private string _downloadStatusImage;

        private bool _isOfflineBasemapLayerEnabled;
        private bool _isDownloadButtonEnabled;
        private bool showPlotDetailInformation;
        private bool additionalDataLoaded;
        private bool showAdditionalDataButton;
        private bool showSavePlotButton;
        private bool showDeletePlotButton;
        private double selectedPlotActivityCost;

        private readonly double tileSize = 130;
        private string tileName = "Guanajuato";
        private string _lastUploadDateString;

        public MapPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            ICameraService cameraService,
            IDocumentViewer documentViewer,
            IStringLocalizer<MapPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            IsDownloadButtonEnabled = true;
            Title = "Map";
            _cameraService = cameraService;
            DocumentViewer = documentViewer;
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
            DownloadDeleteCommand=new DelegateCommand(async () =>
            {
                await DownloadTilesAsync();
            });
            CheckDownloadStatus();
        }



        public string CacheButtonText
        {
            get => _cacheButtonText;
            set
            {
                SetProperty(ref _cacheButtonText, value);
            }
        }
        public string DownloadStatusImage
        {
            get => _downloadStatusImage;
            set
            {
                SetProperty(ref _downloadStatusImage, value);
            }
        }
        private void CheckDownloadStatus()
        {
            if (FileManager.CacheFileExists(Constants.DownloadTileUrl))
            {
                DownloadStatusImage = "ic_delete";
                IsOfflineBasemapLayerEnabled = true;
            }
            else
            {
                DownloadStatusImage = "ic_download";
                IsOfflineBasemapLayerEnabled = false;
            }
        }

        private async Task UpLoadDataAsync()
        {
            try
            {
                TimeSpan dateRes;
                if (_lastUploadDateString != null)
                {
                    DateTime uploadDateTime=DateTime.Parse(_lastUploadDateString);
                     dateRes = DateTime.UtcNow.Subtract(uploadDateTime);
                }

                if (_lastUploadDateString == null||dateRes.Days >=Constants.UploadPlotDataPeriod)
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        HttpClient client = new HttpClient();
                        if (Plots.ToList().Count > 0)
                        {
                            foreach (var plot in Plots)
                            {
                                var jsonObj = JsonConvert.SerializeObject(plot);
                                var res = await client.PostAsync(Constants.UploadDataUrl,
                                    new StringContent(jsonObj, Encoding.UTF8, "application/json"));
                                if (!res.IsSuccessStatusCode)
                                {
                                    //todo:show notification or log the result
                                    return;
                                }

                            }

                            LastUploadDateString = DateTime.UtcNow.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
        
            }


        }

        public async Task DownloadTilesAsync( )
        {
      
            if (FileManager.CacheFileExists(Constants.DownloadTileUrl))
            {
                var deleteOfflineMapMessage =
                    string.Format(StringLocalizer.GetString("delete_offline_map_message"), tileSize);
                var confirm = await MaterialDialog.Instance.ConfirmAsync(deleteOfflineMapMessage, null, StringLocalizer.GetString("download_offline_map_yes"),
                    StringLocalizer.GetString("download_offline_map_cancel"));
                if (confirm.Value)
                {
                    // remove files
                    FileManager.DeleteOfflineCache(Constants.DownloadTileUrl);
                    //  LayerService.UpdateIsChecked( FileManager.GetCacheFilePath(downloadUrl));
                    DownloadStatusImage = "ic_download";
                    IsOfflineBasemapLayerEnabled = false;
                    OfflineBasemapLayerVisible = false;
                }
   
            }
            else
            {
             
                var downloadOfflineMapMessage =
                    string.Format(StringLocalizer.GetString("download_offline_map_message"), tileSize);
                var confirm = await MaterialDialog.Instance.ConfirmAsync(downloadOfflineMapMessage, null, StringLocalizer.GetString("download_offline_map_yes"),
                    StringLocalizer.GetString("download_offline_map_cancel"));
                if (confirm.Value)
                {
             
               await TaskDownloadTiles();
                    
                }
            
            }
        }
        private async Task TaskDownloadTiles()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var downloadOfflineMapInProgress = string.Format(StringLocalizer.GetString("download_offline_map_in_progress"), tileName);
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(downloadOfflineMapInProgress);
                IsDownloadButtonEnabled = false;
                IDownloadFile file = CrossDownloadManager.Current.CreateDownloadFile(Constants.DownloadTileUrl);

                CrossDownloadManager.Current.Start(file);
                file.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Status")
                    {
                        switch (((IDownloadFile)sender).Status)
                        {
                            case DownloadFileStatus.COMPLETED:
                                DownloadStatusImage = "ic_delete";
                                IsDownloadButtonEnabled = true;
                                IsOfflineBasemapLayerEnabled = true;
                                LayerService.UpdateIsChecked(FileManager.GetCacheFilePath(Constants.DownloadTileUrl));
                                System.Console.WriteLine("Downloading finished. " + file.DestinationPathName);
                                loadingDialog.DismissAsync();
                                break;

                            case DownloadFileStatus.FAILED:
                            case DownloadFileStatus.CANCELED:
                                System.Console.WriteLine("Downloading error. ");
                                break;
                        }
                    }
                };

            }
        }
        public bool OfflineBasemapLayerVisible
        {
            get => _offlineBasemapLayerVisible;
            set
            {
                SetProperty(ref _offlineBasemapLayerVisible, value);
                Preferences.Set(Constants.OfflineBasemapLayerVisiblePreference, value);
                LayerService.UpdateIsChecked(FileManager.GetCacheFilePath(Constants.DownloadTileUrl));
            }
        }
        public bool IsOfflineBasemapLayerEnabled
        {
            get => _isOfflineBasemapLayerEnabled;
            set
            {
                SetProperty(ref _isOfflineBasemapLayerEnabled, value);
            }
        }
        public bool IsDownloadButtonEnabled
        {
            get => _isDownloadButtonEnabled;
            set
            {
                SetProperty(ref _isDownloadButtonEnabled, value);
            }
        }
        public bool ShowSatelliteTileLayer
        {
            get => _showSatelliteLayer;
            set
            {
                SetProperty(ref _showSatelliteLayer, value);
                Preferences.Set(Constants.ShowSatelliteTileLayerVisiblePreference, value);
            }
        }
        public string LastUploadDateString
        {
            get => _lastUploadDateString;
            set
            {
                SetProperty(ref _lastUploadDateString, value);
                Preferences.Set(Constants.LastUploadDatePreference, value);
            }
        }
        public ObservableCollection<Polygon> Polygons
        {
            get => _polygons;
            set => SetProperty(ref _polygons, value);
        }

        public CropType CropType
        {
            get => _cropType;
            set => SetProperty(ref _cropType, value);
        }

        public bool AdditionalDataLoaded
        {
            get => additionalDataLoaded;
            set
            {
                SetProperty(ref additionalDataLoaded, value);
                ShowAdditionalDataButton = !value;
            }
        }

        public bool ShowAdditionalDataButton
        {
            get => showAdditionalDataButton;
            set => SetProperty(ref showAdditionalDataButton, value);
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

        public double SelectedPlotActivityCost { get => selectedPlotActivityCost; set => SetProperty(ref selectedPlotActivityCost, value); }

        public DelegateCommand DownloadDeleteCommand { get; set; }
        public DelegateCommand ShowCurrentPlotCost => new DelegateCommand(async () =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Cost==null||SelectedPlot.BemData.Cost.Count==0)
            {
                await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("cost_data_not_available"));
                return;
            }

            await NavigationService.NavigateAsync("ViewCostPage", new NavigationParameters
            {
                {ViewCostPageViewModel.CostsParameterName, SelectedPlot.BemData.Cost}
            });
        });

        public DelegateCommand ShowCurrentPlotIncome => new DelegateCommand(async () =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Income==null||SelectedPlot.BemData.Income.Count==0)
            {
                await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("income_data_not_available"));
                return;
            }

            await NavigationService.NavigateAsync("ViewIncomePage", new NavigationParameters
            {
                {ViewIncomePageViewModel.IncomesParameterName, SelectedPlot.BemData.Income}
            });
        });

        public DelegateCommand ShowCurrentPlotProfit => new DelegateCommand(async () =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Profit==null|| SelectedPlot.BemData.Profit.Count==0)
            {
                await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("profit_data_not_available"));
                return;
            }

            await NavigationService.NavigateAsync("ViewProfitPage", new NavigationParameters
            {
                {ViewProfitPageViewModel.ProfitsParameterName, SelectedPlot.BemData.Profit}
            });
        });

        public DelegateCommand ShowCurrentPlotYield => new DelegateCommand(async () =>
        {
            if (SelectedPlot?.BemData == null || SelectedPlot.BemData.Yield==null||SelectedPlot.BemData.Yield.Count==0)
            {
                await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("yield_data_not_available"));
                return;
            }

            await NavigationService.NavigateAsync("ViewYieldPage", new NavigationParameters
            {
                {ViewYieldPageViewModel.YieldsParameterName, SelectedPlot.BemData.Yield}
            });
        });

        public DelegateCommand ProvideFeedback => new DelegateCommand(() =>
            {
                Device.OpenUri(new Uri($"mailto:{Constants.FeedbackEmail}?subject=AgroTutor-Feedback"));
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

        public bool ShowSavePlotButton { get => showSavePlotButton; set => SetProperty(ref showSavePlotButton, value); }
        public bool ShowDeletePlotButton { get => showDeletePlotButton; set => SetProperty(ref showDeletePlotButton, value); }

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



        public DelegateCommand AddActivityToSelectedPlot =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters
                {
                    {"Plot", SelectedPlot}
                };
                NavigationService.NavigateAsync("ActivityPage", param);
            });

        public DelegateCommand ShowLayerSwitcher =>
            new DelegateCommand(() => { LayerSwitcherIsVisible = true; });

        public DelegateCommand PageAppearingCommand =>
            new DelegateCommand(async () => await PageAppearing());


        public DelegateCommand DeleteCommand =>
            new DelegateCommand(async () => await DeletePlot());

        private async Task DeletePlot()
        {
            try
            {
                var confirm = await MaterialDialog.Instance.ConfirmAsync(StringLocalizer.GetString("delete_plot_confirm_message"), StringLocalizer.GetString("delete_plot_confirm_button"));
                if (confirm.Value)
                {
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("delete_plot_in_progress")))
                    {
                        await AppDataService.RemovePlotAsync(SelectedPlot);
                        RemovePlots();
                        PlotDetailIsVisible = false;
                        DimBackground = false;
                        await LoadPlots();
                    }
                }
            }
            catch (Exception e)
            {
                await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("delete_plot_failed"));
            }

        }

        public DelegateCommand AcceptGPSLocation => new DelegateCommand(() =>
        {
            switch (CurrentMapTask)
            {
                case MapTask.CreatePlotByGPS:
                    CreatePlot();
                    break;
                case MapTask.GetLocationForPlanner:
                    NavigateToPlanner();
                    break;
            }
        });

        public DelegateCommand AcceptSelectLocation => new DelegateCommand(() =>
        {
            switch (CurrentMapTask)
            {
                case MapTask.CreatePlotBySelection:
                    CreatePlot();
                    break;
                case MapTask.SelectLocationForPlanner:
                    NavigateToPlanner();
                    break;
            }
        });

        public IAppDataService AppDataService { get; }

        public DelegateCommand AddParcelChooseLocation =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                CurrentMapTask = MapTask.CreatePlotBySelection;
            });

        public DelegateCommand ClickChooseLocationPlanner =>
            new DelegateCommand(() =>
            {
                DimBackground = false;
                CurrentMapTask = MapTask.SelectLocationForPlanner;
            });

        public DelegateCommand AddParcelGetLocation =>
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
                    try
                    {
                        Preferences.Set(Constants.Lat, args.Point.Latitude);
                        Preferences.Set(Constants.Lng, args.Point.Longitude);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    switch (CurrentMapTask)
                    {
                        case MapTask.CreatePlotBySelection:
                        case MapTask.SelectLocationForPlanner:
                            RemoveTempPin();
                            AddTempPin(args.Point);
                            AddPlotPosition = Position.From(args.Point);
                            break;

                        case MapTask.DelineationNotEnoughPoints:
                        case MapTask.DelineationEnoughPoints:
                            if (CurrentDelineation == null) CurrentDelineation = new List<DelineationPosition>();

                            var pos = Position.From(args.Point);
                            CurrentDelineation.Add(new DelineationPosition{Position= pos });
                            var pin = new Pin
                            {
                                Position = Position.From(args.Point).ForMap(),
                                Label = StringLocalizer.GetString("delineation_pin_label"),
                                Tag = pos
                            };
                            CurrentPin = pin;
                            Pins.Add(pin);
                            RenderDelineationPolygon();
                            break;
                    }
                });


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
            new DelegateCommand(() => { NavigationService.NavigateAsync("WelcomePage"); });

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

        public DelegateCommand DownloadAdditionalPlotData =>
            new DelegateCommand(() =>
            {
                LoadPlotAdditionalData(SelectedPlot);
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
                    await NavigationService.NavigateAsync("CalendarPage", navigationParameters);
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
                    await NavigationService.NavigateAsync("CalendarPage", navigationParameters);
                });

        public DelegateCommand ShowOptions => new DelegateCommand(() => { OptionsIsVisible = true; });

        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.ShowSettingsUI);

        public DelegateCommand NavigateToPlotWeather =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters
                {
                    {
                        WeatherPageViewModel.LocationParameterName,
                        new Location(SelectedPlot.Position.Latitude, SelectedPlot.Position.Longitude)
                    }
                };
                if (SelectedPlot?.WeatherForecast != null)
                {
                    param.Add(WeatherPageViewModel.ForecastParameterName, SelectedPlot.WeatherForecast);
                }
                if (SelectedPlot?.WeatherHistory != null)
                {
                    param.Add(WeatherPageViewModel.HistoryParameterName, SelectedPlot.WeatherHistory);
                }

                NavigationService.NavigateAsync("WeatherPage", param);

            });

        public DelegateCommand ShowWeather =>
            new DelegateCommand(
                async () =>
                {
                    var param = new NavigationParameters();
                    if (CurrentWeather != null) param.Add(WeatherPageViewModel.ForecastParameterName, CurrentWeather);

                    if (WeatherLocation != null)
                    {
                        param.Add(WeatherPageViewModel.LocationParameterName, WeatherLocation);
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(
                            StringLocalizer.GetString("weather_location_missing_title"),
                            StringLocalizer.GetString("weather_location_missing_message"),
                            StringLocalizer.GetString("weather_location_missing_ok"));
                        return;
                    }

                    await NavigationService.NavigateAsync("WeatherPage", param);
                });

        public DelegateCommand StartPlanner => new DelegateCommand(() => { PlannerUIIsVisible = true; });

        public DelegateCommand<string> WriteEmail =>
            new DelegateCommand<string>(
                emailAddress =>
                {
                    Device.OpenUri(new Uri($"mailto:{emailAddress}"));
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

        public List<DelineationPosition> CurrentDelineation { get; set; }

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
                if (today != null) text += $" | {StringLocalizer.GetString("rain")}: {today.precipitationProbability} %";
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

                if (value.IsTemporaryPlot)
                {
                    ShowSavePlotButton = true;
                    ShowDeletePlotButton = false;
                }
                else
                {
                    if (value.Activities != null) SelectedPlotActivityCost = value.Activities.Sum(x => x.Cost);
                    ShowSavePlotButton = false;
                    ShowDeletePlotButton = true;
                }
                LoadPlotData(value);
            }
        }

        private async void UpdateInfo()
        {
            if (SelectedPlot == null) return;
            ShowPlotDetailInformation = !SelectedPlot.IsTemporaryPlot;

            SelectedPlotDate = SelectedPlot.Activities?.FirstOrDefault(x => x.ActivityType == ActivityType.Sowing)?.Date
                .ToShortDateString();
            SelectedPlotIrrigation =
                (SelectedPlot.Activities?.Any(x => x.ActivityType == ActivityType.Irrigation) != null)
                    ? StringLocalizer.GetString("irrigated")
                    : StringLocalizer.GetString("rainfed");

            SelectedPlotMaturity = Helper.GetMaturityTypeString(SelectedPlot.MaturityType);
            SelectedPlotClimate = Helper.GetClimateTypeString(SelectedPlot.ClimateType);
            CropType = SelectedPlot.CropType;
            var gdd = SelectedPlot.WeatherHistory?.Gdd.Series.Sum(x => x.Value);
            var weatherIcon = SelectedPlot.WeatherForecast?.Location?.HourlySummaries?.HourlySummary?.FirstOrDefault()
                ?.WxIcon;
            var cost = SelectedPlot.BemData?.AverageCost;
            var yield = SelectedPlot.BemData?.AverageYield;
            var profit = SelectedPlot.BemData?.AverageProfit;
            var income = SelectedPlot.BemData?.AverageIncome;
            var potentialYield = SelectedPlot.CiatData?.CiatDataIrrigated?.YieldMax;
            var nitrogenNeeded = SelectedPlot.CiatData?.CiatDataIrrigated?.TotalNitrogen;
            var priceForecast = await PriceForecast.FromEmbeddedResource();
            var priceForecastNextMonth = priceForecast.First().Price;
            CurrentPlotPriceForecast = priceForecastNextMonth == null ? "-" : priceForecastNextMonth.ToString();

            CurrentPlotCost = cost == null ? "-" : Math.Round((decimal)cost).ToString();
            CurrentPlotYield = yield == null ? "-" : Math.Round((decimal)yield).ToString();
            CurrentPlotProfit = profit == null ? "-" : Math.Round((decimal)profit).ToString();
            CurrentPlotIncome = income == null ? "-" : Math.Round((decimal)income).ToString();
            CurrentPlotPotentialYield = potentialYield == null ? "-" : potentialYield.ToString();
            CurrentPlotNitrogen = nitrogenNeeded == null ? "-" : nitrogenNeeded.ToString();
            CurrentPlotGdd = gdd == null ? "-" : gdd.ToString();
            CurrentPlotWeatherIcon = weatherIcon ?? "";

            await MapPage.UpdateImages();
        }

        public string CurrentPlotWeatherIcon
        {
            get => _currentPlotWeatherIcon;
            set => SetProperty(ref _currentPlotWeatherIcon, value);
        }

        public string CurrentPlotGdd
        {
            get => _currentPlotGdd;
            set => SetProperty(ref _currentPlotGdd, value);
        }

        public string SelectedPlotClimate
        {
            get => _selectedPlotClimate;
            set => SetProperty(ref _selectedPlotClimate, value);
        }

        public string SelectedPlotMaturity
        {
            get => _selectedPlotMaturity;
            set => SetProperty(ref _selectedPlotMaturity, value);
        }

        public string SelectedPlotIrrigation
        {
            get => _selectedPlotIrrigation;
            set => SetProperty(ref _selectedPlotIrrigation, value);
        }
        public bool ShowPlotDetailInformation
        {
            get => showPlotDetailInformation;
            set => SetProperty(ref showPlotDetailInformation, value);
        }

        public string SelectedPlotDate
        {
            get => _selectedPlotDate;
            set => SetProperty(ref _selectedPlotDate, value);
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
            }
        }

        public DelegateCommand DelineationAccept =>
            new DelegateCommand(
                () =>
                {
                    SelectedPlot.Delineation = CurrentDelineation;
                    AppDataService.UpdatePlotAsync(SelectedPlot);
                    CurrentDelineation = new List<DelineationPosition>();
                    CurrentMapTask = MapTask.Default;
                    EndDelineation();
                });

        public DelegateCommand DelineationCancel =>
            new DelegateCommand(
                () =>
                {
                    CurrentDelineation = new List<DelineationPosition>();
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

        public DelegateCommand NavigateToPotentialYield =>
            new DelegateCommand(async () =>
            {
                var param = new NavigationParameters
                {
                    {PotentialYieldPageViewModel.DataParameterName, SelectedPlot?.CiatData}
                };

                await NavigationService.NavigateAsync("PotentialYieldPage", param);
            });

        public DelegateCommand NavigateToCiat =>
            new DelegateCommand(async () =>
            {
                var param = new NavigationParameters
                {
                    {CiatPageViewModel.PARAMETER_NAME_CIAT_DATA, SelectedPlot.CiatData}
                };
                await NavigationService.NavigateAsync("CiatPage", param);
            });

        public DelegateCommand NavigateToPriceForecast =>
            new DelegateCommand(async () =>
            {
                var param = new NavigationParameters
                {
                    {PriceForecastPageViewModel.PriceForecastParameterName, SelectedPlot.PriceForecast}
                };
                await NavigationService.NavigateAsync("PriceForecastPage", param);
            });

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

        public Pin CurrentPin { get; set; }

        public Polygon CurrentPolygon { get; set; }

        public MapPage MapPage { get; set; }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ListenForLocation = false;
            base.OnNavigatedFrom(parameters);
        }

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
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("rendering_delineations")))
            {
                var plots = await AppDataService.GetAllPlotsAsync();

                foreach (var plot in plots)
                {
                    if (plot.Delineation == null) continue;

                    var positions = plot.Delineation;
                    if (positions != null && positions.Count > 3)
                    {
                        var polygon = new Polygon
                        {
                            Tag = plot.Position
                        };
                        foreach (var position in positions)
                            polygon.Positions.Add(position.Position.ForMap());
                        Polygons.Add(polygon);
                    }
                }
            }
        }

        private void RenderDelineationPolygon()
        {
            if (CurrentDelineation.Count < 3) return;
            RemoveDelineationPolygon();
            var polygon = new Polygon { FillColor = Color.Transparent };
            foreach (var position in CurrentDelineation) polygon.Positions.Add(position.Position.ForMap());
            CurrentPolygon = polygon;
            Polygons.Add(polygon);
        }

        public async void LoadPlotData(Core.Entities.Plot plot)
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync("Getting plot data..."))
            {
                bool updatedPlot = false;
                if (plot.BemData == null)
                {
                    plot.BemData = await BemDataDownloadHelper.LoadBEMData(plot.Position.Latitude,
                        plot.Position.Longitude, plot.CropType);
                    updatedPlot = true;
                }

                if (updatedPlot && !plot.IsTemporaryPlot) await AppDataService.UpdatePlotAsync(plot);

                if (plot.IsTemporaryPlot)
                {
                    UpdateInfo();
                }
                else
                {
                    LoadPlotAdditionalData(plot);
                }
            }
        }

        public async void LoadPlotAdditionalData(Core.Entities.Plot plot)
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync("Getting plot data..."))
            {
                bool updatedPlot = false;

                if (plot.WeatherForecast == null)
                {
                    plot.WeatherForecast = await WeatherForecast.Download(plot.Position.Latitude, plot.Position.Longitude);
                    updatedPlot = true;
                }

                if (plot.WeatherHistory == null)
                {
                    plot.WeatherHistory = await WeatherHistory.Download(plot.Position.Latitude, plot.Position.Longitude);
                    updatedPlot = true;
                }

                if (plot.CiatData == null)
                {
                    plot.CiatData = await CiatDownloadHelper.LoadData(plot.Position, "Maiz");
                    updatedPlot = true;
                }

                if (plot.PriceForecast == null)
                {
                    plot.PriceForecast = await PriceForecast.FromEmbeddedResource();
                    updatedPlot = true;
                }

                if (updatedPlot && !plot.IsTemporaryPlot) await AppDataService.UpdatePlotAsync(plot);
                AdditionalDataLoaded = true;
                UpdateInfo();
            }
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
            foreach (var pin in Pins.ToList().Where(x => x.Tag is Position)) Pins.Remove(pin);
        }

        private void RemoveDelineationPolygon()
        {
            if (CurrentPolygon != null) Polygons.Remove(CurrentPolygon);
            foreach (var polygon in Polygons.ToList().Where(x => x.Tag is Position)) Polygons.Remove(polygon);
        }

        private void RemoveLastDelineationPoint()
        {
            if (CurrentPin != null) Pins.Remove(CurrentPin);
            RenderDelineationPolygon();
        }

        public bool IsInitDone { get; set; }

        private async Task PageAppearing()
        {
            if (!IsInitDone)
            {
                RemoveTempPin();
                //var tasks = new List<Task>();
                var permissionsToRequest = new List<Permission>();
                var locationPermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (locationPermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Location);
                var cameraPermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (cameraPermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Camera);
                var storagePermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (storagePermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Storage);
                if (permissionsToRequest.Count > 0)
                    await CrossPermissions.Current.RequestPermissionsAsync(permissionsToRequest.ToArray());

                //tasks.Add(EnableUserLocation());
                await EnableUserLocation();

                //tasks.Add(LoadMapData());
                await LoadMapData();

                //tasks.Add(LoadPlots());
                await LoadPlots();
                await UpLoadDataAsync();
                //tasks.Add(RefreshWeatherData());
                await RefreshWeatherData();

                //await Task.WhenAll(tasks).ConfigureAwait(false);
                IsInitDone = true;
            }
        }


        public async Task RenderHubsContact()
        {
            if (HubsContact == null) return;
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading_hub_contact")))
            {
                foreach (var hubContact in HubsContact.Features)
                {
                    var pin = new Pin
                    {
                        Position = new MapsPosition(
                            hubContact.Geometry.Coordinates[1],
                            hubContact.Geometry.Coordinates[0]),
                        Tag = hubContact,
                        Label = hubContact.Properties.Hub,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color)PrismApplicationBase.Current.Resources["SecondaryOrange"])
                    };
                    Pins.Add(pin);
                }
            }
        }

        public async Task RenderInvestigationPlatforms()
        {
            if (InvestigationPlatforms == null) return;

            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading_investigation_platforms")))
            {
                foreach (var investigationPlatform in InvestigationPlatforms.Features)
                {
                    var pin = new Pin
                    {
                        Position = new MapsPosition(
                            investigationPlatform.Geometry.Coordinates[1],
                            investigationPlatform.Geometry.Coordinates[0]),
                        Tag = investigationPlatform,
                        Label = investigationPlatform.Properties.Abrviacion,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color)PrismApplicationBase.Current.Resources["SecondaryGreenBrown"])
                    };
                    Pins.Add(pin);
                }
            }
        }

        public async Task RenderMachineryPoints()
        {
            if (MachineryPoints == null) return;

            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading_machinery_points")))
            {
                foreach (var machineryPoint in MachineryPoints.Features)
                {
                    var pin = new Pin
                    {
                        Position = new MapsPosition(
                            machineryPoint.Geometry.Coordinates[1], machineryPoint.Geometry.Coordinates[0]),
                        Tag = machineryPoint,
                        Label = machineryPoint.Properties.Localidad,
                        Icon = BitmapDescriptorFactory.DefaultMarker(
                            (Color)PrismApplicationBase.Current.Resources["MachineryPoints"])
                    };
                    Pins.Add(pin);
                }
            }
        }

        private async void NavigateToPlanner()
        {
            if (AddPlotPosition == null)
            {
                await UserDialogs.Instance.AlertAsync(
                    new AlertConfig
                    {
                        Title = StringLocalizer.GetString("planner_no_position_title"),
                        Message = StringLocalizer.GetString("planner_no_position_message"),
                        OkText = StringLocalizer.GetString("planner_no_position_ok")
                    });
                return;
            }

            AdditionalDataLoaded = false;

            var plot = new Core.Entities.Plot();
            plot.Position = AddPlotPosition;

            if (PickerCropTypesSelectedIndex == -1)
            {
                plot.CropType = CropType.None;
            }
            else
            {
                plot.CropType = (CropType)(PickerCropTypesSelectedIndex + 1); // TODO: verify
            }

            plot.IsTemporaryPlot = true;
            Preferences.Set("TempPlot", JsonConvert.SerializeObject(plot));

            SelectedPlot = plot;
            ShowPlotInformation(plot);
        }

        private async void CreatePlot()
        {
            var confirmPlotCreation = await UserDialogs.Instance.ConfirmAsync(
                new ConfirmConfig
                {
                    Message = StringLocalizer.GetString("new_plot_prompt_message"),
                    OkText = StringLocalizer.GetString("new_plot_prompt_yes"),
                    CancelText = StringLocalizer.GetString("new_plot_prompt_cancel"),
                    Title = StringLocalizer.GetString("new_plot_prompt_title")
                });

            if (SelectedPlot != null && SelectedPlot.IsTemporaryPlot)
            {

                if (confirmPlotCreation)
                {
                    var navigationParams = new NavigationParameters
                    {
                        {
                            AddPlotPageViewModel.PlotParameterName,
                            SelectedPlot
                        }
                    };
                    await NavigationService.NavigateAsync("AddPlotPage", navigationParams);
                }

            }

            else
            {

                if (AddPlotPosition == null)
                {
                    await UserDialogs.Instance.AlertAsync(
                        new AlertConfig
                        {
                            Title = StringLocalizer.GetString("add_plot_no_position_title"),
                            Message = StringLocalizer.GetString("add_plot_no_position_message"),
                            OkText = StringLocalizer.GetString("add_plot_no_position_ok")
                        });
                    return;
                }

                if (confirmPlotCreation)
                {
                    var navigationParams = new NavigationParameters
                    {
                        {
                            AddPlotPageViewModel.PositionParameterName,
                            AddPlotPosition
                        }
                    };
                    await NavigationService.NavigateAsync("AddPlotPage", navigationParams);
                }
            }
        }

        private async Task EnableUserLocation()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("getting_location")))
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
        }

        private async Task GetUserLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                UpdateLocation(location);
            }
            else
            {
                location = await Geolocation.GetLocationAsync();
                UpdateLocation(location);
            }
        }

        private void UpdateLocation(Location location)
        {
            WeatherLocation = location;

            CurrentPosition = Position.FromLocation(location);
            if (CurrentMapTask == MapTask.CreatePlotByGPS) AddPlotPosition = CurrentPosition;

            var lat = Preferences.Get(Constants.Lat, 0.0);
            var lng = Preferences.Get(Constants.Lng, 0.0);
            if (Region == null)
            {
                if (!lat.Equals(0.0) && !lng.Equals(0.0))
                    Region = MapSpan.FromCenterAndRadius(
                        new MapsPosition(lat, lng),
                        Distance.FromKilometers(2));
                else
                    Region = MapSpan.FromCenterAndRadius(
                        new MapsPosition(location.Latitude, location.Longitude),
                        Distance.FromKilometers(2));
            }

            LocationEnabled = true;
        }

        private async Task LoadMapData()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading_data")))
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
                MachineryPointsLayerVisible = Preferences.Get(Constants.MachineryPointsLayerVisiblePreference, false);
                InvestigationPlatformsLayerVisible =
                Preferences.Get(Constants.InvestigationPlatformsLayerVisiblePreference, false);
                OfflineBasemapLayerVisible = Preferences.Get(Constants.OfflineBasemapLayerVisiblePreference, false);
                ShowSatelliteTileLayer = Preferences.Get(Constants.ShowSatelliteTileLayerVisiblePreference, true);
                LastUploadDateString = Preferences.Get(Constants.LastUploadDatePreference,null);
            }
        }

        private async Task LoadPlots()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading_plots")))
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
        }

        private void AddTempPin(MapsPosition pos)
        {
            Pins.Add(new Pin
            {
                Position = pos,
                Label = "",
                Tag = "temp"
            });
        }

        private void RemoveTempPin()
        {
            var toRemove = Pins.SingleOrDefault(x => x.Tag == "temp");
            if (toRemove != null) Pins.Remove(toRemove);
        }

        private async Task AddPlots()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("rendering_plots")))
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
                                            (Color)PrismApplicationBase.Current.Resources["PrimaryGreen"])
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

        private async Task RefreshWeatherData()
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("getting_weather_data")))
            {
                if (WeatherLocation == null) return;
                CurrentWeather = await WeatherForecast.Download(WeatherLocation.Latitude, WeatherLocation.Longitude)
                    .ConfigureAwait(true);
            }
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

                case MapTask.CreatePlotBySelection:
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
            MapPage = mapPage;
        }
    }
}