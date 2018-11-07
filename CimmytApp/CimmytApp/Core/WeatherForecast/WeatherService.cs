namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Threading.Tasks;
    using CimmytApp.Core.Persistence.Entities;
    using Helper.RestfulClient;

    public static class WeatherService
    {
        public static async Task<WeatherData> GetWeatherData(Position location)
        {
            try
            {
                var client = new RestfulClient<WeatherData>();
                var data = await client.RefreshDataAsync(
                    $"https://wsgi.geo-wiki.org/skywise_weather?lat={location.Latitude}&lng={location.Longitude}");
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}