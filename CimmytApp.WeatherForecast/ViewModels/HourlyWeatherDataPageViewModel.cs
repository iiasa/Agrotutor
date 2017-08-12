namespace CimmytApp.WeatherForecast.ViewModels
{
    using Prism.Mvvm;
    using Prism.Navigation;

    using Helper.DTO.SkywiseWeather.Historical;

    public class HourlyWeatherDataPageViewModel : BindableBase, INavigationAware
    {
        private HourlySeries _series;

        public HourlySeries Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public HourlyWeatherDataPageViewModel()
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("Series")) return;
            parameters.TryGetValue("Series", out object series);
            Series = (HourlySeries)series;
        }
    }
}