using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.InputRenderer.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(RadCalendar), typeof(CalendarRenderer))]

namespace CimmytApp.Droid
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using Gcm.Client;
    using Microsoft.Practices.Unity;
    using Plugin.Permissions;
    using Prism.Unity;
    using Xamarin;
    using Xamarin.Forms.Platform.Android;
    using XamForms.Controls.Droid;

    [Activity(Label = "México Produce", Icon = "@drawable/app_logo", MainLauncher = true,
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

                base.OnCreate(bundle);

                Forms.Init(this, bundle);
                FormsMaps.Init(this, bundle);
                Calendar.Init();

                //base.OnCreate (bundle);

                //// Set your view from the "main" layout resource
                //SetContentView (Resource.Layout.Main);

                //// Get your button from the layout resource,
                //// and attach an event to it
                //Button button = FindViewById<Button> (Resource.Id.myButton);

                RegisterWithGCM(); // TODO Store token and only register when token = null
                LoadApplication(new App(new AndroidInitializer()));
            }
            catch (Exception e)
            {
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
        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}