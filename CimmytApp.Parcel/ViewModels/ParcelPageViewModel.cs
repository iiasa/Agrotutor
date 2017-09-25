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
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

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
                _pickerCropTypesSelectedIndex = value;
                if (!EditModeActive) return;
                Parcel.Crop = CropTypes.ElementAt(value);
                Parcel.CropType = (CropType)(value + 1);
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
                _pickerIrrigationTypesSelectedIndex = value;
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
                _pickerYearsSelectedIndex = value;
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
        /// The UpdateTechCheckedUI
        /// </summary>
        private void UpdateTechCheckedUI()
        {
            foreach (var technology in Parcel.TechnologiesUsed)
            {
                switch (technology)
                {
                    case Technology1:
                        SetProperty(ref _tech1Checked, true);
                        break;

                    case Technology2:
                        SetProperty(ref _tech2Checked, true);
                        break;

                    case Technology3:
                        SetProperty(ref _tech3Checked, true);
                        break;

                    case Technology4:
                        SetProperty(ref _tech4Checked, true);
                        break;

                    case Technology5:
                        SetProperty(ref _tech5Checked, true);
                        break;

                    case Technology6:
                        SetProperty(ref _tech6Checked, true);
                        break;

                    case Technology7:
                        SetProperty(ref _tech7Checked, true);
                        break;

                    case Technology8:
                        SetProperty(ref _tech8Checked, true);
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
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Deliniation"))
            {
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