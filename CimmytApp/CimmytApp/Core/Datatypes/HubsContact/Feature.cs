﻿namespace CimmytApp.Core.Datatypes.HubsContact
{
    using Newtonsoft.Json;

    public class Feature
    {
        [JsonProperty("type")]
        public FeatureType Type { get; set; }

        [JsonProperty("properties")]
        public FeatureProperties Properties { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
}