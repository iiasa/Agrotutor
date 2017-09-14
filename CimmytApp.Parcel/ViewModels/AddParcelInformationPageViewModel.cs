namespace CimmytApp.Parcel.ViewModels
{
    using DTO.Parcel;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XLabs.Ioc;
    using XLabs.Platform.Device;
    using XLabs.Platform.Services.Media;

    using BusinessContract;
    using DTO;

    /// <summary>
    /// Defines the <see cref="AddParcelInformationPageViewModel" />
    /// </summary>
    public class AddParcelInformationPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        /// Defines the isActive
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Gets or sets the DeliniateParcelCommand
        /// </summary>
        public DelegateCommand DeliniateParcelCommand { get; set; }

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private INavigationService _navigationService;

        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _needDeliniation
        /// </summary>
        private bool _needDeliniation;

        /// <summary>
        /// Gets or sets the ClickPhoto
        /// </summary>
        public DelegateCommand ClickPhoto { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether NeedsDeliniation
        /// </summary>
        public bool NeedsDeliniation
        {
            get => _needDeliniation;
            set => SetProperty(ref _needDeliniation, value);
        }

        /// <summary>
        /// Gets or sets the ImageSource
        /// </summary>
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        /// <summary>
        /// Defines the _imageSource
        /// </summary>
        private ImageSource _imageSource;

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
        /// The OnTakePhotoClick
        /// </summary>
        private async void OnTakePhotoClick()
        {
            _mediaPicker = DependencyService.Get<IMediaPicker>();

            Setup();

            ImageSource = null;

            await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
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
        /// Defines the _scheduler
        /// </summary>
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddParcelInformationPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations"/></param>
        public AddParcelInformationPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations) : base(eventAggregator)
        {
            DeliniateParcelCommand = new DelegateCommand(DeliniateParcel).ObservesCanExecute(o => NeedsDeliniation);
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;

            ClickPhoto = new DelegateCommand(OnTakePhotoClick);
        }

        /// <summary>
        /// The DeliniateParcel
        /// </summary>
        private void DeliniateParcel()
        {
            NeedsDeliniation = false;
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
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive && !value && _parcel != null)
                {
                    PublishDataset(_parcel);
                }
                isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                _parcel = value;
                OnPropertyChanged("Parcel");
            }
        }

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
        }

        /// <summary>
        /// The OnNavigatingTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The GetDataset
        /// </summary>
        /// <returns>The <see cref="IDataset"/></returns>
        protected override IDataset GetDataset()
        {
            return _parcel;
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="aName">The <see cref="string"/></param>
        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        /// <summary>
        /// The ReadDataset
        /// </summary>
        /// <param name="dataset">The <see cref="IDataset"/></param>
        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
            CheckDeliniation();
        }

        /// <summary>
        /// The CheckDeliniation
        /// </summary>
        private void CheckDeliniation()
        {
            if (Parcel != null)
            {
                if (Parcel.Polygon != null && Parcel.Polygon.ListPoints.Count > 0)
                {
                    NeedsDeliniation = false;
                }
                else
                {
                    NeedsDeliniation = true;
                }
            }
        }
    }
}