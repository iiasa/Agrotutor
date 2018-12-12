namespace CimmytApp.Core.Components.Views
{
    using System.Linq;

    using CimmytApp.WeatherForecast;

    using Xamarin.Forms;

    public partial class WeatherWidget : ContentView
    {
        public static readonly BindableProperty CurrentWeatherProperty = BindableProperty.Create(
            nameof(CurrentWeather),
            typeof(WeatherForecast),
            typeof(WeatherWidget));

        public WeatherWidget()
        {
            InitializeComponent();
        }

        public WeatherForecast CurrentWeather
        {
            get => (WeatherForecast)GetValue(WeatherWidget.CurrentWeatherProperty);
            set
            {
                SetValue(WeatherWidget.CurrentWeatherProperty, value);
                UpdateUI(value);
            }
        }

        private void UpdateUI(WeatherForecast forecast)
        {
            var current = forecast.Location.HourlySummaries.HourlySummary.ElementAt(0);
            this.Icon.Source = current.TinyWxIcon;
            this.Text.Text = current.WxText;
        }
    }
}