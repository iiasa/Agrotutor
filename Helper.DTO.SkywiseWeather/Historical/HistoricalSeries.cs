using System;

namespace Helper.DTO.SkywiseWeather.Historical
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class HistoricalSeries
    {
        public Value[] series { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Unit unit { get; set; }

        public abstract void Sort();
    }
}