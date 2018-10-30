namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class HourlyEvapotranspirationTallCrop : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}