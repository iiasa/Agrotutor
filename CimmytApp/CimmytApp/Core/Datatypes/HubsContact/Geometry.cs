namespace CimmytApp.Core.Datatypes.HubsContact
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Xamarin.Essentials;

    public class Geometry
    {
        [JsonProperty("type")]
        public GeometryType Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        public Location ToLocation()
        {
            return new Location(Coordinates[1], Coordinates[0]);
        }
    }
}