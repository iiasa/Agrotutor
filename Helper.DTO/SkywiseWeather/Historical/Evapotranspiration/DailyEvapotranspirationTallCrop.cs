namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class DailyEvapotranspirationTallCrop : DailySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}