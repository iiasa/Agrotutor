namespace CimmytApp.SQLiteDB
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.BusinessContract;
    using Helper.DTO.SkywiseWeather.Historical;
    using SqLite.Contract;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using Xamarin.Forms;

    public class WeatherDataDbOperations : IWeatherDbOperations
    {
        private readonly SQLiteConnection _databaseConn;

        public WeatherDataDbOperations()
        {
            _databaseConn = DependencyService.Get<IFileHelper>().GetConnection();

            //create the tables

            try
            {
                int res = _databaseConn.CreateTable<WeatherData>();
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {
            }
        }

        public void AddWeatherData(WeatherData weatherData)
        {
            _databaseConn.InsertWithChildren(weatherData, true);
        }

        public int DeleteAllWeatherData()
        {
            return _databaseConn.DeleteAll<WeatherData>();
        }

        public int DeleteWeatherData(int id)
        {
            return _databaseConn.Delete<WeatherData>(id);
        }

        public List<WeatherData> GetAllWeatherData()
        {
            return _databaseConn.GetAllWithChildren<WeatherData>();
        }

        public WeatherData GetWeatherData(int weatherDataId)
        {
            return _databaseConn.GetWithChildren<WeatherData>(weatherDataId);
        }

        public int UpdateWeatherData(WeatherData weatherData)
        {
            return _databaseConn.Update(weatherData);
        }
    }
}