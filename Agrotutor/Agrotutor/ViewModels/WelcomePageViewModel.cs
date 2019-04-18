using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.ViewModels
{
    using System.Windows.Input;
    using Agrotutor.Core;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class WelcomePageViewModel : ViewModelBase
    {
        public WelcomePageViewModel(INavigationService navigationService, IStringLocalizer<WelcomePageViewModel> stringLocalizer)
            : base (navigationService, stringLocalizer)
        { }


        public DelegateCommand PageAppearingCommand =>
            new DelegateCommand(async () => await PageAppearing());

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
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(
            async ()=> {

                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    if (Preferences.ContainsKey(Constants.TermsAccepted))
                    {
                        await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                    }
                    else
                    {
                        await NavigationService.NavigateAsync("TermsPage");
                    }
                }
            });

        
    }
}
