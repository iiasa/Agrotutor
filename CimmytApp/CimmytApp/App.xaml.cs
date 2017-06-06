﻿namespace CimmytApp
{
    using System.Collections.Generic;
    using System.Reflection;
    using Prism.Unity;
    using Xamarin.Forms;

    using Localization;
    using Views;
    using System;
    using Prism.Modularity;
    using DTO;
    using Helper.Map;
    using DTO.Parcel;
    using SQLiteDB;
    using Prism.Navigation;
    using Calendar;

    public partial class App : PrismApplication
    {
        public static Parcel CurrentParcel { get; set; }
        public static List<Parcel> Parcels { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

            //Device.OS marked as obsolete, but proposed Device.RuntimePlatform didn't work last time I checked...
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                Resx.AppResources.Culture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci);
            }

            CimmytDbOperations.GetAllParcels();
        }

        protected override void ConfigureModuleCatalog()
        {
            Type locationeModuleType = typeof(CalenderModuleIntialize);
            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = locationeModuleType.Name,
                    ModuleType = locationeModuleType,
                    InitializationMode = InitializationMode.WhenAvailable
                });

            Type mapModuleIntialize = typeof(MapModuleIntialize);
            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = mapModuleIntialize.Name,
                    ModuleType = mapModuleIntialize,
                    InitializationMode = InitializationMode.WhenAvailable
                });
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var parcel = new Parcel
            {
                ID = 2,
                Crop = "Wheat",
                AgronomicalCycle = 1,
                EstimatedParcelArea = 2.5,
                Cultivar = "Example Cultivar",
                GeoPosition = new GeoPosition
                {
                    Latitude = 46.789,
                    Longitude = 16.78856
                },
                Irrigation = "Irrigated"
            };

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("parcel", parcel);
            NavigationService.NavigateAsync("ParcelPage");
            /*
            if (Current.Properties.ContainsKey("not_first_launch"))
            {
                Current.Properties.Add("not_first_launch", true);
                NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
            }
            else
            {
                NavigationService.NavigateAsync("WelcomePage");
            }*/
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<OfflineTilesDownloadPage>();
            Container.RegisterTypeForNavigation<MapPage>();
            Container.RegisterTypeForNavigation<ParcelPage>();
        }
    }
}