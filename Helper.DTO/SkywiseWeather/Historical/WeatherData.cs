namespace Helper.DTO.SkywiseWeather.Historical
{
    using Helper.DTO.SkywiseWeather.Historical.Evapotranspiration;
    using Helper.DTO.SkywiseWeather.Historical.Humidity;
    using Helper.DTO.SkywiseWeather.Historical.Precipitation;
    using Helper.DTO.SkywiseWeather.Historical.SolarRadiation;
    using Helper.DTO.SkywiseWeather.Historical.Temperature;
    using Helper.DTO.SkywiseWeather.Historical.Wind;
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("WeatherData")]
    public class WeatherData
    {
        [JsonProperty("cdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public CoolingDegreeDays CoolingDegreeDays { get; set; }

        [JsonProperty("desc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationShortCrop DailyEvapotranspirationShortCrop { get; set; }

        [JsonProperty("detc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationTallCrop DailyEvapotranspirationTallCrop { get; set; }

        [JsonProperty("dht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyHighTemperature DailyHighTemperature { get; set; }

        [JsonProperty("dlt")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyLowTemperature DailyLowTemperature { get; set; }

        [JsonProperty("dp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyPrecipitation DailyPrecipitation { get; set; }

        [JsonProperty("dsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailySolarRadiation DailySolarRadiation { get; set; }

        [JsonProperty("gdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public GrowingDegreeDays GrowingDegreeDays { get; set; }

        [JsonProperty("hdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HeatingDegreeDays HeatingDegreeDays { get; set; }

        [JsonProperty("hd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyDewpoint HourlyDewpoint { get; set; }

        [JsonProperty("hesc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationShortCrop HourlyEvapotranspirationShortCrop { get; set; }

        [JsonProperty("hetc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationTallCrop HourlyEvapotranspirationTallCrop { get; set; }

        [JsonProperty("hp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyPrecipitation HourlyPrecipitation { get; set; }

        [JsonProperty("hrh")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyRelativeHumidity HourlyRelativeHumidity { get; set; }

        [JsonProperty("hsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlySolarRadiation HourlySolarRadiation { get; set; }

        [JsonProperty("ht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyTemperature HourlyTemperature { get; set; }

        [JsonProperty("hwd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyWindDirection HourlyWindDirection { get; set; }

        [JsonProperty("hws")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyWindSpeed HourlyWindSpeed { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        public int ParcelId { get; set; }
    }
}