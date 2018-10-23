namespace CimmytApp.Core.Parcel.Views
{
    using CimmytApp.Core.Parcel.ViewModels;
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
            this._bindingContext = BindingContext as ParcelsOverviewPageViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this._bindingContext.RefreshParcelsCommand.Execute();
        }

        protected override bool OnBackButtonPressed()
        {
            this._bindingContext.GoBackCommand.Execute();
            return true;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as ParcelViewModel;
            this._bindingContext?.HideOrShowParcel(selectedItem);
        }
    }
}