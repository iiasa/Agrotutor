using System.Collections.Generic;
using CimmytApp.Core.Datatypes;

namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class HistoricalSeries
    {
        public float latitude { get; set; }

        public float longitude { get; set; }

        public Value[] series { get; set; }

        public Unit unit { get; set; }

        public abstract void Sort();

        public List<EntryWithTime> GetChartEntries()
        {
            var entries = new List<EntryWithTime>();
            foreach (var item in series)
            {
                entries.Add(new EntryWithTime(item.value)
                {
                    Time = item.validTime
                });
            }
            return entries;
        }
    }
}