namespace Helper.DTO.SkywiseWeather.Historical.Evapotranspiration
{
    public class DailyEvapotranspirationShortCrop : DailySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}