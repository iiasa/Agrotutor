namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("CoolingDegreeDays")]
    public class CoolingDegreeDays : DegreeDays
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}