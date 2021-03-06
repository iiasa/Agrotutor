using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Agrotutor.Modules.Weather
{
    public class WeatherHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public double PrecipitationAmount { get; set; }
        public string PrecipitationUnits { get; set; }

        public double RelativeHumidityMin { get; set; }
        public double RelativeHumidityMax { get; set; }

        public double SolarRadiationAmount { get; set; }
        public string SolarRadiationUnits { get; set; }

        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public string TemperatureUnits { get; set; }

        public double WindAverage { get; set; }
        public double WindDayMax { get; set; }
        public double WindMorningMax { get; set; }
        public string WindUnits { get; set; }

        public double CalculateGdd(int? baseTemperature)
        {
            if (baseTemperature == null) return 0;
            var gdd = ((TemperatureMax - TemperatureMin) / 2) - (int)baseTemperature;
            return (gdd > 0) ? gdd : 0;
        }
    }
}
