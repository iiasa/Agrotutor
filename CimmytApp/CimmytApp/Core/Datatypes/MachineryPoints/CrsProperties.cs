namespace CimmytApp.Core.Datatypes.MachineryPoints
{
    using Newtonsoft.Json;

    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}