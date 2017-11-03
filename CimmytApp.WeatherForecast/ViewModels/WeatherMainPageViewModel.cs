namespace CimmytApp.WeatherForecast.ViewModels
{
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.Parcel;

    public class WeatherMainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private WeatherForecast _weatherForecast;

        public WeatherForecast WeatherForecast
        {
            get => _weatherForecast;
            set => SetProperty(ref _weatherForecast, value);
        }

        public Parcel Parcel { get; private set; }

        public WeatherMainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = (Parcel)parcel;
                    LoadData();
                }
            }

            LoadData();
        }

        private async void LoadData()
        {
            WeatherForecast = await WeatherForecast.Download(47.800239, 16.292656);
            //WeatherForecast = await WeatherForecast.Download(Parcel.Latitude, Parcel.Longitude);
        }
    }
}