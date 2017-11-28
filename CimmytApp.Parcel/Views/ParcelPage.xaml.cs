namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;
    using Xamarin.Forms;

    public partial class ParcelPage : ContentPage
    {
        private ParcelPageViewModel _contentObj;

        public ParcelPage()
        {
            InitializeComponent();
            _contentObj = (ParcelPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _contentObj.GoBackCommand.Execute();
            return true;
        }
    }
}