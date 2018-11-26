namespace CimmytApp.ViewModels
{
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class DevPageViewModel : ViewModelBase
    {
        public DevPageViewModel(IStringLocalizer<DevPageViewModel> stringLocalizer, INavigationService navigationService) : base(stringLocalizer)
        {
            this.navigationService = navigationService;
        }
        public DelegateCommand NavigateToMain => new DelegateCommand(
            () => { 
            navigationService.NavigateAsync("MapMainPage"); 
        });
        private INavigationService navigationService;
    }
}
