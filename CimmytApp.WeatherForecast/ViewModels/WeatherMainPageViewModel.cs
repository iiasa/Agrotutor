namespace CimmytApp.WeatherForecast.ViewModels
{
    using CimmytApp.DTO.Parcel;
    using Helper.DTO.SkywiseWeather.Forecast;
    using Prism.Mvvm;
	using Prism.Navigation;

	public class WeatherMainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        public WeatherForecast WeatherForecast
        {
            get;
            set;
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
            if (parameters.ContainsKey("Parcel")){
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel!= null){
					Parcel = (Parcel)parcel;
                    LoadData();
                }
            }

            LoadData();
        }

		private async void LoadData()
		{
			//WeatherForecast = await WeatherForecast.Download(47.800239, 16.292656);
			WeatherForecast = await WeatherForecast.Download(Parcel.Latitude, Parcel.Longitude);
        }
    }
}
