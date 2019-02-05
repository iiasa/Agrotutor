namespace Agrotutor.Core.Cimmyt.MachineryPoints
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Xamarin.Essentials;

    public class Geometry
    {
        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonProperty("type")]
        public GeometryType Type { get; set; }

        public Location ToLocation()
        {
            return new Location(Coordinates[1], Coordinates[0]);
        }
    }
}