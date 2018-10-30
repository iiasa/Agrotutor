namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class HourlyEvapotranspirationShortCrop : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}