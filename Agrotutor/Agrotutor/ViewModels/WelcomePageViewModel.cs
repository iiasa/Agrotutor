namespace Agrotutor.ViewModels
{
    using System;
    using XF.Material.Forms.UI.Dialogs;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;

    public class WelcomePageViewModel : ViewModelBase, INavigatedAware
    {
        private string _startText;

        public string StartText
        {
            get => _startText;
            set => SetProperty(ref _startText, value);
        }

        public WelcomePageViewModel(INavigationService navigationService, IStringLocalizer<WelcomePageViewModel> stringLocalizer)
            : base (navigationService, stringLocalizer)
        {
            StartText = stringLocalizer.GetString("navigateToMap");
        }

        public DelegateCommand NavigateToMainPageCommand => new DelegateCommand(
            async ()=> {

                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                }
                
            });

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("firstLaunch"))
            {
                parameters.TryGetValue<bool>("firstLaunch", out bool firstLaunch);
                StartText = StringLocalizer.GetString(firstLaunch == true ? "start" : "navigateToMap");
            }
            else StartText = StringLocalizer.GetString("navigateToMap");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}
