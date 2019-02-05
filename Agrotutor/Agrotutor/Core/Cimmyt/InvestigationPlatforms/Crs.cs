namespace Agrotutor.Core.Cimmyt.InvestigationPlatforms
{
    using Newtonsoft.Json;

    public class Crs
    {
        [JsonProperty("properties")]
        public CrsProperties Properties { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}