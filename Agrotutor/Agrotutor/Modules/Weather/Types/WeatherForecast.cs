using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Agrotutor.Modules.Weather.Types;

namespace Agrotutor.Modules.Weather
{
    public class WeatherForecast
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateTime { get; set; }

        public CloudCondition CloudCondition { get; set; }
        public RainCondition RainCondition { get; set; }
        public WindCondition WindCondition { get; set; }

        public string WxIcon => GetWeatherIcon();
        public string Date => DateTime.ToString("yy-MM-dd");

        public double Temperature { get; set; }
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
                Temperature = forecastHours.Average(x=>x.Temperature),
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

        public string GetWeatherIcon()
        {
            if (RainCondition == RainCondition.ModerateRain || RainCondition == RainCondition.HeavyRain) {
                return "rain.png";
            }
            switch (CloudCondition)
            {
                case CloudCondition.SunnyDay:
                case CloudCondition.ClearNight:
                case CloudCondition.Clear:
                    return "clear.png";
                case CloudCondition.MostlySunnyDay:
                case CloudCondition.MostlyClearNight:
                case CloudCondition.MostlyClear:
                    return "partly_cloudy.png";
                case CloudCondition.PartlySunnyDay:
                case CloudCondition.PartlyCloudyNight:
                case CloudCondition.PartlyCloudy:
                    return "mostly_cloudy.png";

                case CloudCondition.MostlyCloudyDay:
                case CloudCondition.MostlyCloudyNight:
                case CloudCondition.MostlyCloudy:
                    return "cloudy.png";

                case CloudCondition.CloudyDay:
                case CloudCondition.CloudyNight:
                case CloudCondition.Cloudy:
                    return "cloudy.png";
            }
            return "clear.png";
        }

        internal double CalculateGdd(int? baseTemperature)
        {
            if (baseTemperature == null) return 0;
            var gdd = ((MaxTemperature - MinTemperature) / 2) - (int)baseTemperature;
            return (gdd > 0) ? gdd : 0;
        }

        internal string GetWeatherText()
        {
            return "";
            throw new NotImplementedException();
        }

        internal string GetWindText()
        {
            return "";
            throw new NotImplementedException();
        }
    }
}
