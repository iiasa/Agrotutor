namespace CimmytApp.Core.Plot.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms.GoogleMaps;
    using Position = Persistence.Entities.Position;

    public class AddParcelPageViewModel : ViewModelBase, INavigatedAware
    {

        public AddParcelPageViewModel(INavigationService navigationService,
            IStringLocalizer<AddParcelPageViewModel> localizer, IAppDataService appDataService) : base(localizer)
        {
            _navigationService = navigationService;
            AppDataService = appDataService;

            ClickSave = new DelegateCommand(SavePlot); //.ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            ClickDelineate = new DelegateCommand(Delineate);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Plot = new Plot();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        public IAppDataService AppDataService { get; }

        private readonly INavigationService _navigationService;

        private bool _isSaveBtnEnabled = true;

        private Plot _plot;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private bool _userIsAtPlot;

        public List<string> ClimateTypes { get; } = new List<string>
        {
            "Frío",
            "Templado/Subtropical",
            "Tropical",
            "Híbrido"
        };

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

        public bool InformationMissing => !IsSaveBtnEnabled;

        public List<string> MaturityClasses { get; } = new List<string>
        {
            "Temprana",
            "Semi-temprana",
            "Intermedia",
            "Semi-tardía",
            "Tardía"
        };

        public DelegateCommand ClickChooseLocation { get; set; }

        public DelegateCommand ClickGetLocation { get; set; }

        public DelegateCommand ClickDelineate { get; set; }

        public DelegateCommand ClickSave { get; set; }

        public bool IsSaveBtnEnabled
        {
            get => _isSaveBtnEnabled;
            set => SetProperty(ref _isSaveBtnEnabled, value);
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public Plot Plot
        {
            get => _plot;
            set
            {
                SetProperty(ref _plot, value);
                UpdateSelections();
            }
        }

        public int PickerClimateTypesSelectedIndex
        {
            get => _pickerClimateTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerClimateTypesSelectedIndex, value);
                // Plot.ClimateType = value == -1 ? null : ClimateTypes.ElementAt(value); TODO fix
            }
        }

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
                if (value == -1)
                {
                    Plot.CropType = CropType.None;
                }
                else
                {
                    Plot.CropType = (CropType)(value + 1); // TODO: verify
                }
            }
        }

        public int PickerMaturityClassesSelectedIndex
        {
            get => _pickerMaturityClassesSelectedIndex;
            set
            {
                SetProperty(ref _pickerMaturityClassesSelectedIndex, value);
                // Plot.MaturityClass = value == -1 ? null : MaturityClasses.ElementAt(value); todo: fix
            }
        }

        public bool UserIsAtPlot
        {
            get => _userIsAtPlot;
            set => SetProperty(ref _userIsAtPlot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                if (plot != null)
                {
                    Plot = plot;
                }
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<Activity>>("Activities", out var activities);
                if (Plot.Activities == null)
                {
                    Plot.Activities = activities;
                }
                else
                {
                    if (activities != null)
                    {
                        activities.AddRange(Plot.Activities);
                        Plot.Activities = activities;
                    }
                }
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<Position>("GeoPosition", out var geoPosition);
                if (geoPosition != null)
                {
                    Plot.Position = geoPosition;
                }
            }

            if (parameters.ContainsKey("Delineation"))
            {
                parameters.TryGetValue<List<Position>>("Delineation", out var delineation);
                Plot.Delineation = delineation;

                //_cimmytDbOperations.SavePlotPolygon(Plot.PlotId, polygonObj); TODO ensure saving
            }
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.GetLocation }
            };
            if (Plot.Position != null)
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectLocation }
            };
            if (Plot.Position != null)
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void Delineate()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectPolygon }
            };
            if (Plot.Position != null)
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "AddPlotPage" },
                { "Plot", Plot }
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        private void SavePlot()
        {
            IsSaveBtnEnabled = false;
            // Plot.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice; todo: add this?
            AppDataService.AddPlot(Plot);

            var navigationParameters = new NavigationParameters
            {
                { "id", Plot.ID }
            };
            _navigationService.NavigateAsync("app:///MainPage", navigationParameters, true);
        }

        private void UpdateSelections() // TODO fix
        {
            // for (int i = 0; i < CropTypes.Count; i++)
            // {
            //     if (CropTypes[i] != Plot.Crop)
            //     {
            //         continue;
            //     }
            //
            //     PickerCropTypesSelectedIndex = i;
            //     break;
            // }
            //
            // for (int i = 0; i < MaturityClasses.Count; i++)
            // {
            //     if (MaturityClasses[i] != Plot.MaturityClass)
            //     {
            //         continue;
            //     }
            //
            //     PickerMaturityClassesSelectedIndex = i;
            //     break;
            // }
            //
            // for (int i = 0; i < ClimateTypes.Count; i++)
            // {
            //     if (ClimateTypes[i] != Plot.ClimateType)
            //     {
            //         continue;
            //     }
            //
            //     PickerClimateTypesSelectedIndex = i;
            //     break;
            // }
        }
    }
}