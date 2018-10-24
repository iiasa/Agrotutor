namespace CimmytApp.Views
{
    using CimmytApp.ViewModels;
    using Xamarin.Forms;

    public partial class ParcelMainPage
    {
        private readonly ParcelMainPageViewModel bindingContext;

        public ParcelMainPage()
        {
            InitializeComponent();
            bindingContext = (ParcelMainPageViewModel)BindingContext;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            bindingContext.GoBackCommand.Execute();
            return true;
        }
    }
}