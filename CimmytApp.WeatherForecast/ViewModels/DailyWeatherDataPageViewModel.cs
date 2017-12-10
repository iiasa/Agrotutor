namespace CimmytApp.WeatherForecast.ViewModels
{
    using Helper.DTO.SkywiseWeather.Historical;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class DailyWeatherDataPageViewModel : BindableBase, INavigationAware
    {
        private DailySeries _series;
        private string _variableName;

        public DailySeries Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public string VariableName
        {
            get => _variableName;
            set => SetProperty(ref _variableName, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("Series"))
            {
                VariableName = "Lo sentimos, no hay datos disponibles";
                return;
            }

            parameters.TryGetValue("Series", out object series);
            Series = (DailySeries)series;

            parameters.TryGetValue("VariableName", out object variableName);
            VariableName = (string)variableName;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}