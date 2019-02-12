namespace Agrotutor.ViewModels
{
    using System;
    using Prism.Commands;
    using Xamarin.Forms;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    using Core;

    public class CitationPageViewModel : ViewModelBase
    {
        public CitationPageViewModel(INavigationService navigationService, IStringLocalizer<LinksPageViewModel> localizer)
            : base(navigationService, localizer)
        {}

        public DelegateCommand<string> TapLinkCommand => new DelegateCommand<string>(
            (url) => {
                Device.OpenUri(new Uri(url));
            });
    }
}
