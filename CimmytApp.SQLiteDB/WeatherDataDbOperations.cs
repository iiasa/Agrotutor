namespace CimmytApp.SQLiteDB
{
    using SqLite.Contract;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    using Helper.DTO.SkywiseWeather.Historical;

    using BusinessContract;

    public class WeatherDataDbOperations : IWeatherDbOperations
    {
        private readonly SQLiteConnection _databaseConn;

        public WeatherDataDbOperations()
        {
            _databaseConn = DependencyService.Get<IFileHelper>().GetConnection();

            //create the tables

            try
            {
                var res = _databaseConn.CreateTable<WeatherData>();
            }
            catch (Exception e)
            {
            }
        }

        public void AddWeatherData(WeatherData weatherData)
        {
            _databaseConn.InsertWithChildren(weatherData, true);
        }

        public int DeleteWeatherData(int id)
        {
            return _databaseConn.Delete<WeatherData>(id);
        }

        public WeatherData GetWeatherData(int weatherDataId)
        {
            return _databaseConn.GetWithChildren<WeatherData>(weatherDataId);
        }

        public List<WeatherData> GetAllWeatherData()
        {
            return _databaseConn.GetAllWithChildren<WeatherData>();
        }

        public int UpdateWeatherData(WeatherData weatherData)
        {
            return _databaseConn.Update(weatherData);
        }

        public int DeleteAllWeatherData()
        {
            return _databaseConn.DeleteAll<WeatherData>();
        }
    }
}