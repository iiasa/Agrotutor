namespace Agrotutor.Modules.Charts.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microcharts;

    public class EntryWithTime : Entry
    {
        public EntryWithTime(float value) : base(value) { }
        public EntryWithTime(float value, DateTime time) : base(value) {
            Time = time;
        }

        public DateTime Time { get; set; }

        public static List<EntryWithTime> From(List<double> items, List<DateTime> dates)
        {
            var entries = new List<EntryWithTime>();
            if (items.Count == dates.Count)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    entries.Add(new EntryWithTime((float)items.ElementAt(i), dates.ElementAt(i)));
                }
            }
            return entries;
        }
    }
}
