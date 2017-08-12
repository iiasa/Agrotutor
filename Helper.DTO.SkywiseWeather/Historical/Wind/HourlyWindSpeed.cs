namespace Helper.DTO.SkywiseWeather.Historical.Wind
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyWindSpeed")]
    public class HourlyWindSpeed : HourlySeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}