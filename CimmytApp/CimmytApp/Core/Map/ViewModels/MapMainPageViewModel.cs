namespace CimmytApp.Core.Map.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Essentials;
    using Xamarin.Forms.GoogleMaps;

    public class MapMainPageViewModel : BindableBase
	{
        public MapMainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationService NavigationService { get; set; }
        public DelegateCommand NavigateToMain => new DelegateCommand(() => NavigationService.NavigateAsync("MainPage"));
        public DelegateCommand ShowSettings => new DelegateCommand(AppInfo.OpenSettings);

        public DelegateCommand<MapClickedEventArgs> MapClickedCommand => new DelegateCommand<MapClickedEventArgs>(
            (args) =>
            {

            });
    }
}
