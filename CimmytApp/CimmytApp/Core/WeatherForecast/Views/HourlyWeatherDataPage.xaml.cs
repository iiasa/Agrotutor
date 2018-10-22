namespace CimmytApp.WeatherForecast.Views
{
    using Xamarin.Forms.DataGrid;

    public partial class HourlyWeatherDataPage
    {
        public HourlyWeatherDataPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}