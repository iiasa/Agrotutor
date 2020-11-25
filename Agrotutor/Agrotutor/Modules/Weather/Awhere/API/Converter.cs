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
            if (forecastResponse == null) return null;

            foreach (var forecastResponseItem in forecastResponse.Forecasts)
            {
                foreach (var forecastDay in forecastResponseItem.Forecast)
                {
                    forecasts.Add(GetForecastFromApiResponse(forecastDay));
                }
            }

            return forecasts;
        }

        private static WeatherForecast GetForecastFromApiResponse(ForecastResponseForecastHour forecastHour)
        {
            double temperature = (forecastHour.Temperatures?.Value != null)
                ? (double)Math.Round((decimal)forecastHour.Temperatures?.Value, 1)
                : -999.9999;

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
                Temperature = temperature,
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
            if (historyResponse == null) return null;
            foreach (var observation in historyResponse.Observations)
            {
                history.Add(GetHistoryItemFromApiResponse(observation));
            }
            return history;
        }

        public static WeatherHistory GetHistoryItemFromApiResponse(ObservationResponseObservation history)
        {
            if (history == null) return null;
            var historyElement = new WeatherHistory
            {
                Date = history.Date?.DateTime ?? default,
                PrecipitationAmount = history.Precipitation.Amount ?? default,
                PrecipitationUnits = history.Precipitation.Units,
                RelativeHumidityMax = history.RelativeHumidity.Max ?? default,
                RelativeHumidityMin = history.RelativeHumidity.Min ?? default,
                SolarRadiationAmount = history.Solar.Amount ?? default,
                SolarRadiationUnits = history.Solar.Units,
                TemperatureMax = history.Temperatures.Max ?? default,
                TemperatureMin = history.Temperatures.Min ?? default,
                TemperatureUnits = history.Temperatures.Units,
                WindAverage = history.Wind.Average ?? default,
                WindUnits = history.Wind.Units,
                WindDayMax = history.Wind.DayMax ?? default,
                WindMorningMax = history.Wind.MorningMax ?? default
            };
            return historyElement;
        }
    }
}
