namespace CimmytApp.Core.Datatypes.HubsContact
{
    using Newtonsoft.Json;

    public class Crs
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public CrsProperties Properties { get; set; }
    }
}