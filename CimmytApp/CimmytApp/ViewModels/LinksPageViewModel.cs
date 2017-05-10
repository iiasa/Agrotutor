using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace CimmytApp.ViewModels
{
    public class LinksPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private ICommand TapLinkCommand { get; set; }

        public LinksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            TapLinkCommand = new DelegateCommand(OpenLink);
        }

        private static void OpenLink(string uri)
        {
            Device.OpenUri(new Uri(uri));
        }
    }
}