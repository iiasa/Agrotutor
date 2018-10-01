namespace CimmytApp.Droid
{
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Util;
    using Flurl.Http;
    using IIASA.FloodCitiSense.ApiClient;
    using Xamarin.Forms.Platform.Android;

    [Activity(Label = "Agrotutor", Icon = "@drawable/app_icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : FormsAppCompatActivity
    {
        private static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            ConfigureFlurlHttp();
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(SimulateStartup);
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        private async void SimulateStartup()
        {
            Log.Debug(SplashActivity.TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(200); // Simulate a bit of startup work.
            Log.Debug(SplashActivity.TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        private static void ConfigureFlurlHttp()
        {
            FlurlHttp.Configure(c =>
            {
                c.HttpClientFactory = new ModernHttpClientFactory();
            });
        }
    }
}