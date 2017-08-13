namespace CimmytApp.WeatherForecast.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class HourlyWeatherDataPage : ContentPage
    {
        public HourlyWeatherDataPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}