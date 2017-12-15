namespace Helper.DTO.SkywiseWeather.Historical.SolarRadiation
{
    public class HourlySolarRadiation : HourlySeries
    {
        public int ID { get; set; }

        public int WeatherDataID { get; set; }
    }
}