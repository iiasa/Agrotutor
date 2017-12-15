namespace Helper.Realm.DTO
{
    using System;
    using Realms;

    public class GeoPositionDTO : RealmObject
    {
        public double? Accuracy { get; set; }

        public double? Altitude { get; set; }

        public double? AltitudeAccuracy { get; set; }

        public double? Heading { get; set; }

        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Speed { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string ParcelId { get; set; }

        public bool IsPartOfdelineation { get; set; }
    }
}