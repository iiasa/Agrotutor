namespace CimmytApp.WeatherForecast.Views
{
    using Xamarin.Forms.DataGrid;

    public partial class WeatherForecastPage
    {
        public WeatherForecastPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}