namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public partial class WeatherForecast
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("daily_summaries")]
        public DailySummaries DailySummaries { get; set; }

        [JsonProperty("hourly_summaries")]
        public HourlySummaries HourlySummaries { get; set; }

        [JsonProperty("sfc_ob")]
        public SfcOb SfcOb { get; set; }
    }

    public partial class SfcOb
    {
        [JsonProperty("apparent_temp_C")]
        public string ApparentTempC { get; set; }

        [JsonProperty("cld_cover")]
        public string CldCover { get; set; }

        [JsonProperty("day_night")]
        public string DayNight { get; set; }

        [JsonProperty("day_of_week_local")]
        public string DayOfWeekLocal { get; set; }

        [JsonProperty("dewp_C")]
        public string DewpC { get; set; }

        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }

        [JsonProperty("ob_time")]
        public string ObTime { get; set; }

        [JsonProperty("press_mb")]
        public string PressMb { get; set; }

        [JsonProperty("rh_pct")]
        public string RhPct { get; set; }

        [JsonProperty("stn")]
        public string Stn { get; set; }

        [JsonProperty("stn_lat")]
        public string StnLat { get; set; }

        [JsonProperty("stn_lon")]
        public string StnLon { get; set; }

        [JsonProperty("temp_C")]
        public string TempC { get; set; }

        [JsonProperty("visibility_m")]
        public string VisibilityM { get; set; }

        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        [JsonProperty("wnd_dir_degs")]
        public string WndDirDegs { get; set; }

        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        [JsonProperty("wx")]
        public string Wx { get; set; }

        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    public partial class HourlySummaries
    {
        [JsonProperty("hourly_summary")]
        public List<HourlySummary> HourlySummary { get; set; }
    }

    public partial class HourlySummary
    {
        [JsonProperty("app_temp_C")]
        public string AppTempC { get; set; }

        [JsonProperty("day_night")]
        public string DayNight { get; set; }

        [JsonProperty("day_of_week_utc")]
        public string DayOfWeekUtc { get; set; }

        [JsonProperty("dewp_C")]
        public string DewpC { get; set; }

        [JsonProperty("pmsl_mb")]
        public string PmslMb { get; set; }

        [JsonProperty("pop")]
        public string Pop { get; set; }

        [JsonProperty("qpf_01hr_mm")]
        public Qpf01hrMm Qpf01hrMm { get; set; }

        [JsonProperty("rh_pct")]
        public string RhPct { get; set; }

        [JsonProperty("sky_cov_pct")]
        public string SkyCovPct { get; set; }

        [JsonProperty("temp_C")]
        public string TempC { get; set; }

        [JsonProperty("time_utc")]
        public string TimeUtc { get; set; }

        [JsonProperty("visibility_m")]
        public string VisibilityM { get; set; }

        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        [JsonProperty("wnd_dir_degs")]
        public string WndDirDegs { get; set; }

        [JsonProperty("wnd_spd_100m_kph")]
        public string WndSpd100mKph { get; set; }

        [JsonProperty("wnd_spd_100m_ms")]
        public string WndSpd100mMs { get; set; }

        [JsonProperty("wnd_spd_80m_kph")]
        public string WndSpd80mKph { get; set; }

        [JsonProperty("wnd_spd_80m_ms")]
        public string WndSpd80mMs { get; set; }

        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        [JsonProperty("wx")]
        public string Wx { get; set; }

        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    public partial class DailySummaries
    {
        [JsonProperty("daily_summary")]
        public List<DailySummary> DailySummary { get; set; }
    }

    public partial class DailySummary
    {
        [JsonProperty("cdd1")]
        public string Cdd1 { get; set; }

        [JsonProperty("cdd2")]
        public string Cdd2 { get; set; }

        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }

        [JsonProperty("gdd")]
        public string Gdd { get; set; }

        [JsonProperty("gdu")]
        public string Gdu { get; set; }

        [JsonProperty("hdd1")]
        public string Hdd1 { get; set; }

        [JsonProperty("hdd2")]
        public string Hdd2 { get; set; }

        [JsonProperty("max_temp_C")]
        public string MaxTempC { get; set; }

        [JsonProperty("max_wnd_spd_kph")]
        public string MaxWndSpdKph { get; set; }

        [JsonProperty("min_temp_C")]
        public string MinTempC { get; set; }

        [JsonProperty("min_wnd_spd_kph")]
        public string MinWndSpdKph { get; set; }

        [JsonProperty("pop")]
        public string Pop { get; set; }

        [JsonProperty("qpf_24hr_mm")]
        public string Qpf24hrMm { get; set; }

        [JsonProperty("solunar_moon_state")]
        public string SolunarMoonState { get; set; }

        [JsonProperty("solunar_moonrise_utc")]
        public string SolunarMoonriseUtc { get; set; }

        [JsonProperty("solunar_moonset_utc")]
        public string SolunarMoonsetUtc { get; set; }

        [JsonProperty("solunar_sun_state")]
        public string SolunarSunState { get; set; }

        [JsonProperty("solunar_sunrise_utc")]
        public string SolunarSunriseUtc { get; set; }

        [JsonProperty("solunar_sunset_utc")]
        public string SolunarSunsetUtc { get; set; }

        [JsonProperty("summary_date")]
        public string SummaryDate { get; set; }

        [JsonProperty("text_description")]
        public string TextDescription { get; set; }

        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        [JsonProperty("wnd_gust_kph")]
        public string WndGustKph { get; set; }

        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        [JsonProperty("wx")]
        public string Wx { get; set; }

        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        [JsonProperty("wx_icon_text")]
        public string WxIconText { get; set; }

        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("local_offset_hours")]
        public string LocalOffsetHours { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
    }

    public partial struct Qpf01hrMm
    {
        public string String;
        public string PressMb;
    }

    public partial class WeatherForecast
    {
        public static WeatherForecast FromJson(string json) => JsonConvert.DeserializeObject<WeatherForecast>(json, Converter.Settings);

        /// <summary>
        /// The Download
        /// </summary>
        /// <param name="latitude">The <see cref="double"/></param>
        /// <param name="longitude">The <see cref="double"/></param>
        /// <returns>The <see cref="Task{WeatherForecast}"/></returns>
        public static async Task<WeatherForecast> Download(double latitude, double longitude)
        {
            var serviceUrl = $"https://skywisefeeds.wdtinc.com/feeds/api/mega.php?LAT={latitude}&LON={longitude}&FORMAT=json";
            using (var wc = new HttpClient())
            {
                wc.DefaultRequestHeaders.Add("app_id", "dc9e4567");
                wc.DefaultRequestHeaders.Add("app_key", "9547e002315e9cf9d6f7362675d63f1f");
                var json = await wc.GetStringAsync(serviceUrl);
                return WeatherForecast.FromJson(json);
            }
        }
    }

    public partial struct Qpf01hrMm
    {
        public Qpf01hrMm(JsonReader reader, JsonSerializer serializer)
        {
            String = null;
            PressMb = null;

            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    String = serializer.Deserialize<string>(reader);
                    break;

                case JsonToken.StartObject:
                    PressMb = serializer.Deserialize<string>(reader);
                    break;

                default: throw new Exception("Cannot convert Qpf01hrMm");
            }
        }

        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            if (String != null)
            {
                serializer.Serialize(writer, String);
                return;
            }
            if (PressMb != null)
            {
                serializer.Serialize(writer, PressMb);
                return;
            }
            throw new Exception("Union must not be null");
        }
    }

    public static class Serialize
    {
        public static string ToJson(this WeatherForecast self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Qpf01hrMm);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(Qpf01hrMm))
                return new Qpf01hrMm(reader, serializer);
            throw new Exception("Unknown type");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == typeof(Qpf01hrMm))
            {
                ((Qpf01hrMm)value).WriteJson(writer, serializer);
                return;
            }
            throw new Exception("Unknown type");
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new Converter() },
        };
    }
}