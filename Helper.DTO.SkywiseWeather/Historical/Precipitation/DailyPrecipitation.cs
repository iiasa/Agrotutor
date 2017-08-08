namespace Helper.DTO.SkywiseWeather.Historical.Precipitation
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyPrecipitation")]
    public class DailyPrecipitation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
        public float precipitation { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}