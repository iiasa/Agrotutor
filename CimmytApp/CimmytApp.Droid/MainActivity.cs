using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Gcm.Client;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Android.Runtime;
using System.Threading.Tasks;
using Plugin.Permissions;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.Android.CalendarRenderer))]
namespace CimmytApp.Droid
{
    [Activity(Label = "México Produce", Icon = "@drawable/app_logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
                {
                    var x = args;
                };

                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    var x = args.ExceptionObject;
                };

                // Wire up the unobserved task exception handler
                TaskScheduler.UnobservedTaskException += (sender, args) =>
                {
                    var x = args;
                };

                TabLayoutResource = Resource.Layout.tabs;
                ToolbarResource = Resource.Layout.toolbar;

                base.OnCreate(bundle);

                global::Xamarin.Forms.Forms.Init(this, bundle);
                Xamarin.FormsMaps.Init(this, bundle);
                XamForms.Controls.Droid.Calendar.Init();
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}