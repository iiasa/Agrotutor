namespace Agrotutor.ViewModels
{
    using System;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms;

    using Core;

    public class LinksPageViewModel : ViewModelBase
    {
        public LinksPageViewModel(INavigationService navigationService, IStringLocalizer<LinksPageViewModel> localizer)
            : base(navigationService, localizer)
        {}

        public DelegateCommand ShowIntroductionCommand => new DelegateCommand(
            () => {
                NavigationService.NavigateAsync("WelcomePage");
            });

        public DelegateCommand ShowLicensesCommand => new DelegateCommand(
            ()=> {
                NavigationService.NavigateAsync("CitationPage");
            });

        public DelegateCommand<string> TapLinkCommand => new DelegateCommand<string>(
            (url)=> {
                Device.OpenUri(new Uri(url));
            });
    }
}
