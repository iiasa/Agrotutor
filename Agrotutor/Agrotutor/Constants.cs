namespace Agrotutor
{
    using Xamarin.Essentials;

    public static class Constants
    {
        public const string AppNotFirstLaunch = "APP_NotFirstLaunch";

        public const string AppCenterKeyAndroid = "ccbee3dd-42cc-41c9-92cc-664870cd7c0e";
        public const string AppCenterKeyIOs = "58f35007-f37b-45c5-beb6-885f2eca60b7";

        public const int MainMapWeatherRefreshDistance = 2; // Kilometers
        public const int MainMapWeatherRefreshDuration = 5; // Minutes
        public const int MainMapLocationRefreshPeriod = 15; // Seconds
        public const GeolocationAccuracy MainMapLocationAccuracy = GeolocationAccuracy.Medium;

        public const string PlotsLayerVisiblePreference = "MAP_PlotsLayerVisible";
        public const string PlotDelineationsLayerVisiblePreference = "MAP_PlotDelineationsLayerVisible";
        public const string HubContactsLayerVisiblePreference = "MAP_HubContactsLayerVisible";
        public const string MachineryPointsLayerVisiblePreference = "MAP_MachineryPointsLayerVisible";
        public const string InvestigationPlatformsLayerVisiblePreference = "MAP_InvestigationPlatformsLayerVisible";
        public const string OfflineBasemapLayerVisiblePreference = "MAP_OfflineBasemapLayerVisible";


        public const string Plots = "Plots";
        public const string UserLocation = "UserLocation";
        public const string MapData = "MapData";
    }
}