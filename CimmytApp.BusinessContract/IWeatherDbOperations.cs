

namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;
    using Helper.DTO.SkywiseWeather.Historical;

    public interface IWeatherDbOperations
    {


        void AddWeatherData(WeatherData weatherData);


        int DeleteWeatherData(int id);

        WeatherData GetWeatherData(int weatherDataId);

        List<WeatherData> GetAllWeatherData();

        int UpdateWeatherData(WeatherData weatherData);


        int DeleteAllWeatherData();




    }
}
