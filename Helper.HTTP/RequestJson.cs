namespace Helper.HTTP
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class RequestJson
    {
        public static async Task<T> Get<T>(string url) where T : class
        {
            T res;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.OK) return null;
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
            T res = null;
            using (var httpClient = new HttpClient())
            {
                var parameters = new Dictionary<string, string>() { { "parameter", JsonConvert.SerializeObject(param) } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                var response = await httpClient.PostAsync(url, encodedContent).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (string.IsNullOrEmpty(responseContent)) return null;
                    res = JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            return res;
        }

        public static async Task<T> Post<T>(string url) where T : class
        {
            return await Post<T>(url, null);
        }
    }
}