namespace Helper.DTO.SkywiseWeather.Historical
{
    using Helper.DTO.SkywiseWeather.Historical.Evapotranspiration;
    using Helper.DTO.SkywiseWeather.Historical.Humidity;
    using Helper.DTO.SkywiseWeather.Historical.Precipitation;
    using Helper.DTO.SkywiseWeather.Historical.SolarRadiation;
    using Helper.DTO.SkywiseWeather.Historical.Temperature;
    using Helper.DTO.SkywiseWeather.Historical.Wind;

    public class WeatherData
    {
        public CoolingDegreeDays CoolingDegreeDays { get; set; }

        public DailyEvapotranspirationShortCrop DailyEvapotranspirationShortCrop { get; set; }

        public DailyEvapotranspirationTallCrop DailyEvapotranspirationTallCrop { get; set; }

        public DailyHighTemperature DailyHighTemperature { get; set; }

        public DailyLowTemperature DailyLowTemperature { get; set; }

        public DailyPrecipitation DailyPrecipitation { get; set; }

        public DailySolarRadiation DailySolarRadiation { get; set; }

        public GrowingDegreeDays GrowingDegreeDays { get; set; }

        public HeatingDegreeDays HeatingDegreeDays { get; set; }

        public HourlyDewpoint HourlyDewpoint { get; set; }

        public HourlyEvapotranspirationShortCrop HourlyEvapotranspirationShortCrop { get; set; }

        public HourlyEvapotranspirationTallCrop HourlyEvapotranspirationTallCrop { get; set; }

        public HourlyPrecipitation HourlyPrecipitation { get; set; }

        public HourlyRelativeHumidity HourlyRelativeHumidity { get; set; }

        public HourlySolarRadiation HourlySolarRadiation { get; set; }

        public HourlyTemperature HourlyTemperature { get; set; }

        public HourlyWindDirection HourlyWindDirection { get; set; }

        public HourlyWindSpeed HourlyWindSpeed { get; set; }

        public int ID { get; set; }

        public string ParcelId { get; set; }
    }
}