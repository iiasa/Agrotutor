namespace CimmytApp.DTO.Benchmarking
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using Helper.Datatypes;

    public partial class WdtIncInsightData
    {
        [J("baseTemperature")]
        public long BaseTemperature { get; set; }

        [J("degreeDays")]
        public double DegreeDays { get; set; }

        [J("endDate")]
        public string EndDate { get; set; }

        [J("latitude")]
        public long Latitude { get; set; }

        [J("longitude")]
        public long Longitude { get; set; }

        [J("missingValidTimes")]
        public string[] MissingValidTimes { get; set; }

        [J("series")]
        public Series[] Series { get; set; }

        [J("startDate")]
        public string StartDate { get; set; }

        [J("unit")]
        public Unit Unit { get; set; }
    }

    public class Unit
    {
        [J("description")]
        public string Description { get; set; }

        [J("label")]
        public string Label { get; set; }
    }

    public class Series
    {
        [J("products")]
        public string[] Products { get; set; }

        [J("validDate")]
        public string ValidDate { get; set; }

        [J("value")]
        public double Value { get; set; }
    }

    public partial class WdtIncInsightData
    {
        private static readonly string auth = "ZGJmNmIwMWM6MjMwNjhlN2NjMGVmMTU1OTAyMmI2NDlmYzkxNDY0ODg =";

        public static async Task<WdtIncInsightData> Download(WdtIncDataset dataset, double latitude, double longitude, DateTime start,
            DateTime end)
        {
            var startString = start.ToString("yyyy-MM-dd");
            var endString = end.ToString("yyyy-MM-dd");
            var serviceUrl =
                $"https://insight.api.wdtinc.com/{StringEnum.GetStringValue(dataset)}/{latitude}/{longitude}?start={startString}&end={endString}";
            using (var wc = new HttpClient())
            {
                wc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", WdtIncInsightData.auth);
                var json = await wc.GetStringAsync(serviceUrl);
                return FromJson(json);
            }
        }

        public static WdtIncInsightData FromJson(string json) =>
            JsonConvert.DeserializeObject<WdtIncInsightData>(json, WdtIncInsightDataConverter.Settings);
    }

    public static class GDDSerialize
    {
        public static string ToJson(this WdtIncInsightData self) =>
            JsonConvert.SerializeObject(self, WdtIncInsightDataConverter.Settings);
    }

    public class WdtIncInsightDataConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None
        };
    }
}