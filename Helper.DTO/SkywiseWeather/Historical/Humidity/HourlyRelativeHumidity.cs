namespace Helper.DTO.SkywiseWeather.Historical.Humidity
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyRelativeHumidity")]
    public class HourlyRelativeHumidity : HourlySeries
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}