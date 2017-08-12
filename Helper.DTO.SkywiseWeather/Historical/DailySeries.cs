namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class DailySeries : HistoricalSeries
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}