namespace Helper.DTO.SkywiseWeather.Forecast
{
	using System;
	using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

	public partial class WeatherForecast
	{
		[JsonProperty("location")]
		public Location Location { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("units")]
		public string Units { get; set; }
	}

	public partial class Location
	{
		[JsonProperty("daily_summaries")]
		public DailySummaries DailySummaries { get; set; }

		[JsonProperty("@attributes")]
		public Attributes Attributes { get; set; }

		[JsonProperty("hourly_summaries")]
		public HourlySummaries HourlySummaries { get; set; }

		[JsonProperty("sfc_ob")]
		public Dictionary<string, string> SfcOb { get; set; }
	}

	public partial class DailySummaries
	{
		[JsonProperty("daily_summary")]
		public DailySummary[] DailySummary { get; set; }
	}

	public partial class DailySummary
	{
		[JsonProperty("max_temp_C")]
		public string MaxTempC { get; set; }

		[JsonProperty("solunar_moonset_utc")]
		public string SolunarMoonsetUtc { get; set; }

		[JsonProperty("gdd")]
		public string Gdd { get; set; }

		[JsonProperty("cdd2")]
		public string Cdd2 { get; set; }

		[JsonProperty("cdd1")]
		public string Cdd1 { get; set; }

		[JsonProperty("day_of_week")]
		public string DayOfWeek { get; set; }

		[JsonProperty("hdd1")]
		public string Hdd1 { get; set; }

		[JsonProperty("gdu")]
		public string Gdu { get; set; }

		[JsonProperty("hdd2")]
		public string Hdd2 { get; set; }

		[JsonProperty("pop")]
		public string Pop { get; set; }

		[JsonProperty("min_temp_C")]
		public string MinTempC { get; set; }

		[JsonProperty("max_wnd_spd_kph")]
		public string MaxWndSpdKph { get; set; }

		[JsonProperty("min_wnd_spd_kph")]
		public string MinWndSpdKph { get; set; }

		[JsonProperty("solunar_moon_state")]
		public Qpf01hrMm SolunarMoonState { get; set; }

		[JsonProperty("qpf_24hr_mm")]
		public string Qpf24hrMm { get; set; }

		[JsonProperty("solunar_moonrise_utc")]
		public string SolunarMoonriseUtc { get; set; }

		[JsonProperty("summary_date")]
		public string SummaryDate { get; set; }

		[JsonProperty("wnd_spd_kph")]
		public string WndSpdKph { get; set; }

		[JsonProperty("solunar_sunrise_utc")]
		public string SolunarSunriseUtc { get; set; }

		[JsonProperty("solunar_sun_state")]
		public Qpf01hrMm SolunarSunState { get; set; }

		[JsonProperty("solunar_sunset_utc")]
		public string SolunarSunsetUtc { get; set; }

		[JsonProperty("wnd_dir")]
		public string WndDir { get; set; }

		[JsonProperty("text_description")]
		public string TextDescription { get; set; }

		[JsonProperty("wnd_gust_kph")]
		public string WndGustKph { get; set; }

		[JsonProperty("wx_code")]
		public string WxCode { get; set; }

		[JsonProperty("wx_icon_url")]
		public string WxIconUrl { get; set; }

		[JsonProperty("wx")]
		public string Wx { get; set; }

		[JsonProperty("wx_icon_text")]
		public string WxIconText { get; set; }

		[JsonProperty("wx_icon_url_png")]
		public string WxIconUrlPng { get; set; }
	}

	public partial class Qpf01hrMm
	{
	}

	public partial class Attributes
	{
		[JsonProperty("local_offset_hours")]
		public string LocalOffsetHours { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("lat")]
		public string Lat { get; set; }

		[JsonProperty("offset")]
		public string Offset { get; set; }

		[JsonProperty("timezone")]
		public string Timezone { get; set; }

		[JsonProperty("lon")]
		public string Lon { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("zipcode")]
		public string Zipcode { get; set; }
	}

	public partial class HourlySummaries
	{
		[JsonProperty("hourly_summary")]
		public HourlySummary[] HourlySummary { get; set; }
	}

	public partial class HourlySummary
	{
		[JsonProperty("rh_pct")]
		public string RhPct { get; set; }

		[JsonProperty("wnd_spd_100m_ms")]
		public string WndSpd100mMs { get; set; }

		[JsonProperty("dewp_C")]
		public string DewpC { get; set; }

		[JsonProperty("day_night")]
		public string DayNight { get; set; }

		[JsonProperty("app_temp_C")]
		public string AppTempC { get; set; }

		[JsonProperty("day_of_week_utc")]
		public string DayOfWeekUtc { get; set; }

		[JsonProperty("pop")]
		public string Pop { get; set; }

		[JsonProperty("pmsl_mb")]
		public string PmslMb { get; set; }

		[JsonProperty("qpf_01hr_mm")]
		public OtherQpf01hrMm Qpf01hrMm { get; set; }

		[JsonProperty("visibility_m")]
		public string VisibilityM { get; set; }

		[JsonProperty("temp_C")]
		public string TempC { get; set; }

		[JsonProperty("sky_cov_pct")]
		public string SkyCovPct { get; set; }

		[JsonProperty("time_utc")]
		public string TimeUtc { get; set; }

		[JsonProperty("wnd_dir_degs")]
		public string WndDirDegs { get; set; }

		[JsonProperty("wnd_dir")]
		public string WndDir { get; set; }

		[JsonProperty("wnd_spd_100m_kph")]
		public string WndSpd100mKph { get; set; }

		[JsonProperty("wx")]
		public string Wx { get; set; }

		[JsonProperty("wnd_spd_80m_ms")]
		public string WndSpd80mMs { get; set; }

		[JsonProperty("wnd_spd_80m_kph")]
		public string WndSpd80mKph { get; set; }

		[JsonProperty("wnd_spd_kph")]
		public string WndSpdKph { get; set; }

		[JsonProperty("wx_icon_url")]
		public string WxIconUrl { get; set; }

		[JsonProperty("wx_code")]
		public string WxCode { get; set; }

		[JsonProperty("wx_icon_url_png")]
		public string WxIconUrlPng { get; set; }
	}

	public partial struct OtherQpf01hrMm
	{
		public string String;
		public Qpf01hrMm Qpf01hrMm;
	}

	public partial class WeatherForecast
	{
		public static WeatherForecast FromJson(string json) => JsonConvert.DeserializeObject<WeatherForecast>(json, Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this WeatherForecast self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}

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

	public class Converter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(OtherQpf01hrMm);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (t == typeof(OtherQpf01hrMm))
				return new OtherQpf01hrMm(reader, serializer);
			throw new Exception("Unknown type");
		}

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

		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters = { new Converter() },
		};

        public static async Task<WeatherForecast> Download(double latitude, double longitude){
            var serviceUrl = $"https://skywisefeeds.wdtinc.com/feeds/api/mega.php?LAT={latitude}&LON={longitude}&FORMAT=json";
            using (var wc = new WebClient())
            {
                wc.Headers.Set("app_id","dc9e4567");
                wc.Headers.Set("app_key","9547e002315e9cf9d6f7362675d63f1f");
                var json = await wc.DownloadString(serviceUrl);
                return WeatherForecast.FromJson(json);
            }
        }
	}
}
