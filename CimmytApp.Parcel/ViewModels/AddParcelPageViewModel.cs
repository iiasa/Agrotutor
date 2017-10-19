using System;

namespace CimmytApp.Parcel.ViewModels
{
    using BusinessContract;
    using DTO.Parcel;
    using Helper.Base.DTO;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="AddParcelPageViewModel" />
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
        /// Defines the _pickerYearsSelectedIndex
        /// </summary>
        private int _pickerYearsSelectedIndex;

        /// <summary>
        /// Defines the _userIsAtParcel
        /// </summary>
        private bool _userIsAtParcel;

        /// <summary>
        /// Defines the _years
        /// </summary>
        private List<string> _years;

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
            Years = new List<string>();

            ClickSave = new DelegateCommand(SaveParcel).ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            ClickDelineate = new DelegateCommand(Delineate);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Parcel = new Parcel();
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        private void NavigateAsync(string page)
        {
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
            var parameters = new NavigationParameters { { "ChooseLocation", true } };
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
                _pickerCropTypesSelectedIndex = value;
                Parcel.Crop = CropTypes.ElementAt(value);
                Parcel.CropType = (CropType)(value + 1);
            }
        }

        /// <summary>
        /// Gets or sets the PickerYearsSelectedIndex
        /// </summary>
        public int PickerYearsSelectedIndex
        {
            get => _pickerYearsSelectedIndex;
            set
            {
                _pickerYearsSelectedIndex = value;
                Parcel.Year = Years.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether UserIsAtParcel
        /// </summary>
        public bool UserIsAtParcel { get => _userIsAtParcel; set => SetProperty(ref _userIsAtParcel, value); }

        /// <summary>
        /// Gets or sets the Years
        /// </summary>
        public List<string> Years { get => _years; set => SetProperty(ref _years, value); }

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
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue("GeoPosition", out var geoPosition);
                if (geoPosition == null) return;
                var position = (GeoPosition)geoPosition;
                Parcel.Latitude = position.Latitude;
                Parcel.Longitude = position.Longitude;
            }

            if (parameters.ContainsKey("Deliniation"))
            {
                parameters.TryGetValue("Deliniation", out var delineation);
                var polygonObj = new PolygonDto { ListPoints = (List<DTO.GeoPosition>)delineation };
                if (polygonObj.ListPoints.Count > 0)
                {
                    Parcel.Latitude = polygonObj.ListPoints.ElementAt(0).Latitude;
                    Parcel.Longitude = polygonObj.ListPoints.ElementAt(0).Longitude;
                }
                Parcel.SetDeliniation(polygonObj.ListPoints);
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
        /// The ChooseLocation
        /// </summary>
        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "ChooseLocation", true } };
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
            _navigationService.NavigateAsync("MainPage", navigationParameters, true);
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
                Parcel.MaturityClass = MaturityClasses.ElementAt(value);
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
                Parcel.ClimateType = ClimateTypes.ElementAt(value);
            }
        }
    }
}