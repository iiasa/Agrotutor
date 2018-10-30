using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Helper.RestfulClient
{
    public class RestfulClient<T> where T : class
    {
        private readonly HttpClient _client;

        public RestfulClient()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
            //is used to specify the maximum number of bytes to buffer when reading the content in the HTTP response message.
            //The default size of this property is the maximum size of an integer
        }

        public async Task<T> RefreshDataAsync(string restUrl)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(restUrl));

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(content);
                return res;
            }
            return null;
        }

        public async Task<bool> SaveDataAsync(string restUrl, T item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(restUrl));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await _client.PostAsync(uri, content);
            }
            else
            {
                response = await _client.PutAsync(uri, content);
            }
            if (response != null && response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDataAsync(string restUrl, string id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(restUrl, id));

            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}