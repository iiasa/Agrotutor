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

        public double? Altitude { get; set; }

        public double? AltitudeAccuracy { get; set; }

        public double? Heading { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     Gets or sets the Latitude
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        ///     Gets or sets the Longitude
        /// </summary>
        public double? Longitude { get; set; }

        public double? Speed { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string ParcelId { get; set; }

        public bool IsPartOfdelineation { get; set; }

        public static GeoPosition FromDTO(GeoPositionDTO position)
        {
            if (position == null)
            {
                return null; // Cast is for not having to reference Realm lib
            }

            return new GeoPosition
            {
                Accuracy = position.Accuracy,
                Altitude = position.Altitude,
                AltitudeAccuracy = position.AltitudeAccuracy,
                Heading = position.Heading,
                Id = position.Id,
                IsPartOfdelineation = position.IsPartOfdelineation,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                ParcelId = position.ParcelId,
                Speed = position.Speed,
                Timestamp = position.Timestamp
            };
        }

        public GeoPositionDTO GetDTO(string parcelId)
        {
            return new GeoPositionDTO
            {
                Accuracy = Accuracy,
                Altitude = Altitude,
                AltitudeAccuracy = AltitudeAccuracy,
                Heading = Heading,
                Id = Id,
                IsPartOfdelineation = IsPartOfdelineation,
                Latitude = Latitude,
                Longitude = Longitude,
                ParcelId = parcelId,
                Speed = Speed,
                Timestamp = Timestamp
            };
        }

        public bool IsSet()
        {
            return Latitude != null && Longitude != null;
        }

        public Position ToPosition()
        {
            return new Position((double)Latitude, (double)Longitude);
        }
    }
}