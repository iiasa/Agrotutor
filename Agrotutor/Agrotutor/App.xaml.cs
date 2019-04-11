using System;
using System.Threading.Tasks;
using Agrotutor.Core;
using Agrotutor.Core.Localization;
using Agrotutor.Modules.Ciat.ViewModels;
using Agrotutor.Modules.PriceForecasting.ViewModels;
using Agrotutor.ViewModels;
using Agrotutor.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MonkeyCache.SQLite;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Plugin.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms;
using Device = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Agrotutor
{

    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        private static volatile App _instance;
        private static readonly object _SyncRoot = new object();
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            VersionTracking.Track();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
#if DEBUG
            HotReloader.Current.Start(this);
#endif
            Material.Init(this);
            InitializeLocalizer();
            Barrel.ApplicationId = "AgroTutor";
            var initialPage = "NavigationPage/MapPage";
            if (VersionTracking.IsFirstLaunchEver)
            {
                initialPage = "NavigationPage/WelcomePage";
            }

            await NavigationService.NavigateAsync(initialPage);
        }

        protected override async void OnStart()
        {
            base.OnStart();

            AppCenter.Start(
                $"android={Constants.AppCenterKeyAndroid};ios={Constants.AppCenterKeyIOs}",
                typeof(Analytics),
                typeof(Crashes));
            await Analytics.SetEnabledAsync(true);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterLocalization();
            containerRegistry.RegisterPersistence();
            containerRegistry.RegisterPages();
            containerRegistry.RegisterCameraService();
            containerRegistry.RegisterForNavigation<DevPage, DevPageViewModel>();
            containerRegistry.RegisterTileService();

            containerRegistry.RegisterForNavigation<TermsPage, TermsPageViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.RegisterModules();
        }

        private void InitializeLocalizer()
        {
            if (Device.RuntimePlatform == Device.iOS
                || Device.RuntimePlatform == Device.Android)
            {
                var localizer = Container.Resolve<ILocalizer>();
                var cultureInfo = localizer.GetCurrentCultureInfo();
                localizer.SetLocale(cultureInfo);
            }
        }
    }
}