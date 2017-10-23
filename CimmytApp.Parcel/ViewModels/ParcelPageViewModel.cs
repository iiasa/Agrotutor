﻿namespace CimmytApp.Parcel.ViewModels
{
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XLabs.Ioc;
    using XLabs.Platform.Device;
    using XLabs.Platform.Services.Media;

    using BusinessContract;
    using DTO;
    using DTO.Parcel;

    /// <summary>
    /// Defines the <see cref="ParcelPageViewModel" />
    /// </summary>
    public class ParcelPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Gets or sets the DeleteParcelCommand
        /// </summary>
        public DelegateCommand DeleteParcelCommand { get; set; }

        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

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
                UpdateSelections();
            }
        }

        /// <summary>
        /// The UpdateSelections
        /// </summary>
        private void UpdateSelections()
        {
            for (int i = 0; i < CropTypes.Count; i++)
            {
                if (CropTypes[i] == Parcel.Crop)
                {
                    PickerCropTypesSelectedIndex = i;
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

            for (int i = 0; i < ClimateTypes.Count; i++)
            {
                if (ClimateTypes[i] == Parcel.ClimateType)
                {
                    PickerClimateTypesSelectedIndex = i;
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
        /// The ChooseLocation
        /// </summary>
        public void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                {"Latitude", _parcel.Latitude},
                {"Longitude", _parcel.Longitude},
                {"ChooseLocation", true},
                {"parcelId",_parcel.ParcelId }
            };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

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
        /// Gets the CropTypes
        /// </summary>
        public List<string> CropTypes { get; }
    = new List<string> { "Maíz", "Cebada", "Frijol", "Trigo", "Triticale", "Sorgo", "Alfalfa", "Avena", "Ajonjolí", "Amaranto", "Arroz", "Canola", "Cartamo", "Calabacín", "Garbanzo", "Haba", "Soya", "Ninguno", "Otro" };

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
        /// Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickChooseLocation { get; set; }

        /// <summary>
        /// Gets or sets the ClickChooseLocation
        /// </summary>
        public DelegateCommand ClickGetLocation { get; set; }

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
        public ParcelPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            ClickPhoto = new DelegateCommand(OnTakePhotoClick);
            ClickSave = new DelegateCommand(SaveParcel);
            DeliniateParcelCommand = new DelegateCommand(DeliniateParcel);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
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
        private void GetLocation()
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
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

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
        /// Defines the _showEditToggle
        /// </summary>
        private bool _showEditToggle;

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

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
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
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null) Parcel = (Parcel)parcel;
            }
            if (parameters.ContainsKey("EditEnabled"))
            {
                ShowEditToggle = false;
                parameters.TryGetValue("EditEnabled", out var editEnabled);
                if (editEnabled != null) EditModeActive = (bool)editEnabled;
            }
            if (parameters.ContainsKey("Deliniation"))
            {
                EditModeActive = false;
                parameters.TryGetValue("Deliniation", out var deliniation);
                //   Parcel.SetDeliniation((List<GeoPosition>)deliniation);
                var polygonObj = new PolygonDto { ListPoints = (List<GeoPosition>)deliniation };
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
                                             //  _cimmytDbOperations.UpdateParcel(Parcel);
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue("GeoPosition", out var geoPosition);
                if (geoPosition == null) return;
                var position = (Helper.Base.DTO.GeoPosition)geoPosition;
                Parcel.Latitude = position.Latitude;
                Parcel.Longitude = position.Longitude;
                _cimmytDbOperations.UpdateParcel(Parcel);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowEditToggle
        /// </summary>
        public bool ShowEditToggle { get => _showEditToggle; set => SetProperty(ref _showEditToggle, value); }
    }
}