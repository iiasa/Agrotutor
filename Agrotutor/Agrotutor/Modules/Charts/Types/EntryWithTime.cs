namespace Agrotutor.Modules.Charts.Types
{
    using System;
    using Microcharts;

    public class EntryWithTime : Entry
    {
        public EntryWithTime(float value) : base(value) { }

        public DateTime Time { get; set; }
    }
}
