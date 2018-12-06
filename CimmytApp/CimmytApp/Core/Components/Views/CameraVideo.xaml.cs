namespace CimmytApp.Core.Components.Views
{
    using System;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Prism.Commands;
    using Xamarin.Forms;

    public partial class CameraVideo : ContentView
    {
        private bool takeVideoActive;

        public static readonly BindableProperty IconTextProperty =
            BindableProperty.Create(nameof(IconText), typeof(string), typeof(CameraVideo), default(string));

        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(CameraVideo));

        public static readonly BindableProperty OnVideoTakenProperty =
            BindableProperty.Create(nameof(OnVideoTaken), typeof(DelegateCommand<MediaFile>), typeof(CameraVideo));

        public static readonly BindableProperty FileNameProperty =
            BindableProperty.Create(nameof(FileName), typeof(string), typeof(CameraVideo), string.Empty);

        public static readonly BindableProperty FilePathProperty =
            BindableProperty.Create(nameof(FilePath), typeof(string), typeof(CameraVideo), string.Empty);

        public string IconText
        {
            get => (string)GetValue(CameraVideo.IconTextProperty);
            set => SetValue(CameraVideo.IconTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(CameraVideo.IconSourceProperty);
            set => SetValue(CameraVideo.IconSourceProperty, value);
        }

        public string FileName
        {
            get => (string)GetValue(CameraVideo.FileNameProperty);
            set => SetValue(CameraVideo.FileNameProperty, value);
        }

        public string FilePath
        {
            get => (string)GetValue(CameraVideo.FilePathProperty);
            set => SetValue(CameraVideo.FilePathProperty, value);
        }

        public DelegateCommand<MediaFile> OnVideoTaken
        {
            get => (DelegateCommand<MediaFile>)GetValue(CameraVideo.OnVideoTakenProperty);
            set => SetValue(CameraVideo.OnVideoTakenProperty, value);
        }

        public CameraVideo()
        {
            InitializeComponent();

            this.takeVideoActive = false;

            this.ui.SetBinding(IconWithText.IconTextProperty, new Binding(nameof(IconText), source: this));
            this.ui.SetBinding(IconWithText.IconSourceProperty, new Binding(nameof(IconSource), source: this));

            this.ui.Command = new DelegateCommand(async() =>
            {
                if (this.takeVideoActive) return;

                takeVideoActive = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    this.takeVideoActive = false;
                    return;
                }
                MediaFile file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions()
                {
                    Name = FileName + $"_{DateTime.Now:s}" + ".png"
                });

                this.takeVideoActive = false;

                if (file != null)
                {
                    OnVideoTaken?.Execute(file);
                } });
        }
    }
}
