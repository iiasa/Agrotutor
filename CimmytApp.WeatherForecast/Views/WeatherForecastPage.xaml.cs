namespace CimmytApp.WeatherForecast.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class WeatherForecastPage : ContentPage
    {
        public WeatherForecastPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}