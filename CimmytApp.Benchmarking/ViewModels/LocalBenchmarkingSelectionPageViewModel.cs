namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class LocalBenchmarkingSelectionPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        private void NavigateAsync(string page)
        {
            _navigationService.NavigateAsync(page);
        }
    }
}