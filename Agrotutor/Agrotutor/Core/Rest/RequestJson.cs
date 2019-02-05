namespace Agrotutor.Core.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class RequestJson
    {
        public static async Task<T> Get<T>(string url, NetworkCredential credentials = null) where T : class
        {
            T res;
            HttpClientHandler handler = new HttpClientHandler { Credentials = credentials };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpResponseMessage response = null;
                try
                {
                    response = await httpClient.GetAsync(url);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("HTTP E: " + e.Message);
                    return null;
                }

                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    res = JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("HTTP E: " + e.Message);
                    return null;
                }
            }

            return res;
        }

        public static async Task<T> Get<T>(string url, string param) where T : class
        {
            var fullUrl = url.EndsWith("/") ? url + param : url + "/" + param;
            return await Get<T>(fullUrl);
        }

        public static async Task<T> Post<T>(string url, Dictionary<string, string> param) where T : class
        {
            T res;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = null;
                var encodedContent = new FormUrlEncodedContent(param);
                try
                {
                    response = await httpClient.PostAsync(url, encodedContent).ConfigureAwait(true);
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("HTTP E: " + e.Message);
                    return null;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("E: " + e.Message);
                    return null;
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                if (string.IsNullOrEmpty(responseContent))
                {
                    return null;
                }

                res = JsonConvert.DeserializeObject<T>(responseContent);
            }

            return res;
        }

        public static async Task<T> Post<T>(string url) where T : class
        {
            return await Post<T>(url, null);
        }
    }
}
