using System;
using System.Threading.Tasks;
using CimmytApp.Messaging;
using Xamarin.Forms.Xaml;

namespace CimmytApp.Views
{
    using Xamarin.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool _backButtonRecentlyPressed;

        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_backButtonRecentlyPressed)
                return base.OnBackButtonPressed();

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