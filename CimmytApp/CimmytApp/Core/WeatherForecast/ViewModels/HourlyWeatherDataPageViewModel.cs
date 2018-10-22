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
            if (!parameters.ContainsKey(HourlyWeatherDataPageViewModel.SeriesParameterName))
            {
                VariableName = "Lo sentimos, no hay datos disponibles";
                return;
            }

            parameters.TryGetValue<HourlySeries>(HourlyWeatherDataPageViewModel.SeriesParameterName, out var series);
            Series = series;

            parameters.TryGetValue<string>("VariableName", out var variableName);
            VariableName = variableName;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}