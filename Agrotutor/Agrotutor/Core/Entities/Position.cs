namespace Agrotutor.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Xamarin.Essentials;

    public class Position : Location
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ID { get; set; }
        public static Position From(Xamarin.Forms.GoogleMaps.Position positionFrom)
        {
            Position position = new Position
            {
                Latitude = positionFrom.Latitude,
                Longitude = positionFrom.Longitude
            };
            return position;
        }

        public Xamarin.Forms.GoogleMaps.Position ForMap()
        {
            return new Xamarin.Forms.GoogleMaps.Position(this.Latitude, this.Longitude);
        }

        public static Position FromLocation(Location location)
        {
            if (location == null) return null;
            return new Position
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Accuracy = location.Accuracy,
                Altitude = location.Altitude,
                Course = location.Course,
                Speed = location.Speed,
                Timestamp = location.Timestamp
            };
        }
    }
}
