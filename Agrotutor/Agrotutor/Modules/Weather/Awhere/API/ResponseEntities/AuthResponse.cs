// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;
//
//    var authResponse = AuthResponse.FromJson(jsonString);

namespace Agrotutor.Modules.Weather.Awhere.API.ResponseEntities
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class AuthResponse
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpiresIn { get; set; }
        
        public static AuthResponse FromJson(string json) => JsonConvert.DeserializeObject<AuthResponse>(json, AuthResponseConverter.Settings);
    }

    public static class AuthResponseSerializer
    {
        public static string ToJson(this AuthResponse self) => JsonConvert.SerializeObject(self, AuthResponseConverter.Settings);
    }

    internal static class AuthResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
