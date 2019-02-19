using System;
using System.Net.Http;
using System.Threading.Tasks;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Agrotutor.Core.Rest
{
    public static class HttpHelper
    {
        public static async Task<T> GetCachedAsync<T>(this HttpClient client, string url, int days = 7, bool forceRefresh = false)
        {
            if (client == null) return default(T);

            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(url);

            if (!forceRefresh && !Barrel.Current.IsExpired(url))
                json = Barrel.Current.Get<string>(url);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                    Barrel.Current.Add(url, json, TimeSpan.FromDays(days));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get information from server {ex}");
                //probably re-throw here :)
            }

            return default(T);
        }


        public static async Task<string> GetCachedStringAsync(this HttpClient client, string url, TimeSpan timeSpan = default(TimeSpan), bool forceRefresh = false)
        {
            if (client == null) return default(string);

            var json = string.Empty;

            if (timeSpan == default(TimeSpan))
            {
                timeSpan = TimeSpan.FromDays(7);
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(url);

            if (!forceRefresh && !Barrel.Current.IsExpired(url))
                json = Barrel.Current.Get<string>(url);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                    Barrel.Current.Add(url, json, timeSpan);
                }
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get information from server {ex}");
                //probably re-throw here :)
            }

            return default(string);
        }
    }
}