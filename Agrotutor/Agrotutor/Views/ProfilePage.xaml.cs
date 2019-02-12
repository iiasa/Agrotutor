namespace Agrotutor.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
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