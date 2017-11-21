namespace CimmytApp.WeatherForecast.ViewModels
{
    using Helper.DTO.SkywiseWeather.Historical;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class HourlyWeatherDataPageViewModel : BindableBase, INavigationAware
    {
        private HourlySeries _series;
        private string _variableName;

        public HourlySeries Series
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
            Series = (HourlySeries)series;

            parameters.TryGetValue("VariableName", out object variableName);
            VariableName = (string)variableName;
        }
    }
}