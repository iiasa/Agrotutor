namespace Agrotutor.Modules.Weather.Types
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using Charts.Types;

    public interface Series
    {
        List<EntryWithTime> GetChartEntries();
    }

    public partial class WeatherHistory
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

    public partial class Cdd : Series
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

        public List<EntryWithTime> GetChartEntries()
        {
            var entries = new List<EntryWithTime>();
            foreach (var item in Series)
            {
                if (item.Value == null || item.ValidDate == null) continue;
                entries.Add(new EntryWithTime((float)item.Value)
                {
                    Time = ((DateTimeOffset)item.ValidDate).DateTime,
                    ValueLabel = item.Value?.ToString(),
                    Label = ((DateTimeOffset)item.ValidDate).DateTime.ToShortDateString()
                });
            }
            return entries;
        }
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

    public partial class Hd : Series
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

        public List<EntryWithTime> GetChartEntries()
        {
            var entries = new List<EntryWithTime>();
            foreach (var item in Series)
            {
                if (item.Value == null || item.ValidTime == null) continue;
                entries.Add(new EntryWithTime((float)item.Value)
                {
                    Time = ((DateTimeOffset)item.ValidTime).DateTime,
                    ValueLabel = item.Value?.ToString(),
                    Label = ((DateTimeOffset)item.ValidTime).DateTime.ToShortDateString()
                });
            }
            return entries;
        }
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

    public partial class WeatherHistory
    {
        public static WeatherHistory FromJson(string json) => JsonConvert.DeserializeObject<WeatherHistory>(json, WeatherHistoryConverter.Settings);

        public static async Task<WeatherHistory> Download(double latitude, double longitude)
        {
            var serviceUrl =
                $"https://wsgi.geo-wiki.org/skywise_weather?lat={latitude}&lng={longitude}";
            using (var wc = new HttpClient())
            {
                var json = await wc.GetStringAsync(serviceUrl);
                return FromJson(json);
            }
        }
    }

    public static class WeatherHistorySerialize
    {
        public static string ToJson(this WeatherHistory self) => JsonConvert.SerializeObject(self, WeatherHistoryConverter.Settings);
    }

    internal static class WeatherHistoryConverter
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
