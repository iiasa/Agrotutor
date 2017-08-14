using System;
using System.Threading.Tasks;
using CimmytApp.BusinessContract;
using CimmytApp.DTO;
using Helper.DTO.SkywiseWeather.Historical;
using Helper.RestfulClient;

namespace CimmytApp.WeatherForecast
{
    public static class WeatherService
    {
        public static async Task<WeatherData> GetWeatherData(GeoPosition location)
        {
            var client = new RestfulClient<WeatherData>();
            WeatherData data = null;
            try
            {
                data = await client.RefreshDataAsync(
                    $"https://wsgi.geo-wiki.org/skywise_weather?lat={location.Latitude}&lng={location.Longitude}");
            }
            catch
            {
                // ignored
            }
            return data;
        }
    }
}