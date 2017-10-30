namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    /// Defines the <see cref="LocalBenchmarkingSelectionPageViewModel" />
    /// </summary>
    public class LocalBenchmarkingSelectionPageViewModel : BindableBase
    {
        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalBenchmarkingSelectionPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
        }

        /// <summary>
        /// Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        /// The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string"/></param>
        private void NavigateAsync(string page)
        {
            _navigationService.NavigateAsync(page);
        }
    }
}