namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyLowTemperature")]
    public class DailyLowTemperature : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}