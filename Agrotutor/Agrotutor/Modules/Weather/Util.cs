namespace Agrotutor.Modules.Weather
{
    using System;
    using Xamarin.Essentials;

    public static class Util
    {
        public static bool ShouldRefresh(Xamarin.Essentials.Location oldLocation, Xamarin.Essentials.Location newLocation)
        {
            if (newLocation == null) return false;
            if (oldLocation == null) return true;
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
