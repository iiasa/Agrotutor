namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class DailyEvapotranspirationTallCrop : DailySeries
    {
        public int ID { get; set; }

        public float latitude { get; set; }

        public float longitude { get; set; }
        public int WeatherDataID { get; set; }
    }
}