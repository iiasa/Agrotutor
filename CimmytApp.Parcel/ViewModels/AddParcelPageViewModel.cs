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
    using System.Windows.Input;
    using Xamarin.Forms;

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
        /// Defines the _tech1Checked
        /// </summary>
        private bool _tech1Checked;

        /// <summary>
        /// Defines the _tech2Checked
        /// </summary>
        private bool _tech2Checked;

        /// <summary>
        /// Defines the _tech3Checked
        /// </summary>
        private bool _tech3Checked;

        /// <summary>
        /// Defines the _tech4Checked
        /// </summary>
        private bool _tech4Checked;

        /// <summary>
        /// Defines the _tech5Checked
        /// </summary>
        private bool _tech5Checked;

        /// <summary>
        /// Defines the _tech6Checked
        /// </summary>
        private bool _tech6Checked;

        /// <summary>
        /// Defines the _tech7Checked
        /// </summary>
        private bool _tech7Checked;

        /// <summary>
        /// Defines the _tech8Checked
        /// </summary>
        private bool _tech8Checked;

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
        /// Defines the _activity1Checked
        /// </summary>
        private bool _activity1Checked;

        /// <summary>
        /// Defines the _activity9Checked
        /// </summary>
        private bool _activity9Checked;

        /// <summary>
        /// Defines the _activity8Checked
        /// </summary>
        private bool _activity8Checked;

        /// <summary>
        /// Defines the _activity7Checked
        /// </summary>
        private bool _activity7Checked;

        /// <summary>
        /// Defines the _activity6Checked
        /// </summary>
        private bool _activity6Checked;

        /// <summary>
        /// Defines the _activity5Checked
        /// </summary>
        private bool _activity5Checked;

        /// <summary>
        /// Defines the _activity4Checked
        /// </summary>
        private bool _activity4Checked;

        /// <summary>
        /// Defines the _activity3Checked
        /// </summary>
        private bool _activity3Checked;

        /// <summary>
        /// Defines the _activity2Checked
        /// </summary>
        private bool _activity2Checked;

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
            ClickChooseLocation = new Command(ChooseLocation);

            Parcel = new Parcel();
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
        public ICommand ClickChooseLocation { get; set; }

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
        /// Gets or sets a value indicating whether Tech1Checked
        /// </summary>
        public bool Tech1Checked
        {
            get => _tech1Checked;
            set
            {
                _tech1Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech2Checked
        /// </summary>
        public bool Tech2Checked
        {
            get => _tech2Checked;
            set
            {
                _tech2Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech3Checked
        /// </summary>
        public bool Tech3Checked
        {
            get => _tech3Checked;
            set
            {
                _tech3Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech4Checked
        /// </summary>
        public bool Tech4Checked
        {
            get => _tech4Checked;
            set
            {
                _tech4Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech5Checked
        /// </summary>
        public bool Tech5Checked
        {
            get => _tech5Checked;
            set
            {
                _tech5Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech6Checked
        /// </summary>
        public bool Tech6Checked
        {
            get => _tech6Checked;
            set
            {
                _tech6Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech7Checked
        /// </summary>
        public bool Tech7Checked
        {
            get => _tech7Checked;
            set
            {
                _tech7Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech8Checked
        /// </summary>
        public bool Tech8Checked
        {
            get => _tech8Checked;
            set
            {
                _tech8Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity1Checked
        /// </summary>
        public bool Activity1Checked
        {
            get => _activity1Checked;
            set
            {
                SetProperty(ref _activity1Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity2Checked
        /// </summary>
        public bool Activity2Checked
        {
            get => _activity2Checked;
            set
            {
                SetProperty(ref _activity2Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity3Checked
        /// </summary>
        public bool Activity3Checked
        {
            get => _activity3Checked;
            set
            {
                SetProperty(ref _activity3Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity4Checked
        /// </summary>
        public bool Activity4Checked
        {
            get => _activity4Checked;
            set
            {
                SetProperty(ref _activity4Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity5Checked
        /// </summary>
        public bool Activity5Checked
        {
            get => _activity5Checked;
            set
            {
                SetProperty(ref _activity5Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity6Checked
        /// </summary>
        public bool Activity6Checked
        {
            get => _activity6Checked;
            set
            {
                SetProperty(ref _activity6Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity7Checked
        /// </summary>
        public bool Activity7Checked
        {
            get => _activity7Checked;
            set
            {
                SetProperty(ref _activity7Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity8Checked
        /// </summary>
        public bool Activity8Checked
        {
            get => _activity8Checked;
            set
            {
                SetProperty(ref _activity8Checked, value);
                UpdateActivityChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activity9Checked
        /// </summary>
        public bool Activity9Checked
        {
            get => _activity9Checked;
            set
            {
                SetProperty(ref _activity9Checked, value);
                UpdateActivityChecked();
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
            var parameters = new NavigationParameters { { "GetLocation", true } };
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
        /// The UpdateTechChecked
        /// </summary>
        private void UpdateTechChecked()
        {
            var technologies = new List<string>();
            if (_tech1Checked) technologies.Add("Cambio a variedades mejoradas, nuevas y adaptadas a las zonas con potencial para incrementar el rendimiento ");
            if (_tech2Checked) technologies.Add("Interpretación y uso del análisis de suelo");
            if (_tech3Checked) technologies.Add("Uso del sensor infrarrojo para fertilización óptima");
            if (_tech4Checked) technologies.Add("Uso de biofertilizantes");
            if (_tech5Checked) technologies.Add("Mejoradores de suelo para complementar fertilización");
            if (_tech6Checked) technologies.Add("Mínimo movimiento de suelo, retención de residuos y rotación de cultivos");
            if (_tech7Checked) technologies.Add("Introducción de nuevos cultivos en la rotación (ejemplo: cultivos de forraje)");
            if (_tech8Checked) technologies.Add("Tecnología para mejorar el almacenamiento del grano");
            Parcel.TechnologiesUsed = technologies;
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

        /// <summary>
        /// The UpdateActivityChecked
        /// </summary>
        private void UpdateActivityChecked()
        {
            var activities = new List<string>();
            if (_activity1Checked) activities.Add("Preparación del terreno");
            if (_activity2Checked) activities.Add("Aplicación de fertilizante foliar");
            if (_activity3Checked) activities.Add("Aplicación de fertilizante orgánico");
            if (_activity4Checked) activities.Add("Fertilización química al suelo");
            if (_activity5Checked) activities.Add("Aplicación de herbicidas después de la siembra");
            if (_activity6Checked) activities.Add("Aplicación de herbicidas presiembra");
            if (_activity7Checked) activities.Add("Labores culturales y control físico de malezas");
            if (_activity8Checked) activities.Add("Aplicación de fungicidas");
            if (_activity9Checked) activities.Add("Aplicación de insecticidas");
            Parcel.Activities = activities;
        }
    }
}