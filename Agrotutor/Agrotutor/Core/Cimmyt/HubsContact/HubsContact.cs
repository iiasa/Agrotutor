namespace Agrotutor.Core.Cimmyt.HubsContact
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Microsoft.Extensions.Localization;
    using Prism;
    using XF.Material.Forms.UI.Dialogs;

    public class HubsContact
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("crs")]
        public Crs Crs { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        public static HubsContact FromJson(string json) => JsonConvert.DeserializeObject<HubsContact>(json, Converter.Settings);

        public static async Task<HubsContact> FromEmbeddedResource()
        {
            var stringLocalizer = (IStringLocalizer<HubsContact>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<HubsContact>));
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(message: stringLocalizer.GetString("loading")))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = Constants.HubsContactFile;

                string result;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = await reader.ReadToEndAsync();
                }

                return FromJson(result);
            }
        }

    }

    public enum GeometryType { Point };

    public enum FeatureType { Feature };

    public static class Serialize
    {
        public static string ToJson(this HubsContact self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GeometryTypeConverter.Singleton,
                FeatureTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GeometryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeometryType) || t == typeof(GeometryType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Point")
            {
                return GeometryType.Point;
            }
            throw new Exception("Cannot unmarshal type GeometryType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeometryType)untypedValue;
            if (value == GeometryType.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }
            throw new Exception("Cannot marshal type GeometryType");
        }

        public static readonly GeometryTypeConverter Singleton = new GeometryTypeConverter();
    }

    internal class FeatureTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FeatureType) || t == typeof(FeatureType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Feature")
            {
                return FeatureType.Feature;
            }
            throw new Exception("Cannot unmarshal type FeatureType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FeatureType)untypedValue;
            if (value == FeatureType.Feature)
            {
                serializer.Serialize(writer, "Feature");
                return;
            }
            throw new Exception("Cannot marshal type FeatureType");
        }

        public static readonly FeatureTypeConverter Singleton = new FeatureTypeConverter();
    }
}
