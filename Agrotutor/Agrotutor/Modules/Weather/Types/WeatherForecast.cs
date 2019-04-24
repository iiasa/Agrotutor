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
            switch (CloudCondition) {
                case CloudCondition.SunnyDay:
                    return "clear.png";
                case CloudCondition.MostlySunnyDay:
                    return RainCondition == RainCondition.NoRain ? "clear.png" : "partly_cloudy.png";
                case CloudCondition.PartlySunnyDay:
                    return RainCondition == RainCondition.NoRain ? "partly_cloudy.png" : "storm.png";
                case CloudCondition.MostlyCloudyDay:
                    return GetMostlyCloudyDayIcon();
                case CloudCondition.CloudyDay:
                    return GetCloudyIcon();
                case CloudCondition.ClearNight:
                    return "night.png";
                case CloudCondition.MostlyClearNight:
                    return RainCondition == RainCondition.NoRain ? "night.png" : "cloudy_night.png";
                case CloudCondition.PartlyCloudyNight:
                    return RainCondition == RainCondition.NoRain ? "cloudy_night.png" : "storm_night.png";
                case CloudCondition.MostlyCloudyNight:
                    return GetMostlyCloudyNightIcon();
                case CloudCondition.CloudyNight:
                    return GetCloudyIcon();
                case CloudCondition.Clear:
                    return GetClearIcon();
                case CloudCondition.MostlyClear:
                    return GetMostlyClearIcon();
                case CloudCondition.PartlyCloudy:
                    return GetPartlyCloudyIcon();
                case CloudCondition.MostlyCloudy:
                    return GetMostlyCloudyIcon();
                case CloudCondition.Cloudy:
                    return GetCloudyIcon();
                default:
                    return "clear.png";
            }
        }

        private string GetCloudyIcon()
        {
            switch (RainCondition)
            {
                case RainCondition.NoRain:
                    return "cloudy.png";
                case RainCondition.LightRain:
                    return "rain.png";
                case RainCondition.ModerateRain:
                    return "heavy_drizzle.png";
                case RainCondition.HeavyRain:
                    return "thunderstorms.png";
                default:
                    return "cloudy.png";
            }
        }

        private string GetMostlyCloudyIcon()
        {
            switch (RainCondition)
            {
                case RainCondition.NoRain:
                    return Types.Util.IsNight(DateTime) ? "partly_cloudy.png" : "cloudy_night.png";  
                case RainCondition.LightRain:
                    return Types.Util.IsNight(DateTime) ? "storm.png" : "storm_night.png";  
                case RainCondition.ModerateRain:
                    return "rain.png";
                case RainCondition.HeavyRain:
                    return "heavy_drizzle.png";
                default:
                    return Types.Util.IsNight(DateTime) ? "partly_cloudy.png" : "cloudy_night.png";  
            }
        }

        private string GetPartlyCloudyIcon()
        {
            if (RainCondition == RainCondition.NoRain)
            {
                return Types.Util.IsNight(DateTime) ? "partly_cloudy.png" : "cloudy_night.png";  
            }
            else
            {
                return Types.Util.IsNight(DateTime) ? "storm.png" : "storm_night.png";  
            }
        }

        private string GetMostlyClearIcon()
        {
            if (RainCondition == RainCondition.NoRain)
            {
                return Types.Util.IsNight(DateTime) ? "night.png" : "clear.png";   
            }
            else
            {
                return Types.Util.IsNight(DateTime) ? "partly_cloudy.png" : "cloudy_night.png";  
            }
        }

        private string GetClearIcon()
        {
            return Types.Util.IsNight(DateTime) ? "night.png" : "clear.png";
        }

        private string GetMostlyCloudyNightIcon()
        {
            switch (RainCondition)
            {
                case RainCondition.NoRain:
                    return "cloudy_night.png";
                case RainCondition.LightRain:
                    return "storm_night.png";
                case RainCondition.ModerateRain:
                    return "rain_showers.png";
                case RainCondition.HeavyRain:
                    return "heavy_drizzle.png";
                default:
                    return "cloudy_night.png";
            }
        }

        private string GetMostlyCloudyDayIcon()
        {
            switch (RainCondition)
            {
                case RainCondition.NoRain:
                    return "partly_cloudy.png";
                case RainCondition.LightRain:
                    return "storm.png";
                case RainCondition.ModerateRain:
                    return "rain.png";
                case RainCondition.HeavyRain:
                    return "heavy_drizzle.png";
                default:
                    return "partly_cloudy.png";
            }
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
