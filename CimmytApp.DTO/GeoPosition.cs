namespace CimmytApp.DTO
{
    /// <summary>
    /// Defines the <see cref="GeoPosition" />
    /// </summary>
    public class GeoPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPosition"/> class.
        /// </summary>
        public GeoPosition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPosition"/> class.
        /// </summary>
        /// <param name="latitude">The <see cref="double"/></param>
        /// <param name="longitude">The <see cref="double"/></param>
        public GeoPosition(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        //   public DateTimeOffset Timestamp { get; set; }

        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        //   public DateTimeOffset Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}