using Microsoft.Extensions.Localization;
using Prism;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Core.Cimmyt.InvestigationPlatforms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class InvestigationPlatforms
    {
        [JsonProperty("crs")]
        public Crs Crs { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public static async Task<InvestigationPlatforms> FromEmbeddedResource()
        {
            var stringLocalizer = (IStringLocalizer<InvestigationPlatforms>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<InvestigationPlatforms>));
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(message: stringLocalizer.GetString("loading")))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = Constants.InvestigationPlatformsFile;

                string result;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = await reader.ReadToEndAsync();
                }

                return FromJson(result);
            }
        }

        public static InvestigationPlatforms FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InvestigationPlatforms>(json, Converter.Settings);
        }
    }

    public enum GeometryType
    {
        Point
    }

    public enum CicloAgri
    {
        Ambos,

        Oi,

        Pv
    }

    public enum RegHum
    {
        Na,

        Riego,

        Temporal
    }

    public enum FeatureType
    {
        Feature
    }

    public static class Serialize
    {
        public static string ToJson(this InvestigationPlatforms self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
                                                                 {
                                                                     MetadataPropertyHandling =
                                                                         MetadataPropertyHandling.Ignore,
                                                                     DateParseHandling = DateParseHandling.None,
                                                                     Converters =
                                                                     {
                                                                         GeometryTypeConverter.Singleton,
                                                                         CicloAgriConverter.Singleton,
                                                                         RegHumConverter.Singleton,
                                                                         FeatureTypeConverter.Singleton,
                                                                         new IsoDateTimeConverter
                                                                         {
                                                                             DateTimeStyles =
                                                                                 DateTimeStyles.AssumeUniversal
                                                                         }
                                                                     }
                                                                 };
    }

    internal class GeometryTypeConverter : JsonConverter
    {
        public static readonly GeometryTypeConverter Singleton = new GeometryTypeConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(GeometryType) || t == typeof(GeometryType?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            string value = serializer.Deserialize<string>(reader);
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

            GeometryType value = (GeometryType)untypedValue;
            if (value == GeometryType.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }

            throw new Exception("Cannot marshal type GeometryType");
        }
    }

    internal class CicloAgriConverter : JsonConverter
    {
        public static readonly CicloAgriConverter Singleton = new CicloAgriConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(CicloAgri) || t == typeof(CicloAgri?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            string value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Ambos":
                    return CicloAgri.Ambos;
                case "OI":
                    return CicloAgri.Oi;
                case "PV":
                    return CicloAgri.Pv;
            }

            throw new Exception("Cannot unmarshal type CicloAgri");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            CicloAgri value = (CicloAgri)untypedValue;
            switch (value)
            {
                case CicloAgri.Ambos:
                    serializer.Serialize(writer, "Ambos");
                    return;
                case CicloAgri.Oi:
                    serializer.Serialize(writer, "OI");
                    return;
                case CicloAgri.Pv:
                    serializer.Serialize(writer, "PV");
                    return;
            }

            throw new Exception("Cannot marshal type CicloAgri");
        }
    }

    internal class RegHumConverter : JsonConverter
    {
        public static readonly RegHumConverter Singleton = new RegHumConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(RegHum) || t == typeof(RegHum?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            string value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "NA":
                    return RegHum.Na;
                case "Riego":
                    return RegHum.Riego;
                case "Temporal":
                    return RegHum.Temporal;
            }

            throw new Exception("Cannot unmarshal type RegHum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            RegHum value = (RegHum)untypedValue;
            switch (value)
            {
                case RegHum.Na:
                    serializer.Serialize(writer, "NA");
                    return;
                case RegHum.Riego:
                    serializer.Serialize(writer, "Riego");
                    return;
                case RegHum.Temporal:
                    serializer.Serialize(writer, "Temporal");
                    return;
            }

            throw new Exception("Cannot marshal type RegHum");
        }
    }

    internal class FeatureTypeConverter : JsonConverter
    {
        public static readonly FeatureTypeConverter Singleton = new FeatureTypeConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(FeatureType) || t == typeof(FeatureType?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            string value = serializer.Deserialize<string>(reader);
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

            FeatureType value = (FeatureType)untypedValue;
            if (value == FeatureType.Feature)
            {
                serializer.Serialize(writer, "Feature");
                return;
            }

            throw new Exception("Cannot marshal type FeatureType");
        }
    }
}