namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class ParcelPageViewModel : ViewModelBase, INavigatedAware
    { 

        private readonly INavigationService _navigationService;

        private string _caller;

        private bool _editModeActive;

        private ImageSource _imageSource;

        private Plot _plot;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private bool _showEditToggle;

        private bool _viewModeActive;



        public ParcelPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            AppDataService appDataService, IStringLocalizer<ParcelPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            AppDataService = appDataService;
            ClickSave = new DelegateCommand(SaveParcel);
            DelineateParcelCommand = new DelegateCommand(DelineateParcel);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            DeleteParcelCommand = new DelegateCommand(DeleteParcel);
            EditModeActive = false;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            ViewActivitiesCommand = new DelegateCommand(ViewActivities);
            GoBackCommand = new DelegateCommand(GoBack);
        }

        public IAppDataService AppDataService { get; set; }

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

        public DelegateCommand ClickSave { get; set; }

        public DelegateCommand DeleteParcelCommand { get; set; }

        public DelegateCommand DelineateParcelCommand { get; set; }

        public bool EditModeActive
        {
            get => _editModeActive;
            set
            {
                SetProperty(ref _editModeActive, value);
                ViewModeActive = !EditModeActive;
            }
        }

        public bool EditsDone { get; set; }

        public DelegateCommand EditTechnologiesCommand { get; set; }

        public DelegateCommand GoBackCommand { get; set; }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public Plot Plot
        {
            get => this._plot;
            set
            {
                SetProperty(ref this._plot, value);
                UpdateSelections();
            }
        }

        public int PickerClimateTypesSelectedIndex
        {
            get => _pickerClimateTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerClimateTypesSelectedIndex, value);
                if (!EditModeActive)
                {
                    return;
                }

                // Plot.ClimateType = ClimateTypes.ElementAt(value); TODO: implement selector
            }
        }

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
                if (!EditModeActive)
                {
                    return;
                }

                // Plot.CropType = CropTypes.ElementAt(value); TODO: implement selector
            }
        }

        public int PickerMaturityClassesSelectedIndex
        {
            get => _pickerMaturityClassesSelectedIndex;
            set
            {
                SetProperty(ref _pickerMaturityClassesSelectedIndex, value);
                if (!EditModeActive)
                {
                    return;
                }

                // Plot.MaturityType = MaturityClasses.ElementAt(value); TODO: implement selector
            }
        }

        public bool ShowEditToggle
        {
            get => _showEditToggle;
            set => SetProperty(ref _showEditToggle, value);
        }

        public DelegateCommand ViewActivitiesCommand { get; set; }

        public bool ViewModeActive
        {
            get => _viewModeActive;
            set => SetProperty(ref _viewModeActive, value);
        }

        public void ChooseLocation()
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

        public void DelineateParcel()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectPolygon }
            };
            if (Plot.Position != null)
            {
                var points = new ObservableCollection<Pin>();
                var position = new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude);
                points.Add(new Pin
                {
                    Position = position
                });
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(position, 15)));
                parameters.Add(MapViewModel.PointsParameterName, points);
            }
            var delineation = Plot.Delineation;

            if (delineation != null && delineation.Count > 2)
            {
                var polygons = new List<Polygon>();
                var polygon = new Polygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f
                };

                var listPosition = delineation.Select(positionitem =>
                        new Xamarin.Forms.GoogleMaps.Position((double)positionitem.Latitude, (double)positionitem.Longitude))
                    .ToList();

                foreach (var position in listPosition)
                {
                    polygon.Positions.Add(position);
                }

                polygons.Add(polygon);
                parameters.Add(MapViewModel.PolygonsParameterName, polygons);
            }

            _navigationService.NavigateAsync("Map", parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        private async void LoadPlot(int id)
        {
            Plot = await AppDataService.GetPlot(id);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Caller"))
            {
                parameters.TryGetValue<string>("Caller", out var caller);
                _caller = caller;
            }
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                if (plot != null)
                {
                    Plot = plot;
                }
            }
            if (parameters.ContainsKey("Id"))
            {
                try
                {
                    LoadPlot((int)parameters["Id"]);
                }
                catch (Exception)
                {
                    GoBackCommand?.Execute();
                }
            }

            if (parameters.ContainsKey("EditEnabled"))
            {
                ShowEditToggle = false;
                parameters.TryGetValue<bool>("EditEnabled", out var editEnabled);
                EditModeActive = editEnabled;
            }

            if (parameters.ContainsKey("Delineation"))
            {
                parameters.TryGetValue<List<Persistence.Entities.Position>>("Delineation", out var delineation);
                if (Plot.Position == null && delineation.Count > 0)
                {
                    Plot.Position = new Persistence.Entities.Position
                    {
                        Latitude = delineation.ElementAt(0).Latitude,
                        Longitude = delineation.ElementAt(0).Longitude
                    };
                }
                Plot.Delineation = delineation;
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<Persistence.Entities.Position>("GeoPosition", out var position);
                if (position != null)
                {
                    Plot.Position = position;
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
        }

        private void DeleteParcel()
        {
            var parameters = new NavigationParameters
            {
                { "Plot", Plot }
            };
            _navigationService.NavigateAsync("DeleteParcelPage", parameters);
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

        private void GoBack()
        {
            if (_caller == null)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                _navigationService.NavigateAsync($"app:///{_caller}", new NavigationParameters
                {
                    { "Id", Plot.ID }
                });
            }
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "ParcelPage" },
                { "Plot", Plot }
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        private void SaveParcel()
        {
            if (ShowEditToggle)
            {
                EditModeActive = false;
            }
            EditsDone = false;
            // Plot.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice; TODO maybe add this
            AppDataService.UpdatePlot(Plot);
            var parameters = new NavigationParameters
            {
                { "Id", Plot.ID }
            };
            _navigationService.NavigateAsync("ParcelMainPage", parameters);
        }

        private void UpdateSelections()
        {
            for (var i = 0; i < CropTypes.Count; i++)
            {
                // if (CropTypes[i] != Plot.Crop) TODO
                // {
                //     continue;
                // }

                PickerCropTypesSelectedIndex = i;
                break;
            }

            for (var i = 0; i < MaturityClasses.Count; i++)
            {
                // if (MaturityClasses[i] != Plot.MaturityClass) TODO
                // {
                //     continue;
                // }

                PickerMaturityClassesSelectedIndex = i;
                break;
            }

            for (var i = 0; i < ClimateTypes.Count; i++)
            {
                // if (ClimateTypes[i] != Plot.ClimateType) TODO
                // {
                //     continue;
                // }

                PickerClimateTypesSelectedIndex = i;
                break;
            }
        }

        private void ViewActivities()
        {
            var parameters = new NavigationParameters
            {
                { "Activities", Plot.Activities }
            };
            _navigationService.NavigateAsync("ViewActivitiesPage", parameters);
        }
    }
}