namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyEvapotranspirationTallCrop")]
    public class DailyEvapotranspirationTallCrop : DailySeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public float longitude { get; set; }
        public float latitude { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}