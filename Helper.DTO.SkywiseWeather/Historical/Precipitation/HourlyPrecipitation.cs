namespace Helper.DTO.SkywiseWeather.Historical.Precipitation
{
    using System;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyPrecipitation")]
    public class HourlyPrecipitation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public float precipitation { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}