using System;
using Agrotutor.Core;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using Xamarin;

namespace Agrotutor.iOS
{
    using Foundation;
    using System.IO;
    using UIKit;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
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
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            // Download manager
           FileManager.SavingPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            CrossDownloadManager.Current.PathNameForDownloadedFile = new Func<IDownloadFile, string>(file =>
            {
                string fileName = file.Url.GetHashCode().ToString();
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                return path;
            });
            Xamarin.FormsGoogleMaps.Init("AIzaSyCm-_Fc-5-vvbhTPQg38LlCreorYtsC2Us");
            FormsGoogleMapsBindings.Init(); 
            CachedImageRenderer.Init(true);
            XF.Material.iOS.Material.Init();
            Rg.Plugins.Popup.Popup.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }
}
