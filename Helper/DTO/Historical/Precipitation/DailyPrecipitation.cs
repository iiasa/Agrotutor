namespace Helper.DTO.SkywiseWeather.Historical.Precipitation
{
    public class DailyPrecipitation : DailySeries
    {
        public int ID { get; set; }

        public float precipitation { get; set; }

        public int WeatherDataID { get; set; }
    }
}