namespace CimmytApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using CimmytApp.Benchmarking;
    using CimmytApp.Calendar;
    using CimmytApp.Parcel;
    using CimmytApp.StaticContent;
    using CimmytApp.Views;
    using CimmytApp.WeatherForecast;
    using CommonServiceLocator;
    using Helper.Map;
    using Helper.Realm;
    using Helper.Realm.BusinessContract;
    using Helper.UserRegistration;
    using Prism;
    using Prism.Ioc;
    using Prism.Modularity;
    using Unity.Lifetime;
    using Xamarin.Forms;

    public partial class App
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            try
            {
                Debug.WriteLine("====== resource debug info =========");
                var assembly = typeof(App).GetTypeInfo().Assembly;
                foreach (var res in assembly.GetManifestResourceNames())
                {
                    Debug.WriteLine("found resource: " + res);
                }

                Debug.WriteLine("====================================");

                //Device.OS marked as obsolete, but proposed Device.RuntimePlatform didn't work last time I checked...

                if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                {
                    //var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                    //Helper.Localization.Resx.AppResources.Culture = ci;
                    //DependencyService.Get<ILocalize>().SetLocale(ci);
                }

                //  CimmytDbOperations.GetAllParcels();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static DTO.Parcel.Parcel CurrentParcel { get; set; }

        public static List<DTO.Parcel.Parcel> Parcels { get; set; }

        public static IDictionary<string, object> GetProperties()
        {
            return Current.Properties;
        }

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
                var benchmarkingModule = typeof(BenchmarkingModule);
                moduleCatalog.AddModule(new ModuleInfo(benchmarkingModule)
                {
                    ModuleName = benchmarkingModule.Name,
                    ModuleType = benchmarkingModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var calendarModule = typeof(CalenderModule);
                moduleCatalog.AddModule(new ModuleInfo(calendarModule)
                {
                    ModuleName = calendarModule.Name,
                    ModuleType = calendarModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var mapModule = typeof(MapModule);
                moduleCatalog.AddModule(new ModuleInfo(mapModule)
                {
                    ModuleName = mapModule.Name,
                    ModuleType = mapModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var parcelModule = typeof(ParcelModule);
                moduleCatalog.AddModule(new ModuleInfo(parcelModule)
                {
                    ModuleName = parcelModule.Name,
                    ModuleType = parcelModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var staticContentModule = typeof(StaticContentModule);
                moduleCatalog.AddModule(new ModuleInfo(staticContentModule)
                {
                    ModuleName = staticContentModule.Name,
                    ModuleType = staticContentModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var userRegistrationModule = typeof(UserRegistrationModule);
                moduleCatalog.AddModule(new ModuleInfo(userRegistrationModule)
                {
                    ModuleName = userRegistrationModule.Name,
                    ModuleType = userRegistrationModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var weatherForecastModule = typeof(WeatherForecastModule);
                moduleCatalog.AddModule(new ModuleInfo(weatherForecastModule)
                {
                    ModuleName = weatherForecastModule.Name,
                    ModuleType = weatherForecastModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (Current.Properties.ContainsKey("not_first_launch"))
                NavigationService.NavigateAsync("app:///MainPage");
            else
            {
                Current.Properties.Add("not_first_launch", true);
                NavigationService.NavigateAsync("app:///WelcomePage");
            }
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            if (ServiceLocator.IsLocationProviderSet)
            {
                try
                {
                    var geolocator = ServiceLocator.Current.GetInstance<IPosition>();
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
    }
}