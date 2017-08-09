namespace Helper.GeoWiki.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    using HTTP;

    public static class GeoWikiApi
    {
        private const string Url = "https://www.geo-wiki.org/application/api/";

        public static async Task<T> Get<T>(string controller, string action, string param) where T : class
        {
            var requestUrl = Url + controller + "/" + action + "/";
            return await RequestJson.Get<T>(requestUrl, param);
        }

        public static async Task<T> Post<T>(string controller, string action, object param) where T : class
        {
            var requestUrl = Url + controller + "/" + action;
            var parameters = new Dictionary<string, string>() { { "parameter", JsonConvert.SerializeObject(param) } };
            return await RequestJson.Post<T>(requestUrl, parameters);
        }
    }
}