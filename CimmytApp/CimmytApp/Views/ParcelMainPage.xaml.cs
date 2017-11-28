namespace CimmytApp.Views
{
    using CimmytApp.ViewModels;
    using Xamarin.Forms;

    public partial class ParcelMainPage : ContentPage
    {
        private ParcelMainPageViewModel _contextObj;

        public ParcelMainPage()
        {
            InitializeComponent();
            _contextObj = (ParcelMainPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _contextObj.GoBackCommand.Execute();
            return true;
        }
    }
}