using Prism.Commands;
using Xamarin.Forms;
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

        private void OnTakePhotoClick()
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