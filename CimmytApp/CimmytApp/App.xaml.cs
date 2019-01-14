namespace CimmytApp
{
    using System;
    using System.Globalization;

    using CimmytApp.Core;
    using CimmytApp.Core.Benchmarking;
    using CimmytApp.Core.Calendar;
    using CimmytApp.Core.Localization;
    using CimmytApp.Core.Map;
    using CimmytApp.Core.Parcel;
    using CimmytApp.Core.Persistence;
    using CimmytApp.StaticContent;
    using CimmytApp.Views;
    using CimmytApp.WeatherForecast;

    using CommonServiceLocator;

    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;

    using Prism;
    using Prism.DryIoc;
    using Prism.Ioc;
    using Prism.Modularity;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    using XF.Material.Forms;

    public partial class App : PrismApplication
    {
        public App()
            : this(null)
        {
            Console.WriteLine("App instantiated");
        }

        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
            Console.WriteLine("App instantiated");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            AppCenterLog.Info("App", "ConfigureModuleCatalog called");
            try
            {
                Type benchmarkingModule = typeof(BenchmarkingModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(benchmarkingModule)
                    {
                        ModuleName = benchmarkingModule.Name,
                        ModuleType = benchmarkingModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type mapModule = typeof(MapModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(mapModule)
                    {
                        ModuleName = mapModule.Name,
                        ModuleType = mapModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type parcelModule = typeof(ParcelModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(parcelModule)
                    {
                        ModuleName = parcelModule.Name,
                        ModuleType = parcelModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type staticContentModule = typeof(StaticContentModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(staticContentModule)
                    {
                        ModuleName = staticContentModule.Name,
                        ModuleType = staticContentModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type weatherForecastModule = typeof(WeatherForecastModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(weatherForecastModule)
                    {
                        ModuleName = weatherForecastModule.Name,
                        ModuleType = weatherForecastModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type calendarModule = typeof(CalendarModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(calendarModule)
                    {
                        ModuleName = calendarModule.Name,
                        ModuleType = calendarModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });
            }
            catch (Exception e)
            {
                AppCenterLog.Info("App", "ConfigureModuleCatalog error", e);
                Crashes.TrackError(e);
            }
            AppCenterLog.Info("App", "ConfigureModuleCatalog finished");
        }

        protected override void OnInitialized()
        {
            AppCenterLog.Info("App", "Starting initialization");

            InitializeComponent();
            AppCenterLog.Info("App", "Initialized component (XAML, Templates and Styles seem fine)");
            Material.Init(this);
            AppCenterLog.Info("App", "Initializes XF.Material");

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS
                || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                ILocalizer localizer = Container.Resolve<ILocalizer>();
                CultureInfo cultureInfo = localizer.GetCurrentCultureInfo();
                localizer.SetLocale(cultureInfo);
            }

            AppCenterLog.Info("App", "Navigating to initial page");
            NavigationService.NavigateAsync(GetInitialPage());
            AppCenterLog.Info("App", "Finished initialization");
        }

        protected override void OnSleep()
        {
            if (this != null)
            {
                base.OnSleep();
            }

            if (ServiceLocator.IsLocationProviderSet)
            {
                try
                {
                    // IPosition geolocator = ServiceLocator.Current.GetInstance<IPosition>(); todo use essentials
                    // geolocator?.StopListening();
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e);
                }
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start(
                $"android={Constants.AppCenterKeyAndroid};ios={Constants.AppCenterKeyIOs}",
                typeof(Analytics),
                typeof(Crashes));

            AppCenterLog.Info("App", "OnStart concluded, AppCenter is initialized");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            AppCenterLog.Info("App", "Registering types");
            try
            {
                containerRegistry.RegisterForNavigation<NavigationPage>();
                containerRegistry.RegisterForNavigation<MainPage>();
                containerRegistry.RegisterForNavigation<ParcelMainPage>();
                containerRegistry.RegisterForNavigation<ProfilePage>();
                containerRegistry.RegisterForNavigation<DevPage>();

                containerRegistry.RegisterLocalization();
                containerRegistry.RegisterSharedContextClasses();
                containerRegistry.RegisterAppDataContext();
                containerRegistry.Register<IAppDataService, AppDataService>();
            }
            catch (Exception e)
            {
                AppCenterLog.Info("App", "Registering types error", e);
                Crashes.TrackError(e);
            }
            AppCenterLog.Info("App", "Finished registering types");
        }

        private string GetInitialPage()
        {
            return "app:///NavigationPage/MapMainPage";
            return "app:///NavigationPage/DevPage";

            if (Preferences.Get(Constants.AppNotFirstLaunch, false))
            {
                NavigationService.NavigateAsync("app:///NavigationPage/MainPage");
            }

            Preferences.Set(Constants.AppNotFirstLaunch, true);
            NavigationService.NavigateAsync("app:///IntroductionPage");
        }
    }
}