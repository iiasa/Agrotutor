namespace IIASA.FloodCitiSense.ApiClient
{
    using System.Net;
    using System.Net.Http;
    using Flurl.Http.Configuration;
    using ModernHttpClient;

    public class ModernHttpClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new NativeMessageHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }
    }
}