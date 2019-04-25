using System;
using Microsoft.AppCenter.Crashes;

namespace Agrotutor.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Localization;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Essentials;

    using Core;
    using Core.Entities;

    public class ProfilePageViewModel : ViewModelBase
    {
        /// <summary>
        ///     Defines the _imageSource
        /// </summary>
        private string _imageSource;

        /// <summary>
        ///     Defines the _pictureVisible
        /// </summary>
        private bool _pictureVisible;

        /// <summary>
        ///     Defines the _statesSelectedIndex
        /// </summary>
        private int _statesSelectedIndex = -1;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProfilePageViewModel" /> class.
        /// </summary>
        public ProfilePageViewModel(INavigationService navigationService, IStringLocalizer<ProfilePageViewModel> stringLocalizer)
        : base(navigationService, stringLocalizer)
        {
            TakePictureActive = false;
            TakePictureCommand = new DelegateCommand(TakePicture);
            var picture = Preferences.Get("UserPicture", null);
            if (picture != null)
            {
                ImageSource = picture;
                PictureVisible = true;
            }

            UserProfile = new UserProfile
            {
                UserName = (string)Preferences.Get("UserName", null),
                State = (string)Preferences.Get("UserState", null)
            };

            if (UserProfile?.State != null && UserProfile.State != "")
            {
                for (var i = 0; i < States.Count; i++)
                {
                    if (States.ElementAt(i) != UserProfile.State)
                    {
                        continue;
                    }

                    StatesSelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the ImageSource
        /// </summary>
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether PictureVisible
        /// </summary>
        public bool PictureVisible
        {
            get => _pictureVisible;
            set => SetProperty(ref _pictureVisible, value);
        }

        /// <summary>
        ///     Gets or sets the States
        /// </summary>
        public List<string> States { get; set; } = new List<string>
    {
        "México",
        "Ciudad de México",
        "Veracruz",
        "Jalisco",
        "Puebla",
        "Guanajuato",
        "Chiapas",
        "Nuevo León",
        "Michoacán",
        "Oaxaca",
        "Chihuahua",
        "Guerrero",
        "Tamaulipas",
        "Baja California",
        "Sinaloa",
        "Coahuila de Zaragoza",
        "Hidalgo",
        "Sonora",
        "San Luis Potosí",
        "Tabasco",
        "Yucatán",
        "Querétaro de Arteaga",
        "Morelos",
        "Durango",
        "Zacatecas",
        "Quintana Roo",
        "Aguascalientes",
        "Tlaxcala",
        "Campeche",
        "Nayarit",
        "Baja California Sur",
        "Colima"
    };

        /// <summary>
        ///     Gets or sets the StatesSelectedIndex
        /// </summary>
        public int StatesSelectedIndex
        {
            get => _statesSelectedIndex;
            set
            {
                SetProperty(ref _statesSelectedIndex, value);
                UserProfile.State = value == -1 ? "" : States.ElementAt(value);
            }
        }

        /// <summary>
        ///     Gets or sets the TakePictureCommand
        /// </summary>
        public DelegateCommand TakePictureCommand { get; set; }

        /// <summary>
        ///     Gets or sets the UserProfile
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        ///     The Save
        /// </summary>
        public void Save()
        {
            Preferences.Set("UserName", UserProfile.UserName);
            Preferences.Set("UserState", UserProfile.State);
        }

        private bool TakePictureActive;

        /// <summary>
        ///     The TakePicture
        /// </summary>
        private async void TakePicture()
        {
            if (TakePictureActive) return;
            TakePictureActive = true;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            MediaFile file = null;

            try
            {
                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Cimmyt",
                    Name = "user.jpg"
                });
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }

            if (file != null)
            {
                ImageSource = file.Path;
                PictureVisible = true;
                Preferences.Set("UserPicture", file.Path);
            }

            TakePictureActive = false;
        }
    }
}

