namespace CimmytApp.DTO.Parcel
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using System;

    /// <summary>
    /// Defines the <see cref="AgriculturalActivity" />
    /// </summary>
    [Table("Activity")]
    public class AgriculturalActivity
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Cost
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the AmountApplied
        /// </summary>
        public string AmountApplied { get; set; }

        /// <summary>
        /// Gets or sets the Sown
        /// </summary>
        public string Sown { get; set; }

        /// <summary>
        /// Gets or sets the AppliedProduct
        /// </summary>
        public string AppliedProduct { get; set; }

        /// <summary>
        /// Gets or sets the Dose
        /// </summary>
        public double Dose { get; set; }

        /// <summary>
        /// Gets or sets the WeightOfSeeds
        /// </summary>
        public double WeightOfSeeds { get; set; }

        /// <summary>
        /// Gets or sets the NumberOfSeeds
        /// </summary>
        public double NumberOfSeeds { get; set; }

        /// <summary>
        /// Gets or sets the ProductObtained
        /// </summary>
        public string ProductObtained { get; set; }

        /// <summary>
        /// Gets or sets the Yield
        /// </summary>
        public string Yield { get; set; }

        /// <summary>
        /// Gets or sets the ActivityType
        /// </summary>
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the ParcelId
        /// </summary>
        [ForeignKey(typeof(Parcel))]
        public int ParcelId { get; set; }
    }
}