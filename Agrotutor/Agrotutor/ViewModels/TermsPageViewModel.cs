using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Agrotutor.Core;
using Microsoft.Extensions.Localization;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.ViewModels
{
	public class TermsPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        public TermsPageViewModel(INavigationService navigationService, IStringLocalizer<WebContentPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            Title = "TermsPage";
            _navigationService = navigationService;
            GetStartedCommand = new DelegateCommand(OnGetStartedClick);
            IsNotAccepted = !Preferences.ContainsKey(Constants.TermsAccepted);
        }

        public ICommand PageAppearingCommand => new Command(async () => await PageAppearing());
        private HtmlWebViewSource _termsSource;

        /// <summary>
        ///     Gets or sets the get started command.
        /// </summary>
        /// <value>
        ///     The get started command.
        /// </value>
        public DelegateCommand GetStartedCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is terms accepted.
        /// </summary>
        public bool IsTermsAccepted { get; set; }

        public bool IsNotAccepted { get; set; }


        public HtmlWebViewSource TermsSource
        {
            get => _termsSource;
            set => SetProperty(ref _termsSource, value);
        }

        private async Task PageAppearing()
        {
            TermsSource = new HtmlWebViewSource
                { Html = await FileHelper.ReadTextAsync("Agrotutor.Resources.AppData.TermsText.txt").ConfigureAwait(false) };
            if (Preferences.ContainsKey(Constants.TermsAccepted))
            {
                IsTermsAccepted = Preferences.Get(Constants.TermsAccepted, true);
                IsNotAccepted = !IsTermsAccepted;
            }

        }

        /// <summary>
        ///     Ons the get started click.
        /// </summary>
        public async void OnGetStartedClick()
        {
            if (IsTermsAccepted)
            {
                Preferences.Set(Constants.TermsAccepted, true);
                await _navigationService.NavigateAsync("app:///NavigationPage/MapPage");
            }
            else
            {
                await MaterialDialog.Instance.AlertAsync(
                    "Terms Not Accepted",
                    "Please accept terms and conditions",
                    "ok");
            }
        }
    }
}
