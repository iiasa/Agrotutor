namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;
    using System.Linq;

    public class HourlySeries : HistoricalSeries
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public override void Sort()
        {
            var list = series.ToList();
            list.Sort((x, y) => DateTime.Compare(x.validTime, y.validTime));
            series = list.ToArray();
        }
    }
}