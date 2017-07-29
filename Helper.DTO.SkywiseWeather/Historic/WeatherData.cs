﻿
namespace Helper.DTO.SkywiseWeather.Historic
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
        public GrowingDegreeDays growingDegreeDays { get; set; }
        [JsonProperty("cdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public CoolingDegreeDays cdd { get; set; }
        [JsonProperty("hdd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HeatingDegreeDays hdd { get; set; }
        [JsonProperty("dp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyPrecipitation dp { get; set; }
        [JsonProperty("hp")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyPrecipitation hp { get; set; }
        [JsonProperty("hrh")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyRelativeHumidity hrh { get; set; }
        [JsonProperty("dsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailySolarRadiation dsr { get; set; }
        [JsonProperty("hsr")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlySolarRadiation hsr { get; set; }

        [JsonProperty("ht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyTemperature ht { get; set; }
        [JsonProperty("dht")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyHighTemperature dht { get; set; }
        [JsonProperty("dlt")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyLowTemperature dlt { get; set; }
        [JsonProperty("hd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyDewpoint hd { get; set; }
        [JsonProperty("hws")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyWindSpeed hws { get; set; }
        [JsonProperty("hwd")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hwd hwd { get; set; }
        [JsonProperty("desc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationShortCrop desc { get; set; }
        [JsonProperty("detc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public DailyEvapotranspirationTallCrop detc { get; set; }
        [JsonProperty("hesc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationShortCrop hesc { get; set; }
        [JsonProperty("hetc")]
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public HourlyEvapotranspirationTallCrop hetc { get; set; }
    }

    public class GrowingDegreeDays
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
        public Series[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }


    public class Series
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string validDate { get; set; }
		public DateTime validTime { get; set; }
        public float value { get; set; }
    }

    public class CoolingDegreeDays
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
        public Series[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class HeatingDegreeDays
    {
        [
            PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float degreeDays { get; set; }
        public string endDate { get; set; }
        public float baseTemperature { get; set; }
        public Series2[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class DailyPrecipitation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Series3[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public float precipitation { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyPrecipitation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series4[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public float precipitation { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyRelativeHumidity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series5[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class DailySolarRadiation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public float solarRadiation { get; set; }
        public string endDate { get; set; }
        public Series6[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlySolarRadiation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series7[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyTemperature
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series8[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class DailyHighTemperature
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Series9[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class DailyLowTemperature
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Series10[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyDewpoint
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series11[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyWindSpeed
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series12[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyWindDirection
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series13[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class DailyEvapotranspirationShortCrop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Series14[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class DailyEvapotranspirationTallCrop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Series15[] series { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyEvapotranspirationShortCrop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series16[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }

    public class HourlyEvapotranspirationTallCrop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series17[] series { get; set; }
        public float longitude { get; set; }
        public DateTime startTime { get; set; }
        public float latitude { get; set; }
        public DateTime endTime { get; set; }
        public Unit unit { get; set; }
    }
}
