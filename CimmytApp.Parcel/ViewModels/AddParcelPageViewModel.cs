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
        /// Defines the _doubleYears
        /// </summary>
        private readonly List<string> _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _singleYears
        /// </summary>
        private readonly List<string> _singleYears = new List<string>() { "2015", "2016", "2017" };

        /// <summary>
        /// Defines the _pickerAgriculturalCycleSelectedIndex
        /// </summary>
        private int _pickerAgriculturalCycleSelectedIndex;

        /// <summary>
        /// Defines the _pickerCropTypesSelectedIndex
        /// </summary>
        private int _pickerCropTypesSelectedIndex;

        /// <summary>
        /// Defines the _pickerIrrigationTypesSelectedIndex
        /// </summary>
        private int _pickerIrrigationTypesSelectedIndex;

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
        /// Defines the _pickerSowingTypesSelectedIndex
        /// </summary>
        private int _pickerSowingTypesSelectedIndex;

        /// <summary>
        /// Defines the _pickerHarvestingTypesSelectedIndex
        /// </summary>
        private int _pickerHarvestingTypesSelectedIndex;

        /// <summary>
        /// Defines the _pickerStorageTypesSelectedIndex
        /// </summary>
        private int _pickerStorageTypesSelectedIndex;

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

            Parcel = new Parcel();
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters { { "ChooseLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsSaveBtnEnabled
        /// </summary>
        public bool IsSaveBtnEnabled
        {
            get
            {
                return _isSaveBtnEnabled;
            }
            set
            {
                SetProperty(ref _isSaveBtnEnabled, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether InformationMissing
        /// </summary>
        public bool InformationMissing
        {
            get { return !IsSaveBtnEnabled; }
        }

        /// <summary>
        /// Gets the AgriculturalCycles
        /// </summary>
        public List<string> AgriculturalCycles { get; }

= new List<string> { "Primavera-Verano", "Otoño-Invierno" };

        /// <summary>
        /// Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickChooseLocation { get; set; }

        public DelegateCommand ClickGetLocation { get; set; }

        /// <summary>
        /// Gets or sets the ClickSave
        /// </summary>
        public DelegateCommand ClickSave { get; set; }

        /// <summary>
        /// Gets the CropTypes
        /// </summary>
        public List<string> CropTypes { get; }

= new List<string> { "Maíz", "Cebada", "Frijol", "Trigo", "Triticale", "Sorgo", "Alfalfa", "Avena", "Ajonjolí", "Amaranto", "Arroz", "Canola", "Cartamo", "Calabacín", "Garbanzo", "Haba", "Soya", "Ninguno", "Otro" };

        /// <summary>
        /// Gets the IrrigationTypes
        /// </summary>
        public List<string> IrrigationTypes { get; }

= new List<string> { "Riego", "Riego de punteo", "Temporal" };

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get; set; }

        /// <summary>
        /// Gets or sets the PickerAgriculturalCycleSelectedIndex
        /// </summary>
        public int PickerAgriculturalCycleSelectedIndex
        {
            get => _pickerAgriculturalCycleSelectedIndex;
            set
            {
                _pickerAgriculturalCycleSelectedIndex = value;
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);
                var yrs = Years;
                yrs.Clear();
                yrs.AddRange(value == 0 ? _singleYears : _doubleYears);

                Years = yrs;
            }
        }

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
        /// Gets or sets the PickerIrrigationTypesSelectedIndex
        /// </summary>
        public int PickerIrrigationTypesSelectedIndex
        {
            get => _pickerIrrigationTypesSelectedIndex;
            set
            {
                _pickerIrrigationTypesSelectedIndex = value;
                Parcel.Irrigation = IrrigationTypes.ElementAt(value);
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
        /// Gets or sets a value indicating whether Test
        /// </summary>
        public bool Test { get; set; }

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

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue("GeoPosition", out object geoPosition);
                if (geoPosition == null) return;
                var position = (GeoPosition)geoPosition;
                Parcel.Latitude = position.Latitude;
                Parcel.Longitude = position.Longitude;
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
        /// The CheckFields
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool CheckFields()
        {
            if (Parcel.EstimatedParcelArea == null) return false;
            if (Parcel.EstimatedParcelArea.Equals(string.Empty)) return false;
            if (Parcel.ProducerName == null) return false;
            if (Parcel.ProducerName.Equals(string.Empty)) return false;
            if (Parcel.ParcelName == null) return false;
            if (Parcel.ParcelName.Equals(string.Empty)) return false;
            if (Parcel.Cultivar == null) return false;
            if (Parcel.Cultivar.Equals(string.Empty)) return false;
            return true;
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
        /// Gets or sets the PickerSowingTypesSelectedIndex
        /// </summary>
        public int PickerSowingTypesSelectedIndex
        {
            get => _pickerSowingTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerSowingTypesSelectedIndex, value);
                Parcel.SowingType = SowingTypes.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets the MaturityClasses
        /// </summary>
        public List<string> MaturityClasses { get; }

            = new List<string> { "Temprana", "Semi-temprana", "Intermedia", "Semi-tardía", "Tardía" };

        /// <summary>
        /// Gets the ClimateTypes
        /// </summary>
        public List<string> ClimateTypes { get; }

            = new List<string> { "Frío", "Templado/Subtropical", "Tropical", "Híbrido" };

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

        /// <summary>
        /// Gets the HarvestingTypes
        /// </summary>
        public List<string> HarvestingTypes { get; }

            = new List<string> { "Cosecha manual", "Cosecha mecánica" };

        /// <summary>
        /// Gets the StorageTypes
        /// </summary>
        public List<string> StorageTypes { get; }

            = new List<string> { "Almacenamiento poscosecha con tecnologías herméticas", "Almacenamiento poscosecha tradicional" };

        /// <summary>
        /// Gets or sets the PickerHarvestingTypesSelectedIndex
        /// </summary>
        public int PickerHarvestingTypesSelectedIndex
        {
            get => _pickerHarvestingTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerHarvestingTypesSelectedIndex, value);
                Parcel.HarvestingType = HarvestingTypes.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets or sets the PickerStorageTypesSelectedIndex
        /// </summary>
        public int PickerStorageTypesSelectedIndex
        {
            get => _pickerStorageTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerStorageTypesSelectedIndex, value);
                Parcel.StorageType = StorageTypes.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets the SowingTypes
        /// </summary>
        public List<string> SowingTypes { get; }

            = new List<string> { "Resiembra", "Siembra" };
    }
}