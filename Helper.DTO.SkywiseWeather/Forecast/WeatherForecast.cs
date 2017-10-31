namespace Helper.DTO.SkywiseWeather.Forecast
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="WeatherForecast" />
    /// </summary>
    public partial class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        [JsonProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the Language
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the Units
        /// </summary>
        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Location" />
    /// </summary>
    public partial class Location
    {
        /// <summary>
        /// Gets or sets the DailySummaries
        /// </summary>
        [JsonProperty("daily_summaries")]
        public DailySummaries DailySummaries { get; set; }

        /// <summary>
        /// Gets or sets the Attributes
        /// </summary>
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }

        /// <summary>
        /// Gets or sets the HourlySummaries
        /// </summary>
        [JsonProperty("hourly_summaries")]
        public HourlySummaries HourlySummaries { get; set; }

        /// <summary>
        /// Gets or sets the SfcOb
        /// </summary>
        [JsonProperty("sfc_ob")]
        public Dictionary<string, string> SfcOb { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="DailySummaries" />
    /// </summary>
    public partial class DailySummaries
    {
        /// <summary>
        /// Gets or sets the DailySummary
        /// </summary>
        [JsonProperty("daily_summary")]
        public DailySummary[] DailySummary { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="DailySummary" />
    /// </summary>
    public partial class DailySummary
    {
        /// <summary>
        /// Gets or sets the MaxTempC
        /// </summary>
        [JsonProperty("max_temp_C")]
        public string MaxTempC { get; set; }

        /// <summary>
        /// Gets or sets the SolunarMoonsetUtc
        /// </summary>
        [JsonProperty("solunar_moonset_utc")]
        public string SolunarMoonsetUtc { get; set; }

        /// <summary>
        /// Gets or sets the Gdd
        /// </summary>
        [JsonProperty("gdd")]
        public string Gdd { get; set; }

        /// <summary>
        /// Gets or sets the Cdd2
        /// </summary>
        [JsonProperty("cdd2")]
        public string Cdd2 { get; set; }

        /// <summary>
        /// Gets or sets the Cdd1
        /// </summary>
        [JsonProperty("cdd1")]
        public string Cdd1 { get; set; }

        /// <summary>
        /// Gets or sets the DayOfWeek
        /// </summary>
        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the Hdd1
        /// </summary>
        [JsonProperty("hdd1")]
        public string Hdd1 { get; set; }

        /// <summary>
        /// Gets or sets the Gdu
        /// </summary>
        [JsonProperty("gdu")]
        public string Gdu { get; set; }

        /// <summary>
        /// Gets or sets the Hdd2
        /// </summary>
        [JsonProperty("hdd2")]
        public string Hdd2 { get; set; }

        /// <summary>
        /// Gets or sets the Pop
        /// </summary>
        [JsonProperty("pop")]
        public string Pop { get; set; }

        /// <summary>
        /// Gets or sets the MinTempC
        /// </summary>
        [JsonProperty("min_temp_C")]
        public string MinTempC { get; set; }

        /// <summary>
        /// Gets or sets the MaxWndSpdKph
        /// </summary>
        [JsonProperty("max_wnd_spd_kph")]
        public string MaxWndSpdKph { get; set; }

        /// <summary>
        /// Gets or sets the MinWndSpdKph
        /// </summary>
        [JsonProperty("min_wnd_spd_kph")]
        public string MinWndSpdKph { get; set; }

        /// <summary>
        /// Gets or sets the SolunarMoonState
        /// </summary>
        [JsonProperty("solunar_moon_state")]
        public Qpf01hrMm SolunarMoonState { get; set; }

        /// <summary>
        /// Gets or sets the Qpf24hrMm
        /// </summary>
        [JsonProperty("qpf_24hr_mm")]
        public string Qpf24hrMm { get; set; }

        /// <summary>
        /// Gets or sets the SolunarMoonriseUtc
        /// </summary>
        [JsonProperty("solunar_moonrise_utc")]
        public string SolunarMoonriseUtc { get; set; }

        /// <summary>
        /// Gets or sets the SummaryDate
        /// </summary>
        [JsonProperty("summary_date")]
        public string SummaryDate { get; set; }

        /// <summary>
        /// Gets or sets the WndSpdKph
        /// </summary>
        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        /// <summary>
        /// Gets or sets the SolunarSunriseUtc
        /// </summary>
        [JsonProperty("solunar_sunrise_utc")]
        public string SolunarSunriseUtc { get; set; }

        /// <summary>
        /// Gets or sets the SolunarSunState
        /// </summary>
        [JsonProperty("solunar_sun_state")]
        public Qpf01hrMm SolunarSunState { get; set; }

        /// <summary>
        /// Gets or sets the SolunarSunsetUtc
        /// </summary>
        [JsonProperty("solunar_sunset_utc")]
        public string SolunarSunsetUtc { get; set; }

        /// <summary>
        /// Gets or sets the WndDir
        /// </summary>
        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        /// <summary>
        /// Gets or sets the TextDescription
        /// </summary>
        [JsonProperty("text_description")]
        public string TextDescription { get; set; }

        /// <summary>
        /// Gets or sets the WndGustKph
        /// </summary>
        [JsonProperty("wnd_gust_kph")]
        public string WndGustKph { get; set; }

        /// <summary>
        /// Gets or sets the WxCode
        /// </summary>
        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        /// <summary>
        /// Gets or sets the WxIconUrl
        /// </summary>
        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        /// <summary>
        /// Gets or sets the Wx
        /// </summary>
        [JsonProperty("wx")]
        public string Wx { get; set; }

        /// <summary>
        /// Gets or sets the WxIconText
        /// </summary>
        [JsonProperty("wx_icon_text")]
        public string WxIconText { get; set; }

        /// <summary>
        /// Gets or sets the WxIconUrlPng
        /// </summary>
        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Qpf01hrMm" />
    /// </summary>
    public partial class Qpf01hrMm
    {
    }

    /// <summary>
    /// Defines the <see cref="Attributes" />
    /// </summary>
    public partial class Attributes
    {
        /// <summary>
        /// Gets or sets the LocalOffsetHours
        /// </summary>
        [JsonProperty("local_offset_hours")]
        public string LocalOffsetHours { get; set; }

        /// <summary>
        /// Gets or sets the Country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Lat
        /// </summary>
        [JsonProperty("lat")]
        public string Lat { get; set; }

        /// <summary>
        /// Gets or sets the Offset
        /// </summary>
        [JsonProperty("offset")]
        public string Offset { get; set; }

        /// <summary>
        /// Gets or sets the Timezone
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the Lon
        /// </summary>
        [JsonProperty("lon")]
        public string Lon { get; set; }

        /// <summary>
        /// Gets or sets the Region
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the Zipcode
        /// </summary>
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="HourlySummaries" />
    /// </summary>
    public partial class HourlySummaries
    {
        /// <summary>
        /// Gets or sets the HourlySummary
        /// </summary>
        [JsonProperty("hourly_summary")]
        public HourlySummary[] HourlySummary { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="HourlySummary" />
    /// </summary>
    public partial class HourlySummary
    {
        /// <summary>
        /// Gets or sets the RhPct
        /// </summary>
        [JsonProperty("rh_pct")]
        public string RhPct { get; set; }

        /// <summary>
        /// Gets or sets the WndSpd100mMs
        /// </summary>
        [JsonProperty("wnd_spd_100m_ms")]
        public string WndSpd100mMs { get; set; }

        /// <summary>
        /// Gets or sets the DewpC
        /// </summary>
        [JsonProperty("dewp_C")]
        public string DewpC { get; set; }

        /// <summary>
        /// Gets or sets the DayNight
        /// </summary>
        [JsonProperty("day_night")]
        public string DayNight { get; set; }

        /// <summary>
        /// Gets or sets the AppTempC
        /// </summary>
        [JsonProperty("app_temp_C")]
        public string AppTempC { get; set; }

        /// <summary>
        /// Gets or sets the DayOfWeekUtc
        /// </summary>
        [JsonProperty("day_of_week_utc")]
        public string DayOfWeekUtc { get; set; }

        /// <summary>
        /// Gets or sets the Pop
        /// </summary>
        [JsonProperty("pop")]
        public string Pop { get; set; }

        /// <summary>
        /// Gets or sets the PmslMb
        /// </summary>
        [JsonProperty("pmsl_mb")]
        public string PmslMb { get; set; }

        /// <summary>
        /// Gets or sets the Qpf01hrMm
        /// </summary>
        [JsonProperty("qpf_01hr_mm")]
        public OtherQpf01hrMm Qpf01hrMm { get; set; }

        /// <summary>
        /// Gets or sets the VisibilityM
        /// </summary>
        [JsonProperty("visibility_m")]
        public string VisibilityM { get; set; }

        /// <summary>
        /// Gets or sets the TempC
        /// </summary>
        [JsonProperty("temp_C")]
        public string TempC { get; set; }

        /// <summary>
        /// Gets or sets the SkyCovPct
        /// </summary>
        [JsonProperty("sky_cov_pct")]
        public string SkyCovPct { get; set; }

        /// <summary>
        /// Gets or sets the TimeUtc
        /// </summary>
        [JsonProperty("time_utc")]
        public string TimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the WndDirDegs
        /// </summary>
        [JsonProperty("wnd_dir_degs")]
        public string WndDirDegs { get; set; }

        /// <summary>
        /// Gets or sets the WndDir
        /// </summary>
        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        /// <summary>
        /// Gets or sets the WndSpd100mKph
        /// </summary>
        [JsonProperty("wnd_spd_100m_kph")]
        public string WndSpd100mKph { get; set; }

        /// <summary>
        /// Gets or sets the Wx
        /// </summary>
        [JsonProperty("wx")]
        public string Wx { get; set; }

        /// <summary>
        /// Gets or sets the WndSpd80mMs
        /// </summary>
        [JsonProperty("wnd_spd_80m_ms")]
        public string WndSpd80mMs { get; set; }

        /// <summary>
        /// Gets or sets the WndSpd80mKph
        /// </summary>
        [JsonProperty("wnd_spd_80m_kph")]
        public string WndSpd80mKph { get; set; }

        /// <summary>
        /// Gets or sets the WndSpdKph
        /// </summary>
        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        /// <summary>
        /// Gets or sets the WxIconUrl
        /// </summary>
        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        /// <summary>
        /// Gets or sets the WxCode
        /// </summary>
        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        /// <summary>
        /// Gets or sets the WxIconUrlPng
        /// </summary>
        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="OtherQpf01hrMm" />
    /// </summary>
    public partial struct OtherQpf01hrMm
    {
        /// <summary>
        /// Defines the String
        /// </summary>
        public string String;

        /// <summary>
        /// Defines the Qpf01hrMm
        /// </summary>
        public Qpf01hrMm Qpf01hrMm;
    }

    /// <summary>
    /// Defines the <see cref="WeatherForecast" />
    /// </summary>
    public partial class WeatherForecast
    {
        /// <summary>
        /// The FromJson
        /// </summary>
        /// <param name="json">The <see cref="string"/></param>
        /// <returns>The <see cref="WeatherForecast"/></returns>
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

    /// <summary>
    /// Defines the <see cref="Serialize" />
    /// </summary>
    public static class Serialize
    {
        /// <summary>
        /// The ToJson
        /// </summary>
        /// <param name="self">The <see cref="WeatherForecast"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToJson(this WeatherForecast self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    /// <summary>
    /// Defines the <see cref="OtherQpf01hrMm" />
    /// </summary>
    public partial struct OtherQpf01hrMm
    {
        public OtherQpf01hrMm(JsonReader reader, JsonSerializer serializer)
        {
            String = null;
            Qpf01hrMm = null;

            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    String = serializer.Deserialize<string>(reader);
                    break;

                case JsonToken.StartObject:
                    Qpf01hrMm = serializer.Deserialize<Qpf01hrMm>(reader);
                    break;

                default: throw new Exception("Cannot convert OtherQpf01hrMm");
            }
        }
        /// <summary>
        /// The WriteJson
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/></param>
        /// <param name="serializer">The <see cref="JsonSerializer"/></param>
        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            if (String != null)
            {
                serializer.Serialize(writer, String);
                return;
            }
            if (Qpf01hrMm != null)
            {
                serializer.Serialize(writer, Qpf01hrMm);
                return;
            }
            throw new Exception("Union must not be null");
        }
    }

    /// <summary>
    /// Defines the <see cref="Converter" />
    /// </summary>
    public class Converter : JsonConverter
    {
        /// <summary>
        /// The CanConvert
        /// </summary>
        /// <param name="t">The <see cref="Type"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool CanConvert(Type t) => t == typeof(OtherQpf01hrMm);

        /// <summary>
        /// The ReadJson
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/></param>
        /// <param name="t">The <see cref="Type"/></param>
        /// <param name="existingValue">The <see cref="object"/></param>
        /// <param name="serializer">The <see cref="JsonSerializer"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(OtherQpf01hrMm))
                return new OtherQpf01hrMm(reader, serializer);
            throw new Exception("Unknown type");
        }

        /// <summary>
        /// The WriteJson
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/></param>
        /// <param name="value">The <see cref="object"/></param>
        /// <param name="serializer">The <see cref="JsonSerializer"/></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == typeof(OtherQpf01hrMm))
            {
                ((OtherQpf01hrMm)value).WriteJson(writer, serializer);
                return;
            }
            throw new Exception("Unknown type");
        }

        /// <summary>
        /// Defines the Settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new Converter() },
        };
    }
}