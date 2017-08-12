namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using System;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyDewpoint")]
    public class HourlyDewpoint : HourlySeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}