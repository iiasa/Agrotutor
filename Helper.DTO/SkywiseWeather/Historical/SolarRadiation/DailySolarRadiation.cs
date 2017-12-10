namespace Helper.DTO.SkywiseWeather.Historical.SolarRadiation
{
    public class DailySolarRadiation : DailySeries
    {
        public int ID { get; set; }
        public float solarRadiation { get; set; }
        public int WeatherDataID { get; set; }
    }
}