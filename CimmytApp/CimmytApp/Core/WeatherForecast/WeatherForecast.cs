namespace CimmytApp.WeatherForecast
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Crashes;

    using Newtonsoft.Json;

    /// <summary>
    ///     Defines the <see cref="WeatherForecast" />
    /// </summary>
    public partial class WeatherForecast
    {
        /// <summary>
        ///     Gets or sets the Location
        /// </summary>
        [JsonProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        ///     Gets or sets the Units
        /// </summary>
        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="Location" />
    /// </summary>
    public class Location
    {
        /// <summary>
        ///     Gets or sets the Attributes
        /// </summary>
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }

        /// <summary>
        ///     Gets or sets the DailySummaries
        /// </summary>
        [JsonProperty("daily_summaries")]
        public DailySummaries DailySummaries { get; set; }

        /// <summary>
        ///     Gets or sets the HourlySummaries
        /// </summary>
        [JsonProperty("hourly_summaries")]
        public HourlySummaries HourlySummaries { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="SfcOb" />
    /// </summary>
    public class SfcOb
    {
        /// <summary>
        ///     Gets or sets the ApparentTempC
        /// </summary>
        [JsonProperty("apparent_temp_C")]
        public string ApparentTempC { get; set; }

        /// <summary>
        ///     Gets or sets the DayNight
        /// </summary>
        [JsonProperty("day_night")]
        public string DayNight { get; set; }

        /// <summary>
        ///     Gets or sets the DayOfWeekLocal
        /// </summary>
        [JsonProperty("day_of_week_local")]
        public string DayOfWeekLocal { get; set; }

        /// <summary>
        ///     Gets or sets the MoonPhase
        /// </summary>
        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }

        /// <summary>
        ///     Gets or sets the ObTime
        /// </summary>
        [JsonProperty("ob_time")]
        public string ObTime { get; set; }

        /// <summary>
        ///     Gets or sets the Stn
        /// </summary>
        [JsonProperty("stn")]
        public string Stn { get; set; }

        /// <summary>
        ///     Gets or sets the StnLat
        /// </summary>
        [JsonProperty("stn_lat")]
        public string StnLat { get; set; }

        /// <summary>
        ///     Gets or sets the StnLon
        /// </summary>
        [JsonProperty("stn_lon")]
        public string StnLon { get; set; }

        /// <summary>
        ///     Gets or sets the TempC
        /// </summary>
        [JsonProperty("temp_C")]
        public string TempC { get; set; }

        /// <summary>
        ///     Gets or sets the Wx
        /// </summary>
        [JsonProperty("wx")]
        public string Wx { get; set; }

        /// <summary>
        ///     Gets or sets the WxCode
        /// </summary>
        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        /// <summary>
        ///     Gets or sets the WxIconUrl
        /// </summary>
        [JsonProperty("wx_icon_url")]
        public string WxIconUrl { get; set; }

        /// <summary>
        ///     Gets or sets the WxIconUrlPng
        /// </summary>
        [JsonProperty("wx_icon_url_png")]
        public string WxIconUrlPng { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="HourlySummaries" />
    /// </summary>
    public class HourlySummaries
    {
        /// <summary>
        ///     Gets or sets the HourlySummary
        /// </summary>
        [JsonProperty("hourly_summary")]
        public List<HourlySummary> HourlySummary { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="HourlySummary" />
    /// </summary>
    public class HourlySummary
    {
        /// <summary>
        ///     Gets the WxIcon
        /// </summary>
        public string WxIcon => Util.GetIconSrcForWx(WxCode);
        public string TinyWxIcon => Util.GetTinyIconSrcForWx(WxCode);

        /// <summary>
        ///     Gets the WxText
        /// </summary>
        public string WxText => Util.GetTextForWx(WxCode);

        /// <summary>
        ///     Gets or sets the AppTempC
        /// </summary>
        [JsonProperty("app_temp_C")]
        public string AppTempC { get; set; }

        /// <summary>
        ///     Gets or sets the DayOfWeekUtc
        /// </summary>
        [JsonProperty("day_of_week_utc")]
        public string DayOfWeekUtc { get; set; }

        /// <summary>
        ///     Gets or sets the RhPct
        /// </summary>
        [JsonProperty("rh_pct")]
        public string RhPct { get; set; }

        /// <summary>
        ///     Gets or sets the SkyCovPct
        /// </summary>
        [JsonProperty("sky_cov_pct")]
        public string SkyCovPct { get; set; }

        /// <summary>
        ///     Gets or sets the TempC
        /// </summary>
        [JsonProperty("temp_C")]
        public string TempC { get; set; }

        /// <summary>
        ///     Gets or sets the TimeUtc
        /// </summary>
        [JsonProperty("time_utc")]
        public string TimeUtc { get; set; }

        /// <summary>
        ///     Gets or sets the VisibilityM
        /// </summary>
        [JsonProperty("visibility_m")]
        public string VisibilityM { get; set; }

        /// <summary>
        ///     Gets or sets the WndDir
        /// </summary>
        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        /// <summary>
        ///     Gets or sets the WndDirDegs
        /// </summary>
        [JsonProperty("wnd_dir_degs")]
        public string WndDirDegs { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpd100mKph
        /// </summary>
        [JsonProperty("wnd_spd_100m_kph")]
        public string WndSpd100mKph { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpd100mMs
        /// </summary>
        [JsonProperty("wnd_spd_100m_ms")]
        public string WndSpd100mMs { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpd80mKph
        /// </summary>
        [JsonProperty("wnd_spd_80m_kph")]
        public string WndSpd80mKph { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpd80mMs
        /// </summary>
        [JsonProperty("wnd_spd_80m_ms")]
        public string WndSpd80mMs { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpdKph
        /// </summary>
        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        /// <summary>
        ///     Gets or sets the Wx
        /// </summary>
        [JsonProperty("wx")]
        public string Wx { get; set; }

        /// <summary>
        ///     Gets or sets the WxCode
        /// </summary>
        [JsonProperty("wx_code")]
        public string WxCode { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="DailySummaries" />
    /// </summary>
    public class DailySummaries
    {
        /// <summary>
        ///     Gets or sets the DailySummary
        /// </summary>
        [JsonProperty("daily_summary")]
        public List<DailySummary> DailySummary { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="DailySummary" />
    /// </summary>
    public class DailySummary
    {
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

        /// <summary>
        ///     Gets the WxIcon
        /// </summary>
        public string WxIcon => Util.GetIconSrcForWx(WxCode);

        /// <summary>
        ///     Gets the WxText
        /// </summary>
        public string WxText => Util.GetTextForWx(WxCode);

        /// <summary>
        ///     Gets or sets the DayOfWeek
        /// </summary>
        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }

        /// <summary>
        ///     Gets or sets the Gdd
        /// </summary>
        [JsonProperty("gdd")]
        public string Gdd { get; set; }

        /// <summary>
        ///     Gets or sets the MaxTempC
        /// </summary>
        [JsonProperty("max_temp_C")]
        public string MaxTempC { get; set; }

        /// <summary>
        ///     Gets or sets the MinTempC
        /// </summary>
        [JsonProperty("min_temp_C")]
        public string MinTempC { get; set; }

        /// <summary>
        ///     Gets or sets the SummaryDate
        /// </summary>
        [JsonProperty("summary_date")]
        public string SummaryDate { get; set; }

        /// <summary>
        ///     Gets or sets the WndDir
        /// </summary>
        [JsonProperty("wnd_dir")]
        public string WndDir { get; set; }

        /// <summary>
        ///     Gets or sets the WndSpdKph
        /// </summary>
        [JsonProperty("wnd_spd_kph")]
        public string WndSpdKph { get; set; }

        /// <summary>
        ///     Gets or sets the Wx
        /// </summary>
        [JsonProperty("wx")]
        public string Wx { get; set; }

        /// <summary>
        ///     Gets or sets the WxCode
        /// </summary>
        [JsonProperty("wx_code")]
        public string WxCode { get; set; }

        /// <summary>
        ///     Gets or sets the WxIconText
        /// </summary>
        [JsonProperty("wx_icon_text")]
        public string WxIconText { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="Attributes" />
    /// </summary>
    public class Attributes
    {
        /// <summary>
        ///     Gets or sets the City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the Country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    /// <summary>
    ///     Defines the <see cref="Qpf01hrMm" />
    /// </summary>
    public partial struct Qpf01hrMm
    {
        /// <summary>
        ///     Defines the String
        /// </summary>
        public string String;

        /// <summary>
        ///     Defines the PressMb
        /// </summary>
        public List<object> PressMb;
    }

    /// <summary>
    ///     Defines the <see cref="WeatherForecast" />
    /// </summary>
    public partial class WeatherForecast
    {
        /// <summary>
        ///     The Download
        /// </summary>
        /// <param name="latitude">The <see cref="double" /></param>
        /// <param name="longitude">The <see cref="double" /></param>
        /// <returns>The <see cref="Task{WeatherForecast}" /></returns>
        public static async Task<WeatherForecast> Download(double latitude, double longitude)
        {
            var serviceUrl =
                $"https://skywisefeeds.wdtinc.com/feeds/api/mega.php?LAT={latitude}&LON={longitude}&FORMAT=json";
            using (var wc = new HttpClient())
            {
                wc.DefaultRequestHeaders.Add("app_id", "949a7457");
                wc.DefaultRequestHeaders.Add("app_key", "5851174f1a3e6e1af42f5895098f69f8");
                try
                {
                    var json = await wc.GetStringAsync(serviceUrl);
                    return FromJson(json);
                }
                catch (Exception e)
                {
                    var ex = new AppCenterException("Weather error", e);
                }
            }

            return null;
        }

        /// <summary>
        ///     The FromJson
        /// </summary>
        /// <param name="json">The <see cref="string" /></param>
        /// <returns>The <see cref="WeatherForecast" /></returns>
        public static WeatherForecast FromJson(string json) =>
            JsonConvert.DeserializeObject<WeatherForecast>(json, Converter.Settings);
    }

    /// <summary>
    ///     Defines the <see cref="Qpf01hrMm" />
    /// </summary>
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
                    PressMb = serializer.Deserialize<List<object>>(reader);
                    break;

                default: throw new Exception("Cannot convert Qpf01hrMm");
            }
        }

        /// <summary>
        ///     The WriteJson
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /></param>
        /// <param name="serializer">The <see cref="JsonSerializer" /></param>
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

    /// <summary>
    ///     Defines the <see cref="Serialize" />
    /// </summary>
    public static class Serialize
    {
        /// <summary>
        ///     The ToJson
        /// </summary>
        /// <param name="self">The <see cref="WeatherForecast" /></param>
        /// <returns>The <see cref="string" /></returns>
        public static string ToJson(this WeatherForecast self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    /// <summary>
    ///     Defines the <see cref="Converter" />
    /// </summary>
    public class Converter : JsonConverter
    {
        /// <summary>
        ///     Defines the Settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new Converter()
            }
        };

        /// <summary>
        ///     The CanConvert
        /// </summary>
        /// <param name="t">The <see cref="Type" /></param>
        /// <returns>The <see cref="bool" /></returns>
        public override bool CanConvert(Type t) => t == typeof(Qpf01hrMm);

        /// <summary>
        ///     The ReadJson
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader" /></param>
        /// <param name="t">The <see cref="Type" /></param>
        /// <param name="existingValue">The <see cref="object" /></param>
        /// <param name="serializer">The <see cref="JsonSerializer" /></param>
        /// <returns>The <see cref="object" /></returns>
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(Qpf01hrMm))
            {
                return new Qpf01hrMm(reader, serializer);
            }

            throw new Exception("Unknown type");
        }

        /// <summary>
        ///     The WriteJson
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /></param>
        /// <param name="value">The <see cref="object" /></param>
        /// <param name="serializer">The <see cref="JsonSerializer" /></param>
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

    internal static class Util
    {

        public static string GetIconSrcForWx(string wxCode)
        {
            switch (wxCode)
            {
                case "3":
                    return "blowing_dust.png";

                case "5":
                    return "haze.png";

                case "7":
                    return "blowing_dust.png";

                case "10":
                    return "patchy_fog.png";

                case "18":
                    return "cloudy.png";

                case "31":
                    return "cloudy.png";

                case "34":
                    return "cloudy.png";

                case "36":
                    return "blowing_snow.png";

                case "38":
                    return "blowing_snow.png";

                case "41":
                    return "patchy_fog.png";

                case "45":
                    return "patchy_fog.png";

                case "49":
                    return "patchy_fog.png";

                case "51":
                    return "drizzle.png";

                case "53":
                    return "drizzle.png";

                case "55":
                    return "drizzle.png";

                case "56":
                    return "light freezing drizzle";

                case "57":
                    return "freezing_drizzle.png";

                case "61":
                    return "light_rain.png";

                case "63":
                    return "rain.png";

                case "65":
                    return "rain_showers.png";

                case "66":
                    return "freezing_rain.png";

                case "67":
                    return "freezing_rain.png";

                case "68":
                    return "sleet.png";

                case "69":
                    return "sleet.png";

                case "71":
                    return "snow.png";

                case "73":
                    return "snow.png";

                case "75":
                    return "heavy_snow.png";

                case "79":
                    return "sleet.png";

                case "80":
                    return "light_rain.png";

                case "81":
                    return "rain_showers.png";

                case "83":
                    return "sleet.png";

                case "84":
                    return "sleet.png";

                case "85":
                    return "light_snow.png";

                case "86":
                    return "snow.png";

                case "89":
                    return "light_hail.png";

                case "90":
                    return "hail.png";

                case "95":
                    return "thunderstorms.png";

                case "97":
                    return "thunderstorms.png";

                case "100":
                    return "clear.png";

                case "101":
                    return "mostly_clear.png";

                case "102":
                    return "partly_cloudy.png";

                case "103":
                    return "mostly_cloudy.png";

                case "104":
                    return "partly_cloudy.png";

                default:
                    return "question.png";
            }
        }

        public static string GetTinyIconSrcForWx(string wxCode)
        {
            return "tiny_" + GetIconSrcForWx(wxCode);
        }

        /// <summary>
        ///     The GetTextForWx
        /// </summary>
        /// <param name="wxCode">The <see cref="string" /></param>
        /// <returns>The <see cref="string" /></returns>
        public static string GetTextForWx(string wxCode)
        {
            switch (wxCode)
            {
                case "3":
                    return "fumar";

                case "5":
                    return "calina";

                case "7":
                    return "soplando polvo";

                case "10":
                    return "niebla ligera";

                case "18":
                    return "tempestad";

                case "31":
                    return "tormenta de polvo";

                case "34":
                    return "tormenta de polvo severo";

                case "36":
                    return "nieve que deriva";

                case "38":
                    return "nieve que sopla";

                case "41":
                    return "niebla irregular";

                case "45":
                    return "niebla";

                case "49":
                    return "niebla helada";

                case "51":
                    return "llovizna ligera";

                case "53":
                    return "llovizna";

                case "55":
                    return "fuerte llovizna";

                case "56":
                    return "llovizna helada ligera";

                case "57":
                    return "llovizna helada";

                case "61":
                    return "lluvia ligera";

                case "63":
                    return "lluvia";

                case "65":
                    return "lluvia pesada";

                case "66":
                    return "lluvia helada ligera";

                case "67":
                    return "lluvia helada";

                case "68":
                    return "lluvia ligera y nieve";

                case "69":
                    return "lluvia y nieve";

                case "71":
                    return "nieve ligera";

                case "73":
                    return "nieve";

                case "75":
                    return "fuertes nevadas";

                case "79":
                    return "aguanieve";

                case "80":
                    return "ducha de lluvia ligera";

                case "81":
                    return "ducha de lluvia";

                case "83":
                    return "lluvia ligera y nieve";

                case "84":
                    return "lluvia y nieve";

                case "85":
                    return "ducha de nieve ligera";

                case "86":
                    return "ducha de nieve";

                case "89":
                    return "granizo ligero";

                case "90":
                    return "granizo";

                case "95":
                    return "tormenta";

                case "97":
                    return "fuerte tormenta";

                case "100":
                    return "despejado";

                case "101":
                    return "mayormente despejado";

                case "102":
                    return "parcialmente despejado";

                case "103":
                    return "mayormente despejado";

                case "104":
                    return "nublado";

                default:
                    return "desconocido";
            }
        }
    }
}