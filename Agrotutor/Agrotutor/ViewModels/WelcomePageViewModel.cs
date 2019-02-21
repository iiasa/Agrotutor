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

        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(
            async ()=> {

                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                }
                
            });
    }
}
