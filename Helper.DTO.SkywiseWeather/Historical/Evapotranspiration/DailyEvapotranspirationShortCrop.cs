namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyEvapotranspirationShortCrop")]
    public class DailyEvapotranspirationShortCrop : HistoricalSeries
    {
        public string endDate { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}