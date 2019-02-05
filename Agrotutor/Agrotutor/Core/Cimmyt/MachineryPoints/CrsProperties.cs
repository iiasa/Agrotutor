namespace Agrotutor.Core.Cimmyt.MachineryPoints
{
    using Newtonsoft.Json;

    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}