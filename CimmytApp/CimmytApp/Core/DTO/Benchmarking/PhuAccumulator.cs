namespace CimmytApp.DTO.Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using CimmytApp.Core.Persistence.Entities;
    using Helper.Map;

    public static class PhuAccumulator
    {
        public static List<DateTime> GetWindowsOfOpportunity(int baseTemperature, int targetHeatUnits,
            Position position, DateTime plantingDate)
        {
            var endDate = DateTime.Today.AddDays(10);
            var duration = endDate.Subtract(plantingDate);
            if (duration.Days < 0)
            {
                endDate = plantingDate;
            }
            else if (duration.Days > 365)
            {
                endDate = plantingDate.AddDays(365);
            }

            if (position.Latitude == null || position.Longitude == null)
            {
                return null;
            }

            try
            {
                var dailyLowTemp = WdtIncInsightData.Download(WdtIncDataset.DailyLowTemperature,
                        (double)position.Latitude, (double)position.Longitude, plantingDate, endDate)
                    .Result;
                var dailyHighTemp = WdtIncInsightData.Download(WdtIncDataset.DailyHighTemperature,
                        (double)position.Latitude, (double)position.Longitude, plantingDate, endDate)
                    .Result;

                if (dailyLowTemp.Series.Length != dailyHighTemp.Series.Length)
                {
                    return null;
                }

                var dailyHeatUnits = new List<KeyValuePair<DateTime, double>>();

                for (var i = 0; i < dailyLowTemp.Series.Length; i++)
                {
                    var date = DateTime.ParseExact(dailyLowTemp.Series[i].ValidDate, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture);
                    var lowTemp = dailyLowTemp.Series[i].Value;
                    var highTemp = dailyHighTemp.Series[i].Value;
                    var heatUnits = (lowTemp + highTemp) / 2 - baseTemperature;
                    dailyHeatUnits.Add(new KeyValuePair<DateTime, double>(date, heatUnits));
                }

                var sequence = 1;
                var accumulativeHeatUnits = 0.0;
                var threshold = targetHeatUnits / 3.0;
                var windows = new List<DateTime>();
                foreach (var heatUnits in dailyHeatUnits)
                {
                    accumulativeHeatUnits += heatUnits.Value;
                    if (accumulativeHeatUnits >= threshold)
                    {
                        sequence += 1;
                        if (sequence == 4)
                        {
                            return windows; // (Projected) harvest date reached
                        }

                        windows.Add(heatUnits.Key);
                        threshold = targetHeatUnits * sequence / 3.0;
                    }
                }

                return windows; // Harvest date later than forecast range
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }
    }

    public enum AccumulatorMode
    {
        Past,
        Current,
        Future
    }
}