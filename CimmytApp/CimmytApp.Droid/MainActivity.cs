namespace CimmytApp.Droid
{
    using System;
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using CimmytApp.Core.Localization;
    using Gcm.Client;
    using Java.Lang;
    using Prism;
    using Prism.Ioc;
    using Xamarin;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    [Activity(Label = "Agrotutor", Icon = "@drawable/app_icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private static string TAG = "CIMMYT.DROID";

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info(TAG, "Initializing MainActivity");
            try
            {
                Log.Info(MainActivity.TAG, "Attaching exception handlers");
                AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
                {
                    RaiseThrowableEventArgs x = args;
                    Log.Error("CIMMYT.DROID.DroidEnv", x.Exception as Throwable, "Exception raised.");
                };

                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    object x = args.ExceptionObject;
                    Log.Error("CIMMYT.DROID.AppDomain", x as Throwable, "Exception raised.");
                };

                TaskScheduler.UnobservedTaskException += (sender, args) =>
                {
                    UnobservedTaskExceptionEventArgs x = args;
                    Log.Error("CIMMYT.DROID.AppDomain", x.Exception.GetBaseException() as Throwable, "Exception raised.");
                };

                if (Device.Idiom == TargetIdiom.Phone)
                    RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

                base.OnCreate(bundle);

                Log.Info(MainActivity.TAG, "Initializing Xamarin.Forms");
                Forms.Init(this, bundle);

                // Forms.SetFlags("FastRenderers_Experimental");

                Log.Info(MainActivity.TAG, "Initializing Google Maps");
                FormsGoogleMaps.Init(this, bundle);
                Log.Info(MainActivity.TAG, "Initializing User Dialogs");
                UserDialogs.Init(this);
                Log.Info(MainActivity.TAG, "Initializing XF.Material");
                XF.Material.Droid.Material.Init(this, bundle);
                Log.Info(MainActivity.TAG, "Initializing Xamarin Essentials");
                Xamarin.Essentials.Platform.Init(this, bundle);

                Log.Info(MainActivity.TAG, "Registering GCM");
                RegisterWithGCM(); // TODO- Store token and only register when token = null

                Log.Info(MainActivity.TAG, "Load Application");
                LoadApplication(new App(new AndroidInitializer()));
            }
            catch (System.Exception e)
            {
                Log.Error(TAG, e as Throwable, "Crash during onCreate");
            }
            Log.Info(TAG, "Finished initializing MainActivity");
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            GcmClient.Register(this, Constants.SenderId);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        private static string TAG = "CIMMYT.DROID";

        public void RegisterTypes(IContainerRegistry container)
        {
            Log.Info(TAG, "Doing platform initialization");
            Log.Info(TAG, "Registering localizer");
            container.RegisterSingleton<ILocalizer, Localizer>();

            // Remove after https://github.com/PrismLibrary/Prism/issues/1443 is fixed
            container.RegisterInstance(Forms.Context);
            Log.Info(TAG, "Platform initialization finished");
        }
    }
}