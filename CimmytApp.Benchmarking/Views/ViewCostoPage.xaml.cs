namespace CimmytApp.Benchmarking.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class ViewCostoPage : ContentPage
    {
        public ViewCostoPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}