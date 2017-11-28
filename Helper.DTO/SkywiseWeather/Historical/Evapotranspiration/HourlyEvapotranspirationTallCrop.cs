namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("HourlyEvapotranspirationTallCrop")]
    public class HourlyEvapotranspirationTallCrop : HourlySeries
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

#pragma warning disable CS0108 // 'HourlyEvapotranspirationTallCrop.latitude' hides inherited member 'HistoricalSeries.latitude'. Use the new keyword if hiding was intended.
        public float latitude { get; set; }
#pragma warning restore CS0108 // 'HourlyEvapotranspirationTallCrop.latitude' hides inherited member 'HistoricalSeries.latitude'. Use the new keyword if hiding was intended.

#pragma warning disable CS0108 // 'HourlyEvapotranspirationTallCrop.longitude' hides inherited member 'HistoricalSeries.longitude'. Use the new keyword if hiding was intended.
        public float longitude { get; set; }
#pragma warning restore CS0108 // 'HourlyEvapotranspirationTallCrop.longitude' hides inherited member 'HistoricalSeries.longitude'. Use the new keyword if hiding was intended.

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}