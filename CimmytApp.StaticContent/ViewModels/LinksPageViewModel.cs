using Prism.Commands;

namespace CimmytApp.StaticContent.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    public class LinksPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand TapLinkCommand { get; set; }
        public DelegateCommand ShowIntroductionCommand { get; set; }

        public LinksPageViewModel(INavigationService navigationService)
        {
            TapLinkCommand = new Command(OpenLink);
            _navigationService = navigationService;
            ShowIntroductionCommand = new DelegateCommand(ShowIntroduction);
        }

        private void ShowIntroduction()
        {
            _navigationService.NavigateAsync("WelcomePage");
        }

        private static void OpenLink(object url)
        {
            Device.OpenUri(new Uri((string)url));
        }
    }
}