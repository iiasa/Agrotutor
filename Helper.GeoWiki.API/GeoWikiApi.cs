namespace Helper.GeoWiki.API
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class GeoWikiApi
    {
        private static string _url = "https://www.geo-wiki.org/application/api/";

        public static async Task<T> Get<T>(string controller, string action, string param) where T : class
        {
            T res = null;
            using (var httpClient = new HttpClient())
            {
                var requestUrl = _url + controller + "/" + action + "/" + param;
                var response = await httpClient.GetAsync(requestUrl).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    res = JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            return res;
        }

        public static async Task<T> Post<T>(string controller, string action, object param) where T : class
        {
            T res = null;
            using (var httpClient = new HttpClient())
            {
                var parameters = new Dictionary<string, string>() { { "parameter", JsonConvert.SerializeObject(param) } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                var requestUrl = _url + controller + "/" + action;
                var response = await httpClient.PostAsync(requestUrl, encodedContent).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    res = JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            return res;
        }
    }
}