using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Agrotutor.Core;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Localization;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        public new IStringLocalizer<WelcomePageViewModel> StringLocalizer { get; }

        private ObservableCollection<WelcomeModel> _items;

        public WelcomePageViewModel(INavigationService navigationService,
            IStringLocalizer<WelcomePageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            StringLocalizer = stringLocalizer;
        }

        public ObservableCollection<WelcomeModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        public DelegateCommand PageAppearingCommand =>
            new DelegateCommand(async () => await PageAppearing());

        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(
            async () =>
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    if (Preferences.ContainsKey(Constants.TermsAccepted))
                        await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                    else
                        await NavigationService.NavigateAsync("app:///NavigationPage/TermsPage");
                }
            });

        private async Task PageAppearing()
        {
            try
            {
                var permissionsToRequest = new List<Permission>();
                var locationPermissionStatus =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (locationPermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Location);
                var cameraPermissionStatus =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (cameraPermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Camera);
                var storagePermissionStatus =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (storagePermissionStatus != PermissionStatus.Granted)
                    permissionsToRequest.Add(Permission.Storage);
                if (permissionsToRequest.Count > 0)
                    await CrossPermissions.Current.RequestPermissionsAsync(permissionsToRequest.ToArray());

                //Items = new ObservableCollection<WelcomeModel> {
                //    new WelcomeModel {
                //        Picture = "advice.png",
                //        Label = _stringLocalizer.GetString("welcome1")
                //    },
                //    new WelcomeModel {
                //        Picture = "advice.png",
                //        Label = _stringLocalizer.GetString("welcome2")

                //    },
                //    new WelcomeModel {
                //        Picture = "advice.png",
                //        Label = _stringLocalizer.GetString("welcome3")

                //    },
                //    new WelcomeModel {
                //        Picture = "advice.png",
                //        Label = _stringLocalizer.GetString("welcome4")

                //    },
                //    new WelcomeModel {
                //        ShowGetStarted = true
                //    }
                //};
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}