namespace Helper.Realm.DTO
{
    using System;
    using Realms;

    public class GeoPositionDTO : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public double? Accuracy { get; set; }
        public double? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Speed { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
