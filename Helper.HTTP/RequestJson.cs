namespace Helper.HTTP
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class RequestJson
    {
        public static async Task<T> Get<T>(string url) where T : class
        {
            T res;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    response = await httpClient.GetAsync(url).ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    System.Diagnostics.Debug.WriteLine("HTTP E: " + e.Message);
                    return null;
                }

                if (response == null || response.StatusCode != HttpStatusCode.OK) return null;
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                res = JsonConvert.DeserializeObject<T>(responseContent);
            }

            return res;
        }

        public static async Task<T> Get<T>(string url, string param) where T : class
        {
            var fullUrl = (url.EndsWith("/") ? url + param : url + "/" + param);
            return await Get<T>(fullUrl);
        }

        public static async Task<T> Post<T>(string url, Dictionary<string, string> param) where T : class
        {
            T res;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = null;
                var parameters = new Dictionary<string, string>() { { "parameter", JsonConvert.SerializeObject(param) } };
                var encodedContent = new FormUrlEncodedContent(parameters); try
                {
                    response = await httpClient.PostAsync(url, encodedContent).ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    System.Diagnostics.Debug.WriteLine("HTTP E: " + e.Message);
                    return null;
                }

                if (response.StatusCode != HttpStatusCode.OK) return null;
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (string.IsNullOrEmpty(responseContent)) return null;
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