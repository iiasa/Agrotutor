namespace Agrotutor.Modules.Ciat.Types
{
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class CiatResponseData
    {
        [JsonProperty("nombre_grupo")]
        public string GroupName { get; set; }

        [JsonProperty("cultivo")]
        public string Crop { get; set; }

        [JsonProperty("producto")]
        public string Product { get; set; }

        [JsonProperty("sistema_cultivo")]
        public string CultivationSystem { get; set; }

        [JsonProperty("variable")]
        public string Variable { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("unidad")]
        public string Unit { get; set; }

        [JsonProperty("rango_opt_min")]
        public string RangeOptMin { get; set; }

        [JsonProperty("rango_opt_max")]
        public string RangeOptMax { get; set; }

        [JsonProperty("valor_optimo")]
        public string ValueOptimal { get; set; }

        [JsonProperty("valor_suboptimo")]
        public string ValueSuboptimal { get; set; }
        public static List<CiatResponseData> FromJson(string json) => JsonConvert.DeserializeObject<List<CiatResponseData>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<CiatResponseData> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
