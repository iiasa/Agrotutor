using System;
using SQLiteNetExtensions.Attributes;

namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class HistoricalSeries
    {
        private Value[] series { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Unit unit { get; set; }
    }
}