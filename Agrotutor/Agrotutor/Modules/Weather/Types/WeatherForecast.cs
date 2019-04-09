using System;
using System.Collections.Generic;
using System.Linq;
using Agrotutor.Modules.Weather.Types;

namespace Agrotutor.Modules.Weather
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }

        public CloudCondition CloudCondition { get; set; }
        public RainCondition RainCondition { get; set; }
        public WindCondition WindCondition { get; set; }


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

        public void UpdateConditions(string conditionsCode)
        {
            CloudCondition = (conditionsCode[0] > '9') ? (CloudCondition)(conditionsCode[0] - 'A' + 10) : (CloudCondition)(conditionsCode[0] - '0');
            RainCondition = (RainCondition)(conditionsCode[1] - '0');
            WindCondition = (WindCondition)(conditionsCode[2] - '0');
        }
    }
}
