namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HourlySeries : HistoricalSeries
    {
        public DateTime endTime { get; set; }

        public DateTime startTime { get; set; }

        public override void Sort()
        {
            List<Value> list = series.ToList();
            list.Sort((x, y) => DateTime.Compare(x.validTime, y.validTime));
            series = list.ToArray();
        }
    }
}