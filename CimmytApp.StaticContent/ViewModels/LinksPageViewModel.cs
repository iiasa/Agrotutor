using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace CimmytApp.StaticContent.ViewModels
{
    public class LinksPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand TapLinkCommand { get; set; }

        public LinksPageViewModel(INavigationService navigationService)
        {
            TapLinkCommand = new Command(OpenLink);
            _navigationService = navigationService;
        }

        private static void OpenLink(object url)
        {
            Device.OpenUri(new Uri((string)url));
        }
    }
}