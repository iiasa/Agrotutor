namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    public class DailyLowTemperature : DailySeries
    {
        public int ID { get; set; }

        public int WeatherDataID { get; set; }
    }
}