namespace CimmytApp.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Plugin.Media;

    using DTO;

    /// <summary>
    /// Defines the <see cref="ProfilePageViewModel" />
    /// </summary>
    public class ProfilePageViewModel : BindableBase
    {
        /// <summary>
        /// Defines the _imageSource
        /// </summary>
        private string _imageSource;

        /// <summary>
        /// Defines the _pictureVisible
        /// </summary>
        private bool _pictureVisible = false;

        /// <summary>
        /// Gets or sets the UserProfile
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// Gets or sets the TakePictureCommand
        /// </summary>
        public DelegateCommand TakePictureCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PictureVisible
        /// </summary>
        public bool PictureVisible { get => _pictureVisible; set => SetProperty(ref _pictureVisible, value); }

        /// <summary>
        /// Gets or sets the ImageSource
        /// </summary>
        public string ImageSource { get => _imageSource; set => SetProperty(ref _imageSource, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePageViewModel"/> class.
        /// </summary>
        public ProfilePageViewModel()
        {
            TakePictureCommand = new DelegateCommand(TakePicture);
            var picture = (string)App.GetProperty("UserPicture");
            if (picture != null)
            {
                ImageSource = picture;
                PictureVisible = true;
            }

            UserProfile = new UserProfile
            {
                UserName = (string)App.GetProperty("UserName"),
                State = (string)App.GetProperty("UserState")
            };
        }

        /// <summary>
        /// The TakePicture
        /// </summary>
        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Cimmyt",
                Name = "user.jpg"
            });

            if (file == null)
                return;

            ImageSource = file.Path;
            PictureVisible = true;
            App.InsertOrUpdateProperty("UserPicture", file.Path);
        }

        /// <summary>
        /// The Save
        /// </summary>
        public void Save()
        {
            App.InsertOrUpdateProperty("UserName", UserProfile.UserName);
            App.InsertOrUpdateProperty("UserState", UserProfile.State);
        }
    }
}