namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Threading.Tasks;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.Map;
    using Helper.RestfulClient;

    public static class WeatherService
    {
        public static async Task<WeatherData> GetWeatherData(GeoPosition location)
        {
            try
            {
                RestfulClient<WeatherData> client = new RestfulClient<WeatherData>();
                WeatherData data = await client.RefreshDataAsync(
                    $"https://wsgi.geo-wiki.org/skywise_weather?lat={location.Latitude}&lng={location.Longitude}");
                return data;
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {
                return null;
            }
        }
    }
}