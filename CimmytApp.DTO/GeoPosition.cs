namespace CimmytApp.DTO
{
    using System;
    using SQLite.Net.Attributes;
    
    public class GeoPosition
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

     //   public DateTimeOffset Timestamp { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //public double Speed { get; set; }
        //public double Heading { get; set; }
        //public double Accuracy { get; set; }
        //public double Altitude { get; set; }
        //public double AltitudeAccuracy { get; set; }

      //  public TypeOfAcquisition AcquiredThrough { get; set; }
    }

    //public enum TypeOfAcquisition
    //{
    //    Gps = 0,
    //    SelectedOnMap = 1,
    //    Other = 2
    //}
}