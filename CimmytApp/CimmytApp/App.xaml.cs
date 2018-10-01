namespace CimmytApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using CimmytApp.Benchmarking;
    using CimmytApp.Core.Localization;
    using CimmytApp.Parcel;
    using CimmytApp.StaticContent;
    using CimmytApp.Views;
    using CimmytApp.WeatherForecast;
    using CommonServiceLocator;
    using Helper.Calendar;
    using Helper.Map;
    using Helper.Realm;
    using Helper.Realm.BusinessContract;
    using Helper.UserRegistration;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Prism;
    using Prism.DryIoc;
    using Prism.Ioc;
    using Prism.Modularity;
    using Xamarin.Forms;
    using Xamarin.Live.Reload;

    public partial class App: PrismApplication
    {
        public App():this(null)
        {
        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
#if DEBUG
            LiveReload.Init();
#endif
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS ||
                Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                ILocalizer localizer = Container.Resolve<ILocalizer>();
                CultureInfo cultureInfo = localizer.GetCurrentCultureInfo();
                localizer.SetLocale(cultureInfo);
            }

            NavigationService.NavigateAsync(getInitialPage());
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("android=ccbee3dd-42cc-41c9-92cc-664870cd7c0e;ios=58f35007-f37b-45c5-beb6-885f2eca60b7", typeof(Analytics),
                typeof(Crashes));
        }

        public static DTO.Parcel.Parcel CurrentParcel { get; set; }

        public static List<DTO.Parcel.Parcel> Parcels { get; set; }

        public static object GetProperty(string propertyName)
        {
            return Current.Properties.ContainsKey(propertyName) ? Current.Properties[propertyName] : null;
        }

        public static void InsertOrUpdateProperty(string propertyName, object value)
        {
            if (Current.Properties.ContainsKey(propertyName))
            {
                Current.Properties[propertyName] = value;
            }
            else
            {
                Current.Properties.Add(propertyName, value);
            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            try
            {
                Type benchmarkingModule = typeof(BenchmarkingModule);
                moduleCatalog.AddModule(new ModuleInfo(benchmarkingModule)
                {
                    ModuleName = benchmarkingModule.Name,
                    ModuleType = benchmarkingModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type mapModule = typeof(MapModule);
                moduleCatalog.AddModule(new ModuleInfo(mapModule)
                {
                    ModuleName = mapModule.Name,
                    ModuleType = mapModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type parcelModule = typeof(ParcelModule);
                moduleCatalog.AddModule(new ModuleInfo(parcelModule)
                {
                    ModuleName = parcelModule.Name,
                    ModuleType = parcelModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type staticContentModule = typeof(StaticContentModule);
                moduleCatalog.AddModule(new ModuleInfo(staticContentModule)
                {
                    ModuleName = staticContentModule.Name,
                    ModuleType = staticContentModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type userRegistrationModule = typeof(UserRegistrationModule);
                moduleCatalog.AddModule(new ModuleInfo(userRegistrationModule)
                {
                    ModuleName = userRegistrationModule.Name,
                    ModuleType = userRegistrationModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type weatherForecastModule = typeof(WeatherForecastModule);
                moduleCatalog.AddModule(new ModuleInfo(weatherForecastModule)
                {
                    ModuleName = weatherForecastModule.Name,
                    ModuleType = weatherForecastModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                Type calendarModule = typeof(CalendarModule);
                moduleCatalog.AddModule(new ModuleInfo(calendarModule)
                {
                    ModuleName = calendarModule.Name,
                    ModuleType = calendarModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });
            }
            catch (Exception e)
            {
                // ignored
            }
        }



        protected override void OnSleep()
        {
            base.OnSleep();

            if (ServiceLocator.IsLocationProviderSet)
            {
                try
                {
                    IPosition geolocator = ServiceLocator.Current.GetInstance<IPosition>();
                    geolocator?.StopListening();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            try
            {
                containerRegistry.RegisterForNavigation<NavigationPage>();
                containerRegistry.RegisterForNavigation<MainPage>();
                containerRegistry.RegisterForNavigation<LoginPage>();
                containerRegistry.RegisterForNavigation<ParcelMainPage>();
                containerRegistry.RegisterForNavigation<ProfilePage>();
                containerRegistry.Register<ICimmytDbOperations, CimmytDbOperations>();
                containerRegistry.Register<IPosition, LocationBusiness>();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private string getInitialPage()
        {
            return "app:///NavigationPage/MainPage";
            if (Current.Properties.ContainsKey("not_first_launch"))
                NavigationService.NavigateAsync("app:///NavigationPage/MainPage");
            else
            {
                Current.Properties.Add("not_first_launch", true);
                NavigationService.NavigateAsync("app:///NavigationPage/IntroductionPage");
            }
        }
    }
}