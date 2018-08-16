namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms.GoogleMaps;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    ///     Defines the <see cref="T:CimmytApp.Parcel.ViewModels.AddParcelPageViewModel" />
    /// </summary>
    public class AddParcelPageViewModel : BindableBase, INavigationAware
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
        ///     Defines the _isSaveBtnEnabled
        /// </summary>
        private bool _isSaveBtnEnabled = true;

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
        ///     Defines the _userIsAtParcel
        /// </summary>
        private bool _userIsAtParcel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddParcelPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;

            ClickSave = new DelegateCommand(SaveParcel); //.ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            ClickDelineate = new DelegateCommand(Delineate);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Parcel = new Parcel();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
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
        ///     Gets a value indicating whether InformationMissing
        /// </summary>
        public bool InformationMissing => !IsSaveBtnEnabled;

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
        ///     Gets or sets the ClickDelineate
        /// </summary>
        public DelegateCommand ClickDelineate { get; set; }

        /// <summary>
        ///     Gets or sets the ClickGetLocation
        /// </summary>
        public DelegateCommand ClickGetLocation { get; set; }

        /// <summary>
        ///     Gets or sets the ClickSave
        /// </summary>
        public DelegateCommand ClickSave { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether IsSaveBtnEnabled
        /// </summary>
        public bool IsSaveBtnEnabled
        {
            get => _isSaveBtnEnabled;
            set => SetProperty(ref _isSaveBtnEnabled, value);
        }

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
                Parcel.ClimateType = value == -1 ? null : ClimateTypes.ElementAt(value);
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
                if (value == -1)
                {
                    Parcel.Crop = "Ninguno";
                    Parcel.CropType = CropType.None;
                }
                else
                {
                    Parcel.Crop = CropTypes.ElementAt(value);
                    Parcel.CropType = (CropType)(value + 1);
                }
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
                Parcel.MaturityClass = value == -1 ? null : MaturityClasses.ElementAt(value);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether UserIsAtParcel
        /// </summary>
        public bool UserIsAtParcel
        {
            get => _userIsAtParcel;
            set => SetProperty(ref _userIsAtParcel, value);
        }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
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
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = parcel;
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
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<GeoPosition>("GeoPosition", out var geoPosition);
                if (geoPosition != null)
                {
                    Parcel.Position = geoPosition;
                }
            }

            if (parameters.ContainsKey("Delineation"))
            {
                parameters.TryGetValue<List<GeoPosition>>("Delineation", out var delineation);
                Parcel.Delineation = delineation;

                //_cimmytDbOperations.SaveParcelPolygon(Parcel.ParcelId, polygonObj); TODO ensure saving
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

        /// <summary>
        ///     The OnNavigatingTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The SelectLocation
        /// </summary>
        private void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        /// <summary>
        ///     The Delineate
        /// </summary>
        private void Delineate()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectPolygon }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        /// <summary>
        ///     The GetLocation
        /// </summary>
        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.GetLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "AddParcelPage" },
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        /// <summary>
        ///     The SaveParcel
        /// </summary>
        private void SaveParcel()
        {
            IsSaveBtnEnabled = false;
            Parcel.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            _cimmytDbOperations.SaveParcel(Parcel.GetDTO());

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("app:///MainPage", navigationParameters, true);
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
    }
}