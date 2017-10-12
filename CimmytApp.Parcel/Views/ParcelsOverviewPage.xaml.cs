using CimmytApp.Parcel.ViewModels;
using Xamarin.Forms.Xaml;

namespace CimmytApp.Parcel.Views
{
    using Xamarin.Forms;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcelsOverviewPage : ContentPage
    {
        public ParcelsOverviewPage()
        {
            InitializeComponent();
        }

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var vm = BindingContext as ParcelsOverviewPageViewModel;
			ParcelViewModel selectedItem = e.Item as ParcelViewModel;

			vm?.HideOrShowParcel(selectedItem);
		}

	}
}