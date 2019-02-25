using Microsoft.Extensions.Localization;
using Prism;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Core.Cimmyt.MachineryPoints
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class MachineryPoints
    {
        [JsonProperty("crs")]
        public Crs Crs { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public static async Task<MachineryPoints> FromEmbeddedResource()
        {
            var stringLocalizer = (IStringLocalizer<MachineryPoints>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<MachineryPoints>));
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(message: stringLocalizer.GetString("loading")))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = Constants.MachineryPointsFile;

                string result;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = await reader.ReadToEndAsync();
                }

                return FromJson(result);
            }
        }

        public static MachineryPoints FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MachineryPoints>(json, Converter.Settings);
        }
    }

    public enum GeometryType
    {
        Point
    }

    public enum Precomodat
    {
        Empty,

        Na,

        Ñ
    }

    public enum FeatureType
    {
        Feature
    }

    public static class Serialize
    {
        public static string ToJson(this MachineryPoints self)
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
                    PrecomodatConverter.Singleton,
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

    internal class PrecomodatConverter : JsonConverter
    {
        public static readonly PrecomodatConverter Singleton = new PrecomodatConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(Precomodat) || t == typeof(Precomodat?);
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
                case "-":
                    return Precomodat.Empty;
                case "NA":
                    return Precomodat.Na;
                case "Ñ´":
                    return Precomodat.Ñ;
            }

            throw new Exception("Cannot unmarshal type Precomodat");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            Precomodat value = (Precomodat)untypedValue;
            switch (value)
            {
                case Precomodat.Empty:
                    serializer.Serialize(writer, "-");
                    return;
                case Precomodat.Na:
                    serializer.Serialize(writer, "NA");
                    return;
                case Precomodat.Ñ:
                    serializer.Serialize(writer, "Ñ´");
                    return;
            }

            throw new Exception("Cannot marshal type Precomodat");
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