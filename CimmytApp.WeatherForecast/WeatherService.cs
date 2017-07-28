using System;
using CimmytApp.BusinessContract;
using CimmytApp.DTO;
using Helper.RestfulClient;

namespace CimmytApp.WeatherForecast
{
    public static class WeatherService
    {

        public static async System.Threading.Tasks.Task<WeatherData> GetWeatherData(GeoPosition location)
        {
            var client = new RestfulClient<WeatherData>();
            var data = await client.RefreshDataAsync($"https://wsgi.geo-wiki.org/skywise_weather?lat={location.Latitude}&lng={location.Longitude}");
            return data;
        }
    }
}
