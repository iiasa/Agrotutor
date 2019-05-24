namespace Agrotutor.Modules.Plot.ViewModels
{
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Core.Entities;

    public class ActivityPageViewModel : ViewModelBase, INavigatedAware
    {
        private Plot _plot;

        public ActivityPageViewModel(INavigationService navigationService,
            IStringLocalizer<ActivityPageViewModel> localizer) : base(navigationService, localizer)
        {}

        public DelegateCommand<string> ActivityClickedCommand => new DelegateCommand<string>(activityType =>
        {
            NavigationService.NavigateAsync("ActivityDetail", new NavigationParameters
            {
                { "activityType", activityType },
                { "Plot", Plot }
            });
        });

        public Plot Plot
        {
            get => _plot;
            private set => SetProperty(ref _plot, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                Plot = plot;
            }
            base.OnNavigatedTo(parameters);
        }
    }
}
