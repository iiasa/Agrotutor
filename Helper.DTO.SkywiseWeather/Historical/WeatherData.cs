﻿
namespace Helper.DTO.SkywiseWeather.Historical
{

	using SQLite.Net.Attributes;
	using SQLiteNetExtensions.Attributes;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Newtonsoft.Json;

    [Table("WeatherData")]
    public class WeatherData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [JsonProperty("gdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public GrowingDegreeDays GrowingDegreeDays { get; set; }
        [JsonProperty("cdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public CoolingDegreeDays CoolingDegreeDays { get; set; }
        [JsonProperty("hdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HeatingDegreeDays HeatingDegreeDays { get; set; }
        [JsonProperty("dp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyPrecipitation DailyPrecipitation { get; set; }
        [JsonProperty("hp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyPrecipitation HourlyPrecipitation { get; set; }
        [JsonProperty("hrh")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyRelativeHumidity HourlyRelativeHumidity { get; set; }
        [JsonProperty("dsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailySolarRadiation DailySolarRadiation { get; set; }
        [JsonProperty("hsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlySolarRadiation HourlySolarRadiation { get; set; }

        [JsonProperty("ht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyTemperature HourlyTemperature { get; set; }
        [JsonProperty("dht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyHighTemperature DailyHighTemperature { get; set; }
        [JsonProperty("dlt")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyLowTemperature DailyLowTemperature { get; set; }
        [JsonProperty("hd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyDewpoint HourlyDewpoint { get; set; }
        [JsonProperty("hws")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyWindSpeed HourlyWindSpeed { get; set; }
        [JsonProperty("hwd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyWindDirection HourlyWindDirection { get; set; }
        [JsonProperty("desc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationShortCrop DailyEvapotranspirationShortCrop { get; set; }
        [JsonProperty("detc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationTallCrop DailyEvapotranspirationTallCrop { get; set; }
        [JsonProperty("hesc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationShortCrop HourlyEvapotranspirationShortCrop { get; set; }
        [JsonProperty("hetc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationTallCrop HourlyEvapotranspirationTallCrop { get; set; }
    }

    public class GrowingDegreeDays : HistoricalSeries 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
    }

    public class CoolingDegreeDays : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
    }

    public class HeatingDegreeDays : HistoricalSeries
    {
        [
            PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
    }

    public class DailyPrecipitation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public float precipitation { get; set; }
    }

    public class HourlyPrecipitation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public float precipitation { get; set; }
    }

    public class HourlyRelativeHumidity : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class DailySolarRadiation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float solarRadiation { get; set; }
        public string endDate { get; set; }
    }

    public class HourlySolarRadiation : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class HourlyTemperature : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class DailyHighTemperature : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Value[] series { get; set; }
    }

    public class DailyLowTemperature : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    public class HourlyDewpoint : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class HourlyWindSpeed : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class HourlyWindDirection : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class DailyEvapotranspirationShortCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    public class DailyEvapotranspirationTallCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Value[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
    }

    public class HourlyEvapotranspirationShortCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Value[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
    }

    public class HourlyEvapotranspirationTallCrop : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Value[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
    }
}
