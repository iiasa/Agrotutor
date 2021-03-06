// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;
//
//    var normsResponse = NormsResponse.FromJson(jsonString);

namespace Agrotutor.Modules.Weather.Awhere.API.ResponseEntities
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class NormsResponse
    {
        [JsonProperty("day", NullValueHandling = NullValueHandling.Ignore)]
        public string Day { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseLocation Location { get; set; }

        [JsonProperty("meanTemp", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem MeanTemp { get; set; }

        [JsonProperty("maxTemp", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem MaxTemp { get; set; }

        [JsonProperty("minTemp", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem MinTemp { get; set; }

        [JsonProperty("precipitation", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem Precipitation { get; set; }

        [JsonProperty("solar", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem Solar { get; set; }

        [JsonProperty("minHumidity", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem MinHumidity { get; set; }

        [JsonProperty("maxHumidity", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem MaxHumidity { get; set; }

        [JsonProperty("dailyMaxWind", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem DailyMaxWind { get; set; }

        [JsonProperty("averageWind", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseItem NormsResponseItem { get; set; }

        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseLinks Links { get; set; }
        
        public static NormsResponse FromJson(string json) => JsonConvert.DeserializeObject<NormsResponse>(json, Agrotutor.Modules.Weather.Awhere.API.ResponseEntities.NormsResponseConverter.Settings);
    }

    public class NormsResponseItem
    {
        [JsonProperty("average", NullValueHandling = NullValueHandling.Ignore)]
        public double? Average { get; set; }

        [JsonProperty("stdDev", NullValueHandling = NullValueHandling.Ignore)]
        public double? StdDev { get; set; }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        public string Units { get; set; }
    }

    public class NormsResponseLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public NormsResponseSelf Self { get; set; }
    }

    public class NormsResponseSelf
    {
        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }
    }

    public class NormsResponseLocation
    {
        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }
    }

    public static class NormsResponseSerializer
    {
        public static string ToJson(this NormsResponse self) => JsonConvert.SerializeObject(self, NormsResponseConverter.Settings);
    }

    internal static class NormsResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
