namespace CimmytApp.WeatherForecast.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class DailyWeatherDataPage : ContentPage
    {
        public DailyWeatherDataPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}