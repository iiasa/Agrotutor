namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyEvapotranspirationTallCrop")]
    public class DailyEvapotranspirationTallCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
        public Value[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}