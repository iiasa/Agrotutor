namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    public class HourlyDewpoint : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}