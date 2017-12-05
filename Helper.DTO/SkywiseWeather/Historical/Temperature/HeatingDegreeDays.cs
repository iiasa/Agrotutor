﻿namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HeatingDegreeDays")]
    public class HeatingDegreeDays : DegreeDays
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}