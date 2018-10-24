namespace CimmytApp.Core.Benchmarking.Views
{
    using Microcharts;
    using SkiaSharp;
    using Xamarin.Forms;

    public partial class ViewIngresoPage
    {
        public ViewIngresoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Microcharts.Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SKColor.Parse("#90D585")
                }
            };
            var chart = new LineChart() { Entries = entries };
            this.chartView.Chart = chart;
        }
    }
}