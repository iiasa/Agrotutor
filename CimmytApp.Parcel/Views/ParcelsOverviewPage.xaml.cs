namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcelsOverviewPage : ContentPage
    {
        public ParcelsOverviewPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ParcelsOverviewPageViewModel vm = BindingContext as ParcelsOverviewPageViewModel;
            ParcelViewModel selectedItem = e.Item as ParcelViewModel;

            vm?.HideOrShowParcel(selectedItem);
        }
    }
}