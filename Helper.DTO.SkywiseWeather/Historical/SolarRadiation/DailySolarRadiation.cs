namespace Helper.DTO.SkywiseWeather.Historical.SolarRadiation
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailySolarRadiation")]
    public class DailySolarRadiation : DailySeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public float solarRadiation { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}