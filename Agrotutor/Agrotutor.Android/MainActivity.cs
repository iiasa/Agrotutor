namespace Agrotutor.Droid
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;
    using Acr.UserDialogs;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Plugin.CurrentActivity;
    using Xamarin;

    [Activity(Label = "Agrotutor", Icon = "@drawable/app_icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AttachExceptionHandlers();
            InitializeLayout();
            base.OnCreate(bundle);
            InitializeLibs(bundle);
            
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitializeLibs(Bundle bundle)
        {
            Forms.SetFlags("FastRenderers_Experimental");
            Forms.Init(this, bundle);

            FormsGoogleMaps.Init(this, bundle);
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, bundle);
            XF.Material.Droid.Material.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
        }

        private void InitializeLayout()
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            if (Device.Idiom == TargetIdiom.Phone)
                RequestedOrientation = ScreenOrientation.Portrait;
        }

        private void AttachExceptionHandlers()
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
        }
    }
}

