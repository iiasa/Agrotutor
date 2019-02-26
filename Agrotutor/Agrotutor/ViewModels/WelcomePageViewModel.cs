using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (permissionStatus != PermissionStatus.Granted)
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
        }

        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(
            async ()=> {

                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                }
                
            });

        
    }
}
