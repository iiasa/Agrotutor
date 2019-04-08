namespace Agrotutor
{
    using Xamarin.Essentials;

    public static class Constants
    {
        public const string LocalDBFilename = "localdata.db";
        public const string OfflineBasemapFilename = "mexico-simple.mbtiles";

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
        public const string ShowSatelliteTileLayerVisiblePreference = "MAP_SatelliteTileLayerVisible";
        public const string LastUploadDatePreference = "LastUploadDate";
        public const string Lat = "Lat";
        public const string Lng = "Lng";

        public const string HubsContactFile = "Agrotutor.Resources.AppData.hubs_contact.geojson";
        public const string InvestigationPlatformsFile = "Agrotutor.Resources.AppData.investigation_platforms.geojson";
        public const string MachineryPointsFile = "Agrotutor.Resources.AppData.machinery_points.geojson";

        public const string BemUsername = "cimmy2018";
        public const string BemPassword = "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO";
        public const string BemBaseUrl = "http://104.239.158.49/cimmytapiv2.php";
        public const string MatrizBaseUrl = "http://104.239.158.49/matrizv2.php";
        public const string BemToken = "E31C5F8478566357BA6875B32DC59";

        public const string WeatherAppId = "949a7457";
        public const string WeatherAppKey = "5851174f1a3e6e1af42f5895098f69f8";
        public const string WeatherForecastApiBaseUrl = "https://skywisefeeds.wdtinc.com/feeds/api/mega.php";
        public const string WeatherHistoryApiBaseUrl = "https://wsgi.geo-wiki.org/skywise_weather";
        
        public const string AWhereWeatherAPIUsername = "vRLd6W5w1DdeDjClZmgbvYeRn87tnHcp";
        public const string AWhereWeatherAPIPassword = "ugoBqiZnAcM1suFu";

        public const string Plots = "Plots";
        public const string UserLocation = "UserLocation";
        public const string MapData = "MapData";
        public const string DownloadTileUrl = "https://static.geo-wiki.org/tiles/mexico_5.mbtiles";
        //https://static.geo-wiki.org/tiles/mexico_guanajuato.mbtiles
        public const string TermsAccepted = "TermsAccepted";

        public const string FeedbackEmail = "to@be.set";
        public const string UploadDataUrl = "http://147.125.53.3:45459/api/plots/CreatePlot";
    }
}