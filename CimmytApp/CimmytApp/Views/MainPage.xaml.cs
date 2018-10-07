namespace CimmytApp.Views
{
    using System.Threading.Tasks;
    using CimmytApp.Messaging;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private bool backButtonRecentlyPressed;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.backButtonRecentlyPressed)
            {
                return base.OnBackButtonPressed();
            }

            this.backButtonRecentlyPressed = true;
            StartBackButtonTimer();
            DependencyService.Get<IMessage>().ShortAlert("Presione una vez más para salir de la aplicación");

            return true;
        }

        private async void StartBackButtonTimer()
        {
            await Task.Delay(2000);
            this.backButtonRecentlyPressed = false;
        }
    }
}