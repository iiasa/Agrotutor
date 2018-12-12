
namespace CimmytApp.Core.WeatherForecast
{
    using System;

    using Xamarin.Essentials;

    public static class Util
    {
        public static bool ShouldRefresh(Location oldLocation, Location newLocation)
        {
            if (newLocation.Timestamp.Subtract(oldLocation.Timestamp)
                > TimeSpan.FromMinutes(Constants.MainMapWeatherRefreshDuration))
            {
                return true;
            }

            if (newLocation.CalculateDistance(oldLocation, DistanceUnits.Kilometers)
                > Constants.MainMapWeatherRefreshDistance)
            {
                return true;
            }

            return false;
        }
    }
}
