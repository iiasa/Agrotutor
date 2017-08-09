namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HeatingDegreeDays")]
    public class HeatingDegreeDays : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}