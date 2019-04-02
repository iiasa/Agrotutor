// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;
//
//    var forecastResponse = ForecastResponse.FromJson(jsonString);

namespace Agrotutor.Modules.Weather.Awhere.API.ResponseEntities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ForecastResponse
    {
        [JsonProperty("forecasts", NullValueHandling = NullValueHandling.Ignore)]
        public List<ForecastResponseForecast> Forecasts { get; set; }

        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastLinks Links { get; set; }
        
        public static ForecastResponse FromJson(string json) => JsonConvert.DeserializeObject<ForecastResponse>(json, ForecastResponseConverter.Settings);
    }

    public class ForecastResponseForecast
    {
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Date { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastLocation Location { get; set; }

        [JsonProperty("forecast", NullValueHandling = NullValueHandling.Ignore)]
        public List<ForecastForecast> Forecast { get; set; }

        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastLinks Links { get; set; }
    }

    public class ForecastForecast
    {
        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartTime { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EndTime { get; set; }

        [JsonProperty("conditionsCode", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastConditionsCodeUnion? ConditionsCode { get; set; }

        [JsonProperty("conditionsText", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastConditionsText? ConditionsText { get; set; }

        [JsonProperty("temperatures", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastTemperatures Temperatures { get; set; }

        [JsonProperty("precipitation", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastPrecipitation Precipitation { get; set; }

        [JsonProperty("sky", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastSky Sky { get; set; }

        [JsonProperty("solar", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastDewPoint Solar { get; set; }

        [JsonProperty("relativeHumidity", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastRelativeHumidity RelativeHumidity { get; set; }

        [JsonProperty("wind", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastTemperatures Wind { get; set; }

        [JsonProperty("dewPoint", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastDewPoint DewPoint { get; set; }

        [JsonProperty("soilTemperatures", NullValueHandling = NullValueHandling.Ignore)]
        public List<ForecastRelativeHumidity> SoilTemperatures { get; set; }

        [JsonProperty("soilMoisture", NullValueHandling = NullValueHandling.Ignore)]
        public List<ForecastRelativeHumidity> SoilMoisture { get; set; }
    }

    public class ForecastDewPoint
    {
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastDewPointUnits? Units { get; set; }
    }

    public class ForecastPrecipitation
    {
        [JsonProperty("chance", NullValueHandling = NullValueHandling.Ignore)]
        public double? Chance { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastPrecipitationUnits? Units { get; set; }
    }

    public class ForecastRelativeHumidity
    {
        [JsonProperty("average", NullValueHandling = NullValueHandling.Ignore)]
        public double? Average { get; set; }

        [JsonProperty("max")]
        public object Max { get; set; }

        [JsonProperty("min")]
        public object Min { get; set; }

        [JsonProperty("depth", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastDepth? Depth { get; set; }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastDewPointUnits? Units { get; set; }
    }

    public class ForecastSky
    {
        [JsonProperty("cloudCover", NullValueHandling = NullValueHandling.Ignore)]
        public long? CloudCover { get; set; }

        [JsonProperty("sunshine", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sunshine { get; set; }
    }

    public class ForecastTemperatures
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }

        [JsonProperty("max")]
        public double? Max { get; set; }

        [JsonProperty("min")]
        public double? Min { get; set; }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastTemperaturesUnits? Units { get; set; }

        [JsonProperty("average", NullValueHandling = NullValueHandling.Ignore)]
        public double? Average { get; set; }
    }

    public class ForecastLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public ForecastSelf Self { get; set; }
    }

    public class ForecastSelf
    {
        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }
    }

    public class ForecastLocation
    {
        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }
    }

    public enum ForecastConditionsCodeEnum { A11, A21 };

    public enum ForecastConditionsText { ClearNightLightRainLightWindCalm, ClearNightNoRainLightWindCalm, CloudyDayNoRainLightWindCalm, CloudyNightLightRainLightWindCalm, CloudyNightNoRainLightWindCalm, MostlyClearNightNoRainLightWindCalm, MostlyCloudyDayNoRainLightWindCalm, MostlyCloudyNightNoRainLightWindCalm, MostlySunnyDayNoRainLightWindCalm, PartlyCloudyNightNoRainLightWindCalm, PartlySunnyDayNoRainLightWindCalm, SunnyDayNoRainLightWindCalm };

    public enum ForecastDewPointUnits { C, WhM2 };

    public enum ForecastPrecipitationUnits { Mm };

    public enum ForecastDepth { The001MBelowGround, The0104MBelowGround, The041MBelowGround, The12MBelowGround };

    public enum ForecastTemperaturesUnits { C, MSec };

    public struct ForecastConditionsCodeUnion
    {
        public ForecastConditionsCodeEnum? Enum;
        public long? Integer;

        public static implicit operator ForecastConditionsCodeUnion(ForecastConditionsCodeEnum Enum) => new ForecastConditionsCodeUnion { Enum = Enum };
        public static implicit operator ForecastConditionsCodeUnion(long Integer) => new ForecastConditionsCodeUnion { Integer = Integer };
    }

    public static class ForecastResponseSerializer
    {
        public static string ToJson(this ForecastResponse self) => JsonConvert.SerializeObject(self, Agrotutor.Modules.Weather.Awhere.API.ResponseEntities.ForecastResponseConverter.Settings);
    }

    internal static class ForecastResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ForecastConditionsCodeUnionConverter.Singleton,
                ForecastConditionsCodeEnumConverter.Singleton,
                ForecastConditionsTextConverter.Singleton,
                ForecastDewPointUnitsConverter.Singleton,
                ForecastPrecipitationUnitsConverter.Singleton,
                ForecastDepthConverter.Singleton,
                ForecastTemperaturesUnitsConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ForecastConditionsCodeUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastConditionsCodeUnion) || t == typeof(ForecastConditionsCodeUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "A11":
                            return new ForecastConditionsCodeUnion { Enum = ForecastConditionsCodeEnum.A11 };
                        case "A21":
                            return new ForecastConditionsCodeUnion { Enum = ForecastConditionsCodeEnum.A21 };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new ForecastConditionsCodeUnion { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type ConditionsCodeUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ForecastConditionsCodeUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case ForecastConditionsCodeEnum.A11:
                        serializer.Serialize(writer, "A11");
                        return;
                    case ForecastConditionsCodeEnum.A21:
                        serializer.Serialize(writer, "A21");
                        return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ConditionsCodeUnion");
        }

        public static readonly ForecastConditionsCodeUnionConverter Singleton = new ForecastConditionsCodeUnionConverter();
    }

    internal class ForecastConditionsCodeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastConditionsCodeEnum) || t == typeof(ForecastConditionsCodeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "A11":
                    return ForecastConditionsCodeEnum.A11;
                case "A21":
                    return ForecastConditionsCodeEnum.A21;
            }
            throw new Exception("Cannot unmarshal type ConditionsCodeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastConditionsCodeEnum)untypedValue;
            switch (value)
            {
                case ForecastConditionsCodeEnum.A11:
                    serializer.Serialize(writer, "A11");
                    return;
                case ForecastConditionsCodeEnum.A21:
                    serializer.Serialize(writer, "A21");
                    return;
            }
            throw new Exception("Cannot marshal type ConditionsCodeEnum");
        }

        public static readonly ForecastConditionsCodeEnumConverter Singleton = new ForecastConditionsCodeEnumConverter();
    }

    internal class ForecastConditionsTextConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastConditionsText) || t == typeof(ForecastConditionsText?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Clear Night, Light Rain, Light Wind/Calm":
                    return ForecastConditionsText.ClearNightLightRainLightWindCalm;
                case "Clear Night, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.ClearNightNoRainLightWindCalm;
                case "Cloudy Day, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.CloudyDayNoRainLightWindCalm;
                case "Cloudy Night, Light Rain, Light Wind/Calm":
                    return ForecastConditionsText.CloudyNightLightRainLightWindCalm;
                case "Cloudy Night, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.CloudyNightNoRainLightWindCalm;
                case "Mostly Clear Night, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.MostlyClearNightNoRainLightWindCalm;
                case "Mostly Cloudy Day, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.MostlyCloudyDayNoRainLightWindCalm;
                case "Mostly Cloudy Night, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.MostlyCloudyNightNoRainLightWindCalm;
                case "Mostly Sunny Day, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.MostlySunnyDayNoRainLightWindCalm;
                case "Partly Cloudy Night, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.PartlyCloudyNightNoRainLightWindCalm;
                case "Partly Sunny Day, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.PartlySunnyDayNoRainLightWindCalm;
                case "Sunny Day, No Rain, Light Wind/Calm":
                    return ForecastConditionsText.SunnyDayNoRainLightWindCalm;
            }
            throw new Exception("Cannot unmarshal type ConditionsText");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastConditionsText)untypedValue;
            switch (value)
            {
                case ForecastConditionsText.ClearNightLightRainLightWindCalm:
                    serializer.Serialize(writer, "Clear Night, Light Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.ClearNightNoRainLightWindCalm:
                    serializer.Serialize(writer, "Clear Night, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.CloudyDayNoRainLightWindCalm:
                    serializer.Serialize(writer, "Cloudy Day, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.CloudyNightLightRainLightWindCalm:
                    serializer.Serialize(writer, "Cloudy Night, Light Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.CloudyNightNoRainLightWindCalm:
                    serializer.Serialize(writer, "Cloudy Night, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.MostlyClearNightNoRainLightWindCalm:
                    serializer.Serialize(writer, "Mostly Clear Night, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.MostlyCloudyDayNoRainLightWindCalm:
                    serializer.Serialize(writer, "Mostly Cloudy Day, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.MostlyCloudyNightNoRainLightWindCalm:
                    serializer.Serialize(writer, "Mostly Cloudy Night, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.MostlySunnyDayNoRainLightWindCalm:
                    serializer.Serialize(writer, "Mostly Sunny Day, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.PartlyCloudyNightNoRainLightWindCalm:
                    serializer.Serialize(writer, "Partly Cloudy Night, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.PartlySunnyDayNoRainLightWindCalm:
                    serializer.Serialize(writer, "Partly Sunny Day, No Rain, Light Wind/Calm");
                    return;
                case ForecastConditionsText.SunnyDayNoRainLightWindCalm:
                    serializer.Serialize(writer, "Sunny Day, No Rain, Light Wind/Calm");
                    return;
            }
            throw new Exception("Cannot marshal type ConditionsText");
        }

        public static readonly ForecastConditionsTextConverter Singleton = new ForecastConditionsTextConverter();
    }

    internal class ForecastDewPointUnitsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastDewPointUnits) || t == typeof(ForecastDewPointUnits?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "C":
                    return ForecastDewPointUnits.C;
                case "Wh/m^2":
                    return ForecastDewPointUnits.WhM2;
            }
            throw new Exception("Cannot unmarshal type DewPointUnits");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastDewPointUnits)untypedValue;
            switch (value)
            {
                case ForecastDewPointUnits.C:
                    serializer.Serialize(writer, "C");
                    return;
                case ForecastDewPointUnits.WhM2:
                    serializer.Serialize(writer, "Wh/m^2");
                    return;
            }
            throw new Exception("Cannot marshal type DewPointUnits");
        }

        public static readonly ForecastDewPointUnitsConverter Singleton = new ForecastDewPointUnitsConverter();
    }

    internal class ForecastPrecipitationUnitsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastPrecipitationUnits) || t == typeof(ForecastPrecipitationUnits?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "mm")
            {
                return ForecastPrecipitationUnits.Mm;
            }
            throw new Exception("Cannot unmarshal type PrecipitationUnits");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastPrecipitationUnits)untypedValue;
            if (value == ForecastPrecipitationUnits.Mm)
            {
                serializer.Serialize(writer, "mm");
                return;
            }
            throw new Exception("Cannot marshal type PrecipitationUnits");
        }

        public static readonly ForecastPrecipitationUnitsConverter Singleton = new ForecastPrecipitationUnitsConverter();
    }

    internal class ForecastDepthConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastDepth) || t == typeof(ForecastDepth?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "0-0.1 m below ground":
                    return ForecastDepth.The001MBelowGround;
                case "0.1-0.4 m below ground":
                    return ForecastDepth.The0104MBelowGround;
                case "0.4-1 m below ground":
                    return ForecastDepth.The041MBelowGround;
                case "1-2 m below ground":
                    return ForecastDepth.The12MBelowGround;
            }
            throw new Exception("Cannot unmarshal type Depth");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastDepth)untypedValue;
            switch (value)
            {
                case ForecastDepth.The001MBelowGround:
                    serializer.Serialize(writer, "0-0.1 m below ground");
                    return;
                case ForecastDepth.The0104MBelowGround:
                    serializer.Serialize(writer, "0.1-0.4 m below ground");
                    return;
                case ForecastDepth.The041MBelowGround:
                    serializer.Serialize(writer, "0.4-1 m below ground");
                    return;
                case ForecastDepth.The12MBelowGround:
                    serializer.Serialize(writer, "1-2 m below ground");
                    return;
            }
            throw new Exception("Cannot marshal type Depth");
        }

        public static readonly ForecastDepthConverter Singleton = new ForecastDepthConverter();
    }

    internal class ForecastTemperaturesUnitsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ForecastTemperaturesUnits) || t == typeof(ForecastTemperaturesUnits?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "C":
                    return ForecastTemperaturesUnits.C;
                case "m/sec":
                    return ForecastTemperaturesUnits.MSec;
            }
            throw new Exception("Cannot unmarshal type TemperaturesUnits");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ForecastTemperaturesUnits)untypedValue;
            switch (value)
            {
                case ForecastTemperaturesUnits.C:
                    serializer.Serialize(writer, "C");
                    return;
                case ForecastTemperaturesUnits.MSec:
                    serializer.Serialize(writer, "m/sec");
                    return;
            }
            throw new Exception("Cannot marshal type TemperaturesUnits");
        }

        public static readonly ForecastTemperaturesUnitsConverter Singleton = new ForecastTemperaturesUnitsConverter();
    }
}
