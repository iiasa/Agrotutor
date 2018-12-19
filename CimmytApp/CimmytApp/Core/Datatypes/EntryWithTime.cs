using System;
using Microcharts;

namespace CimmytApp.Core.Datatypes
{
    public class EntryWithTime : Entry
    {
        public EntryWithTime(float value) : base(value) { }

        public DateTime Time { get; set; }
    }
}
