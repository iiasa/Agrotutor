namespace CimmytApp.ViewModels
{
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class ParcelMainPageViewModel : ViewModelBase, INavigatedAware
    {
        public IAppDataService AppDataService { get; set; }

        private readonly INavigationService _navigationService;
        private Plot plot;

        public ParcelMainPageViewModel(INavigationService navigationService, IAppDataService appDataService,
            IStringLocalizer<ParcelMainPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            AppDataService = appDataService;
        }

        public DelegateCommand GoBackCommand { get; set; }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public DelegateCommand NavigateToMapCommand { get; set; }

        public Plot Plot
        {
            get => this.plot;
            set => SetProperty(ref this.plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out Plot plot);
                if (plot != null)
                {
                    Plot = plot;
                }
                else this._navigationService.GoBackAsync();
            }
        }
    }
}