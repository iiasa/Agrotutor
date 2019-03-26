using System;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Agrotutor.Core;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using OxyPlot.Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;
using Plugin.DownloadManager;
using Rg.Plugins.Popup;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid;
using Environment = Android.OS.Environment;
using Forms = Xamarin.Forms.Forms;
using Platform = Xamarin.Essentials.Platform;

namespace Agrotutor.Droid
{
    [Activity(Label = "Agrotutor", Icon = "@drawable/app_icon", Theme = "@style/splashscreen", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AttachExceptionHandlers();
            InitializeLayout();
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);
            InitializeLibs(bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitializeLibs(Bundle bundle)
        {
            Forms.SetFlags("FastRenderers_Experimental");
            CachedImageRenderer.Init(true);
            Popup.Init(this, bundle);
            Forms.Init(this, bundle);
            FileManager.SavingPath =
                ApplicationContext.GetExternalFilesDir(Environment.DirectoryDownloads).AbsolutePath;

            // Donwload manager
            CrossDownloadManager.Current.PathNameForDownloadedFile = file =>
            {
                var fileName = file.Url.GetHashCode().ToString();
                //  string  fileName= Path.GetFileName(file.Url);
                var path = Path.Combine(
                    ApplicationContext.GetExternalFilesDir(Environment.DirectoryDownloads).AbsolutePath, fileName);

                return path;
            };
            FormsGoogleMaps.Init(this, bundle);
            FormsGoogleMapsBindings.Init();
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, bundle);
            Material.Init(this, bundle);
            Popup.Init(this, bundle);
            PlotViewRenderer.Init();
            Platform.Init(this, bundle);
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
        }
    }
}