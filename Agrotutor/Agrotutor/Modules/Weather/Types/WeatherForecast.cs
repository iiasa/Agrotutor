namespace Agrotutor.Modules.Weather.Types
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AppCenter;

    using Newtonsoft.Json;
    
    public partial class WeatherForecast
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("location")]
        public virtual Location Location { get; set; }
        
        [JsonProperty("units")]
        public string Units { get; set; }

        public string Date { get; set; }

        public static async Task<WeatherForecast> Download(double latitude, double longitude)
        {
            var serviceUrl =
                $"https://skywisefeeds.wdtinc.com/feeds/api/mega.php?LAT={latitude}&LON={longitude}&FORMAT=json"; // TODO add LANG=es/en
            using (var wc = new HttpClient())
            {
                wc.DefaultRequestHeaders.Add("app_id", "949a7457");
                wc.DefaultRequestHeaders.Add("app_key", "5851174f1a3e6e1af42f5895098f69f8");
                try
                {
                    var json = await wc.GetStringAsync(serviceUrl);
                    var forecast = FromJson(json);
                    forecast.Date = DateTime.Now.ToShortDateString();
                    return forecast;
                }
                catch (Exception e)
                {
                    var ex = new AppCenterException("Weather error", e);
                }
            }

            return null;
        }

        public static WeatherForecast FromJson(string json) =>
            JsonConvert.DeserializeObject<WeatherForecast>(json, Converter.Settings);
    }
    
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("@attributes")]
        public virtual Attributes Attributes { get; set; }
        
        [JsonProperty("daily_summaries")]
        public virtual DailySummaries DailySummaries { get; set; }
        
        [JsonProperty("hourly_summaries")]
        public virtual HourlySummaries HourlySummaries { get; set; }
    }
    
    public class SfcOb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("apparent_temp_C")]
        public string ApparentTempC { get; set; }
        
        [JsonProperty("day_night")]
        public string DayNight { get; set; }
        
        [JsonProperty("day_of_week_local")]
        public string DayOfWeekLocal { get; set; }
        
        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }
        
        [JsonProperty("ob_time")]
        public string ObTime { get; set; }
        
        [JsonProperty("stn")]
        public string Stn { get; set; }
        
        [JsonProperty("stn_lat")]
        public string StnLat { get; set; }
        
        [JsonProperty("stn_lon")]
        public string StnLon { get; set; }
        
        [JsonProperty("temp_C")]
        public string TempC { get; set; }
        
        [JsonProperty("wx")]
        public string Wx { get; set; }

        [JsonProperty("wx_code")]
        public string WxCode { get; set; }
        
        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }
        
        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }
    
    public class HourlySummaries
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("hourly_summary")]
        public virtual List<HourlySummary> HourlySummary { get; set; }
    }
    
    public class HourlySummary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string WxIcon => Util.GetIconSrcForWx(WxCode);
        public string TinyWxIcon => Util.GetTinyIconSrcForWx(WxCode);
        
        public string WxText => Util.GetTextForWx(WxCode);
        
        [JsonProperty("app_temp_C")]
        public string AppTempC { get; set; }

        [JsonProperty("day_of_week_utc")]
        public string DayOfWeekUtc { get; set; }

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
    }
    
    public class DailySummaries
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("daily_summary")]
        public virtual List<DailySummary> DailySummary { get; set; }
    }

    public class DailySummary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string LocalizedDayOfWeek
        {
            get
            {
                switch (DayOfWeek)
                {
                    case "Monday":
                        return "Lunes";

                    case "Tuesday":
                        return "Martes";

                    case "Wednesday":
                        return "Miércoles";

                    case "Thursday":
                        return "Jueves";

                    case "Friday":
                        return "Viernes";

                    case "Saturday":
                        return "Sábado";

                    case "Sunday":
                        return "Domingo";

                    default:
                        return "";
                }
            }
        }

        public string TemperatureString => $"{MaxTempC}°C ({MinTempC}°C)";
        
        public string WxIcon => Util.GetIconSrcForWx(WxCode);
        
        public string WxText => Util.GetTextForWx(WxCode);
        
        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }
        
        [JsonProperty("gdd")]
        public string Gdd { get; set; }
        
        [JsonProperty("max_temp_C")]
        public string MaxTempC { get; set; }
        
        [JsonProperty("min_temp_C")]
        public string MinTempC { get; set; }
        
        [JsonProperty("summary_date")]
        public string SummaryDate { get; set; }
        
        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }
        
        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        [JsonProperty("pop")]
        public string precipitationProbability { get; set; }

        [JsonProperty("wx")]
        public string Wx { get; set; }

        [JsonProperty("wx_code")]
        public string WxCode { get; set; }
        
        [JsonProperty("wx_icon_text")]
        public string WxIconText { get; set; }
    }
    
    public class Attributes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public struct Qpf01hrMm
    {
        public string String;

        public List<object> PressMb;

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
                    PressMb = serializer.Deserialize<List<object>>(reader);
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
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new Converter()
            }
        };

        public override bool CanConvert(Type t) => t == typeof(Qpf01hrMm);
        
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(Qpf01hrMm))
            {
                return new Qpf01hrMm(reader, serializer);
            }

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
    }
}

