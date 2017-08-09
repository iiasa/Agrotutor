namespace Helper.DTO.SkywiseWeather.Historical.SolarRadiation
{
    using System;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlySolarRadiation")]
    public class HourlySolarRadiation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}