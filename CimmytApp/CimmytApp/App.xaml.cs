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
    using Helper.Map;
    using Helper.Realm;
    using Helper.Realm.BusinessContract;
    using Helper.UserRegistration;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Navigation;
    using Prism.Unity;
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
                if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
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

        protected override void ConfigureModuleCatalog()
        {
            try
            {
                var benchmarkingModule = typeof(BenchmarkingModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = benchmarkingModule.Name,
                    ModuleType = benchmarkingModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var calendarModule = typeof(CalenderModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = calendarModule.Name,
                    ModuleType = calendarModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var mapModule = typeof(MapModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = mapModule.Name,
                    ModuleType = mapModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var parcelModule = typeof(ParcelModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = parcelModule.Name,
                    ModuleType = parcelModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var staticContentModule = typeof(StaticContentModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = staticContentModule.Name,
                    ModuleType = staticContentModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var userRegistrationModule = typeof(UserRegistrationModule);
                ModuleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = userRegistrationModule.Name,
                    ModuleType = userRegistrationModule,
                    InitializationMode = InitializationMode.WhenAvailable
                });

                var weatherForecastModule = typeof(WeatherForecastModule);
                ModuleCatalog.AddModule(new ModuleInfo
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
            {
                NavigationService.NavigateAsync("app:///MainPage");
            }
            else
            {
                Current.Properties.Add("not_first_launch", true);
                var parameters = new NavigationParameters
                {
                    { "ShowGuide", true }
                };
                NavigationService.NavigateAsync("SplashScreenPage", parameters);
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

        protected override void RegisterTypes()
        {
            try
            {
                Container.RegisterTypeForNavigation<MainPage>();
                Container.RegisterTypeForNavigation<LoginPage>();
                Container.RegisterTypeForNavigation<ParcelMainPage>();
                Container.RegisterTypeForNavigation<ProfilePage>();
                Container.RegisterType<ICimmytDbOperations, CimmytDbOperations>(
                    new ContainerControlledLifetimeManager());
                Container.RegisterType<IPosition, LocationBusiness>(new ContainerControlledLifetimeManager());
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}