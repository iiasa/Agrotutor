namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class HourlyEvapotranspirationTallCrop : HourlySeries
    {
        public int ID { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int WeatherDataID { get; set; }
    }
}