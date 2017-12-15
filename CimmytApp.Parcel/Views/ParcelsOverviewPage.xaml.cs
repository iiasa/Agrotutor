namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcelsOverviewPage
    {
        private readonly ParcelsOverviewPageViewModel _bindingContext;

        public ParcelsOverviewPage()
        {
            InitializeComponent();
            _bindingContext = BindingContext as ParcelsOverviewPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _bindingContext.RefreshParcelsCommand.Execute();
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.GoBackCommand.Execute();
            return true;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as ParcelViewModel;
            _bindingContext?.HideOrShowParcel(selectedItem);
        }
    }
}