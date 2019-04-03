using System;
using System.Threading.Tasks;
using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;
using Flurl.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Agrotutor.Modules.Weather.Awhere.API
{
    public static class WeatherAPI
    {
        private const string TOKEN_PREFERENCE = "AwhereAuthToken";
        private const string TOKEN_VALIDITY_PREFERENCE = "AwhereAuthTokenValidityEnd";

        private const string AuthURL = "https://api.awhere.com/oauth/token";
        private const string ApiURL = "https://api.awhere.com/v2/weather/locations/";
        
        
        private static string Token;
        private static DateTime? TokenValidityEnd;

        public static async Task<ForecastResponse> GetForecastAsync(double latitude, double longitude, UserCredentials credentials)
        {
            var token = await GetToken(credentials);
            var URL = $"{ApiURL}{latitude},{longitude}/forecasts";

            var forecast = await URL.WithOAuthBearerToken(token).GetJsonAsync<ForecastResponse>();

            return forecast;
        }

        public static async Task<ObservationsResponse> GetObservationsAsync(double latitude, double longitude, UserCredentials credentials, DateTime? startDate, DateTime? endDate)
        {
            var token = await GetToken(credentials);
            var start = startDate?.ToString("yyyy-MM-dd");
            var end = endDate?.ToString("yyyy-MM-dd");
            var dates = (start != null && end != null) ? $"{start},{end}" : "";
            var url = $"{ApiURL}{latitude},{longitude}/observations/{dates}";

            var observations = await url.WithOAuthBearerToken(token).GetJsonAsync<ObservationsResponse>();

            return observations;
        }

        public static async Task<object> GetNormsAsync(double latitude, double longitude, int month, int day, UserCredentials credentials)
        {
            var token = await GetToken(credentials);
            var url = $"{ApiURL}{latitude},{longitude}/norms/{month}-{day}";

            var norms = await url.WithOAuthBearerToken(token).GetJsonAsync<NormsResponse>();

            return norms;
            
        }
        
        private static async Task<string> GetToken(UserCredentials credentials)
        {
            if (Token == null)
            {
                Token = Preferences.Get(TOKEN_PREFERENCE, null);
                string tokenValidityJSON = Preferences.Get(TOKEN_VALIDITY_PREFERENCE, null);
                TokenValidityEnd = (tokenValidityJSON!=null) ? JsonConvert.DeserializeObject<DateTime?>(tokenValidityJSON) : null;
            }
            if (Token == null || TokenValidityEnd == null || TokenValidityEnd < DateTime.Now)
            {
                Token = await RefreshToken(credentials);
            }

            return Token;
        }
        private static async Task<string> RefreshToken(UserCredentials credentials)
        {
            AuthResponse authResponse = null;

            try
            {
                var responseMessage = await AuthURL
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .WithBasicAuth(credentials.Username, credentials.Password)
                    .PostUrlEncodedAsync(new {grant_type = "client_credentials"});
                authResponse = JsonConvert.DeserializeObject<AuthResponse>(await responseMessage.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (authResponse == null) return "";

            Token = authResponse.AccessToken;
            TokenValidityEnd = DateTime.Now + TimeSpan.FromHours(1);
            
            Preferences.Set(TOKEN_PREFERENCE, Token);
            Preferences.Set(TOKEN_VALIDITY_PREFERENCE, JsonConvert.SerializeObject(TokenValidityEnd));

            return Token;
        }
    }
}