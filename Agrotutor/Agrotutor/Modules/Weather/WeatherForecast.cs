using System;
using System.Collections.Generic;
using System.Linq;

namespace Agrotutor.Modules.Weather
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }
        public string WeatherConditionCode { get; set; }

        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double AvgTemperature { get; set; }
        public string TemperatureUnit { get; set; }

        public double PrecipitationProbability { get; set; }
        public double PrecipitationAmount { get; set; }
        public string PrecipitationUnit { get; set; }

        public double CloudCoverPercent { get; set; }

        public double RelativeHumidity { get; set; }

        public List<WeatherForecast> ForecastHours { get; set; }

        public static WeatherForecast FromHourlyForecasts(List<WeatherForecast> forecastHours)
        {
            var forecast = new WeatherForecast
            {
                AvgTemperature = forecastHours.Average(x => x.AvgTemperature),
                CloudCoverPercent = forecastHours.Average(x => x.CloudCoverPercent),
                DateTime = forecastHours[0].DateTime.Date,
                ForecastHours = forecastHours,
                MaxTemperature = forecastHours.Max(x => x.MaxTemperature),
                MinTemperature = forecastHours.Min(x => x.MinTemperature),
                PrecipitationAmount = forecastHours.Sum(x => x.PrecipitationAmount),
                PrecipitationProbability = forecastHours.Max(x => x.PrecipitationProbability),
                PrecipitationUnit = forecastHours[0].PrecipitationUnit,
                RelativeHumidity = forecastHours.Average(x => x.RelativeHumidity),
                TemperatureUnit = forecastHours[0].TemperatureUnit
            };
            return forecast;
        }
    }
}
