﻿namespace Helper.DTO.SkywiseWeather.Historical.Humidity
{
    public class HourlyRelativeHumidity : HourlySeries
    {
        public int ID { get; set; }
        public int WeatherDataID { get; set; }
    }
}