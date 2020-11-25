namespace Agrotutor
{
    using Xamarin.Essentials;

    /// <summary>
    /// 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The local database filename
        /// </summary>
        public const string LocalDBFilename = "localdata.db";
        /// <summary>
        /// The offline basemap filename
        /// </summary>
        public const string OfflineBasemapFilename = "mexico-simple.mbtiles";

        /// <summary>
        /// The application center key android
        /// </summary>
        public const string AppCenterKeyAndroid = "ccbee3dd-42cc-41c9-92cc-664870cd7c0e";
        /// <summary>
        /// The application center key i os
        /// </summary>
        public const string AppCenterKeyIOs = "58f35007-f37b-45c5-beb6-885f2eca60b7";

        /// <summary>
        /// The main map weather refresh distance
        /// </summary>
        public const int MainMapWeatherRefreshDistance = 2; // Kilometers
        /// <summary>
        /// The main map weather refresh duration
        /// </summary>
        public const int MainMapWeatherRefreshDuration = 5; // Minutes
        /// <summary>
        /// The main map location refresh period
        /// </summary>
        public const int MainMapLocationRefreshPeriod = 15; // Seconds
        /// <summary>
        /// The main map location accuracy
        /// </summary>
        public const GeolocationAccuracy MainMapLocationAccuracy = GeolocationAccuracy.Medium;

        /// <summary>
        /// The plots layer visible preference
        /// </summary>
        public const string PlotsLayerVisiblePreference = "MAP_PlotsLayerVisible";
        /// <summary>
        /// The plot delineations layer visible preference
        /// </summary>
        public const string PlotDelineationsLayerVisiblePreference = "MAP_PlotDelineationsLayerVisible";
        /// <summary>
        /// The hub contacts layer visible preference
        /// </summary>
        public const string HubContactsLayerVisiblePreference = "MAP_HubContactsLayerVisible";
        /// <summary>
        /// The machinery points layer visible preference
        /// </summary>
        public const string MachineryPointsLayerVisiblePreference = "MAP_MachineryPointsLayerVisible";
        /// <summary>
        /// The investigation platforms layer visible preference
        /// </summary>
        public const string InvestigationPlatformsLayerVisiblePreference = "MAP_InvestigationPlatformsLayerVisible";
        /// <summary>
        /// The offline basemap layer visible preference
        /// </summary>
        public const string OfflineBasemapLayerVisiblePreference = "MAP_OfflineBasemapLayerVisible";
        /// <summary>
        /// The show satellite tile layer visible preference
        /// </summary>
        public const string ShowSatelliteTileLayerVisiblePreference = "MAP_SatelliteTileLayerVisible";
        /// <summary>
        /// The last upload date preference
        /// </summary>
        public const string LastUploadDatePreference = "LastUploadDate";
        /// <summary>
        /// The Device ID Preference
        /// </summary>
        public const string DeviceIdPreference = "DeviceId";
        /// <summary>
        /// The lat
        /// </summary>
        public const string Lat = "Lat";
        /// <summary>
        /// The LNG
        /// </summary>
        public const string Lng = "Lng";

        /// <summary>
        /// The hubs contact file
        /// </summary>
        public const string HubsContactFile = "Agrotutor.Resources.AppData.hubs_contact.geojson";
        /// <summary>
        /// The investigation platforms file
        /// </summary>
        public const string InvestigationPlatformsFile = "Agrotutor.Resources.AppData.investigation_platforms.geojson";
        /// <summary>
        /// The machinery points file
        /// </summary>
        public const string MachineryPointsFile = "Agrotutor.Resources.AppData.machinery_points.geojson";

        /// <summary>
        /// The bem username
        /// </summary>
        public const string BemUsername = "cimmy2018";
        /// <summary>
        /// The bem password
        /// </summary>
        public const string BemPassword = "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO";
        /// <summary>
        /// The bem base URL
        /// </summary>
        public const string BemBaseUrl = "http://104.239.158.49/cimmytapiv2.php";
        /// <summary>
        /// The matriz base URL
        /// </summary>
        public const string MatrizBaseUrl = "http://104.239.158.49/matrizv2.php";
        /// <summary>
        /// The bem token
        /// </summary>
        public const string BemToken = "E31C5F8478566357BA6875B32DC59";

        /// <summary>
        /// The weather application identifier
        /// </summary>
        public const string WeatherAppId = "949a7457";
        /// <summary>
        /// The weather application key
        /// </summary>
        public const string WeatherAppKey = "5851174f1a3e6e1af42f5895098f69f8";
        /// <summary>
        /// The weather forecast API base URL
        /// </summary>
        public const string WeatherForecastApiBaseUrl = "https://skywisefeeds.wdtinc.com/feeds/api/mega.php";
        /// <summary>
        /// The weather history API base URL
        /// </summary>
        public const string WeatherHistoryApiBaseUrl = "https://wsgi.geo-wiki.org/skywise_weather";

        /// <summary>
        /// a where weather API username
        /// </summary>
        public const string AWhereWeatherAPIUsername = "vRLd6W5w1DdeDjClZmgbvYeRn87tnHcp";
        /// <summary>
        /// a where weather API password
        /// </summary>
        public const string AWhereWeatherAPIPassword = "ugoBqiZnAcM1suFu";

        /// <summary>
        /// The plots
        /// </summary>
        public const string Plots = "Plots";
        /// <summary>
        /// The user location
        /// </summary>
        public const string UserLocation = "UserLocation";
        /// <summary>
        /// The map data
        /// </summary>
        public const string MapData = "MapData";
        /// <summary>
        /// The download tile URL
        /// </summary>
        public const string DownloadTileUrl = "https://static.geo-wiki.org/tiles/mexico_5.mbtiles";
        //https://static.geo-wiki.org/tiles/mexico_guanajuato.mbtiles
        /// <summary>
        /// The terms accepted
        /// </summary>
        public const string TermsAccepted = "TermsAccepted";

        /// <summary>
        /// The feedback email
        /// </summary>
        public const string FeedbackEmail = "to@be.set";
        /// <summary>
        /// The upload data URL
        /// </summary>
        public const string UploadDataUrl = "https://agrotutorapi.geo-wiki.org/api/plots/CreatePlot";
        /// <summary>
        /// The upload plot data period
        /// </summary>
        public const int UploadPlotDataPeriod = 1;//7;  //days
    }
}