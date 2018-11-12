namespace CimmytApp.iOS
{
    using CimmytApp.Core.Localization;
    using Flurl.Http;
    using Foundation;
    using IIASA.FloodCitiSense.ApiClient;
    using Prism;
    using Prism.Ioc;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

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
            Forms.Init();
            ConfigureFlurlHttp();

            InstallFontPlugins();
            // FormsPlugin.Iconize.iOS.IconControls.Init(); TODO might be necessary
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private static void ConfigureFlurlHttp()
        {
            FlurlHttp.Configure(c =>
            {
                c.HttpClientFactory = new ModernHttpClientFactory();
            });
        }

        private static void InstallFontPlugins()
        {
            Plugin.Iconize
                .Iconize
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule());
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            container.RegisterSingleton<ILocalizer, Localizer>();
        }
    }
}