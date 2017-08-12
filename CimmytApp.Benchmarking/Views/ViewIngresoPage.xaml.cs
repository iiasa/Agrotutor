namespace CimmytApp.Benchmarking.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.DataGrid;

    public partial class ViewIngresoPage : ContentPage
    {
        public ViewIngresoPage()
        {
            InitializeComponent();
            DataGridComponent.Init();
        }
    }
}