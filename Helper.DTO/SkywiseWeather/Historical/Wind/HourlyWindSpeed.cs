namespace Helper.DTO.SkywiseWeather.Historical.Wind
{
    public class HourlyWindSpeed : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}