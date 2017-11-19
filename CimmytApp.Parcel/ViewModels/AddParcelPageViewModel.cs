using Helper.Map;

namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    using BusinessContract;
    using DTO.Parcel;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    /// Defines the <see cref="T:CimmytApp.Parcel.ViewModels.AddParcelPageViewModel" />
    /// </summary>
    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _pickerCropTypesSelectedIndex
        /// </summary>
        private int _pickerCropTypesSelectedIndex;

        /// <summary>
        /// Defines the _userIsAtParcel
        /// </summary>
        private bool _userIsAtParcel;

        /// <summary>
        /// Defines the _isSaveBtnEnabled
        /// </summary>
        private bool _isSaveBtnEnabled = true;

        /// <summary>
        /// Defines the _pickerMaturityClassesSelectedIndex
        /// </summary>
        private int _pickerMaturityClassesSelectedIndex;

        /// <summary>
        /// Defines the _pickerClimateTypesSelectedIndex
        /// </summary>
        private int _pickerClimateTypesSelectedIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddParcelPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations"/></param>
        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;

            ClickSave = new DelegateCommand(SaveParcel).ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            ClickDelineate = new DelegateCommand(Delineate);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Parcel = new Parcel();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "AddParcelPage" }
            };
            _navigationService.NavigateAsync(page);
        }

        /// <summary>
        /// The Delineate
        /// </summary>
        private void Delineate()
        {
            var parameters = new NavigationParameters
            {
                {"Latitude", Parcel.Latitude},
                {"Longitude", Parcel.Longitude},
                {"GetPolygon", true},
                {"parcelId", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// Gets or sets the ClickDelineate
        /// </summary>
        public DelegateCommand ClickDelineate { get; set; }

        /// <summary>
        /// The GetLocation
        /// </summary>
        private void GetLocation()
        {
            var parameters = new NavigationParameters { { "SelectLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsSaveBtnEnabled
        /// </summary>
        public bool IsSaveBtnEnabled { get => _isSaveBtnEnabled; set => SetProperty(ref _isSaveBtnEnabled, value); }

        /// <summary>
        /// Gets a value indicating whether InformationMissing
        /// </summary>
        public bool InformationMissing => !IsSaveBtnEnabled;

        /// <summary>
        /// Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickChooseLocation { get; set; }

        /// <summary>
        /// Gets or sets the ClickGetLocation
        /// </summary>
        public DelegateCommand ClickGetLocation { get; set; }

        /// <summary>
        /// Gets or sets the ClickSave
        /// </summary>
        public DelegateCommand ClickSave { get; set; }

        /// <summary>
        /// Gets the CropTypes
        /// </summary>
        public List<string> CropTypes { get; } = new List<string> { "Maíz", "Cebada", "Frijol", "Trigo", "Triticale", "Sorgo", "Alfalfa", "Avena", "Ajonjolí", "Amaranto", "Arroz", "Canola", "Cartamo", "Calabacín", "Garbanzo", "Haba", "Soya", "Ninguno", "Otro" };

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get; set; }

        /// <summary>
        /// Gets or sets the PickerCropTypesSelectedIndex
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
        /// Gets or sets a value indicating whether UserIsAtParcel
        /// </summary>
        public bool UserIsAtParcel { get => _userIsAtParcel; set => SetProperty(ref _userIsAtParcel, value); }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue("Activities", out var activities);
                if (Parcel.AgriculturalActivities == null)
                    Parcel.AgriculturalActivities = (List<AgriculturalActivity>)activities;
                else
                    Parcel.AgriculturalActivities.AddRange((List<AgriculturalActivity>)activities);
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue("GeoPosition", out var geoPosition);
                if (geoPosition == null) return;
                var position = (GeoPosition)geoPosition;
                Parcel.Latitude = position.Latitude;
                Parcel.Longitude = position.Longitude;
            }

            if (parameters.ContainsKey("Delineation"))
            {
                parameters.TryGetValue("Delineation", out var delineation);
                var polygonObj = new PolygonDto { ListPoints = (List<GeoPosition>)delineation };
                if (polygonObj.ListPoints.Count > 0)
                {
                    Parcel.Latitude = polygonObj.ListPoints.ElementAt(0).Latitude;
                    Parcel.Longitude = polygonObj.ListPoints.ElementAt(0).Longitude;
                }
                Parcel.SetDelineation(polygonObj.ListPoints);
                _cimmytDbOperations.SaveParcelPolygon(Parcel.ParcelId, polygonObj);

                OnPropertyChanged("Parcel");
            }

            if (parameters.ContainsKey(ParcelConstants.TechnologiesParameterName))
            {
                parameters.TryGetValue(ParcelConstants.TechnologiesParameterName, out var technologies);
                if (Parcel != null)
                {
                    Parcel.TechnologiesUsed = (List<string>)technologies;
                }
            }
        }

        /// <summary>
        /// The OnNavigatingTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The SelectLocation
        /// </summary>
        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "SelectLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// The SaveParcel
        /// </summary>
        private void SaveParcel()
        {
            IsSaveBtnEnabled = false;
            _cimmytDbOperations.AddParcel(Parcel);

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("app:///MainPage", navigationParameters, true);
        }

        /// <summary>
        /// Gets the MaturityClasses
        /// </summary>
        public List<string> MaturityClasses { get; } = new List<string> { "Temprana", "Semi-temprana", "Intermedia", "Semi-tardía", "Tardía" };

        /// <summary>
        /// Gets the ClimateTypes
        /// </summary>
        public List<string> ClimateTypes { get; } = new List<string> { "Frío", "Templado/Subtropical", "Tropical", "Híbrido" };

        /// <summary>
        /// Gets or sets the PickerMaturityClassesSelectedIndex
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
        /// Gets or sets the PickerClimateTypesSelectedIndex
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
    }
}