namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    using System;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyEvapotranspirationShortCrop")]
    public class HourlyEvapotranspirationShortCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public Value[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}