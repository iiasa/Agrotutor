// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CimmytApp.WeatherFoecast;
//
//    var data = GrowingDegreeDays.FromJson(jsonString);
//
namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class GrowingDegreeDays
    {
        [J("baseTemperature")] public long BaseTemperature { get; set; }
        [J("degreeDays")] public double DegreeDays { get; set; }
        [J("endDate")] public string EndDate { get; set; }
        [J("latitude")] public long Latitude { get; set; }
        [J("longitude")] public long Longitude { get; set; }
        [J("missingValidTimes")] public string[] MissingValidTimes { get; set; }
        [J("series")] public Series[] Series { get; set; }
        [J("startDate")] public string StartDate { get; set; }
        [J("unit")] public Unit Unit { get; set; }
    }

    public partial class Unit
    {
        [J("description")] public string Description { get; set; }
        [J("label")] public string Label { get; set; }
    }

    public partial class Series
    {
        [J("products")] public string[] Products { get; set; }
        [J("validDate")] public string ValidDate { get; set; }
        [J("value")] public double Value { get; set; }
    }

    public partial class GrowingDegreeDays
    {
        public static GrowingDegreeDays FromJson(string json) => JsonConvert.DeserializeObject<GrowingDegreeDays>(json, GDDConverter.Settings);
    }

    public static class GDDSerialize
    {
        public static string ToJson(this GrowingDegreeDays self) => JsonConvert.SerializeObject(self, GDDConverter.Settings);
    }

    public class GDDConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}