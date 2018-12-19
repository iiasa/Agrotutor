namespace CimmytApp.Views
{
    using System;
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private bool backButtonRecentlyPressed;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.backButtonRecentlyPressed)
            {
                return base.OnBackButtonPressed();
            }
            
            this.backButtonRecentlyPressed = true;
            StartBackButtonTimer();
            UserDialogs.Instance.Toast(new ToastConfig("Presione una vez más para salir de la aplicación")
            {
                Duration = TimeSpan.FromSeconds(2)
            });

            //Position pos = new Position();
            //pos.CalculateDistance(new Position(), GeolocatorUtils.DistanceUnits.Kilometers);

            return true;
        }

        private async void StartBackButtonTimer()
        {
            await Task.Delay(2000);
            this.backButtonRecentlyPressed = false;
        }
    }
}