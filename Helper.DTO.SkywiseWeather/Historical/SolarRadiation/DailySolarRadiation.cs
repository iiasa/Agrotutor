namespace Helper.DTO.SkywiseWeather.Historical.SolarRadiation
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailySolarRadiation")]
    public class DailySolarRadiation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public float solarRadiation { get; set; }
        public string endDate { get; set; }

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}