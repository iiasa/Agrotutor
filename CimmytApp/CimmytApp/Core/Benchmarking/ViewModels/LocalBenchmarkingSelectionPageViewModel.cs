namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class LocalBenchmarkingSelectionPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService,
            IStringLocalizer<LocalBenchmarkingSelectionPageViewModel> localizer) : base(localizer)
        {
            this.navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        private void NavigateAsync(string page)
        {
            this.navigationService.NavigateAsync(page);
        }
    }
}