namespace CimmytApp.Core.Datatypes.HubsContact
{
    using Newtonsoft.Json;

    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}