namespace CimmytApp.Views
{
    using System.Threading.Tasks;
    using CimmytApp.Messaging;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool _backButtonRecentlyPressed;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (_backButtonRecentlyPressed)
            {
                return base.OnBackButtonPressed();
            }

            _backButtonRecentlyPressed = true;
            StartBackButtonTimer();
            DependencyService.Get<IMessage>().ShortAlert("Presione una vez más para salir de la aplicación");

            return true;
        }

        private async void StartBackButtonTimer()
        {
            await Task.Delay(2000);
            _backButtonRecentlyPressed = false;
        }
    }
}