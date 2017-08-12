using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    public abstract class DegreeDays : DailySeries
    {
        public float degreeDays { get; set; }
        public float baseTemperature { get; set; }
    }
}