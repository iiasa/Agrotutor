namespace CimmytApp.Core.Datatypes.InvestigationPlatforms
{
    using Newtonsoft.Json;

    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}