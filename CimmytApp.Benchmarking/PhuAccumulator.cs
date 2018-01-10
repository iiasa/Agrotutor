using System;
using System.Collections.Generic;

namespace CimmytApp.Benchmarking
{
    public class PhuAccumulator
    {
        public double MaxTemperature { get; set; }

        public double MinTemperature { get; set; }

        private AccumulatorMode accumulatorMode { get; set; }

        public List<DateTime> GetWindowsOfOpportunity(double baseTemperature, double targetHeatUnits, DateTime plantingDate){
            var dailyHeatUnits = GetHeatUnits(plantingDate);
            targetHeatUnits = 9999; // TODO calculate, adapt params

            var dates = new List<DateTime>();

            var i = 1;
            var accumulatedHeatUnits = 0;
            foreach (var dailyHeatUnit in dailyHeatUnits){
                accumulatedHeatUnits += dailyHeatUnit.Value;
                if (accumulatedHeatUnits >= targetHeatUnits * (i/3)){
                    dates.Add(dailyHeatUnit.Key);
                    i++;
                    if (i == 4) break;
                }
            }
            return dates;
        }

        private List<KeyValuePair<DateTime, int>> GetHeatUnits(DateTime startDate){
            DateTime endDate;
            if (DateTime.Now.CompareTo(startDate) < 0) accumulatorMode = AccumulatorMode.Future;
            else if (DateTime.Now.Subtract(startDate).TotalDays > 365) accumulatorMode = AccumulatorMode.Past;
            else accumulatorMode = AccumulatorMode.Current;
            var heatUnitList = new List<KeyValuePair<DateTime, int>>();
            if (accumulatorMode != AccumulatorMode.Future)
                heatUnitList.AddRange(GetHistoricalHeatUnits());
            if (accumulatorMode != AccumulatorMode.Past){
                heatUnitList.AddRange(GetForecastHeatUnits());
            }

            return heatUnitList;
        }

        private List<KeyValuePair<DateTime, int>> GetForecastHeatUnits()
        {
            // TODO query data from web service
        }

        private List<KeyValuePair<DateTime, int>> GetHistoricalHeatUnits()
        {
            // TODO query data from web service
        }
    }

    public enum AccumulatorMode{
        Past,
        Current,
        Future
    }
}