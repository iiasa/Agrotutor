namespace CimmytApp.Parcel.ViewModels
{
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO;
    using DTO.Parcel;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XLabs.Ioc;
    using XLabs.Platform.Device;
    using XLabs.Platform.Services.Media;

    /// <summary>
    /// Defines the <see cref="ParcelPageViewModel" />
    /// </summary>
    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        /// <summary>
        /// Gets or sets the DeleteParcelCommand
        /// </summary>
        public DelegateCommand DeleteParcelCommand { get; set; }

        /// <summary>
        /// Defines the Technology1
        /// </summary>
        private const string Technology1 = "Cambio a variedades mejoradas, nuevas y adaptadas a las zonas con potencial para incrementar el rendimiento ";

        /// <summary>
        /// Defines the Technology2
        /// </summary>
        private const string Technology2 = "Interpretación y uso del análisis de suelo";

        /// <summary>
        /// Defines the Technology3
        /// </summary>
        private const string Technology3 = "Uso del sensor infrarrojo para fertilización óptima";

        /// <summary>
        /// Defines the Technology4
        /// </summary>
        private const string Technology4 = "Uso de biofertilizantes";

        /// <summary>
        /// Defines the Technology5
        /// </summary>
        private const string Technology5 = "Mejoradores de suelo para complementar fertilización";

        /// <summary>
        /// Defines the Technology6
        /// </summary>
        private const string Technology6 = "Mínimo movimiento de suelo, retención de residuos y rotación de cultivos";

        /// <summary>
        /// Defines the Technology7
        /// </summary>
        private const string Technology7 = "Introducción de nuevos cultivos en la rotación (ejemplo: cultivos de forraje)";

        /// <summary>
        /// Defines the Technology8
        /// </summary>
        private const string Technology8 = "Tecnología para mejorar el almacenamiento del grano";

        /// <summary>
        /// Defines the Activity1
        /// </summary>
        private const string Activity1 = "Preparación del terreno";

        /// <summary>
        /// Defines the Activity2
        /// </summary>
        private const string Activity2 = "Aplicación de fertilizante foliar";

        /// <summary>
        /// Defines the Activity3
        /// </summary>
        private const string Activity3 = "Aplicación de fertilizante orgánico";

        /// <summary>
        /// Defines the Activity4
        /// </summary>
        private const string Activity4 = "Fertilización química al suelo";

        /// <summary>
        /// Defines the Activity5
        /// </summary>
        private const string Activity5 = "Aplicación de herbicidas después de la siembra";

        /// <summary>
        /// Defines the Activity6
        /// </summary>
        private const string Activity6 = "Aplicación de herbicidas presiembra";

        /// <summary>
        /// Defines the Activity7
        /// </summary>
        private const string Activity7 = "Labores culturales y control físico de malezas";

        /// <summary>
        /// Defines the Activity8
        /// </summary>
        private const string Activity8 = "Aplicación de fungicidas";

        /// <summary>
        /// Defines the Activity9
        /// </summary>
        private const string Activity9 = "Aplicación de insecticidas";

        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        /// The SetProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The <see cref="T"/></param>
        /// <param name="value">The <see cref="T"/></param>
        /// <param name="propertyName">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            /*if (typeof(T) != Parcel.GetType())
            {
                EditsDone = true;
            }*/
            return base.SetProperty(ref storage, value, propertyName);
        }

        /// <summary>
        /// Gets or sets a value indicating whether EditsDone
        /// </summary>
        public bool EditsDone { get; set; }

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                UpdateTechCheckedUI();
                UpdateActivityCheckedUI();
                UpdateSelections();
            }
        }

        /// <summary>
        /// The UpdateSelections
        /// </summary>
        private void UpdateSelections()
        {
            switch (Parcel.AgriculturalCycle)
            {
                case "Primavera-Verano":
                    PickerAgriculturalCycleSelectedIndex = 0;
                    break;

                case "Otoño-Invierno":
                    PickerAgriculturalCycleSelectedIndex = 1;
                    break;
            }

            for (int i = 0; i < CropTypes.Count; i++)
            {
                if (CropTypes[i] == Parcel.Crop)
                {
                    PickerCropTypesSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < IrrigationTypes.Count; i++)
            {
                if (IrrigationTypes[i] == Parcel.Irrigation)
                {
                    PickerIrrigationTypesSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < Years.Count; i++)
            {
                if (Years[i] == Parcel.Year)
                {
                    PickerYearsSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < MaturityClasses.Count; i++)
            {
                if (MaturityClasses[i] == Parcel.MaturityClass)
                {
                    PickerMaturityClassesSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < StorageTypes.Count; i++)
            {
                if (StorageTypes[i] == Parcel.StorageType)
                {
                    PickerStorageTypesSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < SowingTypes.Count; i++)
            {
                if (SowingTypes[i] == Parcel.SowingType)
                {
                    PickerSowingTypesSelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < ClimateTypes.Count; i++)
            {
                if (ClimateTypes[i] == Parcel.ClimateType)
                {
                    PickerClimateTypesSelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < HarvestingTypes.Count; i++)
            {
                if (HarvestingTypes[i] == Parcel.HarvestingType)
                {
                    PickerHarvestingTypesSelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// The DeliniateParcel
        /// </summary>
        public void DeliniateParcel()
        {
            var parameters = new NavigationParameters
            {
                {"Latitude", _parcel.Latitude},
                {"Longitude", _parcel.Longitude},
                {"GetPolygon", true},
                {"parcelId",_parcel.ParcelId }
            };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// Gets the AgriculturalCycles
        /// </summary>
        public List<string> AgriculturalCycles { get; }

= new List<string> { "Primavera-Verano", "Otoño-Invierno" };

        /// <summary>
        /// Gets or sets the Years
        /// </summary>
        public List<string> Years { get => _years; set => SetProperty(ref _years, value); }

        /// <summary>
        /// Gets or sets the PickerCropTypesSelectedIndex
        /// </summary>
        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
                if (!EditModeActive) return;
                Parcel.Crop = CropTypes.ElementAt(value);
                Parcel.CropType = (CropType)(value + 1);
            }
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
                if (!EditModeActive) return;
                Parcel.SowingType = SowingTypes.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets or sets the PickerHarvestingTypesSelectedIndex
        /// </summary>
        public int PickerHarvestingTypesSelectedIndex
        {
            get => _pickerHarvestingTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerHarvestingTypesSelectedIndex, value);
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
                Parcel.StorageType = StorageTypes.ElementAt(value);
            }
        }

        /// <summary>
        /// Gets or sets the PickerMaturityClassesSelectedIndex
        /// </summary>
        public int PickerMaturityClassesSelectedIndex
        {
            get => _pickerMaturityClassesSelectedIndex;
            set
            {
                SetProperty(ref _pickerMaturityClassesSelectedIndex, value);
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
                Parcel.ClimateType = ClimateTypes.ElementAt(value);
            }
        }

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
        /// Gets or sets the PickerIrrigationTypesSelectedIndex
        /// </summary>
        public int PickerIrrigationTypesSelectedIndex
        {
            get => _pickerIrrigationTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerIrrigationTypesSelectedIndex, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _pickerYearsSelectedIndex, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech1Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech2Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech3Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech4Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech5Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech6Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech7Checked, value);
                if (!EditModeActive) return;
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
                SetProperty(ref _tech8Checked, value);
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
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
                if (!EditModeActive) return;
                UpdateActivityChecked();
            }
        }

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
        /// The UpdateTechChecked
        /// </summary>
        private void UpdateTechChecked()
        {
            var technologies = new List<string>();
            if (_tech1Checked) technologies.Add(Technology1);
            if (_tech2Checked) technologies.Add(Technology2);
            if (_tech3Checked) technologies.Add(Technology3);
            if (_tech4Checked) technologies.Add(Technology4);
            if (_tech5Checked) technologies.Add(Technology5);
            if (_tech6Checked) technologies.Add(Technology6);
            if (_tech7Checked) technologies.Add(Technology7);
            if (_tech8Checked) technologies.Add(Technology8);
            Parcel.TechnologiesUsed = technologies;
        }

        /// <summary>
        /// The UpdateActivityCheckedUI
        /// </summary>
        private void UpdateActivityCheckedUI()
        {
            if (Parcel.Activities == null) return;
            foreach (var activity in Parcel.Activities)
            {
                switch (activity)
                {
                    case Activity1:
                        Activity1Checked = true;
                        break;

                    case Activity2:
                        Activity2Checked = true;
                        break;

                    case Activity3:
                        Activity3Checked = true;
                        break;

                    case Activity4:
                        Activity4Checked = true;
                        break;

                    case Activity5:
                        Activity5Checked = true;
                        break;

                    case Activity6:
                        Activity6Checked = true;
                        break;

                    case Activity7:
                        Activity7Checked = true;
                        break;

                    case Activity8:
                        Activity8Checked = true;
                        break;

                    case Activity9:
                        Activity8Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// The UpdateTechCheckedUI
        /// </summary>
        private void UpdateTechCheckedUI()
        {
            if (Parcel.TechnologiesUsed == null) return;
            foreach (var technology in Parcel.TechnologiesUsed)
            {
                switch (technology)
                {
                    case Technology1:
                        Tech1Checked = true;
                        break;

                    case Technology2:
                        Tech2Checked = true;
                        break;

                    case Technology3:
                        Tech3Checked = true;
                        break;

                    case Technology4:
                        Tech4Checked = true;
                        break;

                    case Technology5:
                        Tech5Checked = true;
                        break;

                    case Technology6:
                        Tech6Checked = true;
                        break;

                    case Technology7:
                        Tech7Checked = true;
                        break;

                    case Technology8:
                        Tech8Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Defines the _years
        /// </summary>
        private List<string> _years;

        /// <summary>
        /// Defines the _singleYears
        /// </summary>
        private readonly List<string> _singleYears = new List<string>() { "2015", "2016", "2017" };

        /// <summary>
        /// Defines the _doubleYears
        /// </summary>
        private readonly List<string> _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };

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
        /// Gets the SowingTypes
        /// </summary>
        public List<string> SowingTypes { get; }

            = new List<string> { "Resiembra", "Siembra" };

        /// <summary>
        /// Defines the _pickerAgriculturalCycleSelectedIndex
        /// </summary>
        private int _pickerAgriculturalCycleSelectedIndex;

        /// <summary>
        /// Gets or sets the PickerAgriculturalCycleSelectedIndex
        /// </summary>
        public int PickerAgriculturalCycleSelectedIndex
        {
            get => _pickerAgriculturalCycleSelectedIndex;
            set
            {
                _pickerAgriculturalCycleSelectedIndex = value;
                if (!EditModeActive) return;
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);
                var yrs = Years;
                yrs.Clear();
                yrs.AddRange(value == 0 ? _singleYears : _doubleYears);

                Years = yrs;
            }
        }

        /// <summary>
        /// Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickChooseLocation { get; set; }

        /// <summary>
        /// Defines the _scheduler
        /// </summary>
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        /// Gets or sets the ImageSource
        /// </summary>
        public ImageSource ImageSource { get => _imageSource; set => SetProperty(ref _imageSource, value); }

        /// <summary>
        /// Defines the _imageSource
        /// </summary>
        private ImageSource _imageSource;

        /// <summary>
        /// Defines the _editModeActive
        /// </summary>
        private bool _editModeActive;

        /// <summary>
        /// Gets or sets the ClickPhoto
        /// </summary>
        public DelegateCommand ClickPhoto { get; set; }

        /// <summary>
        /// Gets or sets the ClickSave
        /// </summary>
        public DelegateCommand ClickSave { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EditModeActive
        /// </summary>
        public bool EditModeActive { get => _editModeActive; set => SetProperty(ref _editModeActive, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelPageViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        public ParcelPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations) : base(eventAggregator)
        {
            _navigationService = navigationService;
            Years = _singleYears;
            _cimmytDbOperations = cimmytDbOperations;
            ClickPhoto = new DelegateCommand(OnTakePhotoClick);
            ClickSave = new DelegateCommand(SaveParcel);
            DeliniateParcelCommand = new DelegateCommand(DeliniateParcel);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            DeleteParcelCommand = new DelegateCommand(DeleteParcel);
            EditModeActive = false;
        }

        /// <summary>
        /// The DeleteParcel
        /// </summary>
        private void DeleteParcel()
        {
            var parameters = new NavigationParameters { { "Parcel", Parcel } };
            _navigationService.NavigateAsync("DeleteParcelPage", parameters);
        }

        /// <summary>
        /// The ChooseLocation
        /// </summary>
        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "GetLocation", true } };
            if (Parcel.Latitude != 0 && Parcel.Longitude != 0)
            {
                parameters.Add("Center", new GeoPosition(Parcel.Latitude, Parcel.Longitude));
            }
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        /// <summary>
        /// Gets or sets the DeliniateParcelCommand
        /// </summary>
        public DelegateCommand DeliniateParcelCommand { get; set; }

        /// <summary>
        /// The SaveParcel
        /// </summary>
        private void SaveParcel()
        {
            _cimmytDbOperations.UpdateParcel(Parcel);
            PublishDataset(Parcel);
            EditModeActive = false;
            EditsDone = false;
        }

        /// <summary>
        /// The Setup
        /// </summary>
        private void Setup()
        {
            if (_mediaPicker != null)
            {
                return;
            }

            var device = Resolver.Resolve<IDevice>();

            ////RM: hack for working on windows phone?
            _mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
        }

        /// <summary>
        /// Defines the _mediaPicker
        /// </summary>
        private IMediaPicker _mediaPicker;

        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private INavigationService _navigationService;

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
        /// The OnTakePhotoClick
        /// </summary>
        private async void OnTakePhotoClick()
        {
            var _mediaPicker = DependencyService.Get<IMediaPicker>();

            Setup();

            ImageSource = null;

            await this._mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var s = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    var canceled = true;
                }
                else
                {
                    var mediaFile = t.Result;

                    ImageSource = ImageSource.FromStream(() => mediaFile.Source);

                    return mediaFile;
                }

                return null;
            }, _scheduler);
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

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

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Deliniation"))
            {
                EditModeActive = false;
                object deliniation;
                parameters.TryGetValue("Deliniation", out deliniation);
                //   Parcel.SetDeliniation((List<GeoPosition>)deliniation);
                PolygonDto polygonObj = new PolygonDto();
                polygonObj.ListPoints = (List<GeoPosition>)deliniation;
                if (polygonObj.ListPoints.Count > 0)
                {
                    Parcel.Latitude = polygonObj.ListPoints.ElementAt(0).Latitude;
                    Parcel.Longitude = polygonObj.ListPoints.ElementAt(0).Longitude;
                }
                Parcel.SetDeliniation(polygonObj.ListPoints);
                //if (polygonObj.ListPoints != null && polygonObj.ListPoints.Count > 2)
                //{
                //    NeedsDeliniation = false;
                //}
                _cimmytDbOperations.SaveParcelPolygon(Parcel.ParcelId, polygonObj);

                //var res=_cimmytDbOperations.GetAllParcels();
                OnPropertyChanged("Parcel"); //TODO improve this...
                PublishDataset(_parcel);//TODO improve this..
                                        //  _cimmytDbOperations.UpdateParcel(Parcel);
            }
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
        /// The GetDataset
        /// </summary>
        /// <returns>The <see cref="IDataset"/></returns>
        protected override IDataset GetDataset()
        {
            return Parcel;
        }

        /// <summary>
        /// The ReadDataset
        /// </summary>
        /// <param name="dataset">The <see cref="IDataset"/></param>
        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }
    }
}