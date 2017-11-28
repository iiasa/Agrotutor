namespace CimmytApp.WeatherForecast.ViewModels
{
    using Helper.DTO.SkywiseWeather.Historical;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class HourlyWeatherDataPageViewModel : BindableBase, INavigationAware
    {
        public const string SeriesParameterName = "Series";

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
            if (!parameters.ContainsKey(SeriesParameterName))
            {
                VariableName = "Lo sentimos, no hay datos disponibles";
                return;
            }

            parameters.TryGetValue(SeriesParameterName, out object series);
            Series = (HourlySeries)series;

            parameters.TryGetValue("VariableName", out object variableName);
            VariableName = (string)variableName;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}