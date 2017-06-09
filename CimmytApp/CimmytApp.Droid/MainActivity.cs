namespace CimmytApp.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Util;
    using Gcm.Client;
    using Prism.Unity;
    using Microsoft.Practices.Unity;

    [Activity(Label = "CimmytApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            //base.OnCreate (bundle);

            //// Set your view from the "main" layout resource
            //SetContentView (Resource.Layout.Main);

            //// Get your button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            RegisterWithGCM();
            LoadApplication(new App(new AndroidInitializer()));
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(this, Constants.SenderID);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}