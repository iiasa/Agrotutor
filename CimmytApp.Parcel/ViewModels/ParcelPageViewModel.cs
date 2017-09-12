using Prism.Commands;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;


using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;
    using DTO.Parcel;

    public class ParcelPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;

        public Parcel Parcel
        {
            get => _parcel;
            set => SetProperty(ref _parcel, value);
        }

		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

		public ImageSource ImageSource
		{
			get
			{
				return _imageSource;
			}
			set
			{
				SetProperty(ref _imageSource, value);
			}
		}

		private ImageSource _imageSource;
        private bool _editModeActive;

        public DelegateCommand ClickPhoto { get; set; }

        public bool EditModeActive
        {
            get => _editModeActive;
            set => SetProperty(ref _editModeActive, value);
        }

        public ParcelPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            ClickPhoto = new DelegateCommand(OnTakePhotoClick);
        }

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


		private IMediaPicker _mediaPicker;

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

        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler IsActiveChanged;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        protected override IDataset GetDataset()
        {
            return Parcel;
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }
    }
}