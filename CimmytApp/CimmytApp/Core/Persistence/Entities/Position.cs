namespace CimmytApp.Core.Persistence.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Xamarin.Essentials;

    public class Position : Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public static Position From(Xamarin.Forms.GoogleMaps.Position positionFrom)
        {
            Position position = new Position
            {
                Latitude = positionFrom.Latitude,
                Longitude = positionFrom.Longitude
            };
            return position;
        }
    }
}