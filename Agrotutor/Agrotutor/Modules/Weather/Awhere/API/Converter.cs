using System;
using System.Collections.Generic;
using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;

namespace Agrotutor.Modules.Weather.Awhere.API
{
    public static class Converter
    {
        public static List<WeatherForecast> GetForecastsFromApiResponse(ForecastResponse forecastResponse) 
        {
            List<WeatherForecast> forecasts = new List<WeatherForecast>();

            foreach (var forecastResponseItem in forecastResponse.Forecasts)
            {
                List<WeatherForecast> forecastHours = new List<WeatherForecast>();
                foreach (var forecastHour in forecastResponseItem.Forecast)
                {
                    forecastHours.Add(GetForecastFromApiResponse(forecastHour));
                }

                forecasts.Add(WeatherForecast.FromHourlyForecasts(forecastHours));
            }

            return forecasts;
        }

        private static WeatherForecast GetForecastFromApiResponse(ForecastResponseForecastHour forecastHour)
        {
            double avgTemperature = (forecastHour.Temperatures?.Average != null)
                ? (double)Math.Round((decimal)forecastHour.Temperatures?.Average, 1)
                : -999.9999;

            double cloudCoverPercent = (forecastHour.Sky?.CloudCover != null)
                ? (double)Math.Round((decimal)forecastHour.Sky.CloudCover, 1)
                : -999.9999;

            double maxTemperature = (forecastHour.Temperatures?.Max != null)
                ? (double)Math.Round((decimal)forecastHour.Temperatures?.Max, 1)
                : -999.9999;

            double minTemperature = (forecastHour.Temperatures?.Min != null)
                ? (double)Math.Round((decimal)forecastHour.Temperatures?.Min, 1)
                : -999.9999;

            double precipitationAmount = (forecastHour.Precipitation?.Amount != null)
                ? (double)Math.Round((decimal)forecastHour.Precipitation?.Amount, 1)
                : -999.9999;

            double precipitationProbability = (forecastHour.Precipitation?.Chance != null)
                ? (double)Math.Round((decimal)forecastHour.Precipitation?.Chance, 0)
                : -999.9999;

            double relativeHumidity = (forecastHour.RelativeHumidity?.Average != null)
                ? (double)Math.Round((decimal)forecastHour.RelativeHumidity.Average, 1)
                : -999.9999;

            var forecast = new WeatherForecast
            {
                AvgTemperature = avgTemperature,
                CloudCoverPercent = cloudCoverPercent,
                DateTime = ((DateTimeOffset)forecastHour.StartTime).UtcDateTime,
                MaxTemperature = maxTemperature,
                MinTemperature = minTemperature,
                PrecipitationAmount = precipitationAmount,
                PrecipitationProbability = precipitationProbability,
                PrecipitationUnit = forecastHour.Precipitation.Units,
                RelativeHumidity = relativeHumidity,
                TemperatureUnit = forecastHour.Temperatures.Units
            };
            forecast.UpdateConditions(forecastHour.ConditionsCode);
            return forecast;
        }

        public static List<WeatherHistory> GetHistoryFromApiResponse(ObservationsResponse historyResponse)
        {
            var history = new List<WeatherHistory>();
            foreach (var observation in historyResponse.Observations)
            {
                history.Add(GetHistoryItemFromApiResponse(observation));
            }
            return history;
        }

        public static WeatherHistory GetHistoryItemFromApiResponse(ObservationResponseObservation history)
        {
            var historyElement = new WeatherHistory
            {
                Date = ((DateTimeOffset)history.Date).UtcDateTime,
                PrecipitationAmount = (double)history.Precipitation.Amount,
                PrecipitationUnits = history.Precipitation.Units,
                RelativeHumidityMax = (double)history.RelativeHumidity.Max,
                RelativeHumidityMin = (double)history.RelativeHumidity.Min,
                SolarRadiationAmount = (double)history.Solar.Amount,
                SolarRadiationUnits = history.Solar.Units,
                TemperatureMax = (double)history.Temperatures.Max,
                TemperatureMin = (double)history.Temperatures.Min,
                TemperatureUnits = history.Temperatures.Units,
                WindAverage = (double)history.Wind.Average,
                WindUnits = history.Wind.Units,
                WindDayMax = (double)history.Wind.DayMax,
                WindMorningMax = (double)history.Wind.MorningMax
            };
            return historyElement;
        }
    }
}
