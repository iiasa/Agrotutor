namespace Helper.Map
{
    using System;
    using Helper.Realm.DTO;
    using Xamarin.Forms.Maps;

    /// <summary>
    ///     Defines the <see cref="GeoPosition" />
    /// </summary>
    public class GeoPosition
    {
        /// <summary>
        ///     Gets or sets the Accuracy
        /// </summary>
        public double? Accuracy { get; set; }

        /// <summary>
        ///     Gets or sets the Latitude
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        ///     Gets or sets the Longitude
        /// </summary>
        public double? Longitude { get; set; }

        public DateTimeOffset Timestamp { get; set; }
        public double? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double? Speed { get; set; }

        public GeoPositionDTO GetDTO()
        {
            return new GeoPositionDTO { 
                Accuracy = Accuracy,
                Altitude = Altitude,
                AltitudeAccuracy = AltitudeAccuracy,
                Heading = Heading,
                Latitude = Latitude,
                Longitude = Longitude,
                Speed = Speed,
                Timestamp = Timestamp
            };
        }

        public static GeoPosition FromDTO(GeoPositionDTO position)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(){
            return Latitude != null && Longitude != null;
        }

        public Position ToPosition(){
            return new Position((double)Latitude, (double)Longitude);
        }
    }
}