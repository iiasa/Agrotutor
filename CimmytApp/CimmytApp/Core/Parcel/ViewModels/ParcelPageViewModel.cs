namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class ParcelPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;

        private readonly INavigationService _navigationService;

        private string _caller;

        private bool _editModeActive;

        private ImageSource _imageSource;

        private Parcel _parcel;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private bool _showEditToggle;

        private bool _viewModeActive;

        public ParcelPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            ICimmytDbOperations cimmytDbOperations, IStringLocalizer<ParcelPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            ClickSave = new DelegateCommand(SaveParcel);
            DelineateParcelCommand = new DelegateCommand(DelineateParcel);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            DeleteParcelCommand = new DelegateCommand(DeleteParcel);
            EditModeActive = false;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            ViewActivitiesCommand = new DelegateCommand(ViewActivities);
            ViewTechnologiesCommand = new DelegateCommand(ViewTechnologies);
            EditTechnologiesCommand = new DelegateCommand(EditTechnologies);
            GoBackCommand = new DelegateCommand(GoBack);
        }

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

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
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

                Parcel.ClimateType = ClimateTypes.ElementAt(value);
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

                Parcel.Crop = CropTypes.ElementAt(value);
                Parcel.CropType = (CropType)(value + 1);
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

                Parcel.MaturityClass = MaturityClasses.ElementAt(value);
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

        public DelegateCommand ViewTechnologiesCommand { get; set; }

        public void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        public void DelineateParcel()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectPolygon }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                var points = new ObservableCollection<Pin>();
                var position = new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude);
                points.Add(new Pin
                {
                    Position = position
                });
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(position, 15)));
                parameters.Add(MapViewModel.PointsParameterName, points);
            }
            var delineation = Parcel.Delineation;

            if (delineation != null && delineation.Count > 2)
            {
                var polygons = new List<Polygon>();
                var polygon = new Polygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f
                };

                var listPosition = delineation.Select(positionitem =>
                        new Position((double)positionitem.Latitude, (double)positionitem.Longitude))
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

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Caller"))
            {
                parameters.TryGetValue<string>("Caller", out var caller);
                _caller = caller;
            }
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = parcel;
                }
            }
            if (parameters.ContainsKey("Id"))
            {
                try
                {
                    var id = (string)parameters["Id"];
                    Parcel = Parcel.FromDTO(_cimmytDbOperations.GetParcelById(id));
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
                parameters.TryGetValue<List<Core.Map.GeoPosition>>("Delineation", out var delineation);
                if (Parcel.Position == null && delineation.Count > 0)
                {
                    Parcel.Position = new Core.Map.GeoPosition
                    {
                        Latitude = delineation.ElementAt(0).Latitude,
                        Longitude = delineation.ElementAt(0).Longitude
                    };
                }
                Parcel.Delineation = delineation;
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<Core.Map.GeoPosition>("GeoPosition", out var position);
                if (position != null)
                {
                    Parcel.Position = position;
                }
            }

            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<AgriculturalActivity>>("Activities", out var activities);
                if (Parcel.AgriculturalActivities == null)
                {
                    Parcel.AgriculturalActivities = activities;
                }
                else
                {
                    if (activities != null)
                    {
                        activities.AddRange(Parcel.AgriculturalActivities);
                        Parcel.AgriculturalActivities = activities;
                    }
                }
            }

            if (parameters.ContainsKey(ParcelConstants.TechnologiesParameterName))
            {
                parameters.TryGetValue<List<Technology>>(ParcelConstants.TechnologiesParameterName,
                    out var technologies);
                if (Parcel != null)
                {
                    Parcel.TechnologiesUsed = technologies;
                }
            }
        }

        private void DeleteParcel()
        {
            var parameters = new NavigationParameters
            {
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync("DeleteParcelPage", parameters);
        }

        private void EditTechnologies()
        {
            var technologies = Parcel.TechnologiesUsed;

            var parameters = new NavigationParameters();
            if (technologies != null)
            {
                parameters.Add(ParcelConstants.TechnologiesParameterName, technologies);
            }
            _navigationService.NavigateAsync("SelectTechnologiesPage", parameters);
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.GetLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
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
                    { "Id", Parcel.ParcelId }
                });
            }
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "ParcelPage" },
                { "Parcel", Parcel }
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
            Parcel.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            _cimmytDbOperations.SaveParcel(Parcel.GetDTO(), true);
            var parameters = new NavigationParameters
            {
                { "Id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("ParcelMainPage", parameters);
        }

        private void UpdateSelections()
        {
            for (var i = 0; i < CropTypes.Count; i++)
            {
                if (CropTypes[i] != Parcel.Crop)
                {
                    continue;
                }

                PickerCropTypesSelectedIndex = i;
                break;
            }

            for (var i = 0; i < MaturityClasses.Count; i++)
            {
                if (MaturityClasses[i] != Parcel.MaturityClass)
                {
                    continue;
                }

                PickerMaturityClassesSelectedIndex = i;
                break;
            }

            for (var i = 0; i < ClimateTypes.Count; i++)
            {
                if (ClimateTypes[i] != Parcel.ClimateType)
                {
                    continue;
                }

                PickerClimateTypesSelectedIndex = i;
                break;
            }
        }

        private void ViewActivities()
        {
            var parameters = new NavigationParameters
            {
                { "Activities", Parcel.AgriculturalActivities }
            };
            _navigationService.NavigateAsync("ViewActivitiesPage", parameters);
        }

        private void ViewTechnologies()
        {
            var technologies = Parcel.TechnologiesUsed;
            var parameters = new NavigationParameters();
            if (technologies != null)
            {
                parameters.Add(ParcelConstants.TechnologiesParameterName, technologies);
            }
            _navigationService.NavigateAsync("SelectTechnologiesPage", parameters);
        }
    }
}