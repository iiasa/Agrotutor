namespace CimmytApp.Core.Persistence.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Accuracy { get; set; }
        public double? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double? Speed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}