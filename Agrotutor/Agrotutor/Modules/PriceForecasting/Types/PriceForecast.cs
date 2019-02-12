using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Agrotutor.Modules.PriceForecasting.Types
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PriceForecast
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("Crop", NullValueHandling = NullValueHandling.Ignore)]
        public Crop? Crop { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public long? Year { get; set; }

        [JsonProperty("month", NullValueHandling = NullValueHandling.Ignore)]
        public long? Month { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        [JsonProperty("minPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? MinPrice { get; set; }

        [JsonProperty("maxPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? MaxPrice { get; set; }

        public static async Task<IEnumerable<PriceForecast>> FromEmbeddedResource()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Agrotutor.Resources.AppData.priceforecast.json";

            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = await reader.ReadToEndAsync();
            }

            return FromJson(result);
        }
    }

    public enum Crop { Corn };

    public partial class PriceForecast
    {
        public static List<PriceForecast> FromJson(string json) => JsonConvert.DeserializeObject<List<PriceForecast>>(json, Agrotutor.Modules.PriceForecasting.Types.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<PriceForecast> self) => JsonConvert.SerializeObject(self, Agrotutor.Modules.PriceForecasting.Types.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CropConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CropConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Crop) || t == typeof(Crop?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Corn")
            {
                return Crop.Corn;
            }
            throw new Exception("Cannot unmarshal type Crop");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Crop)untypedValue;
            if (value == Crop.Corn)
            {
                serializer.Serialize(writer, "Corn");
                return;
            }
            throw new Exception("Cannot marshal type Crop");
        }

        public static readonly CropConverter Singleton = new CropConverter();
    }
}
