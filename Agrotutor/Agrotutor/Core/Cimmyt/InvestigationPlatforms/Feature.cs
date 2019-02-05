namespace Agrotutor.Core.Cimmyt.InvestigationPlatforms
{
    using Newtonsoft.Json;

    public class Feature
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public FeatureProperties Properties { get; set; }

        [JsonProperty("type")]
        public FeatureType Type { get; set; }
    }
}