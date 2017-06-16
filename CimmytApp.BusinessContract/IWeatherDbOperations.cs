using CimmytApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.BusinessContract
{
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
