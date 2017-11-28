namespace CimmytApp.StaticContent.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    public class LinksPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public LinksPageViewModel(INavigationService navigationService)
        {
            TapLinkCommand = new Command(OpenLink);
            _navigationService = navigationService;
            ShowIntroductionCommand = new DelegateCommand(ShowIntroduction);
            ShowLicensesCommand = new DelegateCommand(ShowLicenses);
        }

        public DelegateCommand ShowIntroductionCommand { get; set; }

        public DelegateCommand ShowLicensesCommand { get; set; }

        public ICommand TapLinkCommand { get; set; }

        private static void OpenLink(object url)
        {
            Device.OpenUri(new Uri((string)url));
        }

        private void ShowIntroduction()
        {
            _navigationService.NavigateAsync("WelcomePage");
        }

        private void ShowLicenses()
        {
            _navigationService.NavigateAsync("CitationPage");
        }
    }
}