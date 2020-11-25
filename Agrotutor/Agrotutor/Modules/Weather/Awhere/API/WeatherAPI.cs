using System;
using System.Globalization;
using System.Threading.Tasks;
using Agrotutor.Modules.Weather.Awhere.API.ResponseEntities;
using Flurl.Http;
using Microsoft.AppCenter.Crashes;
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
            var URL = $"{ApiURL}{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}/forecasts?useLocalTime=true";
            ForecastResponse forecast = null;
            try
            {
                forecast = await URL
                .WithOAuthBearerToken(token)
                .SetQueryParams(new
                {
                    conditionsType = "standard",
                    blockSize = "24"
                })
                .GetJsonAsync<ForecastResponse>();
            } catch (Exception e) 
            {
                Crashes.TrackError(e);
            }
            return forecast;
        }

        public static async Task<ForecastResponse> GetCurrentAsync(double latitude, double longitude, UserCredentials credentials)
        {
            var token = await GetToken(credentials);
            var URL = $"{ApiURL}{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}/forecasts?useLocalTime=true";
            ForecastResponse forecast = null;
            try
            {
                forecast = await URL
                .WithOAuthBearerToken(token)
                .SetQueryParams(new
                {
                    conditionsType = "standard",
                    blockSize = "1",
                    limit = "1"
                })
                .GetJsonAsync<ForecastResponse>();
            }
            catch (Exception e) 
            {
                Crashes.TrackError(e);
            }
            return forecast;
        }

        public static async Task<ObservationsResponse> GetObservationsAsync(double latitude, double longitude, UserCredentials credentials, DateTime? startDate, DateTime? endDate)
        {
            var token = await GetToken(credentials);
            if (startDate > endDate) startDate = endDate;
            var start = startDate?.ToString("yyyy-MM-dd");
            var end = endDate?.ToString("yyyy-MM-dd");
            var dates = (start != null && end != null) ? $"{start},{end}" : "";
            var url = $"{ApiURL}{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}/observations/{dates}";
            ObservationsResponse observationsResponse = null;
            try
            {
                observationsResponse = await url.WithOAuthBearerToken(token).GetJsonAsync<ObservationsResponse>();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            if (observationsResponse == null)
            {
                return null;
            }
            var observations = observationsResponse;

            while (observationsResponse.Links.Next != null) {
                var offsetParams = observationsResponse.Links.Next.Href.Split('?')[1];
                var pageUrl = $"{url}?{offsetParams}";
                observationsResponse = await pageUrl.WithOAuthBearerToken(token).GetJsonAsync<ObservationsResponse>();
                observations.Observations.AddRange(observationsResponse.Observations);
            }

            return observations;
        }

        public static async Task<object> GetNormsAsync(double latitude, double longitude, int month, int day, UserCredentials credentials)
        {
            var token = await GetToken(credentials);
            var url = $"{ApiURL}{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}/norms/{month}-{day}";

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
            try
            {
                var responseMessage = await AuthURL
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .WithBasicAuth(credentials.Username, credentials.Password)
                    .PostUrlEncodedAsync(new {grant_type = "client_credentials"});
                var dataString = await responseMessage.Content.ReadAsStringAsync();
                var authResponse = JsonConvert.DeserializeObject<AuthResponse>(dataString);
                Token = authResponse.AccessToken;
                TokenValidityEnd = DateTime.Now + TimeSpan.FromHours(1);
                Preferences.Set(TOKEN_PREFERENCE, Token);
                Preferences.Set(TOKEN_VALIDITY_PREFERENCE, JsonConvert.SerializeObject(TokenValidityEnd));
                return Token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}