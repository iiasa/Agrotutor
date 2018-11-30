namespace CimmytApp.Core.Components.Views
{
    using System;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Prism.Commands;
    using Xamarin.Forms;

    public partial class CameraPicture : ContentView
    {
        private bool takePhotoActive;

        public static readonly BindableProperty IconTextProperty =
            BindableProperty.Create(nameof(IconText), typeof(string), typeof(CameraPicture), default(string));

        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(CameraPicture));

        public static readonly BindableProperty OnPictureTakenProperty =
            BindableProperty.Create(nameof(OnPictureTaken), typeof(DelegateCommand<MediaFile>), typeof(CameraPicture));

        public static readonly BindableProperty FileNameProperty =
            BindableProperty.Create(nameof(FileName), typeof(string), typeof(CameraPicture), string.Empty);

        public static readonly BindableProperty FilePathProperty =
            BindableProperty.Create(nameof(FilePath), typeof(string), typeof(CameraPicture), string.Empty);

        public string IconText
        {
            get => (string)GetValue(CameraPicture.IconTextProperty);
            set => SetValue(CameraPicture.IconTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(CameraPicture.IconSourceProperty);
            set => SetValue(CameraPicture.IconSourceProperty, value);
        }

        public string FileName
        {
            get => (string)GetValue(CameraPicture.FileNameProperty);
            set => SetValue(CameraPicture.FileNameProperty, value);
        }

        public string FilePath
        {
            get => (string)GetValue(CameraPicture.FilePathProperty);
            set => SetValue(CameraPicture.FilePathProperty, value);
        }

        public DelegateCommand<MediaFile> OnPictureTaken
        {
            get => (DelegateCommand<MediaFile>)GetValue(CameraPicture.OnPictureTakenProperty);
            set => SetValue(CameraPicture.OnPictureTakenProperty, value);
        }

        public CameraPicture()
        {
            InitializeComponent();

            this.takePhotoActive = false;

            this.ui.SetBinding(IconWithText.IconTextProperty, new Binding(nameof(IconText), source: this));
            this.ui.SetBinding(IconWithText.IconSourceProperty, new Binding(nameof(IconSource), source: this));

            this.ui.Command = new DelegateCommand(async() =>
            {
                if (this.takePhotoActive) return;

                takePhotoActive = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    this.takePhotoActive = false;
                    return;
                }
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = FilePath,
                    Name = FileName + $"_{DateTime.Now:s}" + ".png"
                });

                this.takePhotoActive = false;

                if (file != null)
                {
                    if (OnPictureTaken != null)
                    {
                        OnPictureTaken.Execute(file);
                    }
                } });
        }
    }
}
