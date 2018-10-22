namespace CimmytApp.Benchmarking.Views
{
    using Microcharts;
    using SkiaSharp;
    using Xamarin.Forms.DataGrid;

    public partial class ViewCostoPage
    {
        public ViewCostoPage()
        {
            InitializeComponent();
            DataGridComponent.Init();

            var entries = new[]
            {
                new Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Entry(400)
                {
                Label = "February",
                ValueLabel = "400",
                Color = SKColor.Parse("#68B9C0")
                },
                new Entry(-100)
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