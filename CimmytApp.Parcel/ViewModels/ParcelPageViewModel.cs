namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.Parcel;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Newtonsoft.Json;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    ///     Defines the <see cref="T:CimmytApp.Parcel.ViewModels.ParcelPageViewModel" />
    /// </summary>
    public class ParcelPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     Defines the _scheduler
        /// </summary>
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        private string _caller;

        /// <summary>
        ///     Defines the _editModeActive
        /// </summary>
        private bool _editModeActive;

        /// <summary>
        ///     Defines the _imageSource
        /// </summary>
        private ImageSource _imageSource;

        /// <summary>
        ///     Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        ///     Defines the _pickerClimateTypesSelectedIndex
        /// </summary>
        private int _pickerClimateTypesSelectedIndex;

        /// <summary>
        ///     Defines the _pickerCropTypesSelectedIndex
        /// </summary>
        private int _pickerCropTypesSelectedIndex;

        /// <summary>
        ///     Defines the _pickerMaturityClassesSelectedIndex
        /// </summary>
        private int _pickerMaturityClassesSelectedIndex;

        /// <summary>
        ///     Defines the _showEditToggle
        /// </summary>
        private bool _showEditToggle;

        /// <summary>
        ///     Defines the viewModeActive
        /// </summary>
        private bool _viewModeActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParcelPageViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator" /></param>
        public ParcelPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            ICimmytDbOperations cimmytDbOperations)
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

        /// <summary>
        ///     Gets the ClimateTypes
        /// </summary>
        public List<string> ClimateTypes { get; } = new List<string>
        {
            "Frío",
            "Templado/Subtropical",
            "Tropical",
            "Híbrido"
        };

        /// <summary>
        ///     Gets the CropTypes
        /// </summary>
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

        /// <summary>
        ///     Gets the MaturityClasses
        /// </summary>
        public List<string> MaturityClasses { get; } = new List<string>
        {
            "Temprana",
            "Semi-temprana",
            "Intermedia",
            "Semi-tardía",
            "Tardía"
        };

        /// <summary>
        ///     Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickChooseLocation { get; set; }

        /// <summary>
        ///     Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickGetLocation { get; set; }

        /// <summary>
        ///     Gets or sets the ClickPhoto
        /// </summary>
        public DelegateCommand ClickPhoto { get; set; }

        /// <summary>
        ///     Gets or sets the ClickSave
        /// </summary>
        public DelegateCommand ClickSave { get; set; }

        /// <summary>
        ///     Gets or sets the DeleteParcelCommand
        /// </summary>
        public DelegateCommand DeleteParcelCommand { get; set; }

        /// <summary>
        ///     Gets or sets the DelineateParcelCommand
        /// </summary>
        public DelegateCommand DelineateParcelCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether EditModeActive
        /// </summary>
        public bool EditModeActive
        {
            get => _editModeActive;
            set
            {
                SetProperty(ref _editModeActive, value);
                ViewModeActive = !EditModeActive;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether EditsDone
        /// </summary>
        public bool EditsDone { get; set; }

        public DelegateCommand EditTechnologiesCommand { get; set; }

        public DelegateCommand GoBackCommand { get; set; }

        /// <summary>
        ///     Gets or sets the ImageSource
        /// </summary>
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        ///     Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                UpdateSelections();
            }
        }

        /// <summary>
        ///     Gets or sets the PickerClimateTypesSelectedIndex
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the PickerCropTypesSelectedIndex
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the PickerMaturityClassesSelectedIndex
        /// </summary>
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

        /// <summary>
        ///     Gets or sets a value indicating whether ShowEditToggle
        /// </summary>
        public bool ShowEditToggle
        {
            get => _showEditToggle;
            set => SetProperty(ref _showEditToggle, value);
        }

        /// <summary>
        ///     Gets or sets the ViewActivitiesCommand
        /// </summary>
        public DelegateCommand ViewActivitiesCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether ViewModeActive
        /// </summary>
        public bool ViewModeActive
        {
            get => _viewModeActive;
            set => SetProperty(ref _viewModeActive, value);
        }

        public DelegateCommand ViewTechnologiesCommand { get; set; }

        /// <summary>
        ///     The SelectLocation
        /// </summary>
        public void ChooseLocation()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectLocation }
            };
            if ((bool)Parcel.Position?.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                               CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        ///     The DelineateParcel
        /// </summary>
        public void DelineateParcel()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectPolygon }
            };
            if ((bool)Parcel.Position?.IsSet())
            {
                ObservableCollection<Pin> points = new ObservableCollection<Pin>();
                var position = new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude);
                points.Add(new Pin
                {
                    Position = position
                });
                parameters.Add(MapViewModel.MapCenterParameterName,
                               CameraUpdateFactory.NewCameraPosition(new CameraPosition(position, 15)));
                parameters.Add(MapViewModel.PointsParameterName, points);
            }
            List<GeoPosition> delineation = Parcel.GetDelineation();

            if (delineation != null && delineation.Count > 2)
            {
                List<Polygon> polygons = new List<Polygon>();
                Polygon polygon = new Polygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f
                };

                List<Position> listPosition = delineation
                    .Select(positionitem => new Position((double)positionitem.Latitude, (double)positionitem.Longitude))
                    .ToList();

                foreach (var position in listPosition){
                    polygon.Positions.Add(position);
                }
                polygons.Add(polygon);
                parameters.Add(MapViewModel.PolygonsParameterName, polygons);
            }

            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Caller"))
            {
                parameters.TryGetValue<string>("Caller", out var caller);
                _caller = (string)caller;
            }
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null) Parcel = (Parcel)parcel;
            }
            if (parameters.ContainsKey("Id"))
            {
                try
                {
                    int id = (int)parameters["Id"];
                    Parcel = Parcel.FromDTO(_cimmytDbOperations.GetParcelById(id));
                }
                catch (Exception e)
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
                parameters.TryGetValue<List<GeoPosition>>("Delineation", out var delineation);
                PolygonDto polygonObj = new PolygonDto
                {
                    ListPoints = delineation
                };
                if (polygonObj.ListPoints.Count > 0 && Parcel.Position.Latitude == 0 && Parcel.Position.Longitude == 0)
                {
                    Parcel.Position.Latitude = polygonObj.ListPoints.ElementAt(0).Latitude;
                    Parcel.Position.Longitude = polygonObj.ListPoints.ElementAt(0).Longitude;
                }
                Parcel.SetDelineation(polygonObj.ListPoints);
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<GeoPosition>("GeoPosition", out var position);
                if (position == null) return;
                Parcel.Position.Latitude = position.Latitude;
                Parcel.Position.Longitude = position.Longitude;
                _cimmytDbOperations.UpdateParcel(Parcel.GetDTO());
            }

            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<AgriculturalActivity>>("Activities", out var activities);
                if (Parcel.AgriculturalActivities == null) Parcel.AgriculturalActivities = activities;
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
                parameters.TryGetValue<List<string>>(ParcelConstants.TechnologiesParameterName, out var technologies);
                if (Parcel != null)
                {
                    Parcel.TechnologiesUsed = technologies;
                    Parcel.TechnologiesUsedBlobbed = JsonConvert.SerializeObject(Parcel.TechnologiesUsed);
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The DeleteParcel
        /// </summary>
        private void DeleteParcel()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync("DeleteParcelPage", parameters);
        }

        private void EditTechnologies()
        {
            List<string> technologies = Parcel.TechnologiesUsedBlobbed != null ? JsonConvert.DeserializeObject<List<string>>(Parcel.TechnologiesUsedBlobbed) : null;

            NavigationParameters parameters = new NavigationParameters();
            if (technologies != null) parameters.Add(ParcelConstants.TechnologiesParameterName, technologies);
            _navigationService.NavigateAsync("SelectTechnologiesPage", parameters);
        }

        /// <summary>
        ///     The SelectLocation
        /// </summary>
        private void GetLocation()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectLocation }
            };
            if ((bool)Parcel.Position?.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                               CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("GenericMap", parameters);
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

        /// <summary>
        ///     The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string" /></param>
        private void NavigateAsync(string page)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "Caller", "ParcelPage" },
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        /// <summary>
        ///     The SaveParcel
        /// </summary>
        private void SaveParcel()
        {
            if (ShowEditToggle) EditModeActive = false;
            EditsDone = false;
            _cimmytDbOperations.UpdateParcel(Parcel.GetDTO());
            NavigationParameters parameters = new NavigationParameters
            {
                { "Id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("ParcelMainPage", parameters);
        }

        /// <summary>
        ///     The UpdateSelections
        /// </summary>
        private void UpdateSelections()
        {
            for (int i = 0; i < CropTypes.Count; i++)
            {
                if (CropTypes[i] != Parcel.Crop)
                {
                    continue;
                }

                PickerCropTypesSelectedIndex = i;
                break;
            }

            for (int i = 0; i < MaturityClasses.Count; i++)
            {
                if (MaturityClasses[i] != Parcel.MaturityClass)
                {
                    continue;
                }

                PickerMaturityClassesSelectedIndex = i;
                break;
            }

            for (int i = 0; i < ClimateTypes.Count; i++)
            {
                if (ClimateTypes[i] != Parcel.ClimateType)
                {
                    continue;
                }

                PickerClimateTypesSelectedIndex = i;
                break;
            }
        }

        /// <summary>
        ///     The ViewActivities
        /// </summary>
        private void ViewActivities()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "Activities", Parcel.AgriculturalActivities }
            };
            _navigationService.NavigateAsync("ViewActivitiesPage", parameters);
        }

        private void ViewTechnologies()
        {
            List<string> technologies = Parcel.TechnologiesUsedBlobbed != null ? JsonConvert.DeserializeObject<List<string>>(Parcel.TechnologiesUsedBlobbed) : null;

            NavigationParameters parameters = new NavigationParameters();
            if (technologies != null) parameters.Add(ParcelConstants.TechnologiesParameterName, technologies);
            _navigationService.NavigateAsync("SelectTechnologiesPage", parameters);
        }
    }
}