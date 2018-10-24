﻿namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;
    using System.Linq;

    public abstract class DailySeries : HistoricalSeries
    {
        public string endDate { get; set; }

        public string startDate { get; set; }

        public override void Sort()
        {
            series = series.ToList().OrderBy(x => DateTime.Parse(x.validDate)).ToArray();
        }
    }
}