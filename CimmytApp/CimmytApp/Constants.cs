﻿namespace CimmytApp
{
    using Xamarin.Forms.GoogleMaps;

    public class Constants
    {
        public const string AppNotFirstLaunch = "APP_NotFirstLaunch";

        public const string AppCenterKeyAndroid = "ccbee3dd-42cc-41c9-92cc-664870cd7c0e";
        public const string AppCenterKeyIOs = "58f35007-f37b-45c5-beb6-885f2eca60b7";

        public const string MainMapWeather = "MainMap_Weather";
        public const string MainMapWeatherLocation = "MainMap_WeatherLocation";
        public const int MainMapWeatherRefreshDistance = 1000; // Meters
        public const int MainMapWeatherRefreshDuration = 60; // Minutes
    }
}