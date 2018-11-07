namespace CimmytApp.Core.Map
{
    using System;
    using Xamarin.Forms.GoogleMaps;

    /// <summary>
    ///     Defines the <see cref="GeoPosition" />
    /// </summary>
    public class GeoPositione
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