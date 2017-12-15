namespace Helper.DTO.SkywiseWeather.Historical.Precipitation
{
    public class HourlyPrecipitation : HourlySeries
    {
        public int ID { get; set; }

        public float precipitation { get; set; }

        public int WeatherDataID { get; set; }
    }
}