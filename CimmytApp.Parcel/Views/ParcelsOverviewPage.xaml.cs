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
            contextObj = BindingContext as ParcelsOverviewPageViewModel;
        }

        private ParcelsOverviewPageViewModel contextObj { get; set; }

        protected override bool OnBackButtonPressed()
        {
            contextObj.GoBackCommand.Execute();
            return true;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ParcelViewModel selectedItem = e.Item as ParcelViewModel;

            contextObj?.HideOrShowParcel(selectedItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            contextObj.RefreshParcelsCommand.Execute();
        }
    }
}