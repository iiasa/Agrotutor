using Prism.Modularity;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Agrotutor
{
    using System.Globalization;
    using Prism;
    using Prism.Ioc;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Xamarin.Essentials;
    using XF.Material.Forms;

    using Core;
    using Core.Localization;

    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) {}

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            VersionTracking.Track();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Material.Init(this);
            InitializeLocalizer();

            await NavigationService.NavigateAsync("NavigationPage/MapPage");
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start(
                $"android={Constants.AppCenterKeyAndroid};ios={Constants.AppCenterKeyIOs}",
                typeof(Analytics),
                typeof(Crashes));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterLocalization();
            containerRegistry.RegisterPersistence();
            containerRegistry.RegisterPages();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.RegisterModules();
        }

        private void InitializeLocalizer()
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS
                || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                ILocalizer localizer = Container.Resolve<ILocalizer>();
                CultureInfo cultureInfo = localizer.GetCurrentCultureInfo();
                localizer.SetLocale(cultureInfo);
            }
        }
    }
}
