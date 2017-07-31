namespace CimmytApp.Benchmarking.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class LocalBenchmarkingPage : ContentPage
    {
        public LocalBenchmarkingPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}