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
        public static async Task<T> GetCachedAsync<T>(this HttpClient client, string url, string key, TimeSpan timeSpan = default(TimeSpan), bool forceRefresh = false)
        {
            if (client == null) return default(T);

            var json = string.Empty;

            if (timeSpan == default(TimeSpan))
            {
                timeSpan = TimeSpan.FromDays(1);
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);

            if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                    Barrel.Current.Add(key, json, timeSpan);
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


        public static async Task<string> GetCachedStringAsync(this HttpClient client, string url,string key, TimeSpan timeSpan = default(TimeSpan), bool forceRefresh = false)
        {
            if (client == null) return default(string);

            var json = string.Empty;

            if (timeSpan == default(TimeSpan))
            {
                timeSpan = TimeSpan.FromDays(1);
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);

            if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                    Barrel.Current.Add(key, json, timeSpan);
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