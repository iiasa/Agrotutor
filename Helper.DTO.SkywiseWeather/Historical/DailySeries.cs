using System;
using System.Linq;

namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class DailySeries : HistoricalSeries
    {
        public string startDate { get; set; }
        public string endDate { get; set; }

        public override void Sort()
        {
            series = series.ToList().OrderBy(x => DateTime.Parse(x.validDate)).ToArray();
        }
    }
}