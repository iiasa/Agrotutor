﻿namespace Helper.DTO.SkywiseWeather.Historical.Wind
{
    using System;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyWindDirection")]
    public class HourlyWindDirection : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}