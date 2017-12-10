namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    public class HourlyTemperature : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}