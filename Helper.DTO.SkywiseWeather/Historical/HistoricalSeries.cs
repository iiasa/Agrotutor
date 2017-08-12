namespace Helper.DTO.SkywiseWeather.Historical
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class HistoricalSeries
    {
        private Value[] series { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Unit unit { get; set; }

        public List<Value> Series => series.ToList();
    }
}