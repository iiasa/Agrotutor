namespace CimmytApp.Droid
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using CimmytApp.Core.Localization;
    using Gcm.Client;
    using Plugin.Permissions;
    using Prism;
    using Prism.Ioc;
    using Xamarin;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    [Activity(Label = "Agrotutor", Icon = "@drawable/app_icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
                {
                    RaiseThrowableEventArgs x = args;
                };

                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    object x = args.ExceptionObject;
                };

                // Wire up the unobserved task exception handler
                TaskScheduler.UnobservedTaskException += (sender, args) =>
                {
                    UnobservedTaskExceptionEventArgs x = args;
                };

                if (Device.Idiom == TargetIdiom.Phone) RequestedOrientation = ScreenOrientation.Portrait;

                base.OnCreate(bundle);

                Forms.Init(this, bundle);
                FormsGoogleMaps.Init(this, bundle);

                RegisterWithGCM(); // TODO- Store token and only register when token = null
                LoadApplication(new App(new AndroidInitializer()));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(this, Constants.SenderId);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
        }
    }
}