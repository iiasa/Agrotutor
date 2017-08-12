using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class DailySeries : HistoricalSeries
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}