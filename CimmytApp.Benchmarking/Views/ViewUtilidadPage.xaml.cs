namespace CimmytApp.Benchmarking.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class ViewUtilidadPage : ContentPage
    {
        public ViewUtilidadPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}