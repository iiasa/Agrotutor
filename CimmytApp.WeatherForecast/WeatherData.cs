// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var weatherData = WeatherData.FromJson(jsonString);

namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class WeatherData
    {
        [JsonProperty("gdd", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Gdd { get; set; }

        [JsonProperty("cdd", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Cdd { get; set; }

        [JsonProperty("hdd", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Hdd { get; set; }

        [JsonProperty("dp", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Dp { get; set; }

        [JsonProperty("hp", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hp { get; set; }

        [JsonProperty("hrh", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hrh { get; set; }

        [JsonProperty("dsr", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Dsr { get; set; }

        [JsonProperty("hsr", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hsr { get; set; }

        [JsonProperty("ht", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Ht { get; set; }

        [JsonProperty("dht", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Dht { get; set; }

        [JsonProperty("dlt", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Dlt { get; set; }

        [JsonProperty("hd", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hd { get; set; }

        [JsonProperty("hws", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hws { get; set; }

        [JsonProperty("hwd", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hwd { get; set; }

        [JsonProperty("desc", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Desc { get; set; }

        [JsonProperty("detc", NullValueHandling = NullValueHandling.Ignore)]
        public Cdd Detc { get; set; }

        [JsonProperty("hesc", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hesc { get; set; }

        [JsonProperty("hetc", NullValueHandling = NullValueHandling.Ignore)]
        public Hd Hetc { get; set; }
    }

    public partial class Cdd
    {
        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("degreeDays", NullValueHandling = NullValueHandling.Ignore)]
        public double? DegreeDays { get; set; }

        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("baseTemperature", NullValueHandling = NullValueHandling.Ignore)]
        public long? BaseTemperature { get; set; }

        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public CddSeries[] Series { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public Unit Unit { get; set; }

        [JsonProperty("precipitation", NullValueHandling = NullValueHandling.Ignore)]
        public double? Precipitation { get; set; }

        [JsonProperty("solarRadiation", NullValueHandling = NullValueHandling.Ignore)]
        public double? SolarRadiation { get; set; }
    }

    public partial class CddSeries
    {
        [JsonProperty("validDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidDate { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Products { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }
    }

    public partial class Unit
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
    }

    public partial class Hd
    {
        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public HdSeries[] Series { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Longitude { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartTime { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public long? Latitude { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EndTime { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public Unit Unit { get; set; }

        [JsonProperty("precipitation", NullValueHandling = NullValueHandling.Ignore)]
        public double? Precipitation { get; set; }
    }

    public partial class HdSeries
    {
        [JsonProperty("validTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidTime { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public Product[] Products { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }
    }

    public enum Product { SkywiseGlobal1HrDewpointTemperatureForecast, SkywiseGlobal1HrEvapotranspirationShortForecast, SkywiseGlobal1HrEvapotranspirationTallForecast, SkywiseGlobal1HrPrecipitationForecast, SkywiseGlobal1HrRelativeHumidityForecast, SkywiseGlobal1HrSolarRadiationForecast, SkywiseGlobal1HrTemperatureForecast, SkywiseGlobal1HrWindDirectionForecast, SkywiseGlobal1HrWindSpeedForecast };

    public partial class WeatherData
    {
        public static WeatherData FromJson(string json) => JsonConvert.DeserializeObject<WeatherData>(json, WeatherDataConverter.Settings);

        /// <summary>
        ///     The Download
        /// </summary>
        /// <param name="latitude">The <see cref="double" /></param>
        /// <param name="longitude">The <see cref="double" /></param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        public static async Task<WeatherData> Download(double latitude, double longitude)
        {
            var serviceUrl =
                $"https://wsgi.geo-wiki.org/skywise_weather?lat={latitude}&lon={longitude}";
            using (var wc = new HttpClient())
            {
                var json = await wc.GetStringAsync(serviceUrl);
                return FromJson(json);
            }
        }
    }

    public static class WeatherDataSerialize
    {
        public static string ToJson(this WeatherData self) => JsonConvert.SerializeObject(self, WeatherDataConverter.Settings);
    }

    internal static class WeatherDataConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                ProductConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ProductConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Product) || t == typeof(Product?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "skywise-global-1hr-dewpoint-temperature-forecast":
                    return Product.SkywiseGlobal1HrDewpointTemperatureForecast;

                case "skywise-global-1hr-evapotranspiration-short-forecast":
                    return Product.SkywiseGlobal1HrEvapotranspirationShortForecast;

                case "skywise-global-1hr-evapotranspiration-tall-forecast":
                    return Product.SkywiseGlobal1HrEvapotranspirationTallForecast;

                case "skywise-global-1hr-precipitation-forecast":
                    return Product.SkywiseGlobal1HrPrecipitationForecast;

                case "skywise-global-1hr-relative-humidity-forecast":
                    return Product.SkywiseGlobal1HrRelativeHumidityForecast;

                case "skywise-global-1hr-solar-radiation-forecast":
                    return Product.SkywiseGlobal1HrSolarRadiationForecast;

                case "skywise-global-1hr-temperature-forecast":
                    return Product.SkywiseGlobal1HrTemperatureForecast;

                case "skywise-global-1hr-wind-direction-forecast":
                    return Product.SkywiseGlobal1HrWindDirectionForecast;

                case "skywise-global-1hr-wind-speed-forecast":
                    return Product.SkywiseGlobal1HrWindSpeedForecast;
            }
            throw new Exception("Cannot unmarshal type Product");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Product)untypedValue;
            switch (value)
            {
                case Product.SkywiseGlobal1HrDewpointTemperatureForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-dewpoint-temperature-forecast");
                    return;

                case Product.SkywiseGlobal1HrEvapotranspirationShortForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-evapotranspiration-short-forecast");
                    return;

                case Product.SkywiseGlobal1HrEvapotranspirationTallForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-evapotranspiration-tall-forecast");
                    return;

                case Product.SkywiseGlobal1HrPrecipitationForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-precipitation-forecast");
                    return;

                case Product.SkywiseGlobal1HrRelativeHumidityForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-relative-humidity-forecast");
                    return;

                case Product.SkywiseGlobal1HrSolarRadiationForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-solar-radiation-forecast");
                    return;

                case Product.SkywiseGlobal1HrTemperatureForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-temperature-forecast");
                    return;

                case Product.SkywiseGlobal1HrWindDirectionForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-wind-direction-forecast");
                    return;

                case Product.SkywiseGlobal1HrWindSpeedForecast:
                    serializer.Serialize(writer, "skywise-global-1hr-wind-speed-forecast");
                    return;
            }
            throw new Exception("Cannot marshal type Product");
        }

        public static readonly ProductConverter Singleton = new ProductConverter();
    }
}