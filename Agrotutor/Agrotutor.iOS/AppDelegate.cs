using System;
using System.IO;
using Agrotutor.Core;
using FFImageLoading.Forms.Platform;
using Foundation;
using OxyPlot.Xamarin.Forms.Platform.iOS;
using Plugin.DownloadManager;
using Rg.Plugins.Popup;
using UIKit;
using Xamarin;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS;
using Forms = Xamarin.Forms.Forms;

namespace Agrotutor.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Popup.Init();
            Forms.Init();
            // Download manager
            FileManager.SavingPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            CrossDownloadManager.Current.PathNameForDownloadedFile = file =>
            {
                var fileName = file.Url.GetHashCode().ToString();
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                return path;
            };
            FormsGoogleMaps.Init("AIzaSyCm-_Fc-5-vvbhTPQg38LlCreorYtsC2Us");
            FormsGoogleMapsBindings.Init();
            CachedImageRenderer.Init();
            Material.Init();
            Popup.Init();
            PlotViewRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }
}