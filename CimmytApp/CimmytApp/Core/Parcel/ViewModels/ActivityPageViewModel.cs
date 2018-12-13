namespace CimmytApp.Core.Parcel.ViewModels
{
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class ActivityPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private Plot _plot;

        public ActivityPageViewModel(INavigationService navigationService,
            IStringLocalizer<ActivityPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand<string> ActivityClickedCommand => new DelegateCommand<string>(ExecuteMethod);

        public Plot Plot
        {
            get => this._plot;
            private set => SetProperty(ref this._plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                Plot = plot;
            }
        }

        private void ExecuteMethod(string activityType)
        {
            _navigationService.NavigateAsync("ActivityDetail", new NavigationParameters
            {
                { "activityType", activityType },
                { "Plot", Plot }
            });
        }
    }
}