namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;
    using Helper.DTO.SkywiseWeather.Historical;

    public interface IWeatherDbOperations
    {
        void AddWeatherData(WeatherData weatherData);

        int DeleteAllWeatherData();

        int DeleteWeatherData(int id);

        WeatherData GetWeatherData(int weatherDataId);

        int UpdateWeatherData(WeatherData weatherData);
    }
}