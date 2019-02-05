namespace Agrotutor.Core.Cimmyt.InvestigationPlatforms
{
    using Newtonsoft.Json;

    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}