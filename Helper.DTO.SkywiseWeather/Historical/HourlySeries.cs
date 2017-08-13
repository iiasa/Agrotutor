namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;

    public class HourlySeries : HistoricalSeries
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }
}