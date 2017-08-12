namespace Helper.DTO.SkywiseWeather.Historical.Precipitation
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyPrecipitation")]
    public class HourlyPrecipitation : HourlySeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public float precipitation { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}