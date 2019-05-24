using Agrotutor.Core;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace Agrotutor.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private string _appVersion;

        public AboutPageViewModel(INavigationService navigationService, IStringLocalizer<AboutPageViewModel> stringLocalizer) 
            : base(navigationService, stringLocalizer)
        {
            AppVersion = $"{VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
        }

        public string AppVersion
        {
            get => _appVersion;
            set => SetProperty(ref _appVersion, value);
        }

        public DelegateCommand ShowLicensesCommand => new DelegateCommand(
            ()=> {
                NavigationService.NavigateAsync("CitationPage");
            });
    }
}