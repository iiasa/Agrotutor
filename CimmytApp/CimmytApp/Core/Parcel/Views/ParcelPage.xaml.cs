namespace CimmytApp.Core.Parcel.Views
{
    using CimmytApp.Core.Parcel.ViewModels;
    using Xamarin.Forms;

    public partial class ParcelPage
    {
        private readonly ParcelPageViewModel bindingContext;

        public ParcelPage()
        {
            InitializeComponent();
            bindingContext = (ParcelPageViewModel)BindingContext;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            bindingContext.GoBackCommand.Execute();
            return true;
        }
    }
}