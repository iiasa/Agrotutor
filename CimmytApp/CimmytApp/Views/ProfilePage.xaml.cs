using CimmytApp.ViewModels;
using Xamarin.Forms;

namespace CimmytApp.Views
{
    public partial class ProfilePage : ContentPage
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