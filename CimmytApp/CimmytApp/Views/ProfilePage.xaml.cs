namespace CimmytApp.Views
{
    using CimmytApp.ViewModels;

    public partial class ProfilePage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ProfilePageViewModel)BindingContext).Save();
        }
    }
}